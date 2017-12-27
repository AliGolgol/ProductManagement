import { Component, OnInit, trigger, state, style, animate, transition, ViewChild, Input, Output } from '@angular/core';
import { HTTP_PROVIDERS } from '@angular/http';
import {ROUTER_DIRECTIVES} from '@angular/router';

import {IManufacturer, Pagination, PaginatedResult, IPageList} from '../shared/services/interfaces';

import {CreateComponent} from './manufacturer-create.component';
import {ManufacturerService} from '../shared/services/manufacturer.service';
import {SlimLoadingBarService} from 'ng2-slim-loading-bar/ng2-slim-loading-bar';
import {MODAL_DIRECTIVES, ModalComponent} from 'ng2-bs3-modal/ng2-bs3-modal';
import {PAGINATION_DIRECTIVES, PaginationComponent} from 'ng2-bootstrap';
import {ConfigService} from '../shared/utils/config.service';
import {NotificationService} from '../shared/utils/notification.service';
import {ItemsService} from '../shared/utils/items.service';
import {Cook} from '../shared/services/cookie.service';

@Component({
    moduleId: module.id,
    selector: 'pm-manufacturer',
    templateUrl: 'manufacturer-list.component.html',
    directives: [ROUTER_DIRECTIVES, MODAL_DIRECTIVES, PAGINATION_DIRECTIVES, CreateComponent],
    animations: [
        trigger('flyInOut', [
            state('in', style({ opacity: 1, transform: 'translateX(0)' })),
            transition('void => *', [
                style({
                    opacity: 0,
                    transform: 'translateX(-100%)'
                }),
                animate('0.6s ease-in')
            ]),
            transition('* => void', [
                animate('0.2s 10 ease-out', style({
                    opacity: 0,
                    transform: 'translateX(100%)'
                }))
            ])
        ])
    ]
})
export class ManufacturerListComponent implements OnInit {

    pageTitle: string = 'Repository List';
    errorMessage: string;
    showNew: boolean = false;
    manufacturers: IManufacturer[];

    public itemsPerPage: number = 2;
    public totalItems: number = 10;
    public currentPage: number = 1;

    //Modal Properties
    @ViewChild('modal')
    modal: ModalComponent;
    addMoal: ModalComponent;
    items: string[] = ['item1', 'item2', 'item3'];
    selected: string;
    output: string;
    selectedManId: number;
    manuDetails: IManufacturer;
    selectedManLoaded: boolean = false;
    index: number = 0;
    backdropOptions = [true, false, 'static'];
    animation: boolean = true;
    keyboard: boolean = true;
    backdrop: string | boolean = true;
    p: IPageList = { CurrentPage: 1, ItemsPerPage: 5 }

    constructor(private _manufacturerService: ManufacturerService,
        private slimLoader: SlimLoadingBarService,
        private itemService: ItemsService,
        private notificationService: NotificationService,
        private _cook: Cook) { }

    toggleImage(): void {
        this.showNew = !this.showNew;
    }

    ngOnInit(): void {
        console.log('In OnInit');
        //this.load();
        this.loadPage(this.p);
    }

    //Show details of repository type
    viewDetails(id: number) {
        this.selectedManId = id;
        this.modal.open('lg');
        console.log('test detial');
    }

    addManufacturer() {
        this.modal.open('lg');
        console.log('test detial');
    }

    load() {
        this.slimLoader.start();
        this._manufacturerService.getAll(sessionStorage.getItem("tokenKey"))
            .subscribe((res: IManufacturer[]) => {
                this.manufacturers = res;                
            },
            error => this.errorMessage = <any>error);
        this.notificationService.printSuccessMessage(this.errorMessage);
        this.slimLoader.complete();
    }

    loadManu() {
        this.slimLoader.start();
        this._manufacturerService.getManufacturers(this.currentPage, this.itemsPerPage)
            .subscribe((res: PaginatedResult<IManufacturer[]>) => {
                this.manufacturers = res.result;
            },
            error => this.errorMessage = <any>error);
        this.notificationService.printSuccessMessage(this.errorMessage);
        this.slimLoader.complete();
    }

    //Delete the repsitory type
    delManufacturer(manufacturer: IManufacturer) {
        this.notificationService.openConfirmationDialog('آیا مطمئن هستید؟', () => {
            this.slimLoader.start();
            this._manufacturerService.del(manufacturer.Id)
                .subscribe(() => {
                    this.itemService.removeItemFromArray<IManufacturer>(this.manufacturers, manufacturer);
                    this.notificationService.printSuccessMessage(manufacturer.Name + 'حذف شد ');
                    this.slimLoader.complete();
                },
                error => {
                    this.slimLoader.complete();
                    this.notificationService.printErrorMessage('خطا در حذف ' + manufacturer.Name + ' ' + error);
                });
        });
    }

    onClicked(man: IManufacturer[]): void {
        this.manufacturers = man;
        this.load();
    }

    closed() { this.output = '(closed)' + this.selected; }

    dismissed() { this.output = '(dismissed)' };

    opened() {
        this.slimLoader.start();
        this._manufacturerService.getById(this.selectedManId)
            .subscribe((manufacturer: IManufacturer) => {
                this.manuDetails = this.itemService.getSerialized<IManufacturer>(manufacturer);
                    this.slimLoader.complete();
                    this.selectedManLoaded = true;
                },
                error => {
                    this.slimLoader.complete();
                });
        this.output = '(opened)';
    }

    loadPage(page: IPageList)
    {
        this.slimLoader.start();
        this._manufacturerService.getPage(page)
            .subscribe((res: IManufacturer[]) => {
                this.manufacturers = res;
            },
            error => this.errorMessage = <any>error);
        this.notificationService.printSuccessMessage(this.errorMessage);
        this.slimLoader.complete();
    }

    pageChanged(event: any): void {
        this.currentPage = event.page;
        //this.load();
        this.p.CurrentPage = event.page;
        this.loadPage(this.p);
        
    };
}
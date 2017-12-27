import { Component, OnInit, trigger, state, style, animate, transition, ViewChild, Input, Output } from '@angular/core';
import { HTTP_PROVIDERS } from '@angular/http';
import {ROUTER_DIRECTIVES} from '@angular/router';
import {IBillType, Pagination, PaginatedResult} from '../shared/services/interfaces';
import {CreateComponent} from './billType-create.component';
import {BillTypeService} from '../shared/services/billType.service';
import {SlimLoadingBarService} from 'ng2-slim-loading-bar/ng2-slim-loading-bar';
import {MODAL_DIRECTIVES, ModalComponent} from 'ng2-bs3-modal/ng2-bs3-modal';
import {PAGINATION_DIRECTIVES, PaginationComponent} from 'ng2-bootstrap';
import {ConfigService} from '../shared/utils/config.service';
import {NotificationService} from '../shared/utils/notification.service';
import {ItemsService} from '../shared/utils/items.service';

//import {ItemsService} from '../shared/utils/items.service';

@Component({
    moduleId: module.id,
    selector: 'pm-billType',
    templateUrl: 'billType-list.component.html',
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
export class BillTypeListComponent implements OnInit {

    pageTitle: string = 'Bill-type List';
    errorMessage: string;
    showNew: boolean = false;
    billTypes: IBillType[];

    public itemsPerPage: number = 2;
    public totalItems: number = 1;

    //Modal Properties
    @ViewChild('modal')
    modal: ModalComponent;
    addMoal: ModalComponent;
    items: string[] = ['item1', 'item2', 'item3'];
    selected: string;
    output: string;
    selectedBillId: number;
    billDetails: IBillType;
    selectedBillLoaded: boolean = false;
    index: number = 0;
    backdropOptions = [true, false, 'static'];
    animation: boolean = true;
    keyboard: boolean = true;
    backdrop: string | boolean = true;

    constructor(private _billTypeService: BillTypeService,
        private slimLoader: SlimLoadingBarService,
        private itemService: ItemsService,
        private notificationService: NotificationService) { }

    toggleImage(): void {
        this.showNew = !this.showNew;
    }

    ngOnInit(): void {
        console.log('In OnInit');
        this.load();
    }

    //Show details of repository type
    viewDetails(id: number) {
        this.selectedBillId = id;
        this.modal.open('lg');
        console.log('test detial');
    }

    addBillType() {
        this.modal.open('lg');
        console.log('test detial');
    }
    //Get all repository type
    load() {
        this.slimLoader.start();
        this._billTypeService.getAll()
            .subscribe(
            response => this.billTypes = response,
            error => this.errorMessage = <any>error);
        this.notificationService.printSuccessMessage(this.errorMessage);
        this.slimLoader.complete();
    }

    //Delete the repsitory type
    delBillType(billType: IBillType) {
        this.notificationService.openConfirmationDialog('آیا مطمئن هستید؟', () => {
            this.slimLoader.start();
            this._billTypeService.del(billType.Id)
                .subscribe(() => {
                    this.itemService.removeItemFromArray<IBillType>(this.billTypes, billType);
                    this.notificationService.printSuccessMessage(billType.Name + 'حذف شد ');
                    this.slimLoader.complete();
                },
                error => {
                    this.slimLoader.complete();
                    this.notificationService.printErrorMessage('خطا در حذف ' + billType.Name + ' ' + error);
                });
        });
    }

    closed() { this.output = '(closed)' + this.selected; }

    dismissed() { this.output = '(dismissed)' };

    opened() {
        this.slimLoader.start();
        this._billTypeService.getById(this.selectedBillId)
            .subscribe((billType: IBillType) => {
                this.billDetails = this.itemService.getSerialized<IBillType>(billType);
                this.slimLoader.complete();
                this.selectedBillLoaded = true;
            },
            error => {
                this.slimLoader.complete();
            });
        this.output = '(opened)';
    }
}
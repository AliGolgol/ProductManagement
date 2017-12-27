import { Component, OnInit, trigger, state, style, animate, transition, ViewChild, Input, Output } from '@angular/core';
import { HTTP_PROVIDERS } from '@angular/http';
import {ROUTER_DIRECTIVES} from '@angular/router';
import {IEntrySlipType, Pagination, PaginatedResult} from '../shared/services/interfaces';
import {EntrySlipTypeCreateComponent} from './entrySlipType-create.component';
import {EntrySlipTypeService} from '../shared/services/entrySlipType.service';
import {SlimLoadingBarService} from 'ng2-slim-loading-bar/ng2-slim-loading-bar';
import {MODAL_DIRECTIVES, ModalComponent} from 'ng2-bs3-modal/ng2-bs3-modal';
import {PAGINATION_DIRECTIVES, PaginationComponent} from 'ng2-bootstrap';
import {ConfigService} from '../shared/utils/config.service';
import {NotificationService} from '../shared/utils/notification.service';
import {ItemsService} from '../shared/utils/items.service';


@Component({
    moduleId: module.id,
    selector: 'pm-entrySlipType',
    templateUrl: 'entrySlipType-list.component.html',
    directives: [ROUTER_DIRECTIVES, MODAL_DIRECTIVES, PAGINATION_DIRECTIVES, EntrySlipTypeCreateComponent],
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
export class EntrySlipTypeListComponent implements OnInit {

    pageTitle: string = 'Entry-Slip-type List';
    errorMessage: string;
    showNew: boolean = false;
    entrySlipTypes: IEntrySlipType[];

    public itemsPerPage: number = 2;
    public totalItems: number = 1;

    //Modal Properties
    @ViewChild('modal')
    modal: ModalComponent;
    addMoal: ModalComponent;
    items: string[] = ['item1', 'item2', 'item3'];
    selected: string;
    output: string;
    selectedSlipId: number;
    slipDetails: IEntrySlipType;
    selectedSlipLoaded: boolean = false;
    index: number = 0;
    backdropOptions = [true, false, 'static'];
    animation: boolean = true;
    keyboard: boolean = true;
    backdrop: string | boolean = true;

    constructor(private _entrySlipTypeService: EntrySlipTypeService,
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
        this.selectedSlipId = id;
        this.modal.open('lg');
        console.log('test detial');
    }

    addEntrySlipType() {
        this.modal.open('lg');
        console.log('test detial');
    }
    //Get all repository type
    load() {
        this.slimLoader.start();
        this._entrySlipTypeService.getAll()
            .subscribe(
            response => this.entrySlipTypes = response,
            error => this.errorMessage = <any>error);
        this.notificationService.printSuccessMessage(this.errorMessage);
        this.slimLoader.complete();
    }

    //Delete the repsitory type
    delEntrySlipType(entrySlipType: IEntrySlipType) {
        this.notificationService.openConfirmationDialog('آیا مطمئن هستید؟', () => {
            this.slimLoader.start();
            this._entrySlipTypeService.del(entrySlipType.Id)
                .subscribe(() => {
                    this.itemService.removeItemFromArray<IEntrySlipType>(this.entrySlipTypes, entrySlipType);
                    this.notificationService.printSuccessMessage(entrySlipType.Name + 'حذف شد ');
                    this.slimLoader.complete();
                },
                error => {
                    this.slimLoader.complete();
                    this.notificationService.printErrorMessage('خطا در حذف ' + entrySlipType.Name + ' ' + error);
                });
        });
    }

    closed() { this.output = '(closed)' + this.selected; }

    dismissed() { this.output = '(dismissed)' };

    opened() {
        this.slimLoader.start();
        this._entrySlipTypeService.getById(this.selectedSlipId)
            .subscribe((entrySlipType: IEntrySlipType) => {
                this.slipDetails = this.itemService.getSerialized<IEntrySlipType>(entrySlipType);
                this.slimLoader.complete();
                this.selectedSlipLoaded = true;
            },
            error => {
                this.slimLoader.complete();
            });
        this.output = '(opened)';
    }
}
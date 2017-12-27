import { Component, OnInit, trigger, state, style, animate, transition,
    ViewChild, Input, Output, EventEmitter } from '@angular/core';
import { HTTP_PROVIDERS } from '@angular/http';
import {ROUTER_DIRECTIVES} from '@angular/router';

import {IInvoiceItem, Pagination, PaginatedResult, IRepository} from '../shared/services/interfaces';
import {CreateInvoiceItemComponent} from './invoiceItem-create.component';

import {InvoiceItemService} from '../shared/services/invoiceItem.service';
import {ProductService} from '../shared/services/product.service';
import {RepositoryService} from '../shared/services/repository.service';

import {SlimLoadingBarService} from 'ng2-slim-loading-bar/ng2-slim-loading-bar';
import {MODAL_DIRECTIVES, ModalComponent} from 'ng2-bs3-modal/ng2-bs3-modal';
import {PAGINATION_DIRECTIVES, PaginationComponent} from 'ng2-bootstrap';
import {ConfigService} from '../shared/utils/config.service';
import {NotificationService} from '../shared/utils/notification.service';
import {ItemsService} from '../shared/utils/items.service';
import {Cook} from '../shared/services/cookie.service';

@Component({
    moduleId: module.id,
    selector: 'pm-invoiceItem',
    templateUrl: 'invoiceItem-list.component.html',
    directives: [ROUTER_DIRECTIVES, MODAL_DIRECTIVES,
        PAGINATION_DIRECTIVES, CreateInvoiceItemComponent],
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
export class InvoiceItemListComponent implements OnInit {

    pageTitle: string = 'Buy Slip Item List';
    errorMessage: string;
    showNew: boolean = false;
    invoiceItems: IInvoiceItem[];
    id: number;
    public itemsPerPage: number = 2;
    public totalItems: number = 1;

    @Output() notify = new EventEmitter<any>();

    //Modal Properties
    @ViewChild('modal')
    modal: ModalComponent;
    addMoal: ModalComponent;
    items: string[] = ['item1', 'item2', 'item3'];
    selected: string;
    output: string;
    selectedInvoiceItemId: number;
    invoiceItemDetails: IInvoiceItem;
    selectedInvoiceItemLoaded: boolean = false;
    index: number = 0;
    backdropOptions = [true, false, 'static'];
    animation: boolean = true;
    keyboard: boolean = true;
    backdrop: string | boolean = true;
    addOrDetail: boolean = false;

    constructor(
        private _invoiceItemService: InvoiceItemService,
        private _productService: ProductService,
        private _repositoryService: RepositoryService,
        private slimLoader: SlimLoadingBarService,
        private itemService: ItemsService,
        private notificationService: NotificationService,
        private _cookie: Cook) { }

    toggleImage(): void {
        this.showNew = !this.showNew;
    }

    ngOnInit(): void {
        debugger
        this.id = +localStorage.getItem("invoice");
        console.log('In OnInit');
        this.loadInvoiceItem();
    }

    //Show details of invoiceItem type
    viewDetails(id: number) {
        this.selectedInvoiceItemId = id;
        this.modal.open('lg');
        console.log('test detial');
    }

    addRepType(b: boolean) {
        this.modal.open('lg');
        console.log('test detial');
    }

    //Get all invoiceItem type
    loadInvoiceItem() {
        this.slimLoader.start();
        if (localStorage.getItem('invoice')) {
            this._invoiceItemService.getByInvoiceId(this.id)
                .subscribe((invoiceItem: IInvoiceItem[]) => {
                    this.invoiceItems = invoiceItem
                },
                error => this.errorMessage = <any>error);
            this.notificationService.printSuccessMessage(this.errorMessage);
        }

        this.slimLoader.complete();
    }

    //Delete the repsitory type
    delInvoiceItem(invoiceItem: IInvoiceItem) {
        this.notificationService.openConfirmationDialog('آیا مطمئن هستید؟', () => {
            this.slimLoader.color = "blue";
            this.slimLoader.start();
            this._invoiceItemService.del(invoiceItem.Id)
                .subscribe(() => {
                    this.itemService.removeItemFromArray<IInvoiceItem>(this.invoiceItems, invoiceItem);
                    this.notificationService.printSuccessMessage(invoiceItem.Id + 'حذف شد');
                    this.slimLoader.complete();
                },
                error => {
                    this.slimLoader.complete();
                    this.notificationService.printErrorMessage('خطا در حذف ' + invoiceItem.Id + ' ' + error);
                });
        });
    }

    onClicked(): void {

        //this.loadInvoiceItem();
        this.ngOnInit();
        this.notificationService.printErrorMessage("");
    }

    closed() { this.output = '(closed)' + this.selected; }

    dismissed() { this.output = '(dismissed)' };

    opened() {
        this.slimLoader.start();

        this._invoiceItemService.getById(this.selectedInvoiceItemId)
            .subscribe((invoiceItem: IInvoiceItem) => {
                this.invoiceItemDetails = this.itemService.getSerialized<IInvoiceItem>(invoiceItem);
                this.slimLoader.complete();
                this.selectedInvoiceItemLoaded = true;
            },
            error => {
                this.slimLoader.complete();
            });

        this.output = '(opened)';
    }
}
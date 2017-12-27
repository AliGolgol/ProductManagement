import { Component, OnInit, trigger, state, style, animate, transition,
    ViewChild, Input, Output, EventEmitter } from '@angular/core';
import { HTTP_PROVIDERS } from '@angular/http';
import {ROUTER_DIRECTIVES, Router} from '@angular/router';

import {Pagination, PaginatedResult, IInvoice, IInvoiceDetails, IPageList} from '../shared/services/interfaces';

import {CreateInvoiceItemComponent} from '../InvoiceItems/invoiceItem-create.component';
import {InvoiceService} from '../shared/services/invoice.service';

import {SlimLoadingBarService} from 'ng2-slim-loading-bar/ng2-slim-loading-bar';
import {MODAL_DIRECTIVES, ModalComponent} from 'ng2-bs3-modal/ng2-bs3-modal';
import {PAGINATION_DIRECTIVES, PaginationComponent} from 'ng2-bootstrap';
import {ConfigService} from '../shared/utils/config.service';
import {NotificationService} from '../shared/utils/notification.service';
import {ItemsService} from '../shared/utils/items.service';

@Component({
    moduleId: module.id,
    selector: 'pm-invoice',
    templateUrl: 'invoice-list.component.html',
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
export class InvoiceListComponent implements OnInit {

    pageTitle: string = 'Invoice List';
    errorMessage: string;
    showNew: boolean = false;
    invoices: IInvoice[];

    public itemsPerPage: number = 2;
    public totalItems: number = 5;
    public currentPage: number = 1;
    public totalPage: number = 4;
    p: IPageList = { CurrentPage: 1, ItemsPerPage: 13 }

    
    //Modal Properties
    @ViewChild('modal')
    modal: ModalComponent;
    addMoal: ModalComponent;
    items: string[] = ['item1', 'item2', 'item3'];
    selected: string;
    output: string;
    selectedInvoiceId: number;
    invoiceDetails: IInvoice;
    selectedInvoiceLoaded: boolean = false;
    index: number = 0;
    backdropOptions = [true, false, 'static'];
    animation: boolean = true;
    keyboard: boolean = true;
    backdrop: string | boolean = true;

    constructor(private _invoiceService: InvoiceService,
        private slimLoader: SlimLoadingBarService,
        private itemService: ItemsService,
        private notificationService: NotificationService,
        private _router: Router) { }

    toggleImage(): void {
        this.showNew = !this.showNew;
    }

    ngOnInit(): void {
        console.log('In OnInit');
        this.loadInvoice();
    }

    //Show details of invoice type
    viewDetails(id: number) {
        this.selectedInvoiceId = id;
        this.modal.open('lg');
        console.log('test detial');
    }

    //Get all invoice type
    loadInvoice() {
        this.slimLoader.color = "blue";
        this.slimLoader.start();
        this._invoiceService.getAll(this.p)
            .subscribe(
            invoice => this.invoices = invoice,
            error => this.errorMessage = <any>error);
        this.notificationService.printSuccessMessage(this.errorMessage);
        this.slimLoader.complete();
    }

    //Delete the repsitory type
    delInvoice(invoice: IInvoice) {
        this.notificationService.openConfirmationDialog('آیا مطمئن هستید؟', () => {
            this.slimLoader.color = "blue";
            this.slimLoader.start();
            this._invoiceService.del(invoice.Id)
                .subscribe(() => {
                    this.itemService.removeItemFromArray<IInvoice>(this.invoices, invoice);
                    this.notificationService.printSuccessMessage(invoice.Id + 'حذف شد');
                    this.slimLoader.complete();
                },
                error => {
                    this.slimLoader.complete();
                    this.notificationService.printErrorMessage('خطا در حذف ' + invoice.Id + ' ' + error);
                });
        });
    }

    closed() { this.output = '(closed)' + this.selected; }

    dismissed() { this.output = '(dismissed)' };

    opened() {
        this.slimLoader.start();

        this._invoiceService.getById(this.selectedInvoiceId)
            .subscribe((invoice: IInvoice) => {
                this.invoiceDetails = this.itemService.getSerialized<IInvoice>(invoice);
                this.slimLoader.complete();
                this.selectedInvoiceLoaded = true;
            },
            error => {
                this.slimLoader.complete();
            });

        this.output = '(opened)';
    }

    onClicked(): void {
    }
    createInvoice() {
        this._router.navigate(['./createInvoice']);
    }

    pageChanged(event: any): void {
        this.currentPage = event.page;
        this.p.CurrentPage = event.page;
        this.loadInvoice();
        
    };
}
import {Component, trigger, state, style, animate,
    transition, ViewChild, Input, Output, EventEmitter, OnInit } from '@angular/core';
import {Router} from '@angular/router';
import {IInvoiceItem, IInvoiceItemDetails,
    IProduct, IRepository, IInvoice, IDate, ISelectQauntity} from '../shared/services/interfaces';

import {InvoiceItemService} from '../shared/services/invoiceItem.service';
import {ProductService} from '../shared/services/product.service';
import {InvoiceService} from '../shared/services/invoice.service';
import {RepositoryService} from '../shared/services/repository.service';
import {DateService} from '../shared/services/date.service';

import {MODAL_DIRECTIVES, ModalComponent} from 'ng2-bs3-modal/ng2-bs3-modal';
import {ItemsService} from '../shared/utils/items.service';
import {NotificationService} from '../shared/utils/notification.service';
import {SlimLoadingBarService} from 'ng2-slim-loading-bar/ng2-slim-loading-bar';
import {MappingService} from '../shared/utils/mapping.service';
import {Cook} from '../shared/services/cookie.service';

@Component({
    selector: 'invoiceItem-add',
    templateUrl: 'app/InvoiceItems/invoiceItem-create.component.html',
    directives: [MODAL_DIRECTIVES],
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
export class CreateInvoiceItemComponent implements OnInit {
    invoiceItemModel = <IInvoiceItemDetails>{};//creates an empty object for an interface
    invoice = <IInvoice>{};
    invoiceId: number;
    pageTitle: string = "add invoiceItem";
    productSelected: number = 1;
    repositorySelelcted: number = 1;
    @Input() invoiceItem: IInvoiceItem[];
    date: IDate;
    selectQuantity= <ISelectQauntity>{};
    quantity: number;

    constructor(
        private _router: Router,
        private _invoiceItemService: InvoiceItemService,
        private _productService: ProductService,
        private _invoiceService: InvoiceService,
        private _repositoryService: RepositoryService,
        private itemsService: ItemsService,
        private notificationService: NotificationService,
        private slimLoader: SlimLoadingBarService,
        private mapping: MappingService,
        private _cook: Cook,
        private _date: DateService) { }

    ngOnInit() {
        //debugger
        //if (localStorage.getItem("invoice"))
        //    localStorage.setItem("invoiceList", "ok");
        this.getDate();
        this.getProducts();
        this.getRepositories();
    }

    @Output() clicked: EventEmitter<void> = new EventEmitter<void>();



    insert(): void {
        console.log(this.invoiceItemModel);

        this.invoiceId = +localStorage.getItem("invoice");
        var mapInvoiceItem = this.mapping.mapInvoiceItemDetailsToInvoiceItem(this.invoiceItemModel);

        mapInvoiceItem.ProductId = this.productSelected;
        mapInvoiceItem.RepositoryId = this.repositorySelelcted;
        mapInvoiceItem.InvoiceId = this.invoiceId;

        this._invoiceItemService.add(mapInvoiceItem)
            .subscribe((invoiceItem: IInvoiceItem) => {
                console.log(`ID: ${invoiceItem.Id}`);
                this.modalAdd.close();
            });

        this._router.navigate(['./updateInvoice', localStorage.getItem("invoice")]);
    }


    @ViewChild('mymodal')
    modalAdd: ModalComponent;
    selected: string;
    output: string;
    selectedRepositoryLoaded: boolean = false;
    backdropOptions = [true, false, 'static'];
    animation: boolean = true;
    keyboard: boolean = true;
    backdrop: string | boolean = true;
    addOrDetail: boolean = false;

    getProducts() {
        this._productService.getAll()
            .subscribe((products: IProduct[]) => {
                this.invoiceItemModel.Products = products;
            },
            error => {
                this.slimLoader.complete();
                this.notificationService.printErrorMessage('Failed to load schedule. ' + error);
            });
    }

    getInvoice(): void {
        this._invoiceService.add(this.invoice)
            .subscribe((invoice: IInvoice) => {
                this.invoiceId = invoice.Id;
            },
            error => {
                this.slimLoader.complete();
                this.notificationService.printErrorMessage('Failed to load schedule. ' + error);
            });
    }

    getRepositories() {
        this._repositoryService.getAll()
            .subscribe((repositories: IRepository[]) => {
                this.invoiceItemModel.Repositories = repositories;
            },
            error => {
                this.slimLoader.complete();
                this.notificationService.printErrorMessage('Failed to load schedule. ' + error);
            });
    }

    getDate(): void {
        this._date.get()
            .subscribe((resp: IDate) => {
                this.date = resp;
            },
            error => {
                this.slimLoader.complete();
                this.notificationService.printErrorMessage('Failed to load schedule. ' + error);
            });
    }

    log(Name): void {
        console.log(Name);
    }

    onChange(prdValue) {
        console.log(prdValue);
        this.notificationService.printErrorMessage(prdValue);
        this.productSelected = prdValue;

        this._invoiceItemService.selectPrice(prdValue)
            .subscribe((res: number) => {
                this.invoiceItemModel.Price = res;
                this.selectQuantity.prdId = prdValue;
            },
            error => { })
    }

    onChanged(repValue) {
        console.log(repValue);
        this.notificationService.printErrorMessage(repValue);
        this.repositorySelelcted = repValue;
    }

    close() {
        this.modalAdd.close();
    }

    openIt() {
        this.modalAdd.open();
    }

    opened() {
    }

    onSubmit(form): void
    {
        this.invoiceId = +localStorage.getItem("invoice");
        var mapInvoiceItem = this.mapping.mapInvoiceItemDetailsToInvoiceItem(this.invoiceItemModel);

        mapInvoiceItem.ProductId = this.productSelected;
        mapInvoiceItem.RepositoryId = this.repositorySelelcted;
        mapInvoiceItem.InvoiceId = this.invoiceId;

        this._invoiceItemService.add(mapInvoiceItem)
            .subscribe((invoiceItem: IInvoiceItem) => {
                console.log(`ID: ${invoiceItem.Id}`);
                this.modalAdd.close();
            });

        this._router.navigate(['./updateInvoice', this.invoiceId]);
    }
    closed() { this.output = '(closed)' + this.selected; }

    dismissed() { this.output = '(dismissed)' };

    SelectQuantity(quantity) {
        this.selectQuantity.id = quantity;
        this._invoiceItemService.SelectQuantity(this.selectQuantity)
            .subscribe((res: number) => {
                this.quantity = res;
            },
            error => { })
        
    }
}
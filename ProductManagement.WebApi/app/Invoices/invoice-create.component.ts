import {Component, trigger, OnInit, state, style, animate, transition, ViewChild, Input, Output } from '@angular/core';
import {Router} from '@angular/router';
import {IInvoiceDetails, IInvoice, IPaymentType, IBillType} from '../shared/services/interfaces';
import {InvoiceService} from '../shared/services/invoice.service';
import {MODAL_DIRECTIVES, ModalComponent} from 'ng2-bs3-modal/ng2-bs3-modal';
import {ItemsService} from '../shared/utils/items.service';
import {NotificationService} from '../shared/utils/notification.service';
import {SlimLoadingBarService} from 'ng2-slim-loading-bar/ng2-slim-loading-bar';
import {MappingService} from '../shared/utils/mapping.service';
import {BillTypeService} from '../shared/services/billType.service';
import {PaymentTypeService} from '../shared/services/paymentType.service';

@Component({
    selector: 'invoice-add',
    templateUrl: 'app/Invoices/invoice-create.component.html',
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

export class CreateInvoiceComponent implements OnInit {
    invoiceModel = <IInvoiceDetails>{};//creates an empty object for an interface
    //repoistory: IProduct;
    pageTitle: string = "add invoice";
    billTypeSelected: number;
    paymentSelected: number;

    constructor(
        private _router: Router,
        private _invoiceService: InvoiceService,
        private _paymentTypeService: PaymentTypeService,
        private _billTypeService: BillTypeService,
        private itemsService: ItemsService,
        private notificationService: NotificationService,
        private slimLoader: SlimLoadingBarService,
        private mapping: MappingService) { }

    ngOnInit() {
        this.getPaymentTypes();
        this.getBillTypes();
    }

    getPaymentTypes() {
        this._paymentTypeService.getAll()
            .subscribe((paymentTypes: IPaymentType[]) => {
                this.invoiceModel.PaymentTypes = paymentTypes;
            },
            error => {
                this.slimLoader.complete();
                this.notificationService.printErrorMessage('Failed to load schedule. ' + error);
            });
    }

    getBillTypes() {
        this._billTypeService.getAll()
            .subscribe((billTypes: IBillType[]) => {
                this.invoiceModel.BillTypes = billTypes;
            },
            error => {
                this.slimLoader.complete();
                this.notificationService.printErrorMessage('Failed to load schedule. ' + error);
            });
    }

    log(Name): void {
        console.log(Name);
    }

    //onSubmit(form): void {
    //    console.log(form);
    //    console.log(this.invoiceModel);
    //    var mapProduct = this.mapping.mapProductDetailToProduct(this.invoiceModel);
    //    mapProduct.ProductCategoryId = this.billTypeSelected;
    //    mapProduct.ManufacturerId = this.paymentSelected;

    //    this._invoiceService.add(mapProduct)
    //        .subscribe((invoice: IProduct) => {
    //            console.log(`ID: ${invoice.Id}`);
    //            this.modalAdd.close();
    //        });
    //}

    onChange(billValue) {
        console.log(billValue);
        this.notificationService.printErrorMessage(billValue);
        this.billTypeSelected = billValue;
    }

    onChanged(payValue) {
        console.log(payValue);
        this.notificationService.printErrorMessage(payValue);
        this.paymentSelected = payValue;
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

    close() {
        this.modalAdd.close();
    }

    openIt() {
        this.modalAdd.open();
    }

    opened() {
    }

    closed() { this.output = '(closed)' + this.selected; }
    dismissed() { this.output = '(dismissed)' };
}
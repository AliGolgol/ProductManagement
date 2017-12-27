import {Component, OnInit, OnDestroy} from '@angular/core';
import {Router, ActivatedRoute}from '@angular/router';
import {NgForm} from '@angular/forms';

import {IPaymentType, IInvoiceDetails, IBillType,
    IInvoiceItem, IInvoice} from '../shared/services/interfaces';
import {ItemsService} from '../shared/utils/items.service';
import {NotificationService} from '../shared/utils/notification.service';
import {ConfigService} from '../shared/utils/config.service';
import {SlimLoadingBarService} from 'ng2-slim-loading-bar/ng2-slim-loading-bar';
import {ROUTER_DIRECTIVES} from '@angular/router';
import {MappingService} from '../shared/utils/mapping.service';

import {InvoiceService} from '../shared/services/invoice.service';
import {BillTypeService} from '../shared/services/billType.service';
import {PaymentTypeService} from '../shared/services/paymentType.service';

import {InvoiceItemListComponent} from '../InvoiceItems/invoiceItem-list.component';

import {MODAL_DIRECTIVES, ModalComponent} from 'ng2-bs3-modal/ng2-bs3-modal';
import {PAGINATION_DIRECTIVES, PaginationComponent} from 'ng2-bootstrap';
import {Cook} from '../shared/services/cookie.service';
import {EntrySlipTypeService} from '../shared/services/entrySlipType.Service';

@Component({
    moduleId: module.id,
    selector: 'invoice-edit',
    templateUrl: 'invoice-update.component.html',
    directives: [ROUTER_DIRECTIVES, MODAL_DIRECTIVES,
        PAGINATION_DIRECTIVES, InvoiceItemListComponent],
    providers: [EntrySlipTypeService]
})

export class InvoiceUpdateComponent implements OnInit {
    private sub: any;
    invoiceModel = <IInvoiceDetails>{};
    pageTitle: string = 'buy slip';
    errorMessage: string;
    paymentType: IPaymentType;
    apiHost: string;
    id: number;
    invoiceLoaded: boolean = false;
    name: string;
    invoiceDetails: IInvoice;
    billTypeSelected: number;
    paymentSelected: number;
    invoiceItem: IInvoiceItem[];

    constructor(private _route: ActivatedRoute,
        private _router: Router,
        private _billTypeService: BillTypeService,
        private _invoiceService: InvoiceService,
        private _paymentTypeService: PaymentTypeService,
        private _cookie: Cook,
        private itemService: ItemsService,
        private notificationService: NotificationService,
        private configService: ConfigService,
        private slimLoader: SlimLoadingBarService,
        private mapmapping: MappingService) { }


    ngOnInit() {
        this.id = +this._route.snapshot.params['id'];
        localStorage.setItem("invoice", this._route.snapshot.params['id']);
        this.apiHost = this.configService.getApiHost();
        this.loadInvoiceDetail();
        this.getPaymentTypes();
        this.getBillTypes();
    }

    loadInvoiceDetail() {
        this.slimLoader.start();

        this._invoiceService.getById(this.id)
            .subscribe((invoice: IInvoice) => {

                this.invoiceDetails = this.itemService.getSerialized<IInvoice>(invoice);
                //this.invoiceItem = invoice.BuySlipItems;
                this.invoiceModel.Id = invoice.Id;
                this.invoiceModel.CreatedDate = invoice.CreatedDate;
                this.invoiceModel.Description = invoice.Description;
                this.paymentSelected = invoice.PaymentTypeId;
                this.billTypeSelected = invoice.BillTypeId;
                this.invoiceModel.Reciver = invoice.Reciver;

                this.slimLoader.complete();

                this.invoiceLoaded = true;
                this.notificationService.printSuccessMessage(invoice.Description);
            },
            error => {
                this.slimLoader.complete();
                this.notificationService.printErrorMessage('خطا ' + error);
            });
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
                this.notificationService.printErrorMessage('خطا در  ' + error);
            });
    }

    updateInvoice(editInvoiceForm: NgForm) {
        console.log(editInvoiceForm.value);

        this.slimLoader.start();
        var mapInvoice = this.mapmapping.mapInvoiceDetailsToInvoice(this.invoiceModel);
        mapInvoice.PaymentTypeId = this.paymentSelected;
        mapInvoice.BillTypeId = this.billTypeSelected;
        

        this._invoiceService.update(mapInvoice)
            .subscribe(() => {
                this.notificationService.printSuccessMessage('انجام شد');
                this.slimLoader.complete();
                localStorage.removeItem('invoice');
                this._router.navigate(['/invoiceList']);
            },
            error => {
                this.slimLoader.complete();
                this.notificationService.printErrorMessage('خطا  ' + error);
            });
    }

    onChanged(billTypeValue) {
        console.log(billTypeValue);
        this.notificationService.printErrorMessage(billTypeValue);
        this.billTypeSelected = billTypeValue;
    }

    onChange(paymentValue) {
        console.log(paymentValue);
        this.notificationService.printErrorMessage(paymentValue);
        this.paymentSelected = paymentValue;
    }    

    onBack(): void {
        this._cookie.deleteCookie("invoice");
        this._router.navigate(['/invoiceList']);
    }
}

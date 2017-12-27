import {Component, OnInit, OnDestroy} from '@angular/core';
import {Router, ActivatedRoute}from '@angular/router';
import {NgForm} from '@angular/forms';

import {IInvoiceItemDetails, IInvoiceItem, IProduct, IRepository} from '../shared/services/interfaces';

import {InvoiceItemService} from '../shared/services/invoiceItem.service';
import {ProductService} from '../shared/services/product.service';
import {RepositoryService} from '../shared/services/repository.service';

import {ItemsService} from '../shared/utils/items.service';
import {NotificationService} from '../shared/utils/notification.service';
import {ConfigService} from '../shared/utils/config.service';
import {SlimLoadingBarService} from 'ng2-slim-loading-bar/ng2-slim-loading-bar';
import {ROUTER_DIRECTIVES} from '@angular/router';
import {MappingService} from '../shared/utils/mapping.service';
import {Cook} from '../shared/services/cookie.service';

@Component({
    moduleId: module.id,
    selector: 'invoiceItem-edit',
    templateUrl: 'invoiceItem-update.component.html'
})

export class InvoiceItemUpdateComponent implements OnInit {
    private sub: any;
    invoiceItemModel = <IInvoiceItemDetails>{};
    pageTitle: string = 'invoiceItem';
    errorMessage: string;
    invoiceItem: IInvoiceItem;
    apiHost: string;
    id: number;
    buySlipId: number;
    invoiceItemLoaded: boolean = false;
    name: string;
    invoiceItemDetails: IInvoiceItem;
    productSelected: number;
    repositorySelected: number;

    constructor(private _route: ActivatedRoute,
        private _router: Router,
        private _invoiceItemService: InvoiceItemService,
        private _productService: ProductService,
        private _repositoryService: RepositoryService,
        private itemService: ItemsService,
        private notificationService: NotificationService,
        private configService: ConfigService,
        private slimLoader: SlimLoadingBarService,
        private mapmapping: MappingService,
        private _cookie: Cook) { }


    ngOnInit() {
        this.id = +this._route.snapshot.params['id'];
        this.apiHost = this.configService.getApiHost();
        this.loadInvoiceItemDetail();
        this.getProducts();
        this.getRepositories();
        this.buySlipId = +this._cookie.getCookie("invoice");
    }

    loadInvoiceItemDetail() {
        this.slimLoader.start();

        this._invoiceItemService.getById(this.id)
            .subscribe((invoiceItem: IInvoiceItem) => {

                this.invoiceItemDetails = this.itemService.getSerialized<IInvoiceItem>(invoiceItem);
                this.productSelected = this.invoiceItemDetails.ProductId;
                this.repositorySelected = this.invoiceItemDetails.RepositoryId;

                this.invoiceItemModel.Id = invoiceItem.Id;
                this.invoiceItemModel.Price = invoiceItem.Price;
                this.invoiceItemModel.Description = invoiceItem.Description;
                this.invoiceItemModel.Quantity = invoiceItem.Quantity;

                this.slimLoader.complete();

                this.invoiceItemLoaded = true;
                this.notificationService.printSuccessMessage(this.invoiceItemDetails.Id.toString());
            },
            error => {
                this.slimLoader.complete();
                this.notificationService.printErrorMessage('خطا ' + error);
            });
    }

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

    updateInvoiceItem(editInvoiceItemForm: NgForm) {
        console.log(editInvoiceItemForm.value);

        this.slimLoader.start();
        var mapInvoiceItem = this.mapmapping.mapInvoiceItemDetailsToInvoiceItem(this.invoiceItemModel);
        mapInvoiceItem.ProductId = this.productSelected;
        mapInvoiceItem.RepositoryId = this.repositorySelected;

        this._invoiceItemService.update(mapInvoiceItem)
            .subscribe(() => {
                this.notificationService.printSuccessMessage('انجام شد');
                this.slimLoader.complete();


                this._router.navigate(['/updateInvoice/' + this.buySlipId]);
            },
            error => {
                this.slimLoader.complete();
                this.notificationService.printErrorMessage('خطا  ' + error);
            });
    }

    onChange(prdValue) {
        console.log(prdValue);
        this.notificationService.printErrorMessage(prdValue);
        this.productSelected = prdValue;
    }

    onChanged(repValue) {
        console.log(repValue);
        this.notificationService.printErrorMessage(repValue);
        this.repositorySelected = repValue;
    }

    onSubmit(form): void {
        this.slimLoader.start();
        this.notificationService.printErrorMessage(this.invoiceItem.Id.toString());
        this._invoiceItemService.update(this.invoiceItemDetails)
            .subscribe(() => {
                this.notificationService.printSuccessMessage('انجام شد');
                this.slimLoader.complete();
            },
            error => {
                this.slimLoader.complete();
                this.notificationService.printErrorMessage('خطا  ' + error);
            });
    }

    onBack(): void {
        this._router.navigate(['/invoiceItemList']);
    }
}

import {Component, OnInit, OnDestroy} from '@angular/core';
import {Router, ActivatedRoute}from '@angular/router';
import {NgForm} from '@angular/forms';

import {IBuySlipItemDetails, IBuySlipItem, IProduct, IRepository} from '../shared/services/interfaces';

import {BuySlipItemService} from '../shared/services/buySlipItem.service';
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
    selector: 'buySlipItem-edit',
    templateUrl: 'buySlipItem-update.component.html'
})

export class BuySlipItemUpdateComponent implements OnInit {
    private sub: any;
    buySlipItemModel = <IBuySlipItemDetails>{};
    pageTitle: string = 'buySlipItem';
    errorMessage: string;
    buySlipItem: IBuySlipItem;
    apiHost: string;
    id: number;
    buySlipId: number;
    buySlipItemLoaded: boolean = false;
    name: string;
    buySlipItemDetails: IBuySlipItem;
    productSelected: number;
    repositorySelected: number;

    constructor(private _route: ActivatedRoute,
        private _router: Router,
        private _buySlipItemService: BuySlipItemService,
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
        this.loadRepositoryDetail();
        this.getProducts();
        this.getRepositories();
        this.buySlipId=+this._cookie.getCookie("buySlip");
    }

    loadRepositoryDetail() {
        this.slimLoader.start();

        this._buySlipItemService.getById(this.id)
            .subscribe((buySlipItem: IBuySlipItem) => {

                this.buySlipItemDetails = this.itemService.getSerialized<IBuySlipItem>(buySlipItem);
                this.productSelected = this.buySlipItemDetails.ProductId;
                this.repositorySelected = this.buySlipItemDetails.RepositoryId;

                this.buySlipItemModel.Id = buySlipItem.Id;
                this.buySlipItemModel.Price = buySlipItem.Price;
                this.buySlipItemModel.Description = buySlipItem.Description;
                this.buySlipItemModel.Quantity = buySlipItem.Quantity;

                this.slimLoader.complete();

                this.buySlipItemLoaded = true;
                this.notificationService.printSuccessMessage(this.buySlipItemDetails.Id.toString());
            },
            error => {
                this.slimLoader.complete();
                this.notificationService.printErrorMessage('خطا ' + error);
            });
    }

    getProducts() {
        this._productService.getAll()
            .subscribe((products: IProduct[]) => {
                this.buySlipItemModel.Products = products;
            },
            error => {
                this.slimLoader.complete();
                this.notificationService.printErrorMessage('Failed to load schedule. ' + error);
            });
    }

    getRepositories() {
        this._repositoryService.getAll()
            .subscribe((repositories: IRepository[]) => {
                this.buySlipItemModel.Repositories = repositories;
            },
            error => {
                this.slimLoader.complete();
                this.notificationService.printErrorMessage('Failed to load schedule. ' + error);
            });
    }

    updateBuySlipItem(editBuySlipItemForm: NgForm) {
        console.log(editBuySlipItemForm.value);

        this.slimLoader.start();
        var mapBuySlipItem = this.mapmapping.mapBuySlipItemDetailToBuySlipItem(this.buySlipItemModel);
        mapBuySlipItem.ProductId = this.productSelected;
        mapBuySlipItem.RepositoryId = this.repositorySelected;

        this._buySlipItemService.update(mapBuySlipItem)
            .subscribe(() => {
                this.notificationService.printSuccessMessage('انجام شد');
                this.slimLoader.complete();


                this._router.navigate(['/updateBuySlip/' + this.buySlipId]);
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
        this.notificationService.printErrorMessage(this.buySlipItem.Id.toString());
        this._buySlipItemService.update(this.buySlipItemDetails)
            .subscribe(() => {
                this.notificationService.printSuccessMessage('انجام شد');
                this.slimLoader.complete();
            },
            error => {
                this.slimLoader.complete();
                this.notificationService.printErrorMessage('خطا  ' + error);
            });
    }

    //ngOnDestroy(): void {
    //    this.sub.unsubscribe();//we must unsubscribe before Angular destroys the component.Failure to do so could create a memory leak.
    //}

    onBack(): void {
        this._router.navigate(['/buySlipItemList']);
    }
}

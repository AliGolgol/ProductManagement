import {Component, OnInit, OnDestroy} from '@angular/core';
import {Router, ActivatedRoute}from '@angular/router';
import {NgForm} from '@angular/forms';

import {IProductDetails, IProduct, IProductCategory, IManufacturer} from '../shared/services/interfaces';
import {ProductService} from '../shared/services/product.service';
import {ItemsService} from '../shared/utils/items.service';
import {NotificationService} from '../shared/utils/notification.service';
import {ConfigService} from '../shared/utils/config.service';
import {SlimLoadingBarService} from 'ng2-slim-loading-bar/ng2-slim-loading-bar';
import {ROUTER_DIRECTIVES} from '@angular/router';
import {MappingService} from '../shared/utils/mapping.service';
import {ProductCategoryService} from '../shared/services/productCategory.service';
import {ManufacturerService} from '../shared/services/manufacturer.service';


@Component({
    moduleId: module.id,
    selector: 'product-edit',
    templateUrl: 'product-update.component.html'
})

export class ProductUpdateComponent implements OnInit {
    private sub: any;
    productModel = <IProductDetails>{};
    pageTitle: string = 'product Type';
    errorMessage: string;
    product: IProduct;
    apiHost: string;
    id: number;
    productLoaded: boolean = false;
    name: string;
    productDetails: IProduct;
    productCategorySelected: number;
    manufacturerSelected: number;

    constructor(private _route: ActivatedRoute,
        private _router: Router,
        private _productService: ProductService,
        private _productCategoryService: ProductCategoryService,
        private _manufacturerSrvice: ManufacturerService,
        private itemService: ItemsService,
        private notificationService: NotificationService,
        private configService: ConfigService,
        private slimLoader: SlimLoadingBarService,
        private mapmapping: MappingService) { }


    ngOnInit() {
        this.id = +this._route.snapshot.params['id'];
        this.apiHost = this.configService.getApiHost();
        this.loadRepositoryDetail();
        this.getProductCategories();
        this.getManufacturers();
    }

    loadRepositoryDetail() {
        this.slimLoader.start();

        this._productService.getById(this.id)
            .subscribe((product: IProduct) => {

                this.productDetails = this.itemService.getSerialized<IProduct>(product);
                this.productCategorySelected = this.productDetails.ProductCategoryId;
                this.manufacturerSelected = this.productDetails.ManufacturerId;

                this.productModel.Id = product.Id;
                this.productModel.Name = product.Name;
                this.productModel.Fee = product.Fee;
                this.productModel.MinimumBalance = product.MinimumBalance;
                this.productModel.PackageCount = product.PackageCount;
                this.productModel.QuantityPerUnit = product.QuantityPerUnit;

                this.slimLoader.complete();

                this.productLoaded = true;
                this.notificationService.printSuccessMessage(this.productDetails.Id.toString());
            },
            error => {
                this.slimLoader.complete();
                this.notificationService.printErrorMessage('خطا ' + error);
            });
    }

    getProductCategories() {
        this._productCategoryService.getAll()
            .subscribe((productCategory: IProductCategory[]) => {
                this.productModel.ProductCategory = productCategory;
            },
            error => {
                this.slimLoader.complete();
                this.notificationService.printErrorMessage('Failed to load schedule. ' + error);
            });
    }

    getManufacturers() {
        this._manufacturerSrvice.getMan(sessionStorage.getItem('tokenKey'))
            .subscribe((manufacturer: IManufacturer[]) => {
                this.productModel.Manufacturer = manufacturer;
            },
            error => {
                this.slimLoader.complete();
                this.notificationService.printErrorMessage('Failed to load schedule. ' + error);
            });
    }

    updateProduct(editProductForm: NgForm) {
        console.log(editProductForm.value);

        this.slimLoader.start();
        var mapProduct = this.mapmapping.mapProductDetailToProduct(this.productModel);
        mapProduct.ProductCategoryId = this.productCategorySelected;
        mapProduct.ManufacturerId = this.manufacturerSelected;

        this._productService.update(mapProduct)
            .subscribe(() => {
                this.notificationService.printSuccessMessage('انجام شد');
                this.slimLoader.complete();
                this._router.navigate(['/productList']);
            },
            error => {
                this.slimLoader.complete();
                this.notificationService.printErrorMessage('خطا  ' + error);
            });
    }

    onChange(prdCatValue) {
        console.log(prdCatValue);
        this.notificationService.printErrorMessage(prdCatValue);
        this.productCategorySelected = prdCatValue;
    }

    onChanged(manValue) {
        console.log(manValue);
        this.notificationService.printErrorMessage(manValue);
        this.manufacturerSelected = manValue;
    }

    onSubmit(form): void {
        this.slimLoader.start();
        this.notificationService.printErrorMessage(this.product.Id.toString());
        this._productService.update(this.productDetails)
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
        this._router.navigate(['/productList']);
    }
}

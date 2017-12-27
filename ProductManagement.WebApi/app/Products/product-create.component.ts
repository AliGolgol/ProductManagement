import {Component, trigger, OnInit, state, style, animate, transition, ViewChild, Input, Output } from '@angular/core';
import {Router} from '@angular/router';
import {IProduct, IProductDetails, IProductCategory, IManufacturer} from '../shared/services/interfaces';
import {ProductService} from '../shared/services/product.service';
import {MODAL_DIRECTIVES, ModalComponent} from 'ng2-bs3-modal/ng2-bs3-modal';
import {ItemsService} from '../shared/utils/items.service';
import {NotificationService} from '../shared/utils/notification.service';
import {SlimLoadingBarService} from 'ng2-slim-loading-bar/ng2-slim-loading-bar';
import {MappingService} from '../shared/utils/mapping.service';
import {ProductCategoryService} from '../shared/services/productCategory.service';
import {ManufacturerService} from '../shared/services/manufacturer.service';

@Component({
    selector: 'product-add',
    templateUrl: 'app/Products/product-create.component.html',
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
export class CreateProductComponent implements OnInit {
    productModel = <IProductDetails>{};//creates an empty object for an interface
    repoistory: IProduct;
    pageTitle: string = "add product";
    productCatSelected: number;
    manufacturerSelelcted: number;

    constructor(
        private _router: Router,
        private _productService: ProductService,
        private _productCategoryService: ProductCategoryService,
        private _manufacturerSrvice: ManufacturerService,
        private itemsService: ItemsService,
        private notificationService: NotificationService,
        private slimLoader: SlimLoadingBarService,
        private mapping: MappingService) { }

    ngOnInit() {
        this.getProductCategories();
        this.getManufacturers();
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
        this._manufacturerSrvice.getAll(sessionStorage.getItem('tokenKey'))
            .subscribe((manufacturer: IManufacturer[]) => {
                this.productModel.Manufacturer = manufacturer;
            },
            error => {
                this.slimLoader.complete();
                this.notificationService.printErrorMessage('Failed to load schedule. ' + error);
            });
    }

    log(Name): void {
        console.log(Name);
    }

    onSubmit(form): void {
        console.log(form);
        console.log(this.productModel);
        var mapProduct = this.mapping.mapProductDetailToProduct(this.productModel);
        mapProduct.ProductCategoryId = this.productCatSelected;
        mapProduct.ManufacturerId = this.manufacturerSelelcted;

        this._productService.add(mapProduct)
            .subscribe((product: IProduct) => {
                console.log(`ID: ${product.Id}`);
                this.modalAdd.close();
            });
    }

    onChange(prdCatValue) {
        console.log(prdCatValue);
        this.notificationService.printErrorMessage(prdCatValue);
        this.productCatSelected = prdCatValue;
    }

    onChanged(manValue) {
        console.log(manValue);
        this.notificationService.printErrorMessage(manValue);
        this.manufacturerSelelcted = manValue;
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
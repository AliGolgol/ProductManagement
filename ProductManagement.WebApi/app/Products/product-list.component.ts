import { Component, OnInit, trigger, state, style, animate, transition, ViewChild, Input, Output } from '@angular/core';
import { HTTP_PROVIDERS } from '@angular/http';
import {ROUTER_DIRECTIVES} from '@angular/router';
import {IProduct, Pagination, PaginatedResult} from '../shared/services/interfaces';
import {CreateProductComponent} from './product-create.component';
import {ProductService} from '../shared/services/product.service';
import {SlimLoadingBarService} from 'ng2-slim-loading-bar/ng2-slim-loading-bar';
import {MODAL_DIRECTIVES, ModalComponent} from 'ng2-bs3-modal/ng2-bs3-modal';
import {PAGINATION_DIRECTIVES, PaginationComponent} from 'ng2-bootstrap';
import {ConfigService} from '../shared/utils/config.service';
import {NotificationService} from '../shared/utils/notification.service';
import {ItemsService} from '../shared/utils/items.service';

@Component({
    moduleId: module.id,
    selector: 'pm-product',
    templateUrl: 'product-list.component.html',
    directives: [ROUTER_DIRECTIVES, MODAL_DIRECTIVES,
        PAGINATION_DIRECTIVES, CreateProductComponent],
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
export class ProductListComponent implements OnInit {

    pageTitle: string = 'Product List';
    errorMessage: string;
    showNew: boolean = false;
    products: IProduct[];

    public itemsPerPage: number = 2;
    public totalItems: number = 1;

    //Modal Properties
    @ViewChild('modal')
    modal: ModalComponent;
    addMoal: ModalComponent;
    items: string[] = ['item1', 'item2', 'item3'];
    selected: string;
    output: string;
    selectedProductId: number;
    productDetails: IProduct;
    selectedProductLoaded: boolean = false;
    index: number = 0;
    backdropOptions = [true, false, 'static'];
    animation: boolean = true;
    keyboard: boolean = true;
    backdrop: string | boolean = true;
    addOrDetail: boolean = false;

    constructor(private _productService: ProductService,
        private slimLoader: SlimLoadingBarService,
        private itemService: ItemsService,
        private notificationService: NotificationService) { }

    toggleImage(): void {
        this.showNew = !this.showNew;
    }

    ngOnInit(): void {
        console.log('In OnInit');
        this.loadProduct();
    }

    //Show details of product type
    viewDetails(id: number) {
        this.addOrDetail = true;
        this.selectedProductId = id;
        this.modal.open('lg');
        console.log('test detial');
    }

    addRepType(b: boolean) {
        this.addOrDetail = b;
        this.modal.open('lg');
        console.log('test detial');
    }

    //Get all product type
    loadProduct() {
        this.slimLoader.color = "blue";
        this.slimLoader.start();
        this._productService.getAll()
            .subscribe(
            product => this.products = product,
            error => this.errorMessage = <any>error);
        this.notificationService.printSuccessMessage(this.errorMessage);
        this.slimLoader.complete();
    }

    //Delete the repsitory type
    delProduct(product: IProduct) {
        this.notificationService.openConfirmationDialog('آیا مطمئن هستید؟', () => {
            this.slimLoader.color = "blue";
            this.slimLoader.start();
            this._productService.del(product.Id)
                .subscribe(() => {
                    this.itemService.removeItemFromArray<IProduct>(this.products, product);
                    this.notificationService.printSuccessMessage(product.Name + 'حذف شد');
                    this.slimLoader.complete();
                },
                error => {
                    this.slimLoader.complete();
                    this.notificationService.printErrorMessage('خطا در حذف ' + product.Name + ' ' + error);
                });
        });
    }

    closed() { this.output = '(closed)' + this.selected; }

    dismissed() { this.output = '(dismissed)' };
    opened() {
        this.slimLoader.start();

        this._productService.getById(this.selectedProductId)
            .subscribe((product: IProduct) => {
                this.productDetails = this.itemService.getSerialized<IProduct>(product);
                this.slimLoader.complete();
                this.selectedProductLoaded = true;
            },
            error => {
                this.slimLoader.complete();
            });

        this.output = '(opened)';
    }
}
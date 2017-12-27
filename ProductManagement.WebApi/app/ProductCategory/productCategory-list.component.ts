﻿import { Component, OnInit, trigger, state, style, animate, transition, ViewChild, Input, Output } from '@angular/core';
import { HTTP_PROVIDERS } from '@angular/http';
import {ROUTER_DIRECTIVES} from '@angular/router';
import {IProductCategory, Pagination, PaginatedResult} from '../shared/services/interfaces';
import {CreateProductCategoryComponent} from './productCategory-create.component';
import {ProductCategoryService} from '../shared/services/productCategory.service';
import {SlimLoadingBarService} from 'ng2-slim-loading-bar/ng2-slim-loading-bar';
import {MODAL_DIRECTIVES, ModalComponent} from 'ng2-bs3-modal/ng2-bs3-modal';
import {PAGINATION_DIRECTIVES, PaginationComponent} from 'ng2-bootstrap';
import {ConfigService} from '../shared/utils/config.service';
import {NotificationService} from '../shared/utils/notification.service';
import {ItemsService} from '../shared/utils/items.service';

@Component({
    moduleId: module.id,
    selector: 'pm-productCategory',
    templateUrl: 'productCategory-list.component.html',
    directives: [ROUTER_DIRECTIVES, MODAL_DIRECTIVES,
        PAGINATION_DIRECTIVES, CreateProductCategoryComponent],
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
export class ProductCategoryListComponent implements OnInit {

    pageTitle: string = 'ProductCategory List';
    errorMessage: string;
    showNew: boolean = false;
    productCategories: IProductCategory[];

    public itemsPerPage: number = 2;
    public totalItems: number = 1;

    //Modal Properties
    @ViewChild('modal')
    modal: ModalComponent;
    addMoal: ModalComponent;
    items: string[] = ['item1', 'item2', 'item3'];
    selected: string;
    output: string;
    selectedProductCategoryId: number;
    productCategoryDetails: IProductCategory;
    selectedProductCategoryLoaded: boolean = false;
    index: number = 0;
    backdropOptions = [true, false, 'static'];
    animation: boolean = true;
    keyboard: boolean = true;
    backdrop: string | boolean = true;
    addOrDetail: boolean = false;

    constructor(private _productCategoryService: ProductCategoryService,
        private slimLoader: SlimLoadingBarService,
        private itemService: ItemsService,
        private notificationService: NotificationService) { }

    toggleImage(): void {
        this.showNew = !this.showNew;
    }

    ngOnInit(): void {
        console.log('In OnInit');
        this.loadProductCategory();
    }

    //Show details of productCategory type
    viewDetails(id: number) {
        this.addOrDetail = true;
        this.selectedProductCategoryId = id;
        this.modal.open('lg');
        console.log('test detial');
    }

    addRepType(b: boolean) {
        this.addOrDetail = b;
        this.modal.open('lg');
        console.log('test detial');
    }

    //Get all productCategory type
    loadProductCategory() {
        this.slimLoader.color = "blue";
        this.slimLoader.start();
        this._productCategoryService.getAll()
            .subscribe(
            productCategories => this.productCategories = productCategories,
            error => this.errorMessage = <any>error);
        this.notificationService.printSuccessMessage(this.errorMessage);
        this.slimLoader.complete();
    }

    //Delete the repsitory type
    delProductCategory(productCategory: IProductCategory) {
        this.notificationService.openConfirmationDialog('آیا مطمئن هستید؟', () => {
            this.slimLoader.color = "blue";
            this.slimLoader.start();
            this._productCategoryService.del(productCategory.Id)
                .subscribe(() => {
                    this.itemService.removeItemFromArray<IProductCategory>(this.productCategories, productCategory);
                    this.notificationService.printSuccessMessage(productCategory.Name + 'حذف شد');
                    this.slimLoader.complete();
                },
                error => {
                    this.slimLoader.complete();
                    this.notificationService.printErrorMessage('خطا در حذف ' + productCategory.Name + ' ' + error);
                });
        });
    }

    closed() { this.output = '(closed)' + this.selected; }

    dismissed() { this.output = '(dismissed)' };
    opened() {
        this.slimLoader.start();

        this._productCategoryService.getById(this.selectedProductCategoryId)
            .subscribe((productCategory: IProductCategory) => {
                this.productCategoryDetails = this.itemService.getSerialized<IProductCategory>(productCategory);
                this.slimLoader.complete();
                this.selectedProductCategoryLoaded = true;
            },
            error => {
                this.slimLoader.complete();
            });

        this.output = '(opened)';
    }
}
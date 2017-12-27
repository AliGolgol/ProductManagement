import { Component, OnInit, trigger, state, style, animate, transition,
    ViewChild, Input, Output, EventEmitter } from '@angular/core';
import { HTTP_PROVIDERS } from '@angular/http';
import {ROUTER_DIRECTIVES} from '@angular/router';

import {IBuySlipItem, Pagination, PaginatedResult, IRepository} from '../shared/services/interfaces';
import {CreateBuySlipItemComponent} from './buySlipItem-create.component';

import {BuySlipItemService} from '../shared/services/buySlipItem.service';
import {ProductService} from '../shared/services/product.service';
import {RepositoryService} from '../shared/services/repository.service';

import {SlimLoadingBarService} from 'ng2-slim-loading-bar/ng2-slim-loading-bar';
import {MODAL_DIRECTIVES, ModalComponent} from 'ng2-bs3-modal/ng2-bs3-modal';
import {PAGINATION_DIRECTIVES, PaginationComponent} from 'ng2-bootstrap';
import {ConfigService} from '../shared/utils/config.service';
import {NotificationService} from '../shared/utils/notification.service';
import {ItemsService} from '../shared/utils/items.service';
import {Cook} from '../shared/services/cookie.service';

@Component({
    moduleId: module.id,
    selector: 'pm-buySlipItem',
    templateUrl: 'buySlipItem-list.component.html',
    directives: [ROUTER_DIRECTIVES, MODAL_DIRECTIVES,
        PAGINATION_DIRECTIVES, CreateBuySlipItemComponent],
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
export class BuySlipItemListComponent implements OnInit {

    pageTitle: string = 'Buy Slip Item List';
    errorMessage: string;
    showNew: boolean = false;
    buySlipItems: IBuySlipItem[];
    id: number;
    public itemsPerPage: number = 2;
    public totalItems: number = 1;
    isCreated: boolean = true;

    @Output() notify = new EventEmitter<any>();

    //Modal Properties
    @ViewChild('modal')
    modal: ModalComponent;
    addMoal: ModalComponent;
    items: string[] = ['item1', 'item2', 'item3'];
    selected: string;
    output: string;
    selectedBuySlipItemId: number;
    buySlipItemDetails: IBuySlipItem;
    selectedProductLoaded: boolean = false;
    index: number = 0;
    backdropOptions = [true, false, 'static'];
    animation: boolean = true;
    keyboard: boolean = true;
    backdrop: string | boolean = true;
    addOrDetail: boolean = false;

    constructor(private _buySlipItemService: BuySlipItemService,
        private _productService: ProductService,
        private _repositoryService: RepositoryService,
        private slimLoader: SlimLoadingBarService,
        private itemService: ItemsService,
        private notificationService: NotificationService,
        private _cookie: Cook) { }

    toggleImage(): void {
        this.showNew = !this.showNew;
    }

    ngOnInit(): void {
        
        this.id = localStorage.getItem("buy");
        if (localStorage.getItem('buy'))
        {
            this.isCreated = false;
        }
        console.log('In OnInit');
        this.loadSlipItem();
    }

    //Show details of buySlipItem type
    viewDetails(id: number) {
        this.selectedBuySlipItemId = id;
        this.modal.open('lg');
        console.log('test detial');
    }

    addRepType(b: boolean) {
        this.modal.open('lg');
        console.log('test detial');
    }

    //Get all buySlipItem type
    loadSlipItem() {
        this.slimLoader.color = "blue";
        this.slimLoader.start();
        if (localStorage.getItem("buy")) {
            this._buySlipItemService.getByBuySlipId(this.id)
                .subscribe((buySlipItem: IBuySlipItem[]) => {
                    this.buySlipItems = buySlipItem
                },
                error => this.errorMessage = <any>error);
        }
        //this.notificationService.printSuccessMessage('شناسه' + this.id.toString());
        this.slimLoader.complete();
    }

    //Delete the repsitory type
    delBuySlipItem(buySlipItem: IBuySlipItem) {
        this.notificationService.openConfirmationDialog('آیا مطمئن هستید؟', () => {
            this.slimLoader.color = "blue";
            this.slimLoader.start();
            this._buySlipItemService.del(buySlipItem.Id)
                .subscribe(() => {
                    this.itemService.removeItemFromArray<IBuySlipItem>(this.buySlipItems, buySlipItem);
                    this.notificationService.printSuccessMessage(buySlipItem.Id + 'حذف شد');
                    this.slimLoader.complete();
                },
                error => {
                    this.slimLoader.complete();
                    this.notificationService.printErrorMessage('خطا در حذف ' + buySlipItem.Id + ' ' + error);
                });
        });
    }

    onClicked(b: IBuySlipItem[]): void {
        //this.buySlipItems = b;
        this.loadSlipItem();
        //this.ngOnInit();
    }

    closed() { this.output = '(closed)' + this.selected; }

    dismissed() { this.output = '(dismissed)' };

    opened() {
        this.slimLoader.start();

        this._buySlipItemService.getById(this.selectedBuySlipItemId)
            .subscribe((buySlipItem: IBuySlipItem) => {
                this.buySlipItemDetails = this.itemService.getSerialized<IBuySlipItem>(buySlipItem);
                this.slimLoader.complete();
                this.selectedProductLoaded = true;
            },
            error => {
                this.slimLoader.complete();
            });

        this.output = '(opened)';
    }
}
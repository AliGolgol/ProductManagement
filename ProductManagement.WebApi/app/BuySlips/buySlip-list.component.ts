import { Component, OnInit, trigger, state, style, animate, transition, ViewChild, Input, Output } from '@angular/core';
import { HTTP_PROVIDERS } from '@angular/http';
import {ROUTER_DIRECTIVES} from '@angular/router';
import {Pagination, PaginatedResult, IBuySlip, IBuySlipDetails, IPageList} from '../shared/services/interfaces';
import {CreateBuySlipComponent} from '../BuySlips/buySlip-create.component';
import {BuySlipService} from '../shared/services/buySlip.service';
import {SlimLoadingBarService} from 'ng2-slim-loading-bar/ng2-slim-loading-bar';
import {MODAL_DIRECTIVES, ModalComponent} from 'ng2-bs3-modal/ng2-bs3-modal';
import {PAGINATION_DIRECTIVES, PaginationComponent} from 'ng2-bootstrap';
import {ConfigService} from '../shared/utils/config.service';
import {NotificationService} from '../shared/utils/notification.service';
import {ItemsService} from '../shared/utils/items.service';

@Component({
    moduleId: module.id,
    selector: 'pm-buySlip',
    templateUrl: 'buySlip-list.component.html',
    directives: [ROUTER_DIRECTIVES, MODAL_DIRECTIVES,
        PAGINATION_DIRECTIVES, CreateBuySlipComponent],
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
export class BuySlipListComponent implements OnInit {

    pageTitle: string = 'Product List';
    errorMessage: string;
    showNew: boolean = false;
    buySlips: IBuySlip[];

    public itemsPerPage: number = 2;
    public totalItems: number = 5;
    public currentPage: number = 1;
    public totalPage: number = 4;
    p: IPageList = { CurrentPage: 1, ItemsPerPage: 13 }

    //Modal Properties
    @ViewChild('modal')
    modal: ModalComponent;
    addMoal: ModalComponent;
    items: string[] = ['item1', 'item2', 'item3'];
    selected: string;
    output: string;
    selectedBuySlipId: number;
    buySlipDetails: IBuySlip;
    selectedBuySlipLoaded: boolean = false;
    index: number = 0;
    backdropOptions = [true, false, 'static'];
    animation: boolean = true;
    keyboard: boolean = true;
    backdrop: string | boolean = true;

    constructor(private _buySlipService: BuySlipService,
        private slimLoader: SlimLoadingBarService,
        private itemService: ItemsService,
        private notificationService: NotificationService) { }

    toggleImage(): void {
        this.showNew = !this.showNew;
    }

    ngOnInit(): void {
        console.log('In OnInit');
        this.loadBuySlip();
    }

    //Show details of buySlip type
    viewDetails(id: number) {
        this.selectedBuySlipId = id;
        this.modal.open('lg');
        console.log('test detial');
    }

    //Get all buySlip type
    loadBuySlip() {
        this.slimLoader.color = "blue";
        this.slimLoader.start();
        this._buySlipService.getAll(this.p)
            .subscribe(
            buySlip => this.buySlips = buySlip,
            error => this.errorMessage = <any>error);
        this.notificationService.printSuccessMessage(this.errorMessage);
        this.slimLoader.complete();
    }

    //Delete the repsitory type
    delBuySlip(buySlip: IBuySlip) {
        this.notificationService.openConfirmationDialog('آیا مطمئن هستید؟', () => {
            this.slimLoader.color = "blue";
            this.slimLoader.start();
            this._buySlipService.del(buySlip.Id)
                .subscribe(() => {
                    this.itemService.removeItemFromArray<IBuySlip>(this.buySlips, buySlip);
                    this.notificationService.printSuccessMessage(buySlip.Id + 'حذف شد');
                    this.slimLoader.complete();
                },
                error => {
                    this.slimLoader.complete();
                    this.notificationService.printErrorMessage('خطا در حذف ' + buySlip.Id + ' ' + error);
                });
        });
    }

    closed() { this.output = '(closed)' + this.selected; }

    dismissed() { this.output = '(dismissed)' };

    opened() {
        this.slimLoader.start();

        this._buySlipService.getById(this.selectedBuySlipId)
            .subscribe((buySlip: IBuySlip) => {
                this.buySlipDetails = this.itemService.getSerialized<IBuySlip>(buySlip);
                this.slimLoader.complete();
                this.selectedBuySlipLoaded = true;
            },
            error => {
                this.slimLoader.complete();
            });

        this.output = '(opened)';
    }

    onClicked(b: IBuySlip[]): void {
        debugger
        this.buySlips = b;
        //this.load();
        this.notificationService.printSuccessMessage(localStorage.getItem("buys"));
    }

    pageChanged(event: any): void {
        this.currentPage = event.page;
        this.p.CurrentPage = event.page;
        this.loadBuySlip();
    }
}
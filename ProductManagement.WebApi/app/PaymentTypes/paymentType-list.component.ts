import { Component, OnInit, trigger, state, style, animate, transition, ViewChild, Input, Output } from '@angular/core';
import { HTTP_PROVIDERS } from '@angular/http';
import {ROUTER_DIRECTIVES} from '@angular/router';

import {IPaymentType, Pagination, PaginatedResult} from '../shared/services/interfaces';

import {PaymentTypeCreateComponent} from './paymentType-create.component';
import {PaymentTypeService} from '../shared/services/paymentType.service';
import {SlimLoadingBarService} from 'ng2-slim-loading-bar/ng2-slim-loading-bar';
import {MODAL_DIRECTIVES, ModalComponent} from 'ng2-bs3-modal/ng2-bs3-modal';
import {PAGINATION_DIRECTIVES, PaginationComponent} from 'ng2-bootstrap';
import {ConfigService} from '../shared/utils/config.service';
import {NotificationService} from '../shared/utils/notification.service';
import {ItemsService} from '../shared/utils/items.service';
import {Cook} from '../shared/services/cookie.service';


@Component({
    moduleId: module.id,
    selector: 'pm-paymentType',
    templateUrl: 'paymentType-list.component.html',
    directives: [ROUTER_DIRECTIVES, MODAL_DIRECTIVES, PAGINATION_DIRECTIVES, PaymentTypeCreateComponent],
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
export class PaymentTypeListComponent implements OnInit {

    pageTitle: string = 'Paymeny List';
    errorMessage: string;
    showNew: boolean = false;
    paymentTypes: IPaymentType[];

    public itemsPerPage: number = 2;
    public totalItems: number = 10;
    public currentPage: number = 1;

    //Modal Properties
    @ViewChild('modal')
    modal: ModalComponent;
    items: string[] = ['item1', 'item2', 'item3'];
    selected: string;
    output: string;
    selectedPayId: number;
    payDetails: IPaymentType;
    selectedPayLoaded: boolean = false;
    index: number = 0;
    backdropOptions = [true, false, 'static'];
    animation: boolean = true;
    keyboard: boolean = true;
    backdrop: string | boolean = true;

    constructor(private _paymentTypeService: PaymentTypeService,
        private slimLoader: SlimLoadingBarService,
        private itemService: ItemsService,
        private notificationService: NotificationService,
        private _cook: Cook) { }

    toggleImage(): void {
        this.showNew = !this.showNew;
    }

    ngOnInit(): void {
        console.log('In OnInit');
        this.load();        
    }

    //Show details of repository type
    viewDetails(id: number) {
        this.selectedPayId = id;
        this.modal.open('lg');
        console.log('test detial');
    }

    addPaymentType() {
        this.modal.open('lg');
        console.log('test detial');
    }

    load() {
        this.slimLoader.start();
        this._paymentTypeService.getAll()
            .subscribe((res: IPaymentType[]) => {
                this.paymentTypes = res;
            },
            error => this.errorMessage = <any>error);
        this.notificationService.printSuccessMessage(this.errorMessage);
        this.slimLoader.complete();
        
        //this.notificationService.printErrorMessage(this.paymentTypes.length.toString());
    }    

    //Delete the repsitory type
    delPaymentType(paymentType: IPaymentType) {
        this.notificationService.openConfirmationDialog('آیا مطمئن هستید؟', () => {
            this.slimLoader.start();
            this._paymentTypeService.del(paymentType.Id)
                .subscribe(() => {
                    this.itemService.removeItemFromArray<IPaymentType>(this.paymentTypes, paymentType);
                    this.notificationService.printSuccessMessage(paymentType.Name + 'حذف شد ');
                    this.slimLoader.complete();
                },
                error => {
                    this.slimLoader.complete();
                    this.notificationService.printErrorMessage('خطا در حذف ' + paymentType.Name + ' ' + error);
                });
        });
    }


    onClicked(man: IPaymentType[]): void {
        this.paymentTypes = man;
        this.load();
        this.ngOnInit();
    }

    closed() { this.output = '(closed)' + this.selected; }

    dismissed() { this.output = '(dismissed)' };

    opened() {
        this.slimLoader.start();
        this._paymentTypeService.getById(this.selectedPayId)
            .subscribe((paymentType: IPaymentType) => {
                this.payDetails = this.itemService.getSerialized<IPaymentType>(paymentType);
                this.slimLoader.complete();
                this.selectedPayLoaded = true;
            },
            error => {
                this.slimLoader.complete();
            });
        this.output = '(opened)';
    }

    pageChanged(event: any): void {
        this.currentPage = event.page;
        this.notificationService.printErrorMessage(this.currentPage.toString());
        this.load();
        //console.log('Page changed to: ' + event.page);
        //console.log('Number items per page: ' + event.itemsPerPage);
    };
}
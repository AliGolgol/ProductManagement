import { Component, OnInit, trigger, state, style, animate, transition, ViewChild, Input, Output } from '@angular/core';
import { HTTP_PROVIDERS } from '@angular/http';
import {ROUTER_DIRECTIVES} from '@angular/router';
import {ISupplier, Pagination, PaginatedResult} from '../shared/services/interfaces';
import {SupplierCreateComponent} from './supplier-create.component';
import {SupplierService} from '../shared/services/supplier.service';
import {SlimLoadingBarService} from 'ng2-slim-loading-bar/ng2-slim-loading-bar';
import {MODAL_DIRECTIVES, ModalComponent} from 'ng2-bs3-modal/ng2-bs3-modal';
import {PAGINATION_DIRECTIVES, PaginationComponent} from 'ng2-bootstrap';
import {ConfigService} from '../shared/utils/config.service';
import {NotificationService} from '../shared/utils/notification.service';
import {ItemsService} from '../shared/utils/items.service';

//import {ItemsService} from '../shared/utils/items.service';

@Component({
    moduleId: module.id,
    selector: 'pm-supplier',
    templateUrl: 'supplier-list.component.html',
    directives: [ROUTER_DIRECTIVES, MODAL_DIRECTIVES, PAGINATION_DIRECTIVES, SupplierCreateComponent],
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
export class SupplierListComponent implements OnInit {

    pageTitle: string = 'Repository List';
    errorMessage: string;
    showNew: boolean = false;
    suppliers: ISupplier[];

    public itemsPerPage: number = 2;
    public totalItems: number = 1;

    //Modal Properties
    @ViewChild('modal')
    modal: ModalComponent;
    addMoal: ModalComponent;
    items: string[] = ['item1', 'item2', 'item3'];
    selected: string;
    output: string;
    selectedSupId: number;
    supDetails: ISupplier;
    selectedSupLoaded: boolean = false;
    index: number = 0;
    backdropOptions = [true, false, 'static'];
    animation: boolean = true;
    keyboard: boolean = true;
    backdrop: string | boolean = true;

    constructor(private _supplierService: SupplierService,
        private slimLoader: SlimLoadingBarService,
        private itemService: ItemsService,
        private notificationService: NotificationService) { }

    toggleImage(): void {
        this.showNew = !this.showNew;
    }

    ngOnInit(): void {
        console.log('In OnInit');
        this.load();
    }

    //Show details of repository type
    viewDetails(id: number) {
        this.selectedSupId = id;
        this.modal.open('lg');
        console.log('test detial');
    }

    addSupplier() {
        this.modal.open('lg');
        console.log('test detial');
    }
    //Get all repository type
    load() {
        this.slimLoader.start();
        this._supplierService.getAll()
            .subscribe(
            response => this.suppliers = response,
            error => this.errorMessage = <any>error);
        this.notificationService.printSuccessMessage(this.errorMessage);
    }

    //Delete the repsitory type
    delsupplier(supplier: ISupplier) {
        this.notificationService.openConfirmationDialog('آیا مطمئن هستید؟', () => {
            this.slimLoader.start();
            this._supplierService.del(supplier.Id)
                .subscribe(() => {
                    this.itemService.removeItemFromArray<ISupplier>(this.suppliers, supplier);
                    this.notificationService.printSuccessMessage(supplier.Id.toString() + 'حذف شد  ');
                    this.slimLoader.complete();
                },
                error => {
                    this.slimLoader.complete();
                    this.notificationService.printErrorMessage('خطا در حذف ' + supplier.Id.toString() + ' ' + error);
                });
        });
    }

    closed() { this.output = '(closed)' + this.selected; }

    dismissed() { this.output = '(dismissed)' };

    opened() {
        this.slimLoader.start();
        this._supplierService.getById(this.selectedSupId)
            .subscribe((supplier: ISupplier) => {
                this.supDetails = this.itemService.getSerialized<ISupplier>(supplier);
                this.slimLoader.complete();
                this.selectedSupLoaded = true;
            },
            error => {
                this.slimLoader.complete();
            });
        this.output = '(opened)';
    }
}
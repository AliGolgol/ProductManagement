import {Component, trigger, state, style, animate,
    transition, ViewChild, Input, Output, EventEmitter, OnInit } from '@angular/core';

import {Router} from '@angular/router';
import {IBuySlipItem, IBuySlipItemDetails, IProduct, IRepository, IBuySlip, IDate,
    IBuySlipDetails}
from '../shared/services/interfaces';

import {BuySlipItemService} from '../shared/services/buySlipItem.service';
import {ProductService} from '../shared/services/product.service';
import {BuySlipService} from '../shared/services/buySlip.service';
import {RepositoryService} from '../shared/services/repository.service';
import {DateService} from '../shared/services/date.service';

import {MODAL_DIRECTIVES, ModalComponent} from 'ng2-bs3-modal/ng2-bs3-modal';
import {ItemsService} from '../shared/utils/items.service';
import {NotificationService} from '../shared/utils/notification.service';
import {SlimLoadingBarService} from 'ng2-slim-loading-bar/ng2-slim-loading-bar';
import {MappingService} from '../shared/utils/mapping.service';
import {Cook} from '../shared/services/cookie.service';

@Component({
    selector: 'buySlipItem-add',
    templateUrl: 'app/BuySlipItems/buySlipItem-create.component.html',
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

export class CreateBuySlipItemComponent implements OnInit {
    buySlipItemModel = <IBuySlipItemDetails>{};//creates an empty object for an interface
    buySlip = <IBuySlip>{};
    buySlipId: number;
    pageTitle: string = "add buySlipItem";
    productSelected: number = 1;
    repositorySelelcted: number = 1;
    @Input() buySlipItem: IBuySlipItem[];
    date: IDate;

    @ViewChild('mymodal')
    modalAdd: ModalComponent;
    selected: string;
    output: string;
    selectedRepositoryLoaded: boolean = false;
    backdropOptions = [true, false, 'static'];
    animation: boolean = true;
    keyboard: boolean = true;
    backdrop: string | boolean = true;

    constructor(
        private _router: Router,
        private _buySlipItemService: BuySlipItemService,
        private _productService: ProductService,
        private _buySlipService: BuySlipService,
        private _repositoryService: RepositoryService,
        private itemsService: ItemsService,
        private notificationService: NotificationService,
        private slimLoader: SlimLoadingBarService,
        private mapping: MappingService,
        private _cook: Cook,
        private _date: DateService) { }

        buySlipModel = <IBuySlipDetails>{};

    ngOnInit() {
        this.getProducts();
        this.getRepositories();
        this.getDate();
    }

    @Output() clicked = new EventEmitter<any>();

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

    log(Name): void {
        console.log(Name);
    }

    onSubmit(form): void {
        console.log(form);
        console.log(this.buySlipItemModel);
        
        this.buySlipId = localStorage.getItem('buy');
        var mapProduct = this.mapping.mapBuySlipItemDetailToBuySlipItem(this.buySlipItemModel);
        mapProduct.ProductId = this.productSelected;
        mapProduct.RepositoryId = this.repositorySelelcted;
        mapProduct.BuySlipId = this.buySlipId;

        this._buySlipItemService.add(mapProduct)
            .subscribe((buySlipItem: IBuySlipItem) => {
                console.log(`ID: ${buySlipItem.Id}`);
                this.modalAdd.close();
            });
        
            this._router.navigate(['/updateBuySlip', localStorage.getItem("buy")]);
       
    }

    insert() {

        this.buySlipId = +localStorage.getItem('buy');
        var mapProduct = this.mapping.mapBuySlipItemDetailToBuySlipItem(this.buySlipItemModel);
        mapProduct.ProductId = this.productSelected;
        mapProduct.RepositoryId = this.repositorySelelcted;
        mapProduct.BuySlipId = this.buySlipId;

        this._buySlipItemService.add(mapProduct)
            .subscribe((buySlipItem: IBuySlipItem) => {
                console.log(`ID: ${buySlipItem.Id}`);
                this.modalAdd.close();
            });
        
            this._router.navigate(['/updateBuySlip', localStorage.getItem("buy")]);
        
    }

    getDate(): void {
        this._date.get()
            .subscribe((resp: IDate) => {
                this.date = resp;
            },
            error => {
                this.slimLoader.complete();
                this.notificationService.printErrorMessage('Failed to load schedule. ' + error);
            });
    }

    onChange(prdValue) {
        console.log(prdValue);
        this.productSelected = prdValue;
    }

    onChanged(repValue) {
        console.log(repValue);
        this.repositorySelelcted = repValue;
    }

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

    back() {
        if (localStorage.getItem('buy')) {
            this._router.navigate(['/updateBuySlip', localStorage.getItem("buy")]);
        }
        else {
            this._router.navigate(['/buySlipList']);
        }
    }
}
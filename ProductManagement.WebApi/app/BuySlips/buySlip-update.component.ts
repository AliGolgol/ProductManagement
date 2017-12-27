import {Component, OnInit, OnDestroy} from '@angular/core';
import {Router, ActivatedRoute}from '@angular/router';
import {NgForm} from '@angular/forms';

import {IProduct, IBuySlipDetails, IBuySlipUpdate, ISupplier, IEntrySlipType,
    IBuySlipItem, IBuySlip} from '../shared/services/interfaces';
import {ProductService} from '../shared/services/product.service';
import {ItemsService} from '../shared/utils/items.service';
import {NotificationService} from '../shared/utils/notification.service';
import {ConfigService} from '../shared/utils/config.service';
import {SlimLoadingBarService} from 'ng2-slim-loading-bar/ng2-slim-loading-bar';
import {ROUTER_DIRECTIVES} from '@angular/router';
import {MappingService} from '../shared/utils/mapping.service';
import {BuySlipService} from '../shared/services/buySlip.service';
import {SupplierService} from '../shared/services/supplier.service';
import {BuySlipItemListComponent} from '../BuySlipItems/buySlipItem-list.component';
import {MODAL_DIRECTIVES, ModalComponent} from 'ng2-bs3-modal/ng2-bs3-modal';
import {PAGINATION_DIRECTIVES, PaginationComponent} from 'ng2-bootstrap';
import {Cook} from '../shared/services/cookie.service';
import {EntrySlipTypeService} from '../shared/services/entrySlipType.Service';

@Component({
    moduleId: module.id,
    selector: 'buySlip-edit',
    templateUrl: 'buySlip-update.component.html',
    directives: [ROUTER_DIRECTIVES, MODAL_DIRECTIVES,
        PAGINATION_DIRECTIVES, BuySlipItemListComponent],
    providers: [EntrySlipTypeService]
})

export class BuySlipUpdateComponent implements OnInit {
    private sub: any;
    buySlipModel = <IBuySlipDetails>{};
    pageTitle: string = 'buy slip';
    errorMessage: string;
    product: IProduct;
    apiHost: string;
    id: number;
    buySlipLoaded: boolean = false;
    name: string;
    buySlipDetails: IBuySlip;
    supplierSelected: number;
    entrySlipTypeSelected: number;
    buySlipItem: IBuySlipItem[];
    idPlus: number;

    constructor(private _route: ActivatedRoute,
        private _router: Router,
        private _productService: ProductService,
        private _buySlipService: BuySlipService,
        private _supplierService: SupplierService,
        private _cookie: Cook,
        private _entrySlipTypeService: EntrySlipTypeService,
        private itemService: ItemsService,
        private notificationService: NotificationService,
        private configService: ConfigService,
        private slimLoader: SlimLoadingBarService,
        private mapmapping: MappingService) { }


    ngOnInit() {
        debugger
        
            localStorage.setItem("buy", this._route.snapshot.params['id']);
            this.id = +localStorage.getItem("buy");
       

        //this.id = +this._route.snapshot.params['id'];
        //this.getLast();

        this.apiHost = this.configService.getApiHost();
        this.loadBuySlipDetail();
        this.getSuppliers();
        this.getEntrySlipTypes();

    }

    loadBuySlipDetail() {
        this.slimLoader.start();
        debugger
       
            //this._buySlipService.getLast()
            //    .subscribe((buy: IBuySlip) => {
            //        this.notificationService.printSuccessMessage(buy.Id.toString());
            //        this.id = buy.Id;
            //    },
            //    error => {
            //        this.notificationService.printErrorMessage(error);
            //    })
    

        this._buySlipService.getById(this.id)
            .subscribe((buySlip: IBuySlip) => {

                this.buySlipDetails = this.itemService.getSerialized<IBuySlip>(buySlip);
                //this.buySlipItem = buySlip.BuySlipItems;
                this.buySlipModel.Id = buySlip.Id;
                this.buySlipModel.DateCreation = buySlip.DateCreation;
                this.buySlipModel.Description = buySlip.Description;
                this.entrySlipTypeSelected = buySlip.EntrySlipTypeId;
                this.supplierSelected = buySlip.SupplierId;
                this.buySlipModel.DeliveryMan = buySlip.DeliveryMan;

                this.slimLoader.complete();

                this.buySlipLoaded = true;
                this.notificationService.printSuccessMessage(buySlip.DateCreation);
            },
            error => {
                this.slimLoader.complete();
                this.notificationService.printErrorMessage('خطا ' + error);
            });

    }

    getLast() {
        this._buySlipService.getLast()
            .subscribe((buy: IBuySlip) => {
                this.notificationService.printSuccessMessage(buy.Id.toString());
                this.idPlus = buy.Id;
            },
            error => {
                this.notificationService.printErrorMessage(error);
            });
    }


    getSuppliers() {
        this._supplierService.getAll()
            .subscribe((suppliers: ISupplier[]) => {
                this.buySlipModel.Suppliers = suppliers;
            },
            error => {
                this.slimLoader.complete();
                this.notificationService.printErrorMessage('Failed to load schedule. ' + error);
            });
    }

    getEntrySlipTypes() {
        this._entrySlipTypeService.getAll()
            .subscribe((entrySlipTypes: IEntrySlipType[]) => {
                this.buySlipModel.EntrySlipTypes = entrySlipTypes;
            },
            error => {
                this.slimLoader.complete();
                this.notificationService.printErrorMessage('Failed to load schedule. ' + error);
            });
    }

    updateBuySlip(editBuySlipForm: NgForm) {
        console.log(editBuySlipForm.value);

        this.slimLoader.start();
        var mapBuySlip = this.mapmapping.mapBuySlipDetailsToBuySlip(this.buySlipModel);
        mapBuySlip.EntrySlipTypeId = this.entrySlipTypeSelected;
        mapBuySlip.SupplierId = this.supplierSelected;

        this._buySlipService.update(mapBuySlip)
            .subscribe(() => {
                this.notificationService.printSuccessMessage('انجام شد');
                this.slimLoader.complete();
                this._cookie.deleteCookie("buySlip");
                this._router.navigate(['/buySlipList']);
            },
            error => {
                this.slimLoader.complete();
                this.notificationService.printErrorMessage('خطا  ' + error);
            });
    }

    onChange(supplierValue) {
        console.log(supplierValue);
        this.notificationService.printErrorMessage(supplierValue);
        this.supplierSelected = supplierValue;
    }

    onChanged(entryTypeValue) {
        console.log(entryTypeValue);
        this.notificationService.printErrorMessage(entryTypeValue);
        this.entrySlipTypeSelected = entryTypeValue;
    }

    onSubmit(form): void {

        this.slimLoader.start();
        var mapBuySlip = this.mapmapping.mapBuySlipDetailsToBuySlip(this.buySlipModel);

        mapBuySlip.EntrySlipTypeId = this.entrySlipTypeSelected;
        mapBuySlip.SupplierId = this.supplierSelected;
        debugger
        this._buySlipService.update(mapBuySlip)
            .subscribe(() => {
                this.notificationService.printSuccessMessage('انجام شد');
                this.slimLoader.complete();
                localStorage.removeItem('buy');
                this._router.navigate(['/buySlipList']);

            },
            error => {
                this.slimLoader.complete();
                this.notificationService.printErrorMessage('خطا  ' + error);
            });
    }

    onBack(): void {
        localStorage.removeItem('buy');
        this._router.navigate(['/buySlipList']);
    }
}

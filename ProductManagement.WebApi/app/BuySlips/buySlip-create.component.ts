import {Component, trigger, OnInit, state, style, animate, transition,
    ViewChild, Input, Output, EventEmitter } from '@angular/core';
import {Router} from '@angular/router';
import {IBuySlipDetails, IBuySlip, ISupplier, IEntrySlipType} from '../shared/services/interfaces';

import {BuySlipService} from '../shared/services/buySlip.service';
import {MODAL_DIRECTIVES, ModalComponent} from 'ng2-bs3-modal/ng2-bs3-modal';
import {ItemsService} from '../shared/utils/items.service';
import {NotificationService} from '../shared/utils/notification.service';
import {SlimLoadingBarService} from 'ng2-slim-loading-bar/ng2-slim-loading-bar';
import {MappingService} from '../shared/utils/mapping.service';
import {SupplierService} from '../shared/services/supplier.service';
import {EntrySlipTypeService} from '../shared/services/entrySlipType.service';

@Component({
    selector: 'buySlip-add',
    templateUrl: 'app/BuySlips/buySlip-create.component.html',
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

export class CreateBuySlipComponent implements OnInit {
    buySlipModel = <IBuySlipDetails>{};//creates an empty object for an interface
    //repoistory: IProduct;
    pageTitle: string = "add buySlip";
    suppSelected: number;
    entryTypeSelected: number;
    buySlipId: number;
    buySlip: IBuySlip;
    @Output() clicked = new EventEmitter<any>();

    constructor(
        private _router: Router,
        private _buySlipService: BuySlipService,
        private _supplier: SupplierService,
        private _entrySlipType: EntrySlipTypeService,
        private itemsService: ItemsService,
        private notificationService: NotificationService,
        private slimLoader: SlimLoadingBarService,
        private mapping: MappingService) { }

    ngOnInit() {
        this.getSuppliers();
        this.getEntrySlipType();
    }

    getSuppliers() {
        this._supplier.getAll()
            .subscribe((suppliers: ISupplier[]) => {
                this.buySlipModel.Suppliers = suppliers;
            },
            error => {
                this.slimLoader.complete();
                this.notificationService.printErrorMessage('Failed to load schedule. ' + error);
            });
    }

    getEntrySlipType() {
        this._entrySlipType.getAll()
            .subscribe((entrySlipTypes: IEntrySlipType[]) => {
                this.buySlipModel.EntrySlipTypes = entrySlipTypes;
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
        console.log(this.buySlipModel);
        var mapProduct = this.mapping.mapBuySlipDetailsToBuySlip(this.buySlipModel);
        mapProduct.SupplierId = this.suppSelected;
        mapProduct.EntrySlipTypeId = this.entryTypeSelected;

        debugger
        this._buySlipService.add(mapProduct)
            .subscribe((buySlip: IBuySlip) => {
                console.log(`ID: ${buySlip.Id}`);
                localStorage.setItem('buy', buySlip.Id.toString());
                this.buySlip = this.itemsService.getSerialized<IBuySlip>(buySlip);
                this.modalAdd.close();
            });
        
        //this._router.navigate(['/updateBuySlip/0']);
        localStorage.setItem("buyS", this.buySlip.Id.toString());
        this.clicked.emit(this.buySlip);
    }

    onChange(suppValue) {
        console.log(suppValue);
        this.notificationService.printErrorMessage(suppValue);
        this.suppSelected = suppValue;
    }

    onChanged(entValue) {
        console.log(entValue);
        this.notificationService.printErrorMessage(entValue);
        this.entryTypeSelected = entValue;
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
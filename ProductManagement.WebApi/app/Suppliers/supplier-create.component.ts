﻿import {Component, trigger, state, style, animate, transition, ViewChild, Input, Output } from '@angular/core';
import {Router} from '@angular/router';
import {ISupplier} from '../shared/services/interfaces';
import {SupplierService} from '../shared/services/supplier.service';
import {MODAL_DIRECTIVES, ModalComponent} from 'ng2-bs3-modal/ng2-bs3-modal';

@Component({
    selector: 'supplier-add',
    templateUrl: 'app/Suppliers/supplier-create.component.html',
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
export class SupplierCreateComponent {
    supplierModel = <ISupplier>{};//creates an empty object for an interface
    pageTitle: string = "add repository type";
    constructor(private _supplierService: SupplierService, private _router: Router) { }

    log(Name): void {
        console.log(Name);
    }

    onSubmit(form): void {
        console.log(form);
        console.log(this.supplierModel);

        this._supplierService.add(this.supplierModel)
            .subscribe((supplier: ISupplier) => {
                console.log(`ID: ${supplier.Id}`);
                this.modalAdd.close();
            });
    }

    @ViewChild('mymodal')
    modalAdd: ModalComponent;
    selected: string;
    output: string;
    selectedSupLoaded: boolean = false;
    backdropOptions = [true, false, 'static'];
    animation: boolean = true;
    keyboard: boolean = true;
    backdrop: string | boolean = true;
    addOrDetail: boolean = false;

    close() { this.modalAdd.close(); }

    openIt() { this.modalAdd.open(); }

    opened() { }

    closed() { this.output = '(closed)' + this.selected; }

    dismissed() { this.output = '(dismissed)' };
}
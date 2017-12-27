import {Component, trigger, state, style, animate,
    transition, ViewChild, Input, Output, EventEmitter } from '@angular/core';
import {Router} from '@angular/router';
import {IPaymentType} from '../shared/services/interfaces';
import {PaymentTypeService} from '../shared/services/paymentType.service';
import {MODAL_DIRECTIVES, ModalComponent} from 'ng2-bs3-modal/ng2-bs3-modal';
import {Cook} from '../shared/services/cookie.service';
import {NotificationService} from '../shared/utils/notification.service';

@Component({
    selector: 'pay-add',
    templateUrl: 'app/PaymentTypes/paymentType-create.component.html',
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
export class PaymentTypeCreateComponent {
    paymentTypeModel = <IPaymentType>{};//creates an empty object for an interface
    @Input() paymentTypes: IPaymentType[];
    errorMessage: string;
    pageTitle: string = "add payment type";
    id: number;

    constructor(private _paymentTypeService: PaymentTypeService,
        private _router: Router,
        private _cook: Cook,
        private _notification: NotificationService) { }

    @Output() clicked = new EventEmitter<any>();

    log(Name): void {
        console.log(Name);
    }

    onSubmit(form): void {
        console.log(form);
        console.log(this.paymentTypeModel);
        this._paymentTypeService.add(this.paymentTypeModel)
            .subscribe((paymentType: IPaymentType) => {
                console.log(`ID: ${paymentType.Id}`);
                this.modalAdd.close();
            });

        this.clicked.emit(this.paymentTypes);
    }

    insert(): void {
        
        this._paymentTypeService.add(this.paymentTypeModel)
            .subscribe((paymentType: IPaymentType) => {
                console.log(`ID: ${paymentType.Id}`);
                //this.id = +`${paymentType.Id}`;
                //localStorage.setItem('id', this.id.toString());
                this.modalAdd.close();
                
            });

        this.clicked.emit(this.paymentTypes);
    }

    load() {

        this._paymentTypeService.getAll()
            .subscribe(
            response => this.paymentTypes = response,
            error => this.errorMessage = <any>error);
    }

    @ViewChild('mymodal')
    modalAdd: ModalComponent;
    selected: string;
    output: string;
    selectedManLoaded: boolean = false;
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
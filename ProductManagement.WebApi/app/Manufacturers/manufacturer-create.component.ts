import {Component, trigger, state, style, animate,
    transition, ViewChild, Input, Output, EventEmitter } from '@angular/core';
import {Router} from '@angular/router';
import {IManufacturer} from '../shared/services/interfaces';
import {ManufacturerService} from '../shared/services/manufacturer.service';
import {MODAL_DIRECTIVES, ModalComponent} from 'ng2-bs3-modal/ng2-bs3-modal';
import {Cook} from '../shared/services/cookie.service';
import {NotificationService} from '../shared/utils/notification.service';

@Component({
    selector: 'man-add',
    templateUrl: 'app/Manufacturers/manufacturer-create.component.html',
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
export class CreateComponent {
    manufacturerModel = <IManufacturer>{};//creates an empty object for an interface
    @Input() manufacturers: IManufacturer[];
    errorMessage: string;
    pageTitle: string = "add repository type";

    constructor(private _manufacuturerService: ManufacturerService,
        private _router: Router,
        private _cook: Cook,
        private _notification: NotificationService) { }

    @Output() clicked = new EventEmitter<any>();

    log(Name): void {
        console.log(Name);
    }

    onSubmit(form): void {
        console.log(form);
        console.log(this.manufacturerModel);

        this._notification.printErrorMessage(this._cook.getCookie("cookieName"));

        this._manufacuturerService.add(this.manufacturerModel)
            .subscribe((manufacturer: IManufacturer) => {
                console.log(`ID: ${manufacturer.Id}`);
                this.modalAdd.close();
            });

        this.clicked.emit(this.manufacturers);
    }

    load() {

        this._manufacuturerService.getAll(sessionStorage.getItem('tokenKey'))
            .subscribe(
            response => this.manufacturers = response,
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
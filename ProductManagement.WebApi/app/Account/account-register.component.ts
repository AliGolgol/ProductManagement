import {Component, trigger, state, style, animate,
    transition, ViewChild, Input, Output, EventEmitter } from '@angular/core';
import {Router} from '@angular/router';

import {IRegisterBindingModel} from '../shared/services/interfaces';
import {AccountService} from '../shared/services/account.service';

import {MODAL_DIRECTIVES, ModalComponent} from 'ng2-bs3-modal/ng2-bs3-modal';
import {Cook} from '../shared/services/cookie.service';
import {NotificationService} from '../shared/utils/notification.service';


@Component({
    selector: 'reg-add',
    templateUrl: 'app/Account/account-register.component.html',
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
export class RegisterAccountComponent {
    registerBindingModel = <IRegisterBindingModel>{};//creates an empty object for an interface
    @Input() registers: IRegisterBindingModel[];
    errorMessage: string;
    pageTitle: string = "add repository type";

    constructor(private _accountService: AccountService,
        private _router: Router,
        private _cook: Cook,
        private _notification: NotificationService) { }


    log(Name): void {
        console.log(Name);
    }

    onSubmit(form): void {
       
        console.log(form);
        console.log(this.registerBindingModel);

        this._notification.printErrorMessage(this._cook.getCookie("cookieName"));

        this._accountService.register(this.registerBindingModel)
            .subscribe((register: IRegisterBindingModel) => {
            });

        
    }    
}
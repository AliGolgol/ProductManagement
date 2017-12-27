import {Component, trigger, state, style, animate,
    transition, ViewChild, Input, Output, EventEmitter } from '@angular/core';
import {Router} from '@angular/router';

import {IChangePasswordBindingModel} from '../shared/services/interfaces';
import {AccountService} from '../shared/services/account.service';

import {MODAL_DIRECTIVES, ModalComponent} from 'ng2-bs3-modal/ng2-bs3-modal';
import {Cook} from '../shared/services/cookie.service';
import {NotificationService} from '../shared/utils/notification.service';
import {LocalStorageService, LocalStorageSubscriber} from '../SessionManagement/LocalStorageEmitter';
import {SessionStorage, LocalStorage} from '../SessionManagement/WebStorage';

@Component({
    selector: 'changePassword-add',
    templateUrl: 'app/Account/account-changePassword.component.html',
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
export class ChangePasswordAccountComponent {
    changePasswordBindingModel = <IChangePasswordBindingModel>{};//creates an empty object for an interface
    errorMessage: string;

    constructor(private _accountService: AccountService,
        private _router: Router,
        private _cook: Cook,
        private _notification: NotificationService) { }


    log(Name): void {
        console.log(Name);
    }

    onSubmit(form): void {
        debugger
        console.log(form);
        console.log(this.changePasswordBindingModel);
        this._accountService.changePassword(this.changePasswordBindingModel)
            .subscribe(() => {
                this._notification.printSuccessMessage('رمز عبور با موقیت تغییر کرد !');
            },
            error => {
                this._notification.printErrorMessage(error);
            });
        

    }
}
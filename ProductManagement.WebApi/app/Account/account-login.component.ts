import {Component, trigger, state, style, animate,
    transition, ViewChild, Input, Output, EventEmitter } from '@angular/core';
import {Router} from '@angular/router';

import {ILogin} from '../shared/services/interfaces';
import {AccountService} from '../shared/services/account.service';

import {MODAL_DIRECTIVES, ModalComponent} from 'ng2-bs3-modal/ng2-bs3-modal';
import {Cook} from '../shared/services/cookie.service';
import {NotificationService} from '../shared/utils/notification.service';
import {LocalStorageService, LocalStorageSubscriber} from '../SessionManagement/LocalStorageEmitter';
import {SessionStorage, LocalStorage} from '../SessionManagement/WebStorage';
import {LoginStatusService} from '../shared/services/loginStatus.service';

@Component({
    selector: 'login-add',
    templateUrl: 'app/Account/account-login.component.html',
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
export class LoginAccountComponent {
    loginBindingModel = <ILogin>{};//creates an empty object for an interface
    errorMessage: string;
    pageTitle: string = "add repository type";

    constructor(private _accountService: AccountService,
        private _router: Router,
        private _cook: Cook,
        private _notification: NotificationService,
        private _loginStatus: LoginStatusService) { }


    log(Name): void {
        console.log(Name);
    }

    onSubmit(form): void {
        debugger
        console.log(form);
        console.log(this.loginBindingModel);
        this.loginBindingModel.grant_type = 'password';
        this._accountService.login(this.loginBindingModel)
            .subscribe(() => {
                this._loginStatus.status();
            }
            );


        this._router.navigate(['']);



    }

    authorize(): void {
        debugger

        this._accountService.authorize(sessionStorage.getItem('tokenKey'), this.loginBindingModel);
    }
}
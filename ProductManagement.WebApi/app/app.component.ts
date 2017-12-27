import { Component, OnInit, ViewContainerRef,Pipe } from '@angular/core';
import {HTTP_PROVIDERS} from '@angular/http';
// Add the RxJS Observable operators we need in this app.
import './rxjs-operators';

import 'rxjs/Rx';   // Load all features
import {ROUTER_DIRECTIVES, Router} from '@angular/router';
import {SlimLoadingBar} from 'ng2-slim-loading-bar/ng2-slim-loading-bar';
import {APP_PROVIDERS} from './app.providers';
import {LoginAccountComponent} from './Account/account-login.component';
import {LoginStatusService} from './shared/services/loginStatus.service';
import { Subject } from 'rxjs/Subject';
import { Observable } from 'rxjs/Observable';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import {ILoginStatus} from './shared/services/interfaces';

@Component({
    selector: 'pm-app',
    templateUrl: 'app/app.component.html',
    directives: [ROUTER_DIRECTIVES, SlimLoadingBar, LoginAccountComponent],
    providers: [APP_PROVIDERS]
})

export class AppComponent implements OnInit {
    pageTitle: string = "AGG AngularJS 2.0 APP";
    public userFlag: boolean = false;
    uFlag: Observable<ILoginStatus>;
    private flag$: Observable<boolean>;

    constructor(
        private _router: Router,
        private _loginStatus: LoginStatusService) { }

    ngOnInit() {
        
        
        this._loginStatus.check().subscribe(data => {
            console.log('nav init check() ' + data);
            this.userFlag = data;
        });

        if (sessionStorage.getItem('tokenKey')) {
            this.userFlag = true;
        }
        else {
            this._router.navigate(['./login']);
        }
    }

    logout() {
        sessionStorage.removeItem('tokenKey');
        this._router.navigate(['/login']);
    }
}
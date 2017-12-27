/// <reference path="../typings/browser/ambient/core-js/index.d.ts" />
/// <reference path="../typings/browser/ambient/node/index.d.ts" />
/// <reference path="../typings/browser/ambient/jssha/index.d.ts" />
/// <reference path="../typings/browser/ambient/jquery/index.d.ts" />

import { bootstrap } from '@angular/platform-browser-dynamic';
import { enableProdMode } from '@angular/core';
import {disableDeprecatedForms, provideForms} from '@angular/forms';
import {LocationStrategy, Location, HashLocationStrategy, PlatformLocation, PathLocationStrategy,
    APP_BASE_HREF} from '@angular/common'; 


// Our main component
import { AppComponent } from "./app.component";
import { APP_ROUTER_PROVIDERS } from './app.routes';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/toPromise';
import {provideAuth} from 'angular2-jwt';

//if (process.env.ENV === "production") {
//    enableProdMode();
//}

bootstrap(AppComponent, [
    disableDeprecatedForms(),
    provideForms(),
    APP_ROUTER_PROVIDERS, { provide: LocationStrategy, useClass: HashLocationStrategy },
    provideAuth({
        headerName: 'Authorization',
        headerPrefix: 'Bearer ',
        tokenName: 'tokentKey',
        tokenGetter: (() => sessionStorage.getItem('tokenKey')),
        globalHeaders: [{ 'Content-Type': 'application/json' }],
        noJwtError: true,
        noTokenScheme: true
    })
])
    .catch(err => console.error(err));
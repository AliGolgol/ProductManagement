"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var core_1 = require('@angular/core');
var router_1 = require('@angular/router');
var account_service_1 = require('../shared/services/account.service');
var cookie_service_1 = require('../shared/services/cookie.service');
var notification_service_1 = require('../shared/utils/notification.service');
var ChangPasswordAccountComponent = (function () {
    function ChangPasswordAccountComponent(_accountService, _router, _cook, _notification) {
        this._accountService = _accountService;
        this._router = _router;
        this._cook = _cook;
        this._notification = _notification;
        this.changePasswordBindingModel = {}; //creates an empty object for an interface
    }
    ChangPasswordAccountComponent.prototype.log = function (Name) {
        console.log(Name);
    };
    ChangPasswordAccountComponent.prototype.onSubmit = function (form) {
        //debugger
        console.log(form);
        console.log(this.changePasswordBindingModel);
        this._accountService.changePassword(this.changePasswordBindingModel)
            .subscribe();
    };
    ChangPasswordAccountComponent = __decorate([
        core_1.Component({
            selector: 'changePassword-add',
            templateUrl: 'app/Account/account-changePassword.component.html',
            animations: [
                core_1.trigger('flyInOut', [
                    core_1.state('in', core_1.style({ opacity: 1, transform: 'translateX(0)' })),
                    core_1.transition('void => *', [
                        core_1.style({
                            opacity: 0,
                            transform: 'translateX(-100%)'
                        }),
                        core_1.animate('0.6s ease-in')
                    ]),
                    core_1.transition('* => void', [
                        core_1.animate('0.2s 10 ease-out', core_1.style({
                            opacity: 0,
                            transform: 'translateX(100%)'
                        }))
                    ])
                ])
            ]
        }), 
        __metadata('design:paramtypes', [account_service_1.AccountService, router_1.Router, cookie_service_1.Cook, notification_service_1.NotificationService])
    ], ChangPasswordAccountComponent);
    return ChangPasswordAccountComponent;
}());
exports.ChangPasswordAccountComponent = ChangPasswordAccountComponent;
//# sourceMappingURL=account-chagenPassword.component.js.map
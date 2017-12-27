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
//import {CreateComponent} from './create.component';
var about_service_1 = require('../shared/services/about.service');
var ng2_slim_loading_bar_1 = require('ng2-slim-loading-bar/ng2-slim-loading-bar');
var ng2_bs3_modal_1 = require('ng2-bs3-modal/ng2-bs3-modal');
var ng2_bootstrap_1 = require('ng2-bootstrap');
var notification_service_1 = require('../shared/utils/notification.service');
var items_service_1 = require('../shared/utils/items.service');
var AboutListComponent = (function () {
    function AboutListComponent(_aboutService, slimLoader, itemService, notificationService) {
        this._aboutService = _aboutService;
        this.slimLoader = slimLoader;
        this.itemService = itemService;
        this.notificationService = notificationService;
        this.pageTitle = 'About';
        this.showNew = false;
        this.itemsPerPage = 2;
        this.totalItems = 1;
        this.selectedAboutLodaded = false;
        this.index = 0;
        this.backdropOptions = [true, false, 'static'];
        this.animationStartTime = true;
        this.keyboard = true;
        this.backdrop = true;
    }
    AboutListComponent.prototype.toggleImage = function () {
        this.showNew = !this.showNew;
    };
    AboutListComponent.prototype.ngOnInit = function () {
        console.log('In OnInit');
        this.loadAbout();
    };
    //load all about
    AboutListComponent.prototype.loadAbout = function () {
        var _this = this;
        this.slimLoader.start();
        this._aboutService.getAll()
            .subscribe(function (about) { return _this.about = about; }, function (error) { return _this.errorMessage = error; });
    };
    //show details
    AboutListComponent.prototype.viewDetial = function (id) {
        this.selectedAboutId = id;
        this.modal.open('lg');
    };
    AboutListComponent.prototype.delAbout = function (about) {
        var _this = this;
        this.notificationService.openConfirmationDialog('آیا مطمئن هستید؟', function () {
            _this.slimLoader.start();
            _this._aboutService.del(about.Id)
                .subscribe(function () {
                _this.itemService.removeItemFromArray(_this.about, about);
                _this.notificationService.printSuccessMessage(about.Name + ' حذف شد');
                _this.slimLoader.complete();
            }, function (error) {
                _this.slimLoader.complete();
                _this.notificationService.printErrorMessage(' خطا در حذف' + about.Name + ' ' + error);
            });
        });
    };
    AboutListComponent.prototype.closed = function () {
        this.output = '(closed)' + this.selected;
    };
    AboutListComponent.prototype.dismissed = function () { this.output = '(dismissed)'; };
    ;
    AboutListComponent.prototype.opened = function () {
        var _this = this;
        this.slimLoader.start();
        this._aboutService.getById(this.selectedAboutId)
            .subscribe(function (about) {
            _this.aboutDetails = _this.itemService.getSerialized(about);
            _this.slimLoader.complete();
            _this.selectedAboutLodaded = true;
        }, function (error) {
            _this.slimLoader.complete();
        });
        this.output = '(opened)';
    };
    __decorate([
        core_1.ViewChild('modal'), 
        __metadata('design:type', ng2_bs3_modal_1.ModalComponent)
    ], AboutListComponent.prototype, "modal", void 0);
    AboutListComponent = __decorate([
        core_1.Component({
            moduleId: module.id,
            selector: 'pm-about',
            templateUrl: 'app/About/list.html',
            directives: [router_1.ROUTER_DIRECTIVES, ng2_bs3_modal_1.MODAL_DIRECTIVES, ng2_bootstrap_1.PAGINATION_DIRECTIVES],
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
        __metadata('design:paramtypes', [about_service_1.AboutService, ng2_slim_loading_bar_1.SlimLoadingBarService, items_service_1.ItemsService, notification_service_1.NotificationService])
    ], AboutListComponent);
    return AboutListComponent;
}());
exports.AboutListComponent = AboutListComponent;
//# sourceMappingURL=about.component.js.map
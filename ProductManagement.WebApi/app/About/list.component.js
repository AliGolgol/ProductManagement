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
var about_service_1 = require('../shared/services/about.service');
var AboutListComponent = (function () {
    function AboutListComponent(_repositoryService) {
        this._repositoryService = _repositoryService;
        this.pageTitle = 'About';
        this.showNew = false;
    }
    AboutListComponent.prototype.toggleImage = function () {
        this.showNew = !this.showNew;
    };
    AboutListComponent.prototype.ngOnInit = function () {
        var _this = this;
        console.log('In OnInit');
        this._repositoryService.getRepositories()
            .subscribe(function (repositories) { return _this.repositories = repositories; }, function (error) { return _this.errorMessage = error; });
    };
    AboutListComponent = __decorate([
        core_1.Component({
            templateUrl: 'app/About/list.html'
        }), 
        __metadata('design:paramtypes', [about_service_1.AboutService])
    ], AboutListComponent);
    return AboutListComponent;
}());
exports.AboutListComponent = AboutListComponent;
//# sourceMappingURL=list.component.js.map
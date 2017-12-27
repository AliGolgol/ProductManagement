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
var http_1 = require('@angular/http');
var Observable_1 = require('rxjs/Observable');
require('rxjs/add/operator/map');
require('rxjs/add/operator/catch');
var config_service_1 = require('../shared/utils/config.service');
var items_service_1 = require('../shared/utils/items.service');
var http_2 = require('@angular/http');
var AboutService = (function () {
    function AboutService(_http, configureService, itemsService) {
        this._http = _http;
        this.configureService = configureService;
        this.itemsService = itemsService;
        this._Url = '/api/Abouts';
        this._Url = configureService.getApiURI() + 'RepositoryTypes';
    }
    AboutService.prototype.getRepositories = function () {
        return this._http.get(this._Url)
            .map(function (res) {
            return res.json();
        })
            .do(function (data) { return console.log("All: " + JSON.stringify(data)); })
            .catch(this.handleError);
    };
    AboutService.prototype.addRepositoryType = function (repositoryType) {
        var headers = new http_2.Headers({ 'Content-Type': 'application/json' }); //for ASP.Net MVC
        var options = new http_2.RequestOptions({ headers: headers });
        return this._http.post(this._Url, JSON.stringify(repositoryType), options)
            .map(function (response) { return response.json(); })
            .do(function (data) { return console.log("RepositoryType: " + JSON.stringify(data)); })
            .catch(this.handleError);
    };
    //getRepositoryTypeById(): Observable<IAbout> {
    //    return this._http.get(this._Url, this.id)
    //    map((response: Response) => <IAbout>response.json())
    //        .do(data => console.log("ALL: " + JSON.stringify(data)))
    //        .catch(this.handleError);
    //}
    AboutService.prototype.handleError = function (error) {
        console.error(error);
        return Observable_1.Observable.throw(error.json().error || 'Server error');
    };
    AboutService = __decorate([
        core_1.Injectable(), 
        __metadata('design:paramtypes', [http_1.Http, config_service_1.ConfigService, items_service_1.ItemsService])
    ], AboutService);
    return AboutService;
}());
exports.AboutService = AboutService;
//# sourceMappingURL=about.service.js.map
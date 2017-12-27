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
var http_2 = require('@angular/http');
var RepositoryTypeService = (function () {
    function RepositoryTypeService(_http) {
        this._http = _http;
        this._repositoryUrl = '/api/RepositoryTypes/';
    }
    RepositoryTypeService.prototype.getRepositories = function () {
        return this._http.get(this._repositoryUrl)
            .map(function (response) { return response.json(); })
            .do(function (data) { return console.log("All: " + JSON.stringify(data)); })
            .catch(this.handleError);
    };
    RepositoryTypeService.prototype.addRepositoryType = function (repositoryType) {
        var headers = new http_2.Headers({ 'Content-Type': 'application/json' }); //for ASP.Net MVC
        var options = new http_2.RequestOptions({ headers: headers });
        return this._http.post(this._repositoryUrl, JSON.stringify(repositoryType), options)
            .map(function (response) { return response.json(); })
            .do(function (data) { return console.log("RepositoryType: " + JSON.stringify(data)); })
            .catch(this.handleError);
    };
    RepositoryTypeService.prototype.getRepositoryTypeById = function (id) {
        //let headers = new Headers({ 'Content-Type': 'application/json' });
        //let options = new RequestOptions({ headers: headers });
        //let params: URLSearchParams = new URLSearchParams();
        //params.set('id', aboutId.toString());
        //let url: string = `${this._repositoryUrl}/${aboutId}`;
        return this._http.get('http://localhost:1946/api/RepositoryTypes/2')
            .map(function (response) { return response.json(); })
            .do(function (data) { return console.log("ALL: " + JSON.stringify(data)); })
            .catch(this.handleError);
        //return this.getRepositories()
        //    .then(reps => reps.find(rep => rep.Id === aboutId));
    };
    RepositoryTypeService.prototype.handleError = function (error) {
        console.error('An error is happened : ', error);
        console.error(error.status);
        return Observable_1.Observable.throw(error.json().error || 'Server error');
    };
    RepositoryTypeService = __decorate([
        core_1.Injectable(), 
        __metadata('design:paramtypes', [http_1.Http])
    ], RepositoryTypeService);
    return RepositoryTypeService;
}());
exports.RepositoryTypeService = RepositoryTypeService;
//# sourceMappingURL=repositoryType.service.js.map
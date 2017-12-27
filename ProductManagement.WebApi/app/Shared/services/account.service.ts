import { Injectable, Injector, ReflectiveInjector } from '@angular/core';
import { IRegisterBindingModel, Pagination, PaginatedResult, ILogin, IChangePasswordBindingModel }
from './interfaces';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import {Observer} from 'rxjs/Observer';

import { Subject } from 'rxjs/Subject';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';

import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import {ConfigService} from '../utils/config.service';
import {ItemsService} from '../utils/items.service';
import {NotificationService} from '../utils/notification.service';
import {LocalStorageService, LocalStorageSubscriber} from '../../SessionManagement/LocalStorageEmitter';
import {SessionStorage, LocalStorage} from '../../SessionManagement/WebStorage';

import {LoginStatusService} from './loginStatus.service';

@Injectable()
export class AccountService {
    private _Url: string = '';
    private urlLogin: string = '';
    private id: number;
    private urlChgPass: string;

    auth: LoginStatusService;
    injector: ReflectiveInjector;

    constructor(private _http: Http,
        private configureService: ConfigService,
        private itemsService: ItemsService,
        private notification: NotificationService) {
        this._Url = configureService.getApiURI() + 'Account/';
        this.urlLogin = 'http://localhost:1900/Token';
        this.urlChgPass = configureService.getApiURI + 'ChangePassword';
        this.injector = ReflectiveInjector.resolveAndCreate([LoginStatusService]);

        this.auth = this.injector.get(LoginStatusService);
    }

    getAll(): Observable<IRegisterBindingModel[]> {
        return this._http.get(this._Url)
            //.map((response: Response) => <IAbout[]>response.json())
            .map((res: Response) => {
                return res.json();
            })
            .do(data => console.log("All: " + JSON.stringify(data)))
            .catch(this.handleError);
    }

    add(buySlip: IRegisterBindingModel): Observable<IRegisterBindingModel> {
        let headers = new Headers({ 'Content-Type': 'application/json' });//for ASP.Net MVC
        let options = new RequestOptions({ headers: headers });

        return this._http.post(this._Url, JSON.stringify(buySlip), options)
            .map((response: Response) => <IRegisterBindingModel>response.json())
            .do(data => console.log("Manufacturer: " + JSON.stringify(data)))
            .catch(this.handleError);
    }

    register(register: IRegisterBindingModel): Observable<IRegisterBindingModel> {

        let headers = new Headers({ 'Content-Type': 'application/json' });//for ASP.Net MVC
        let options = new RequestOptions({ headers: headers });

        return this._http.post(this._Url + 'Register', JSON.stringify(register), options)
            .map((response: Response) => <IRegisterBindingModel>response.json())
            .do(data => console.log("Register Account: " + JSON.stringify(data)))
            .catch(this.handleError);
    }

    login(login: ILogin): Observable<any> {

        let head = new Headers();
        let body = 'grant_type=password&username=' + login.username + '&password=' + login.password;
        head.append('Authorization', 'Bearer ' + sessionStorage.getItem('tokenKey'));
        head.append('Content-Type', 'application/x-www-form-urlencoded');
        let options = new RequestOptions({
            headers: head
        });

        return this._http.post(this.urlLogin, body, options)
            .map((response: Response) => <any>response.json())
            .do(data => {
                console.log("Register Account: " + JSON.stringify(data));
                sessionStorage.setItem('tokenKey', data.access_token);
                
            })
            .catch(this.handleError);
    }

    authorize(token: string, login: ILogin): Observable<any> {
        let headers = new Headers([{ 'Content-Type': 'application/x-www-form-urlencoded' }
            , { 'Authorization': 'Bearer ' + token }]);//for ASP.Net MVC

        let head = new Headers();
        head.append('Authorization', token);
        head.append('Content-Type', 'application/x-www-form-urlencoded');
        let options = new RequestOptions({
            headers: headers
        });

        return this._http.get('http://localhost:1900/api/Values/1', options)
            .map((response: Response) => { return response.json(); })
            .do(data => {
                console.log("Register Account: " + JSON.stringify(data));
            })
            .catch(this.handleError);
    }

    getById(id: number): Observable<IRegisterBindingModel> {
        return this._http.get(this._Url + id)
            .map((response: Response) => <IRegisterBindingModel>response.json())
            .do(data => console.log("ALL: " + JSON.stringify(data)))
            .catch(this.handleError);
    }

    del(id: number): Observable<void> {
        return this._http.delete(this._Url + id)
            .map((response: Response) => {
                return;
            })
            .catch(this.handleError);
    }

    //update(buySlip: IRegisterBindingModel): Observable<void> {
    //    let headers = new Headers();
    //    headers.append('Content-Type', 'application/json');
    //    return this._http.put(this._Url + buySlip.Id, JSON.stringify(buySlip), {
    //        headers: headers
    //    })
    //        .map((response: Response) => {
    //            return;
    //        })
    //        .catch(this.handleError);
    //}

    changePassword(chagenPass: IChangePasswordBindingModel): Observable<void> {
        let body = 'grant_type=password&OldPassword=' + chagenPass.OldPassword +
            '&NewPassword=' + chagenPass.NewPassword +
            '&ConfirmPassword=' + chagenPass.ConfirmPassword;
        let head = new Headers();
        head.append('Authorization', 'Bearer ' + sessionStorage.getItem('tokenKey'));
        head.append('Content-Type', 'application/x-www-form-urlencoded');
        let options = new RequestOptions({ headers: head });

        return this._http.post(this._Url + 'ChangePassword', body, options)
            .map((response: Response) => <any>response.json())
            .do(data => { console.log("chagene password : " + JSON.stringify(data)); })
            .catch(this.handleError);
    }

    private handleError(error: Response) {
        console.error(error);
        return Observable.throw(error.json().error || 'Server error');
    }
}
import { Injectable } from '@angular/core';
import { ISupplier } from './interfaces';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import {Observer} from 'rxjs/Observer';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import {ConfigService} from '../utils/config.service';
import {ItemsService} from '../utils/items.service';
import {NotificationService} from '../utils/notification.service';

@Injectable()
export class SupplierService {
    private _Url: string = '';
    private id: number;

    constructor(private _http: Http,
        private configureService: ConfigService,
        private itemsService: ItemsService,
        private notification: NotificationService) {
        this._Url = configureService.getApiURI() + 'Suppliers';
    }

    getAll(): Observable<ISupplier[]> {
        return this._http.get(this._Url)
            //.map((response: Response) => <ISupplier[]>response.json())
            .map((res: Response) => {
                return res.json();
            })
            .do(data => console.log("All: " + JSON.stringify(data)))
            .catch(this.handleError);
    }

    add(supplier: ISupplier): Observable<ISupplier> {
        let headers = new Headers({ 'Content-Type': 'application/json' });//for ASP.Net MVC
        let options = new RequestOptions({ headers: headers });

        return this._http.post(this._Url, JSON.stringify(supplier), options)
            .map((response: Response) => <ISupplier>response.json())
            .do(data => console.log("Supplier: " + JSON.stringify(data)))
            .catch(this.handleError);
    }

    getById(id: number): Observable<ISupplier> {
        return this._http.get(this._Url + '/' + id)
            .map((response: Response) => <ISupplier>response.json())
            .do(data => console.log("ALL: " + JSON.stringify(data)))
            .catch(this.handleError);
    }

    del(id: number): Observable<void> {
        return this._http.delete(this._Url + '/' + id)
            .map((response: Response) => {
                return;
            })
            .catch(this.handleError);
    }

    update(supplier: ISupplier): Observable<void> {
        let headers = new Headers();
        headers.append('Content-Type', 'application/json');
        return this._http.put(this._Url + '/' + supplier.Id, JSON.stringify(supplier), {
            headers: headers
        })
            .map((response: Response) => {
                return;
            })
            .catch(this.handleError);
    }
    private handleError(error: Response) {
        console.error(error);
        return Observable.throw(error.json().error || 'Server error');
    }
}
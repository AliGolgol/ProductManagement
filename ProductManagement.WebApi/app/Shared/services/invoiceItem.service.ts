import { Injectable } from '@angular/core';
import { IInvoiceItem, Pagination, PaginatedResult, ISelectQauntity } from './interfaces';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import {Observer} from 'rxjs/Observer';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import {ConfigService} from '../utils/config.service';
import {ItemsService} from '../utils/items.service';
import {NotificationService} from '../utils/notification.service';

@Injectable()
export class InvoiceItemService {
    private _Url: string = '';
    private id: number;

    constructor(private _http: Http,
        private configureService: ConfigService,
        private itemsService: ItemsService,
        private notification: NotificationService) {
        this._Url = configureService.getApiURI() + 'InvoiceItems/';
    }

    getAll(): Observable<IInvoiceItem[]> {
        return this._http.get(this._Url)
            //.map((response: Response) => <IAbout[]>response.json())
            .map((res: Response) => {
                return res.json();
            })
            .do(data => console.log("All: " + JSON.stringify(data)))
            .catch(this.handleError);
    }

    add(invoiceItem: IInvoiceItem): Observable<IInvoiceItem> {
        let headers = new Headers({ 'Content-Type': 'application/json' });//for ASP.Net MVC
        let options = new RequestOptions({ headers: headers });

        return this._http.post(this._Url, JSON.stringify(invoiceItem), options)
            .map((response: Response) => <IInvoiceItem>response.json())
            .do(data => console.log("Manufacturer: " + JSON.stringify(data)))
            .catch(this.handleError);
    }

    getById(id: number): Observable<IInvoiceItem> {
        return this._http.get(this._Url + id)
            .map((response: Response) => <IInvoiceItem>response.json())
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

    update(invoiceItem: IInvoiceItem): Observable<void> {
        let headers = new Headers();
        headers.append('Content-Type', 'application/json');
        return this._http.put(this._Url + invoiceItem.Id, JSON.stringify(invoiceItem), {
            headers: headers
        })
            .map((response: Response) => {
                return;
            })
            .catch(this.handleError);
    }

    getInvoiceItems(page?: number, itemsPerPage?: number): Observable<PaginatedResult<IInvoiceItem[]>> {
        var peginatedResult: PaginatedResult<IInvoiceItem[]> = new PaginatedResult<IInvoiceItem[]>();

        let headers = new Headers();
        if (page != null && itemsPerPage != null) {
            headers.append('Pagination', page + ',' + itemsPerPage);
        }

        return this._http.get(this._Url, {
            headers: headers
        })
            .map((res: Response) => {
                console.log(res.headers.keys());
                peginatedResult.result = res.json();

                if (res.headers.get("Pagination") != null) {
                    //var pagination = JSON.parse(res.headers.get("Pagination"));
                    var paginationHeader: Pagination = this.itemsService.getSerialized<Pagination>(JSON.parse(res.headers.get("Pagination")));
                    console.log(paginationHeader);
                    peginatedResult.pagination = paginationHeader;
                }
                return peginatedResult;
            })
            .catch(this.handleError);
    }

    getByInvoiceId(id: number): Observable<IInvoiceItem[]> {

        return this._http.get(this._Url + id + "/Invoice")
            .map((response: Response) => <IInvoiceItem>response.json())
            .do(data => console.log("ALL: " + JSON.stringify(data)))
            .catch(this.handleError);

    }

    selectPrice(id: number): Observable<any> {

        return this._http.get(this._Url+ id  + "/SelectPrice")
            .map((response: Response) => <IInvoiceItem>response.json())
            .do(data => console.log("ALL: " + JSON.stringify(data)))
            .catch(this.handleError);

    } 

    SelectQuantity(sQuantity: ISelectQauntity): Observable<any> {
        let headers = new Headers({ 'Content-Type': 'application/json' });//for ASP.Net MVC
        let options = new RequestOptions({ headers: headers });

        return this._http.post(this._Url + "SelectQuantity", JSON.stringify(sQuantity), options)
            .map((response: Response) => <IInvoiceItem>response.json())
            .do(data => console.log("ALL: " + JSON.stringify(data)))
            .catch(this.handleError);
    }

    private handleError(error: Response) {
        console.error(error);
        return Observable.throw(error.json().error || 'Server error');
    }
}
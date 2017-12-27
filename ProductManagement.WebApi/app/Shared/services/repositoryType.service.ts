import { Injectable } from '@angular/core';
import { IRepositoryType } from './interfaces';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import {Observer} from 'rxjs/Observer';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import {ConfigService} from '../utils/config.service';
import {ItemsService} from '../utils/items.service';
import {NotificationService} from '../utils/notification.service';

@Injectable()
export class RepositoryTypeService {
    private _repositoryUrl: string = ''; 
    private id: number;

    constructor(private _http: Http,
        private configureService: ConfigService,
        private itemsService: ItemsService,
        private notification: NotificationService)
    {
        this._repositoryUrl = configureService.getApiURI()+'RepositoryTypes';
    }

    getRepositories(): Observable<IRepositoryType[]> {
        return this._http.get(this._repositoryUrl)
            .map((res: Response) => {
                return res.json();
            })
            .do(data => console.log("All: " + JSON.stringify(data)))
            .catch(this.handleError);
    }

    addRepositoryType(repositoryType: IRepositoryType): Observable<IRepositoryType> {
        let headers = new Headers({ 'Content-Type': 'application/json' });//for ASP.Net MVC
        let options = new RequestOptions({ headers: headers });

        return this._http.post(this._repositoryUrl, JSON.stringify(repositoryType), options)
            .map((response: Response) => <IRepositoryType>response.json())
            .do(data => console.log("RepositoryType: " + JSON.stringify(data)))
            .catch(this.handleError);
    }

    getRepositoryTypeById(id: number): Observable<IRepositoryType> {
        return this._http.get(this._repositoryUrl +'/'+ id)
            .map((response: Response) => <IRepositoryType>response.json())
            .do(data => console.log("ALL: " + JSON.stringify(data)))
            .catch(this.handleError);
    }

    deleteRepType(id: number): Observable<void> {
        return this._http.delete(this._repositoryUrl + '/' + id)
            .map((res: Response) => {
                return;
            })
            .catch(this.handleError);
    }

    updateRepTyp(repTyp: IRepositoryType): Observable<void> {
        this.notification.printSuccessMessage(repTyp.Name);
        let headers = new Headers();
        headers.append('Content-Type', 'application/json');

        return this._http.put(this._repositoryUrl + '/' + repTyp.Id, JSON.stringify(repTyp), {
            headers: headers
        })
            .map((res: Response) => {
                return;
            })
            .catch(this.handleError);
    }

    private handleError(error: Response) {
        console.error('An error is happened : ',error);
        console.error(error.status);
        return Observable.throw(error.json().error || 'Server error');
    }
}
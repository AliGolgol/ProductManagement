import { Injectable,Injector } from '@angular/core';
import { IAbout } from './interfaces';
import { Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import {Observer} from 'rxjs/Observer';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import {ConfigService} from '../utils/config.service';
import {ItemsService} from '../utils/items.service';
import {NotificationService} from '../utils/notification.service';
import { Subject } from 'rxjs/Subject';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import {ILoginStatus} from './interfaces';

@Injectable()
export class LoginStatusService {
    private _Url: string = '';
    private id: number;
    private _flag$ :Subject<boolean> = new BehaviorSubject<boolean>(null);
    //private _flag$: Subject<boolean>;
    private statusFlag: boolean = false;
    private flag: boolean = false;
    private login: ILoginStatus;
    externalBS$;

    constructor(
        //private configureService: ConfigService,
        //private itemsService: ItemsService,
        //private notification: NotificationService
    ) {
        //this._flag$ = <Subject<boolean>>new Subject();
        this._flag$.next(this.statusFlag);
    }

    get flag$() {
        return this._flag$.asObservable();
    }

    status()
    {
        
            this.statusFlag = true;
            this._flag$.next(true);
            console.log(this.statusFlag);
        
    }

    check(): any
    {
       //return this.externalBS$.asObservable().startWith(this.statusFlag);
        return this._flag$.asObservable();
    }

    private handleError(error: Response) {
        console.error(error);
        return Observable.throw(error.json().error || 'Server error');
    }

}
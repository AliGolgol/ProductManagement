import {Component, OnInit, OnDestroy} from '@angular/core';
import {Router, ActivatedRoute}from '@angular/router';
import {NgForm} from '@angular/forms';

import {IBillType} from '../shared/services/interfaces';
import {BillTypeService} from '../shared/services/billType.service';
import {ItemsService} from '../shared/utils/items.service';
import {NotificationService} from '../shared/utils/notification.service';
import {ConfigService} from '../shared/utils/config.service';
import {SlimLoadingBarService} from 'ng2-slim-loading-bar/ng2-slim-loading-bar';
import {ROUTER_DIRECTIVES} from '@angular/router';

@Component({
    moduleId: module.id,
    selector: 'app-billType-edit',
    templateUrl: 'billType-update.component.html'
})

export class BillTypeUpdateComponent implements OnInit {
    private sub: any;
    pageTitle: string = 'repository Type';
    errorMessage: string;
    billType: IBillType;
    apiHost: string;
    id: number;
    billTypeLoaded: boolean = false;
    name: string;
    billTypeDetails: IBillType;


    constructor(private _route: ActivatedRoute,
        private _router: Router,
        private _billTypeService: BillTypeService,
        private itemService: ItemsService,
        private notificationService: NotificationService,
        private configService: ConfigService,
        private slimLoader: SlimLoadingBarService) { }


    ngOnInit() {
        this.id = +this._route.snapshot.params['id'];
        this.apiHost = this.configService.getApiHost();
        this.loadBillTypeDetail();

    }

    loadBillTypeDetail() {
        this.slimLoader.start();
        this._billTypeService.getById(this.id)
            .subscribe((billType: IBillType) => {
                this.billTypeDetails = this.itemService.getSerialized<IBillType>(billType);
                this.slimLoader.complete();
                this.billTypeLoaded = true;
                this.notificationService.printSuccessMessage(this.billTypeDetails.Id.toString());
            },
            error => {
                this.slimLoader.complete();
                this.notificationService.printErrorMessage('خطا ' + error);
            });
    }

    updateBillType(editBillTypeForm: NgForm) {
        console.log(editBillTypeForm.value);

        this.slimLoader.start();
        this._billTypeService.update(this.billTypeDetails)
            .subscribe(() => {
                this.notificationService.printSuccessMessage('انجام شد');
                this.slimLoader.complete();
                this._router.navigate(['/billTypeList']);
            },
            error => {
                this.slimLoader.complete();
                this.notificationService.printErrorMessage('خطا  ' + error);
            });
    }

    onSubmit(form): void {
        this.slimLoader.start();
        this._billTypeService.update(this.billTypeDetails)
            .subscribe(() => {
                this.notificationService.printSuccessMessage('انجام شد');
                this.slimLoader.complete();
            },
            error => {
                this.slimLoader.complete();
                this.notificationService.printErrorMessage('خطا  ' + error);
            });
    }
    //ngOnDestroy(): void {
    //    this.sub.unsubscribe();//we must unsubscribe before Angular destroys the component.Failure to do so could create a memory leak.
    //}

    onBack(): void {
        this._router.navigate(['/billTypeList']);
    }
}

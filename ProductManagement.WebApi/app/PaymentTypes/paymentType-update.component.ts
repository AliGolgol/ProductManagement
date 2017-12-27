import {Component, OnInit, OnDestroy} from '@angular/core';
import {Router, ActivatedRoute}from '@angular/router';
import {NgForm} from '@angular/forms';

import {IPaymentType} from '../shared/services/interfaces';
import {PaymentTypeService} from '../shared/services/paymentType.service';
import {ItemsService} from '../shared/utils/items.service';
import {NotificationService} from '../shared/utils/notification.service';
import {ConfigService} from '../shared/utils/config.service';
import {SlimLoadingBarService} from 'ng2-slim-loading-bar/ng2-slim-loading-bar';
import {ROUTER_DIRECTIVES} from '@angular/router';

@Component({
    moduleId: module.id,
    selector: 'app-paymentType-edit',
    templateUrl: 'paymentType-update.component.html'
})

export class PaymentTypeUpdateComponent implements OnInit {
    private sub: any;
    pageTitle: string = 'Payment Type';
    errorMessage: string;
    paymentType: IPaymentType;
    apiHost: string;
    id: number;
    paymentTypeLoaded: boolean = false;
    name: string;
    paymentTypeDetails: IPaymentType;


    constructor(private _route: ActivatedRoute,
        private _router: Router,
        private _paymentTypeService: PaymentTypeService,
        private itemService: ItemsService,
        private notificationService: NotificationService,
        private configService: ConfigService,
        private slimLoader: SlimLoadingBarService) { }


    ngOnInit() {
        this.id = +this._route.snapshot.params['id'];
        this.apiHost = this.configService.getApiHost();
        this.loadPaymentTypeDetail();

    }

    loadPaymentTypeDetail() {
        this.slimLoader.start();
        this._paymentTypeService.getById(this.id)
            .subscribe((paymentType: IPaymentType) => {
                this.paymentTypeDetails = this.itemService.getSerialized<IPaymentType>(paymentType);
                this.slimLoader.complete();
                this.paymentTypeLoaded = true;
                this.notificationService.printSuccessMessage(this.paymentTypeDetails.Id.toString());
            },
            error => {
                this.slimLoader.complete();
                this.notificationService.printErrorMessage('خطا ' + error);
            });
    }

    updatePaymentType(editPaymentTypeForm: NgForm) {
        console.log(editPaymentTypeForm.value);

        this.slimLoader.start();
        this._paymentTypeService.update(this.paymentTypeDetails)
            .subscribe(() => {
                this.notificationService.printSuccessMessage('انجام شد');
                this.slimLoader.complete();
                this._router.navigate(['/paymentTypeList']);
            },
            error => {
                this.slimLoader.complete();
                this.notificationService.printErrorMessage('خطا  ' + error);
            });
    }

    onBack(): void {
        this._router.navigate(['/paymentTypeList']);
    }
}

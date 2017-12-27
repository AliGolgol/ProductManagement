import {Component, OnInit, OnDestroy} from '@angular/core';
import {Router, ActivatedRoute}from '@angular/router';
import {NgForm} from '@angular/forms';

import {ISupplier} from '../shared/services/interfaces';
import {SupplierService} from '../shared/services/supplier.service';
import {ItemsService} from '../shared/utils/items.service';
import {NotificationService} from '../shared/utils/notification.service';
import {ConfigService} from '../shared/utils/config.service';
import {SlimLoadingBarService} from 'ng2-slim-loading-bar/ng2-slim-loading-bar';
import {ROUTER_DIRECTIVES} from '@angular/router';

@Component({
    moduleId: module.id,
    selector: 'app-supplier-edit',
    templateUrl: 'supplier-update.component.html'
})

export class SupplierUpdateComponent implements OnInit {
    private sub: any;
    pageTitle: string = 'repository Type';
    errorMessage: string;
    supplier: ISupplier;
    apiHost: string;
    id: number;
    supplierLoaded: boolean = false;
    name: string;
    supplierDetails: ISupplier;


    constructor(private _route: ActivatedRoute,
        private _router: Router,
        private _supplierService: SupplierService,
        private itemService: ItemsService,
        private notificationService: NotificationService,
        private configService: ConfigService,
        private slimLoader: SlimLoadingBarService) { }


    ngOnInit() {
        this.id = +this._route.snapshot.params['id'];
        this.apiHost = this.configService.getApiHost();
        this.loadSupplierDetail();

    }

    loadSupplierDetail() {
        this.slimLoader.start();
        this._supplierService.getById(this.id)
            .subscribe((supplier: ISupplier) => {
                this.supplierDetails = this.itemService.getSerialized<ISupplier>(supplier);
                this.slimLoader.complete();
                this.supplierLoaded = true;
                this.notificationService.printSuccessMessage(this.supplierDetails.Id.toString());
            },
            error => {
                this.slimLoader.complete();
                this.notificationService.printErrorMessage('خطا ' + error);
            });
    }

    updatesupplier(editSupplierForm: NgForm) {
        console.log(editSupplierForm.value);

        this.slimLoader.start();
        this._supplierService.update(this.supplierDetails)
            .subscribe(() => {
                this.notificationService.printSuccessMessage('انجام شد');
                this.slimLoader.complete();
                this._router.navigate(['/supplierList']);
            },
            error => {
                this.slimLoader.complete();
                this.notificationService.printErrorMessage('خطا  ' + error);
            });
    }

    onSubmit(form): void {
        this.slimLoader.start();
        this._supplierService.update(this.supplierDetails)
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
        this._router.navigate(['/supplierList']);
    }
}

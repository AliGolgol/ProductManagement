import {Component, OnInit, OnDestroy} from '@angular/core';
import {Router, ActivatedRoute}from '@angular/router';
import {NgForm} from '@angular/forms';

import {IManufacturer} from '../shared/services/interfaces';
import {ManufacturerService} from '../shared/services/manufacturer.service';
import {ItemsService} from '../shared/utils/items.service';
import {NotificationService} from '../shared/utils/notification.service';
import {ConfigService} from '../shared/utils/config.service';
import {SlimLoadingBarService} from 'ng2-slim-loading-bar/ng2-slim-loading-bar';
import {ROUTER_DIRECTIVES} from '@angular/router';

@Component({
    moduleId: module.id,
    selector: 'app-manufacturer-edit',
    templateUrl: 'manufacturer-update.component.html'
})

export class ManufacturerUpdateComponent implements OnInit {
    private sub: any;
    pageTitle: string = 'repository Type';
    errorMessage: string;
    manufacturer: IManufacturer;
    apiHost: string;
    id: number;
    manufacturerLoaded: boolean = false;
    name: string;
    manufacturerDetails: IManufacturer;


    constructor(private _route: ActivatedRoute,
        private _router: Router,
        private _manufacturerService: ManufacturerService,
        private itemService: ItemsService,
        private notificationService: NotificationService,
        private configService: ConfigService,
        private slimLoader: SlimLoadingBarService) { }


    ngOnInit() {
        this.id = +this._route.snapshot.params['id'];
        this.apiHost = this.configService.getApiHost();
        this.loadManufacturerDetail();

    }

    loadManufacturerDetail() {
        this.slimLoader.start();
        this._manufacturerService.getById(this.id)
            .subscribe((manufacturer: IManufacturer) => {
                this.manufacturerDetails = this.itemService.getSerialized<IManufacturer>(manufacturer);
                this.slimLoader.complete();
                this.manufacturerLoaded = true;
                this.notificationService.printSuccessMessage(this.manufacturerDetails.Id.toString());
            },
            error => {
                this.slimLoader.complete();
                this.notificationService.printErrorMessage('خطا ' + error);
            });
    }

    updateManufacturer(editManufacturerForm: NgForm) {
        console.log(editManufacturerForm.value);

        this.slimLoader.start();
        this._manufacturerService.update(this.manufacturerDetails)
            .subscribe(() => {
                this.notificationService.printSuccessMessage('انجام شد');
                this.slimLoader.complete();
                this._router.navigate(['/manufacturerList']);
            },
            error => {
                this.slimLoader.complete();
                this.notificationService.printErrorMessage('خطا  ' + error);
            });
    }        

    onBack(): void {
        this._router.navigate(['/manufacturerList']);
    }
}

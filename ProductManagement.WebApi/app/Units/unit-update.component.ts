import {Component, OnInit, OnDestroy} from '@angular/core';
import {Router, ActivatedRoute}from '@angular/router';
import {NgForm} from '@angular/forms';

import {IUnit} from '../shared/services/interfaces';
import {UnitService} from '../shared/services/unit.service';
import {ItemsService} from '../shared/utils/items.service';
import {NotificationService} from '../shared/utils/notification.service';
import {ConfigService} from '../shared/utils/config.service';
import {SlimLoadingBarService} from 'ng2-slim-loading-bar/ng2-slim-loading-bar';
import {ROUTER_DIRECTIVES} from '@angular/router';

@Component({
    moduleId: module.id,
    selector: 'app-unit-edit',
    templateUrl: 'unit-update.component.html'
})

export class UnitUpdateComponent implements OnInit {
    private sub: any;
    pageTitle: string = 'repository Type';
    errorMessage: string;
    unit: IUnit;
    apiHost: string;
    id: number;
    unitLoaded: boolean = false;
    name: string;
    unitDetails: IUnit;


    constructor(private _route: ActivatedRoute,
        private _router: Router,
        private _unitService: UnitService,
        private itemService: ItemsService,
        private notificationService: NotificationService,
        private configService: ConfigService,
        private slimLoader: SlimLoadingBarService) { }


    ngOnInit() {
        this.id = +this._route.snapshot.params['id'];
        this.apiHost = this.configService.getApiHost();
        this.loadUnitDetail();

    }

    loadUnitDetail() {
        this.slimLoader.start();
        this._unitService.getById(this.id)
            .subscribe((unit: IUnit) => {
                this.unitDetails = this.itemService.getSerialized<IUnit>(unit);
                this.slimLoader.complete();
                this.unitLoaded = true;
                this.notificationService.printSuccessMessage(this.unitDetails.Id.toString());
            },
            error => {
                this.slimLoader.complete();
                this.notificationService.printErrorMessage('خطا ' + error);
            });
    }

    updateUnit(editUnitForm: NgForm) {
        console.log(editUnitForm.value);

        this.slimLoader.start();
        this._unitService.update(this.unitDetails)
            .subscribe(() => {
                this.notificationService.printSuccessMessage('انجام شد');
                this.slimLoader.complete();
                this._router.navigate(['/unitList']);
            },
            error => {
                this.slimLoader.complete();
                this.notificationService.printErrorMessage('خطا  ' + error);
            });
    }

    onSubmit(form): void {
        this.slimLoader.start();
        this._unitService.update(this.unitDetails)
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
        this._router.navigate(['/unitList']);
    }
}

import {Component, OnInit, OnDestroy} from '@angular/core';
import {Router, ActivatedRoute}from '@angular/router';
import {NgForm} from '@angular/forms';

import {IEntrySlipType} from '../shared/services/interfaces';
import {EntrySlipTypeService} from '../shared/services/entrySlipType.service';
import {ItemsService} from '../shared/utils/items.service';
import {NotificationService} from '../shared/utils/notification.service';
import {ConfigService} from '../shared/utils/config.service';
import {SlimLoadingBarService} from 'ng2-slim-loading-bar/ng2-slim-loading-bar';
import {ROUTER_DIRECTIVES} from '@angular/router';

@Component({
    moduleId: module.id,
    selector: 'app-entrySlipType-edit',
    templateUrl: 'entrySlipType-update.component.html'
})

export class EntrySlipTypeUpdateComponent implements OnInit {
    private sub: any;
    pageTitle: string = 'repository Type';
    errorMessage: string;
    entrySlipType: IEntrySlipType;
    apiHost: string;
    id: number;
    entrySlipTypeLoaded: boolean = false;
    name: string;
    entrySlipTypeDetails: IEntrySlipType;


    constructor(private _route: ActivatedRoute,
        private _router: Router,
        private _entrySlipTypeService: EntrySlipTypeService,
        private itemService: ItemsService,
        private notificationService: NotificationService,
        private configService: ConfigService,
        private slimLoader: SlimLoadingBarService) { }


    ngOnInit() {
        this.id = +this._route.snapshot.params['id'];
        this.apiHost = this.configService.getApiHost();
        this.loadEntrySlipTypeDetail();

    }

    loadEntrySlipTypeDetail() {
        this.slimLoader.start();
        this._entrySlipTypeService.getById(this.id)
            .subscribe((entrySlipType: IEntrySlipType) => {
                this.entrySlipTypeDetails = this.itemService.getSerialized<IEntrySlipType>(entrySlipType);
                this.slimLoader.complete();
                this.entrySlipTypeLoaded = true;
                this.notificationService.printSuccessMessage(this.entrySlipTypeDetails.Id.toString());
            },
            error => {
                this.slimLoader.complete();
                this.notificationService.printErrorMessage('خطا ' + error);
            });
    }

    updateEntrySlipType(editEntrySlipTypeForm: NgForm) {
        console.log(editEntrySlipTypeForm.value);

        this.slimLoader.start();
        this._entrySlipTypeService.update(this.entrySlipTypeDetails)
            .subscribe(() => {
                this.notificationService.printSuccessMessage('انجام شد');
                this.slimLoader.complete();
                this._router.navigate(['/entrySlipTypeList']);
            },
            error => {
                this.slimLoader.complete();
                this.notificationService.printErrorMessage('خطا  ' + error);
            });
    }

    onSubmit(form): void {
        this.slimLoader.start();
        this._entrySlipTypeService.update(this.entrySlipTypeDetails)
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
        this._router.navigate(['/entrySlipTypeList']);
    }
}

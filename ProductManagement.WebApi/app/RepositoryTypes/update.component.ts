import {Component, OnInit, OnDestroy} from '@angular/core';
import {Router, ActivatedRoute}from '@angular/router';
import {NgForm} from '@angular/forms';

import {IRepositoryType} from '../shared/services/interfaces';
import {RepositoryTypeService} from '../shared/services/repositoryType.service';
import {ItemsService} from '../shared/utils/items.service';
import {NotificationService} from '../shared/utils/notification.service';
import {ConfigService} from '../shared/utils/config.service';
import {SlimLoadingBarService} from 'ng2-slim-loading-bar/ng2-slim-loading-bar';
import {ROUTER_DIRECTIVES} from '@angular/router';

@Component({
    moduleId: module.id,
    selector: 'app-repositoryType-edit',
    templateUrl: 'update.html'
})

export class UpdateComponent implements OnInit {
    private sub: any;
    pageTitle: string = 'repository Type';
    errorMessage: string;
    repositoryType: IRepositoryType;
    apiHost: string;
    id: number;
    repostitoryTypeLoaded: boolean = false;
    name: string;
    repTypDetails: IRepositoryType;


    constructor(private _route: ActivatedRoute,
        private _router: Router,
        private _repositoryService: RepositoryTypeService,
        private itemService: ItemsService,
        private notificationService: NotificationService,
        private configService: ConfigService,
        private slimLoader: SlimLoadingBarService) { }


    ngOnInit() {
        this.id = +this._route.snapshot.params['id'];
        this.apiHost = this.configService.getApiHost();
        this.loadRepositoryTypeDetail();

    }

    loadRepositoryTypeDetail() {
        this.slimLoader.start();
        this._repositoryService.getRepositoryTypeById(this.id)
            .subscribe((repositoryType: IRepositoryType) => {
                this.repTypDetails = this.itemService.getSerialized<IRepositoryType>(repositoryType);
                this.slimLoader.complete();
                this.repostitoryTypeLoaded = true;
                this.notificationService.printSuccessMessage(this.repTypDetails.Id.toString());
            },
            error => {
                this.slimLoader.complete();
                this.notificationService.printErrorMessage('خطا ' + error);
            });
    }

    updateRepTyp(editRepTypForm: NgForm) {
        console.log(editRepTypForm.value);
        
        this.slimLoader.start();
        this._repositoryService.updateRepTyp(this.repTypDetails)
            .subscribe(() => {
                this.notificationService.printSuccessMessage('انجام شد');
                this.slimLoader.complete();
            },
            error => {
                this.slimLoader.complete();
                this.notificationService.printErrorMessage('خطا  ' + error);
            });
    }

    onSubmit(form): void {
        this.slimLoader.start();
        this.notificationService.printErrorMessage(this.repositoryType.Id.toString());
        this._repositoryService.updateRepTyp(this.repTypDetails)
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
        this._router.navigate(['/repositoryTypeList']);
    }
}

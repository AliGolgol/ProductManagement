import {Component, OnInit, OnDestroy} from '@angular/core';
import {Router, ActivatedRoute}from '@angular/router';
import {NgForm} from '@angular/forms';

import {IRepository, IPriceEstimate, IRepositoryDetails} from '../shared/services/interfaces';
import {RepositoryService} from '../shared/services/repository.service';
import {RepositoryTypeService} from '../shared/services/repositoryType.service';
import {ItemsService} from '../shared/utils/items.service';
import {NotificationService} from '../shared/utils/notification.service';
import {ConfigService} from '../shared/utils/config.service';
import {SlimLoadingBarService} from 'ng2-slim-loading-bar/ng2-slim-loading-bar';
import {ROUTER_DIRECTIVES} from '@angular/router';
import {MappingService} from '../shared/utils/mapping.service';

@Component({
    moduleId: module.id,
    selector: 'repository-edit',
    templateUrl: 'update.html'
})

export class RepositoryUpdateComponent implements OnInit {
    private sub: any;
    repositoryModel = <IRepositoryDetails>{};
    pageTitle: string = 'repository Type';
    errorMessage: string;
    repository: IRepository;
    apiHost: string;
    id: number;
    repostitoryLoaded: boolean = false;
    name: string;
    repositoryDetails: IRepository;
    repositorySelected: number;
    priceEstimate: IPriceEstimate[] = [{ name: 'FIFO', value: 1 },
        { name: 'LIFO', value: 2 }, { name: 'AVG', value: 3 }];
    priceEstimateSelected: number = 1;


    constructor(private _route: ActivatedRoute,
        private _router: Router,
        private _repositoryService: RepositoryService,
        private _repositoryTypeService: RepositoryTypeService,
        private itemService: ItemsService,
        private notificationService: NotificationService,
        private configService: ConfigService,
        private slimLoader: SlimLoadingBarService,
        private mapmapping: MappingService) { }


    ngOnInit() {
        this.id = +this._route.snapshot.params['id'];
        this.apiHost = this.configService.getApiHost();
        this.loadRepositoryDetail();
        this.getRepositoryTypes();
    }

    loadRepositoryDetail() {
        this.slimLoader.start();
        this._repositoryService.getById(this.id)
            .subscribe((repository: IRepository) => {

                this.repositoryDetails = this.itemService.getSerialized<IRepository>(repository);
                this.priceEstimateSelected = this.repositoryDetails.PriceEstimateId;
                this.repositorySelected = this.repositoryDetails.RepositoryTypeId;

                this.repositoryModel.Id = repository.Id;
                this.repositoryModel.Code = repository.Code;
                this.repositoryModel.Name = repository.Name;
                this.slimLoader.complete();

                this.repostitoryLoaded = true;
                this.notificationService.printSuccessMessage(this.repositoryDetails.Id.toString());
            },
            error => {
                this.slimLoader.complete();
                this.notificationService.printErrorMessage('خطا ' + error);
            });
    }

    getRepositoryTypes() {
        this._repositoryTypeService.getRepositories()
            .subscribe((repositoryType: IRepository[]) => {
                this.repositoryModel.RepositoryType = repositoryType;
            },
            error => {
                this.slimLoader.complete();
                this.notificationService.printErrorMessage('Failed to load schedule. ' + error);
            });
    }
    updateRepository(editRepositoryForm: NgForm) {
        console.log(editRepositoryForm.value);
        this.slimLoader.start();
        var mapRepository = this.mapmapping.mapRepositoryTypeToRepository(this.repositoryModel);
        mapRepository.PriceEstimateId = this.priceEstimateSelected;
        mapRepository.RepositoryTypeId = this.repositorySelected;

        this._repositoryService.update(mapRepository)
            .subscribe(() => {
                this.notificationService.printSuccessMessage('انجام شد');
                this.slimLoader.complete();
                this._router.navigate(['/repository']);
            },
            error => {
                this.slimLoader.complete();
                this.notificationService.printErrorMessage('خطا  ' + error);
            });
    }

    onChange(repositoryValue) {
        console.log(repositoryValue);
        this.notificationService.printErrorMessage(repositoryValue);
        this.repositorySelected = repositoryValue;
    }

    onChanged(priceValue) {
        console.log(priceValue);
        this.notificationService.printErrorMessage(priceValue);
        this.priceEstimateSelected = priceValue;
    }

    onSubmit(form): void {
        this.slimLoader.start();
        this.notificationService.printErrorMessage(this.repository.Id.toString());
        this._repositoryService.update(this.repositoryDetails)
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
        this._router.navigate(['/repository']);
    }
}

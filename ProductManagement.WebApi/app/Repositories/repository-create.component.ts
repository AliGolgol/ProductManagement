import {Component, trigger, OnInit, state, style, animate, transition, ViewChild, Input, Output } from '@angular/core';
import {Router} from '@angular/router';
import {IRepository, IRepositoryType, IRepositoryDetails, IPriceEstimate} from '../shared/services/interfaces';
import {RepositoryService} from '../shared/services/repository.service';
import {RepositoryTypeService} from '../shared/services/repositoryType.service';
import {MODAL_DIRECTIVES, ModalComponent} from 'ng2-bs3-modal/ng2-bs3-modal';
import {ItemsService} from '../shared/utils/items.service';
import {NotificationService} from '../shared/utils/notification.service';
import {SlimLoadingBarService} from 'ng2-slim-loading-bar/ng2-slim-loading-bar';
import {MappingService} from '../shared/utils/mapping.service';


@Component({
    selector: 'repository-add',
    templateUrl: 'app/Repositories/create.html',
    directives: [MODAL_DIRECTIVES],
    animations: [
        trigger('flyInOut', [
            state('in', style({ opacity: 1, transform: 'translateX(0)' })),
            transition('void => *', [
                style({
                    opacity: 0,
                    transform: 'translateX(-100%)'
                }),
                animate('0.6s ease-in')
            ]),
            transition('* => void', [
                animate('0.2s 10 ease-out', style({
                    opacity: 0,
                    transform: 'translateX(100%)'
                }))
            ])
        ])
    ]
})
export class CreateRepositoryComponent implements OnInit {
    repositoryModel = <IRepositoryDetails>{};//creates an empty object for an interface
    repoistory: IRepository;
    pageTitle: string = "add repository type";
    repositorySelected: number;
    priceEstimate: IPriceEstimate[] = [{ name: 'FIFO', value: 1 },
        { name: 'LIFO', value: 2 }, { name: 'AVG', value: 3 }];
    priceEstimateSelected: number=1;

    constructor(private _repositoryService: RepositoryService,
        private _router: Router,
        private _repositoryTypeService: RepositoryTypeService,
        private itemsService: ItemsService,
        private notificationService: NotificationService,
        private slimLoader: SlimLoadingBarService,
        private mapping: MappingService) { }

    ngOnInit() {
        this._repositoryTypeService.getRepositories()
            .subscribe((repositoryType: IRepository[]) => {
                this.repositoryModel.RepositoryType = repositoryType;
                this.repositoryModel.Name = '';
            },
            error => {
                this.slimLoader.complete();
                this.notificationService.printErrorMessage('Failed to load schedule. ' + error);
            });
    }
    log(Name): void {
        console.log(Name);
    }

    onSubmit(form): void {
        console.log(form);
        console.log(this.repositoryModel);
        var mapRepository = this.mapping.mapRepositoryTypeToRepository(this.repositoryModel);
        mapRepository.RepositoryTypeId = this.repositorySelected;
        mapRepository.PriceEstimateId = this.priceEstimateSelected;
        this._repositoryService.add(mapRepository)
            .subscribe((repository: IRepository) => {
                console.log(`ID: ${repository.Id}`);
                this.modalAdd.close();
                this._router.navigate(['/repository']);
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

    @ViewChild('mymodal')
    modalAdd: ModalComponent;
    selected: string;
    output: string;
    selectedRepositoryLoaded: boolean = false;
    backdropOptions = [true, false, 'static'];
    animation: boolean = true;
    keyboard: boolean = true;
    backdrop: string | boolean = true;
    addOrDetail: boolean = false;

    close() {
        this.modalAdd.close();
    }

    openIt() {
        this.modalAdd.open();
    }

    opened() {
    }
    closed() { this.output = '(closed)' + this.selected; }
    dismissed() { this.output = '(dismissed)' };
}
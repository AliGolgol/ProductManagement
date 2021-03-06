﻿import { Component, OnInit, trigger, state, style, animate, transition, ViewChild, Input, Output } from '@angular/core';
import { HTTP_PROVIDERS } from '@angular/http';
import {ROUTER_DIRECTIVES} from '@angular/router';
import {IRepository, Pagination, PaginatedResult} from '../shared/services/interfaces';
import {CreateRepositoryComponent} from './repository-create.component';
import {RepositoryService} from '../shared/services/repository.service';
import {SlimLoadingBarService} from 'ng2-slim-loading-bar/ng2-slim-loading-bar';
import {MODAL_DIRECTIVES, ModalComponent} from 'ng2-bs3-modal/ng2-bs3-modal';
import {PAGINATION_DIRECTIVES, PaginationComponent} from 'ng2-bootstrap';
import {ConfigService} from '../shared/utils/config.service';
import {NotificationService} from '../shared/utils/notification.service';
import {ItemsService} from '../shared/utils/items.service';

//import {ItemsService} from '../shared/utils/items.service';

@Component({
    moduleId: module.id,
    selector: 'pm-repositories',
    templateUrl: 'list.html',
    directives: [ROUTER_DIRECTIVES, MODAL_DIRECTIVES,
        PAGINATION_DIRECTIVES, CreateRepositoryComponent],
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
export class RepositoryListComponent implements OnInit {

    pageTitle: string = 'Repository List';
    errorMessage: string;
    showNew: boolean = false;
    repositories: IRepository[];

    public itemsPerPage: number = 2;
    public totalItems: number = 1;

    //Modal Properties
    @ViewChild('modal')
    modal: ModalComponent;
    addMoal: ModalComponent;
    items: string[] = ['item1', 'item2', 'item3'];
    selected: string;
    output: string;
    selectedRepositoryId: number;
    repositoryDetails: IRepository;
    selectedRepositoryLoaded: boolean = false;
    index: number = 0;
    backdropOptions = [true, false, 'static'];
    animation: boolean = true;
    keyboard: boolean = true;
    backdrop: string | boolean = true;
    addOrDetail: boolean = false;

    constructor(private _repositoryService: RepositoryService,
        private slimLoader: SlimLoadingBarService,
        private itemService: ItemsService,
        private notificationService: NotificationService) { }

    toggleImage(): void {
        this.showNew = !this.showNew;
    }

    ngOnInit(): void {
        console.log('In OnInit');
        this.loadRepository();
    }

    //Show details of repository type
    viewDetails(id: number) {
        this.addOrDetail = true;
        this.selectedRepositoryId = id;
        this.modal.open('lg');
        console.log('test detial');
    }

    addRepType(b: boolean) {
        this.addOrDetail = b;
        this.modal.open('lg');
        console.log('test detial');
    }
    //Get all repository type
    loadRepository() {
        this.slimLoader.color = "blue";
        this.slimLoader.start();
        this._repositoryService.getAll()
            .subscribe(
            repositories => this.repositories = repositories,
            error => this.errorMessage = <any>error);
        this.notificationService.printSuccessMessage(this.errorMessage);
        this.slimLoader.complete();
    }

    //Delete the repsitory type
    delRepository(repository: IRepository) {
        this.notificationService.openConfirmationDialog('آیا مطمئن هستید؟', () => {
            this.slimLoader.color = "blue";
            this.slimLoader.start();
            this._repositoryService.del(repository.Id)
                .subscribe(() => {
                    this.itemService.removeItemFromArray<IRepository>(this.repositories, repository);
                    this.notificationService.printSuccessMessage(repository.Name + 'حذف شد');
                    this.slimLoader.complete();
                },
                error => {
                    this.slimLoader.complete();
                    this.notificationService.printErrorMessage('خطا در حذف ' + repository.Name + ' ' + error);
                });
        });
    }

    closed() { this.output = '(closed)' + this.selected; }

    dismissed() { this.output = '(dismissed)' };
    opened() {
        this.slimLoader.start();
        if (this.addOrDetail) {
            this._repositoryService.getById(this.selectedRepositoryId)
                .subscribe((repository: IRepository) => {
                    this.repositoryDetails = this.itemService.getSerialized<IRepository>(repository);
                    this.slimLoader.complete();
                    this.selectedRepositoryLoaded = true;
                },
                error => {
                    this.slimLoader.complete();
                });
        }
        this.output = '(opened)';
    }
}
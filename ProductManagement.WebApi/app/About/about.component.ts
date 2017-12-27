import { Component, OnInit, trigger, state, style, animate, transition, ViewChild, Input, Output } from '@angular/core';
import { HTTP_PROVIDERS } from '@angular/http';
import {ROUTER_DIRECTIVES} from '@angular/router';
import {IAbout, Pagination, PaginatedResult} from '../shared/services/interfaces';
import {CreateComponent} from './create.component';
import {AboutService} from '../shared/services/about.service';
import {SlimLoadingBarService} from 'ng2-slim-loading-bar/ng2-slim-loading-bar';
import {MODAL_DIRECTIVES, ModalComponent} from 'ng2-bs3-modal/ng2-bs3-modal';
import {PAGINATION_DIRECTIVES, PaginationComponent} from 'ng2-bootstrap';
import {ConfigService} from '../shared/utils/config.service';
import {NotificationService} from '../shared/utils/notification.service';
import {ItemsService} from '../shared/utils/items.service';
import {RuntimeCompiler} from '@angular/compiler';
@Component({
    moduleId: module.id,
    selector:'pm-about',
    templateUrl: 'list.html',
    directives: [ROUTER_DIRECTIVES, MODAL_DIRECTIVES, PAGINATION_DIRECTIVES, CreateComponent],
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

export class AboutListComponent implements OnInit {
    pageTitle: string = 'About';
    errorMessage: string;
    showNew: boolean = false;
    about: IAbout[];

    public itemsPerPage: number = 2;
    public totalItems: number = 1;

    //modal properties
    @ViewChild('modal')
    modal: ModalComponent;
    addModal: ModalComponent;
    items: string[] = ['item1', 'item2', 'item3'];
    selected: string;
    output: string;
    selectedAboutId: number;
    abtDetails: IAbout;
    selectedAboutLoaded: boolean = false;
    index: number = 0;
    backdropOptions = [true, false, 'static'];
    animationStartTime: boolean = true;
    keyboard: boolean = true;
    backdrop: string | boolean = true;

    constructor(private _aboutService: AboutService,
        private slimLoader: SlimLoadingBarService,
        private itemService: ItemsService,
        private notificationService: NotificationService,
        private _runtimeCompiler: RuntimeCompiler) { }

    toggleImage(): void {
        this.showNew = !this.showNew;
    }

    ngOnInit(): void {
        console.log('In OnInit');
        this.loadAbout();
    }

    //load all about
    loadAbout() {
        
        this.slimLoader.start();
        this._aboutService.getAll()
            .subscribe(
            about => this.about = about,
            error => this.errorMessage = <any>error);
        this.slimLoader.complete();
    }

    //show details
    viewDetails(id: number) {
        //this._runtimeCompiler.clearCache();
        this.selectedAboutLoaded = true;
        this.selectedAboutId = id;
        this.modal.open('lg');
    }

    delAbout(about: IAbout) {
        this.notificationService.openConfirmationDialog('آیا مطمئن هستید؟', () => {
            this.slimLoader.start();
            this._aboutService.del(about.Id)
                .subscribe(() => {
                    this.itemService.removeItemFromArray<IAbout>(this.about, about);
                    this.notificationService.printSuccessMessage(about.Name + ' حذف شد');
                    this.slimLoader.complete();
                },
                error => {
                    this.slimLoader.complete();
                    this.notificationService.printErrorMessage(' خطا در حذف' + about.Name + ' ' + error);
                });
        });
    }

    closed() {
        this.output = '(closed)' + this.selected;
    }

    dismissed() { this.output = '(dismissed)' };

    opened() {
        this.slimLoader.start();
        this._aboutService.getById(this.selectedAboutId)
            .subscribe((about: IAbout) => {
                this.abtDetails = this.itemService.getSerialized<IAbout>(about);
                //this.aboutDetails = about;
                this.notificationService.printErrorMessage(this.abtDetails.Name);
                this.notificationService.printErrorMessage(this.abtDetails.Address);
                this.slimLoader.complete();
                this.selectedAboutLoaded = true;
                },
                error => {
                    this.slimLoader.complete();
                });
        this.output = '(opened)';
    }
}
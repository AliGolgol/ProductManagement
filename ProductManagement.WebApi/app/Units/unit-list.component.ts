import { Component, OnInit, trigger, state, style, animate, transition, ViewChild, Input, Output } from '@angular/core';
import { HTTP_PROVIDERS } from '@angular/http';
import {ROUTER_DIRECTIVES} from '@angular/router';
import {IUnit, Pagination, PaginatedResult} from '../shared/services/interfaces';
import {UnitCreateComponent} from './unit-create.component';
import {UnitService} from '../shared/services/unit.service';
import {SlimLoadingBarService} from 'ng2-slim-loading-bar/ng2-slim-loading-bar';
import {MODAL_DIRECTIVES, ModalComponent} from 'ng2-bs3-modal/ng2-bs3-modal';
import {PAGINATION_DIRECTIVES, PaginationComponent} from 'ng2-bootstrap';
import {ConfigService} from '../shared/utils/config.service';
import {NotificationService} from '../shared/utils/notification.service';
import {ItemsService} from '../shared/utils/items.service';

@Component({
    moduleId: module.id,
    selector: 'pm-unit',
    templateUrl: 'unit-list.component.html',
    directives: [ROUTER_DIRECTIVES, MODAL_DIRECTIVES, PAGINATION_DIRECTIVES, UnitCreateComponent],
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
export class UnitListComponent implements OnInit {

    pageTitle: string = 'Unit List';
    errorMessage: string;
    showNew: boolean = false;
    units: IUnit[];

    public itemsPerPage: number = 2;
    public totalItems: number = 1;

    //Modal Properties
    @ViewChild('modal')
    modal: ModalComponent;
    addMoal: ModalComponent;
    items: string[] = ['item1', 'item2', 'item3'];
    selected: string;
    output: string;
    selectedUnitId: number;
    unitDetails: IUnit;
    selectedManLoaded: boolean = false;
    index: number = 0;
    backdropOptions = [true, false, 'static'];
    animation: boolean = true;
    keyboard: boolean = true;
    backdrop: string | boolean = true;

    constructor(private _unitService: UnitService,
        private slimLoader: SlimLoadingBarService,
        private itemService: ItemsService,
        private notificationService: NotificationService) { }

    toggleImage(): void {
        this.showNew = !this.showNew;
    }

    ngOnInit(): void {
        console.log('In OnInit');
        this.load();
    }

    //Show details of repository type
    viewDetails(id: number) {
        this.selectedUnitId = id;
        this.modal.open('lg');
        console.log('test detial');
    }

    addUnit() {
        this.modal.open('lg');
        console.log('test detial');
    }
    //Get all repository type
    load() {
        this.slimLoader.start();
        this._unitService.getAll()
            .subscribe(
            response => this.units = response,
            error => this.errorMessage = <any>error);
        this.notificationService.printSuccessMessage(this.errorMessage);
    }

    //Delete the repsitory type
    delUnit(unit: IUnit) {
        this.notificationService.openConfirmationDialog('آیا مطمئن هستید؟', () => {
            this.slimLoader.start();
            this._unitService.del(unit.Id)
                .subscribe(() => {
                    this.itemService.removeItemFromArray<IUnit>(this.units, unit);
                    this.notificationService.printSuccessMessage(unit.Name + 'حذف شد ');
                    this.slimLoader.complete();
                },
                error => {
                    this.slimLoader.complete();
                    this.notificationService.printErrorMessage('خطا در حذف ' + unit.Name + ' ' + error);
                });
        });
    }

    closed() { this.output = '(closed)' + this.selected; }

    dismissed() { this.output = '(dismissed)' };

    opened() {
        this.slimLoader.start();
        this._unitService.getById(this.selectedUnitId)
            .subscribe((unit: IUnit) => {
                this.unitDetails = this.itemService.getSerialized<IUnit>(unit);
                this.slimLoader.complete();
                this.selectedManLoaded = true;
            },
            error => {
                this.slimLoader.complete();
            });
        this.output = '(opened)';
    }
}
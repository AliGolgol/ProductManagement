import {Component,trigger, state, style, animate, transition, ViewChild, Input, Output } from '@angular/core';
import {Router} from '@angular/router';
import {IRepositoryType} from '../shared/services/interfaces';
import {RepositoryTypeService} from '../shared/services/repositoryType.service';
import {MODAL_DIRECTIVES, ModalComponent} from 'ng2-bs3-modal/ng2-bs3-modal';

@Component({
    selector:'repType-add',
    templateUrl: 'app/RepositoryTypes/create.html',
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
export class CreateComponent {
    repositoryTypeModel = <IRepositoryType>{};//creates an empty object for an interface
    pageTitle: string = "add repository type";
    constructor(private _repositoryTypeService: RepositoryTypeService, private _router: Router) { }

    log(Name): void {
        console.log(Name);
    }

    onSubmit(form): void {
        console.log(form);
        console.log(this.repositoryTypeModel);

        this._repositoryTypeService.addRepositoryType(this.repositoryTypeModel)
            .subscribe((repositoryType: IRepositoryType) => {
                console.log(`ID: ${repositoryType.Id}`);
                this.modalAdd.close();
            });
    }

    @ViewChild('mymodal')
    modalAdd: ModalComponent;
    selected: string;
    output: string;
    selectedRepTypLoaded: boolean = false;
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
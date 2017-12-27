import {Component, trigger, state, style, animate, transition, ViewChild, Input, Output } from '@angular/core';
import {Router} from '@angular/router';
import {IEntrySlipType} from '../shared/services/interfaces';
import {EntrySlipTypeService} from '../shared/services/entrySlipType.service';
import {MODAL_DIRECTIVES, ModalComponent} from 'ng2-bs3-modal/ng2-bs3-modal';

@Component({
    selector: 'entrySlipType-add',
    templateUrl: 'app/EntrySlipTypes/entrySlipType-create.component.html',
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
export class EntrySlipTypeCreateComponent {
    entrySlipTypeModel = <IEntrySlipType>{};//creates an empty object for an interface
    pageTitle: string = "add slip type";
    constructor(private _entrySlipTypeService: EntrySlipTypeService, private _router: Router) { }

    log(Name): void {
        console.log(Name);
    }

    onSubmit(form): void {
        console.log(form);
        console.log(this.entrySlipTypeModel);

        this._entrySlipTypeService.add(this.entrySlipTypeModel)
            .subscribe((entrySlipType: IEntrySlipType) => {
                console.log(`ID: ${entrySlipType.Id}`);
                this.modalAdd.close();
            });
    }

    @ViewChild('mymodal')
    modalAdd: ModalComponent;
    selected: string;
    output: string;
    selectedSlipLoaded: boolean = false;
    backdropOptions = [true, false, 'static'];
    animation: boolean = true;
    keyboard: boolean = true;
    backdrop: string | boolean = true;

    close() { this.modalAdd.close(); }

    openIt() { this.modalAdd.open(); }

    opened() { }

    closed() { this.output = '(closed)' + this.selected; }

    dismissed() { this.output = '(dismissed)' };
}
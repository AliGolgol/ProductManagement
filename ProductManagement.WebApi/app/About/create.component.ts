import {Component, trigger, state, style, animate, transition, ViewChild, Input, Output } from '@angular/core';
import {Router} from '@angular/router';
import {IAbout} from '../shared/services/interfaces';
import {AboutService} from '../shared/services/about.service';
import {MODAL_DIRECTIVES, ModalComponent} from 'ng2-bs3-modal/ng2-bs3-modal';
import {NotificationService} from '../shared/utils/notification.service';
@Component({
    selector: 'about-add',
    templateUrl: 'app/About/create.html',
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
    aboutModel = <IAbout>{};//creates an empty object for an interface
    pageTitle: string = "add repository type";
    constructor(private _aboutService: AboutService,
        private _router: Router,
        private notification: NotificationService) { }

    log(Name): void {
        console.log(Name);
    }

    onSubmit(form): void {
        console.log(form);
        console.log(this.aboutModel);
        this.notification.printErrorMessage(this.aboutModel.Name);
        this._aboutService.add(this.aboutModel)
            .subscribe((about: IAbout) => {
                console.log(`ID: ${about.Id}`);
                this.modalAdd.close();
                
            });this._router.navigate(['/about']);
    }

    save(form): void {
        console.log(form);
        console.log(this.aboutModel);
        this.notification.printErrorMessage(this.aboutModel.Name);
        this._aboutService.add(this.aboutModel)
            .subscribe((about: IAbout) => {
                console.log(`ID: ${about.Id}`);
                this.modalAdd.close();
                this._router.navigate(['/about']);
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
import {Component, OnInit, OnDestroy} from '@angular/core';
import {Router, ActivatedRoute}from '@angular/router';
import {NgForm} from '@angular/forms';

import {IAbout} from '../shared/services/interfaces';
import {AboutService} from '../shared/services/about.service';
import {ItemsService} from '../shared/utils/items.service';
import {NotificationService} from '../shared/utils/notification.service';
import {ConfigService} from '../shared/utils/config.service';
import {SlimLoadingBarService} from 'ng2-slim-loading-bar/ng2-slim-loading-bar';
import {ROUTER_DIRECTIVES} from '@angular/router';

@Component({
    moduleId: module.id,
    selector: 'app-aboutEdit',
    templateUrl: 'update.html'
})

export class UpdateAboutComponent implements OnInit {
    private sub: any;
    pageTitle: string = 'about';
    errorMessage: string;
    about: IAbout;
    apiHost: string;
    id: number;
    aboutLoaded: boolean = false;
    name: string;
    aboutDetails: IAbout;


    constructor(private _route: ActivatedRoute,
        private _router: Router,
        private _aboutService: AboutService,
        private itemService: ItemsService,
        private notificationService: NotificationService,
        private configService: ConfigService,
        private slimLoader: SlimLoadingBarService) { }


    ngOnInit() {
        this.id = +this._route.snapshot.params['id'];
        this.apiHost = this.configService.getApiHost();
        this.loadRAboutDetail();

    }

    loadRAboutDetail() {
        this.slimLoader.start();
        this._aboutService.getById(this.id)
            .subscribe((about: IAbout) => {
                this.aboutDetails = this.itemService.getSerialized<IAbout>(about);
                this.slimLoader.complete();
                this.aboutLoaded = true;
                this.notificationService.printSuccessMessage(this.aboutDetails.Id.toString());
            },
            error => {
                this.slimLoader.complete();
                this.notificationService.printErrorMessage('خطا ' + error);
            });
    }

    updateAbout(editAboutForm: NgForm) {
        console.log(editAboutForm.value);

        this.slimLoader.start();
        this._aboutService.update(this.aboutDetails)
            .subscribe(() => {
                this.notificationService.printSuccessMessage('انجام شد');
                this.slimLoader.complete();
                this._router.navigate(['/about']);
            },
            error => {
                this.slimLoader.complete();
                this.notificationService.printErrorMessage('خطا  ' + error);
            });
    }

    onSubmit(form): void {
        this.slimLoader.start();
        this.notificationService.printErrorMessage(this.about.Id.toString());
        this._aboutService.update(this.aboutDetails)
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
        this._router.navigate(['/about']);
    }
}

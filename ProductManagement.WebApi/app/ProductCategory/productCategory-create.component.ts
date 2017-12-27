import {Component, trigger, OnInit, state, style, animate, transition, ViewChild, Input, Output } from '@angular/core';
import {Router} from '@angular/router';
import {IProductCategory,IProductCategoryDetails} from '../shared/services/interfaces';
import {ProductCategoryService} from '../shared/services/productCategory.service';
import {MODAL_DIRECTIVES, ModalComponent} from 'ng2-bs3-modal/ng2-bs3-modal';
import {ItemsService} from '../shared/utils/items.service';
import {NotificationService} from '../shared/utils/notification.service';
import {SlimLoadingBarService} from 'ng2-slim-loading-bar/ng2-slim-loading-bar';
import {MappingService} from '../shared/utils/mapping.service';


@Component({
    selector: 'productCategory-add',
    templateUrl: 'app/ProductCategory/productCategory-create.component.html',
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
export class CreateProductCategoryComponent implements OnInit {
    productCategoryModel = <IProductCategoryDetails>{};//creates an empty object for an interface
    repoistory: IProductCategory;
    pageTitle: string = "add productCategory type";
    productCategorySelected: number;

    constructor(
        private _router: Router,
        private _productCategoryService: ProductCategoryService,
        private itemsService: ItemsService,
        private notificationService: NotificationService,
        private slimLoader: SlimLoadingBarService,
        private mapping: MappingService) { }

    ngOnInit() {
        this._productCategoryService.getAll()
            .subscribe((productCategory: IProductCategory[]) => {
                this.productCategoryModel.Parent = productCategory;
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
        console.log(this.productCategoryModel);
        var mapRepository = this.mapping.mapProductCategoryToProductCategory(this.productCategoryModel);
        mapRepository.ParentId = this.productCategorySelected;

        this._productCategoryService.add(mapRepository)
            .subscribe((productCategory: IProductCategory) => {
                console.log(`ID: ${productCategory.Id}`);
                this.modalAdd.close();
            });
    }
    
    onChange(prdCatValue) {
        console.log(prdCatValue);
        this.notificationService.printErrorMessage(prdCatValue);
        this.productCategorySelected = prdCatValue;
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
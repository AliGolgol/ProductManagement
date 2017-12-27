import {Component, OnInit, OnDestroy} from '@angular/core';
import {Router, ActivatedRoute}from '@angular/router';
import {NgForm} from '@angular/forms';

import {IProductCategoryDetails, IProductCategory} from '../shared/services/interfaces';
import {ProductCategoryService} from '../shared/services/productCategory.service';
import {ItemsService} from '../shared/utils/items.service';
import {NotificationService} from '../shared/utils/notification.service';
import {ConfigService} from '../shared/utils/config.service';
import {SlimLoadingBarService} from 'ng2-slim-loading-bar/ng2-slim-loading-bar';
import {ROUTER_DIRECTIVES} from '@angular/router';
import {MappingService} from '../shared/utils/mapping.service';

@Component({
    moduleId: module.id,
    selector: 'productCategory-edit',
    templateUrl: 'productCategory-update.component.html'
})

export class ProductCategoryUpdateComponent implements OnInit {
    private sub: any;
    productCategoryModel = <IProductCategoryDetails>{};
    pageTitle: string = 'productCategory Type';
    errorMessage: string;
    productCategory: IProductCategory;
    apiHost: string;
    id: number;
    productCategoryLoaded: boolean = false;
    name: string;
    productCategoryDetails: IProductCategory;
    productCategorySelected: number;

    constructor(private _route: ActivatedRoute,
        private _router: Router,
        private _productCategoryService: ProductCategoryService,
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

        this._productCategoryService.getById(this.id)
            .subscribe((productCategory: IProductCategory) => {

                this.productCategoryDetails = this.itemService.getSerialized<IProductCategory>(productCategory);
                this.productCategorySelected = this.productCategoryDetails.ParentId;

                this.productCategoryModel.Id = productCategory.Id;
                this.productCategoryModel.Name = productCategory.Name;
                this.productCategoryModel.Description = productCategory.Description;
                this.productCategoryModel.Fee = productCategory.Fee;
                this.productCategoryModel.IsLastLevel = productCategory.IsLastLevel;
                this.productCategoryModel.MinimumBalance = productCategory.MinimumBalance;
                this.productCategoryModel.PackageCount = productCategory.PackageCount;

                this.slimLoader.complete();

                this.productCategoryLoaded = true;
                this.notificationService.printSuccessMessage(this.productCategoryDetails.Id.toString());
            },
            error => {
                this.slimLoader.complete();
                this.notificationService.printErrorMessage('خطا ' + error);
            });
    }

    getRepositoryTypes() {
        this._productCategoryService.getAll()
            .subscribe((productCategory: IProductCategory[]) => {
                this.productCategoryModel.Parent = productCategory;
            },
            error => {
                this.slimLoader.complete();
                this.notificationService.printErrorMessage('Failed to load schedule. ' + error);
            });
    }

    updateProductCategory(editProductCategoryForm: NgForm) {
        console.log(editProductCategoryForm.value);

        this.slimLoader.start();
        var mapRepository = this.mapmapping.mapProductCategoryToProductCategory(this.productCategoryModel);
        mapRepository.ParentId = this.productCategorySelected;

        this._productCategoryService.update(mapRepository)
            .subscribe(() => {
                this.notificationService.printSuccessMessage('انجام شد');
                this.slimLoader.complete();
                this._router.navigate(['/productCategoryList']);
            },
            error => {
                this.slimLoader.complete();
                this.notificationService.printErrorMessage('خطا  ' + error);
            });
    }

    onChange(productCategoryValue) {
        console.log(productCategoryValue);
        this.notificationService.printErrorMessage(productCategoryValue);
        this.productCategorySelected = productCategoryValue;
    }

    onSubmit(form): void {
        this.slimLoader.start();
        this.notificationService.printErrorMessage(this.productCategory.Id.toString());
        this._productCategoryService.update(this.productCategoryDetails)
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
        this._router.navigate(['/productCategoryList']);
    }
}

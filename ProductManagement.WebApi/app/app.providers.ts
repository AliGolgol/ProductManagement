import { bind } from '@angular/core';
import { HTTP_PROVIDERS, Http } from '@angular/http';
import { Location, LocationStrategy, HashLocationStrategy} from '@angular/common';

import { RepositoryTypeService } from './shared/services/repositoryType.service';
import { ConfigService } from './shared/utils/config.service';
import { ItemsService } from './shared/utils/items.service';
import { NotificationService } from './shared/utils/notification.service';
import {AboutService} from './shared/services/about.service';
import {RepositoryService} from './shared/services/repository.service';
import {SlimLoadingBarService} from 'ng2-slim-loading-bar/ng2-slim-loading-bar';
import {MappingService} from './shared/utils/mapping.service';
import {ManufacturerService} from './shared/services/manufacturer.service';
import {UnitService} from './shared/services/unit.service';
import {SupplierService} from './shared/services/supplier.service';
import {ProductCategoryService} from './shared/services/productCategory.service';
import {ProductService} from './shared/services/product.service';
import {BillTypeService} from './shared/services/billType.service';
import {EntrySlipTypeService} from './shared/services/entrySlipType.service';
import {Cook} from './shared/services/cookie.service';
import {BuySlipService} from './shared/services/buySlip.service';
import {BuySlipItemService} from './shared/services/buySlipItem.service';
import {InvoiceService} from './shared/services/invoice.service';
import {InvoiceItemService} from './shared/services/invoiceItem.service';
import {PaymentTypeService} from './shared/services/paymentType.service';
import {DateService} from './shared/services/date.service';
import {AccountService} from './shared/services/account.service';
import {LocalStorageService} from '../app/SessionManagement/LocalStorageEmitter';
import {HttpClient} from './shared/services/HttpClient.service';
import {LoginStatusService} from './shared/services/loginStatus.service';
import { ChartsModule } from 'ng2-charts';


export const APP_PROVIDERS = [
    ConfigService,
    RepositoryTypeService,
    ItemsService,
    NotificationService,
    HTTP_PROVIDERS,
    SlimLoadingBarService,
    AboutService,
    RepositoryService,
    MappingService,
    ManufacturerService,
    UnitService,
    SupplierService,
    ProductCategoryService,
    ProductService,
    BillTypeService,
    EntrySlipTypeService,
    Cook,
    BuySlipService,
    BuySlipItemService,
    InvoiceItemService,
    InvoiceService,
    PaymentTypeService,
    DateService,
    AccountService,
    LocalStorageService,
    HttpClient,
    LoginStatusService
];
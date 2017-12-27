import {provideRouter, RouterConfig} from '@angular/router';
import {RepositoryTypeListComponent} from './RepositoryTypes/repositoryType-list.component';
import {CreateComponent} from './RepositoryTypes/create.component';
import {UpdateComponent} from './RepositoryTypes/update.component';
import {AboutListComponent} from './About/about.component';
import {HomeComponent} from './Home/home.component';
import {UpdateAboutComponent} from './About/update.component';
import {RepositoryListComponent} from './Repositories/repository-list.component';
import {RepositoryUpdateComponent} from './Repositories/repository-update.component';
import {ManufacturerUpdateComponent} from './Manufacturers/manufacturer-update.component';
import {ManufacturerListComponent} from './Manufacturers/manufacturer-list.component';
import {UnitListComponent} from './Units/unit-list.component';
import {UnitUpdateComponent} from './Units/Unit-Update.Component';
import {SupplierUpdateComponent} from './Suppliers/supplier-update.component';
import {SupplierListComponent} from './Suppliers/supplier-list.component';
import {ProductCategoryListComponent} from './ProductCategory/productCategory-list.component';
import {ProductCategoryUpdateComponent} from './productCategory/productCategory-update.component';
import {ProductUpdateComponent} from './products/product-update.component';
import {ProductListComponent} from './products/product-list.component';
import {BillTypeListComponent} from './BillTypes/billType-list.component';
import {BillTypeUpdateComponent} from './BillTypes/billType-update.component';
import {EntrySlipTypeUpdateComponent} from './EntrySlipTypes/entrySlipType-update.component';
import {EntrySlipTypeListComponent} from './EntrySlipTypes/entrySlipType-list.component';
import {BuySlipListComponent} from './BuySlips/buySlip-list.component';
import {BuySlipUpdateComponent} from './BuySlips/buySlip-update.component';
import {BuySlipItemUpdateComponent} from './BuySlipItems/buySlipItem-update.component';
import {InvoiceListComponent} from './Invoices/invoice-list.component';
import {InvoiceUpdateComponent} from './Invoices/invoice-update.component';
import {InvoiceItemUpdateComponent} from './InvoiceItems/invoiceItem-update.component';
import {PaymentTypeUpdateComponent} from './PaymentTypes/paymentType-update.component';
import {PaymentTypeListComponent} from './PaymentTypes/paymentType-list.component';
import {RegisterAccountComponent} from './Account/account-register.component';
import {LoginAccountComponent} from './Account/account-login.component';
import {CreateBuySlipItemComponent} from './BuySlipItems/buySlipItem-create.component';
import {CreateBuySlipComponent} from './BuySlips/buySlip-create.component';
import {ChangePasswordAccountComponent} from './Account/account-changePassword.component';
import {AddBuySlipComponent} from './BuySlips/buySlip-add.component';
import {AddInvoiceComponent} from './Invoices/invoice-add.component';
import {CreateInvoiceItemComponent} from './InvoiceItems/invoiceItem-create.component';

export const App_Routes: RouterConfig = [
    { path: '', component: HomeComponent, terminal: true },
    { path: 'addRepositoryType', component: CreateComponent },
    { path: 'updateRepositoryType/:id', component: UpdateComponent },
    { path: 'about', component: AboutListComponent },
    { path: 'repositoryTypeList', component: RepositoryTypeListComponent },
    { path: 'updateAbout/:id', component: UpdateAboutComponent },
    { path: 'repository', component: RepositoryListComponent },
    { path: 'updateRepository/:id', component: RepositoryUpdateComponent },
    { path: 'updateManufacturer/:id', component: ManufacturerUpdateComponent },
    { path: 'manufacturerList', component: ManufacturerListComponent },
    { path: 'unitList', component: UnitListComponent },
    { path: 'updateUnit/:id', component: UnitUpdateComponent },
    { path: 'updateSupplier/:id', component: SupplierUpdateComponent },
    { path: 'supplierList', component: SupplierListComponent },
    { path: 'productCategoryList', component: ProductCategoryListComponent },
    { path: 'updateProductCategory/:id', component: ProductCategoryUpdateComponent },
    { path: 'updateProduct/:id', component: ProductUpdateComponent },
    { path: 'productList', component: ProductListComponent },
    { path: 'billTypeList', component: BillTypeListComponent },
    { path: 'updateBillType/:id', component: BillTypeUpdateComponent },
    { path: 'updateEntrySlipType/:id', component: EntrySlipTypeUpdateComponent },
    { path: 'entrySlipTypeList', component: EntrySlipTypeListComponent },
    { path: 'buySlipList', component: BuySlipListComponent },
    { path: 'updateBuySlip/:id', component: BuySlipUpdateComponent },
    { path: 'updateBuySlipItem/:id', component: BuySlipItemUpdateComponent },
    { path: 'invoiceList', component: InvoiceListComponent },
    { path: 'updateInvoice/:id', component: InvoiceUpdateComponent },
    { path: 'updateInvoiceItem/:id', component: InvoiceItemUpdateComponent },
    { path: 'updatePaymentType/:id', component: PaymentTypeUpdateComponent },
    { path: 'paymentTypeList', component: PaymentTypeListComponent },
    { path: 'register', component: RegisterAccountComponent },
    { path: 'login', component: LoginAccountComponent },
    { path: 'addBuySlipItem', component: CreateBuySlipItemComponent },
    { path: 'addBuySlip', component: CreateBuySlipComponent },
    { path: 'changePassword', component: ChangePasswordAccountComponent },
    { path: 'createBuySlip', component: AddBuySlipComponent },
    { path: 'createInvoice', component: AddInvoiceComponent },
    { path: 'addInvoiceItem', component: CreateInvoiceItemComponent }
];

export const APP_ROUTER_PROVIDERS = [
    provideRouter(App_Routes)
];
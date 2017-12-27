export interface Pagination {
    CurrentPage: number;
    ItemsPerPage: number;
    TotalItems: number;
    TotalPages: number;
}

export class PaginatedResult<T> {
    result: T;
    pagination: Pagination;
}

export interface Predicate<T> {
    (item: T): boolean
}

export interface IRepositoryType {
    Id: number;
    Name: string;
}

export interface IAbout {
    Id: number;
    Name: string;
    Address: string;
    Tel: string;
}

export interface IRepository {
    Id: number;
    Code: number;
    Name: string;
    RepositoryTypeId: number;
    PriceEstimateId: number;
}

export interface IRepositoryDetails {
    Id: number;
    Code: number;
    Name: string;
    RepositoryType: IRepositoryType[];
}

export interface IPriceEstimate {
    name: string;
    value: number;
}

export interface IManufacturer {
    Id: number;
    Name: string;
    Address: string;
    Description: string;
    Tel: string;
}

export interface IUnit {
    Id: number;
    Name: string;
    Description: string;
}

export interface ISupplier {
    Id: number;
    FirstName: string;
    LastName: string;
    Address: string;
    Description: string;
}

export interface IProductCategory {
    Id: number;
    Name: string;
    Description: string;
    PackageCount: number;
    MinimumBalance: number;
    Fee: number;
    IsLastLevel: boolean;
    ParentId: number;
}

export interface IProductCategoryDetails {
    Id: number;
    Name: string;
    Description: string;
    PackageCount: number;
    MinimumBalance: number;
    Fee: number;
    IsLastLevel: boolean;
    Parent: IProductCategory[];
}

export interface IProduct {
    Id: number;
    Name: string;
    QuantityPerUnit: string;
    PackageCount: number;
    MinimumBalance: number;
    Fee: number;
    ProductCategoryId: number;
    ManufacturerId: number;
}

export interface IProductDetails {
    Id: number;
    Name: string;
    QuantityPerUnit: string;
    PackageCount: number;
    MinimumBalance: number;
    Fee: number;
    ProductCategory: IProductCategory[];
    Manufacturer: IManufacturer[];
}

export interface IBillType {
    Id: number;
    Name: string;
    Description: string;
    IsRemoval: boolean;
}

export interface IEntrySlipType {
    Id: number;
    Name: string;
}

export interface IBuySlipItem {
    Id: number;
    Price: number;
    Quantity: number;
    Description: string;
    ProductId: number;
    RepositoryId: number;
    BuySlipId: number;
}

export interface IBuySlipItemDetails {
    BuySlipItme: IBuySlipItem;
    Id: number;
    Price: number;
    Quantity: number;
    Description: string;
    Products: IProduct[];
    Repositories: IRepository[];

}

export interface IBuySlip {
    Id: number;
    DateCreation: string;
    Description: string;
    DeliveryMan: string;
    SupplierId: number;
    PeriodId: number;
    AppUserId: number;
    EntrySlipTypeId: number;
}

export interface IBuySlipUpdate {
    Id: number;
    DateCreation: string;
    Description: string;
    DeliveryMan: string;
    SupplierId: number;
    PeriodId: number;
    AppUserId: number;
    EntrySlipTypeId: number;
    BuySlipItems: IBuySlipItem[];
}

export interface IEntryType {
    Id: number;
    Name: string;
}

export interface IBuySlipDetails {
    Id: number;
    DateCreation: string;
    Description: string;
    DeliveryMan: string;
    SupplierId: number;
    PeriodId: number;
    AppUserId: number;
    BuySlipItems: IBuySlipItem[];
    Suppliers: ISupplier[];
    EntrySlipTypes: IEntrySlipType[];
}

export interface IInvoice {
    Id: number;
    CreatedDate: string;
    Description: string;
    Reciver: string;
    PeriedId: number;
    AppUserId: number;
    PaymentTypeId: number;
    BillTypeId: number;
}

export interface IInvoiceItem {
    Id: number;
    Description: string;
    Quantity: number;
    Price: number;
    ProductId: number;
    InvoiceId: number;
    RepositoryId: number;
}

export interface IInvoiceItemDetails {
    Id: number;
    Description: string;
    Quantity: number;
    Price: number;
    Products: IProduct[];
    Repositories: IRepository[];
}

export interface IInvoiceDetails {
    Id: number;
    CreatedDate: string;
    Description: string;
    Reciver: string;
    PeriedId: number;
    AppUserId: number;
    PaymentTypes: IPaymentType[];
    BillTypes: IBillType[];
}

export interface IPaymentType {
    Id: number;
    Name: string;
    Description: string;
}

export interface IDate {
    Year: string;
    Month: string;
    Day: string;
}

export interface IRegisterBindingModel {
    Email: string;
    Password: string;
    ConfirmPassword: string;
}

export interface ILogin {
    username: string;
    password: string;
    grant_type: string;
}

export interface IChangePasswordBindingModel {
    OldPassword: string;
    NewPassword: string;
    ConfirmPassword: string;
}

export interface ISelectQauntity {
    id: number;
    prdId: number;
}

export interface IPageList {
    CurrentPage: number;
    ItemsPerPage: number;
    //PageSize: number;
    //TotalRecords: number;
    //TotalPages: number;
    //Page: number;
    //Filter: string;
    //Sort: string;
    //SortDir: string;
}

export interface ILoginStatus
{
    flag: boolean;
}
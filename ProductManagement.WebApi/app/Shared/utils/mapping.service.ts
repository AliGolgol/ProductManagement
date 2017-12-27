import { Injectable } from '@angular/core';

import { IRepositoryType, IRepository, IRepositoryDetails,
    IProductCategoryDetails, IProductCategory, IProduct,
    IProductDetails, IBuySlipItem, IBuySlipItemDetails,
    IBuySlip, IBuySlipDetails, IInvoice, IInvoiceDetails,
    IInvoiceItem, IInvoiceItemDetails} from '../services/interfaces';

import { ItemsService } from './items.service'

@Injectable()
export class MappingService {

    constructor(private itemsService: ItemsService) { }
    name: IRepository;

    mapRepositoryTypeToRepository(repositoryDetails: IRepositoryDetails): IRepository {
        var repository: IRepository = {
            Id: repositoryDetails.Id,
            Code: repositoryDetails.Code,
            Name: repositoryDetails.Name,
            RepositoryTypeId: 1,
            PriceEstimateId: 1
            //RepositoryTypeId:this.itemsService.getPropertyValues<IRepositoryType, number[]>(repositoryDetails.RepositoryType, 'Id')
        }
        return repository;
    }

    mapProductCategoryToProductCategory(productCategoryDetails: IProductCategoryDetails): IProductCategory {
        var productCategory: IProductCategory = {
            Id: productCategoryDetails.Id,
            Name: productCategoryDetails.Name,
            Description: productCategoryDetails.Description,
            PackageCount: productCategoryDetails.PackageCount,
            MinimumBalance: productCategoryDetails.MinimumBalance,
            Fee: productCategoryDetails.Fee,
            IsLastLevel: productCategoryDetails.IsLastLevel,
            ParentId: 1
        }
        return productCategory;
    }

    mapProductDetailToProduct(productDetails: IProductDetails): IProduct {
        var product: IProduct = {
            Id: productDetails.Id,
            Name: productDetails.Name,
            Fee: productDetails.Fee,
            ManufacturerId: 1,
            MinimumBalance: productDetails.MinimumBalance,
            PackageCount: productDetails.PackageCount,
            ProductCategoryId: 1,
            QuantityPerUnit: productDetails.QuantityPerUnit
        }
        return product;
    }

    mapBuySlipItemDetailToBuySlipItem(buySlipItemDetails: IBuySlipItemDetails): IBuySlipItem {
        var buySlipItem: IBuySlipItem = {
            Id: buySlipItemDetails.Id,
            BuySlipId: 2,
            Description: buySlipItemDetails.Description,
            Price: buySlipItemDetails.Price,
            ProductId: 1,
            Quantity: buySlipItemDetails.Quantity,
            RepositoryId: 1
        }
        return buySlipItem;
    }

    mapBuySlipDetailsToBuySlip(buySlipDetails: IBuySlipDetails): IBuySlip {
        var buySlip: IBuySlip = {
            Id: buySlipDetails.Id,
            EntrySlipTypeId: 1,
            Description: buySlipDetails.Description,
            SupplierId: buySlipDetails.SupplierId,
            DateCreation: buySlipDetails.DateCreation,
            DeliveryMan: buySlipDetails.DeliveryMan,
            PeriodId:1,
            AppUserId:1
        }
        return buySlip;
    }

    mapBuySlipDetailsTo(date: string): IBuySlip {
        var buySlip: IBuySlip = {
            DateCreation: date,
            DeliveryMan: '',
            Description: '',
            EntrySlipTypeId: 1,
            Id: 1,
            SupplierId:1,
            PeriodId: 1,
            AppUserId: 1
        }
        return buySlip;
    }
    mapInvoiceDetailsToInvoice(invoiceDetails: IInvoiceDetails): IInvoice {
        var invoice: IInvoice = {
            Id: invoiceDetails.Id,
            Description: invoiceDetails.Description,
            BillTypeId: 1,
            CreatedDate: invoiceDetails.CreatedDate,
            Reciver: invoiceDetails.Reciver,
            PeriedId: 1,
            AppUserId: 1,
            PaymentTypeId:1
        }
        return invoice;
    }

    mapInvoiceItemDetailsToInvoiceItem(invoiceItmeDetails: IInvoiceItemDetails): IInvoiceItem {
        var invoiceItem: IInvoiceItem = {
            Id: invoiceItmeDetails.Id,
            Description: invoiceItmeDetails.Description,
            Price: invoiceItmeDetails.Price,
            ProductId: 1,
            Quantity: invoiceItmeDetails.Quantity,
            RepositoryId: 1,
            InvoiceId:1
        }
        return invoiceItem;
    }
}
namespace Repository.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Abouts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Address = c.String(nullable: false, maxLength: 500),
                        Tel = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Address1 = c.String(),
                        Tell = c.String(),
                        Fax = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AddressId = c.Int(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.AddressId)
                .Index(t => t.AddressId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.UserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.EntrySlips",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedDate = c.String(nullable: false),
                        Description = c.String(maxLength: 500),
                        DeliveryMan = c.String(),
                        UserId = c.Int(),
                        ESTypeId = c.Int(),
                        PeriodId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .ForeignKey("dbo.EntrySlipTypes", t => t.ESTypeId)
                .ForeignKey("dbo.Periods", t => t.PeriodId)
                .Index(t => t.UserId)
                .Index(t => t.ESTypeId)
                .Index(t => t.PeriodId);
            
            CreateTable(
                "dbo.EntrySlipTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BuySlips",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateCreation = c.String(nullable: false),
                        Description = c.String(),
                        DeliveryMan = c.String(),
                        SupplierId = c.Int(),
                        PeriodId = c.Int(),
                        AppUserId = c.Int(),
                        EntrySlipTypeId = c.Int(),
                        Supplier_Id = c.Int(),
                        EntrySlipType_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.AppUserId)
                .ForeignKey("dbo.Suppliers", t => t.Supplier_Id)
                .ForeignKey("dbo.EntrySlipTypes", t => t.EntrySlipTypeId)
                .ForeignKey("dbo.Periods", t => t.PeriodId)
                .ForeignKey("dbo.Suppliers", t => t.SupplierId)
                .ForeignKey("dbo.EntrySlipTypes", t => t.EntrySlipType_Id)
                .Index(t => t.SupplierId)
                .Index(t => t.PeriodId)
                .Index(t => t.AppUserId)
                .Index(t => t.EntrySlipTypeId)
                .Index(t => t.Supplier_Id)
                .Index(t => t.EntrySlipType_Id);
            
            CreateTable(
                "dbo.BuySlipItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Description = c.String(maxLength: 500),
                        ProductId = c.Int(nullable: false),
                        RepositoryId = c.Int(nullable: false),
                        BuySlipId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BuySlips", t => t.BuySlipId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .ForeignKey("dbo.Repositories", t => t.RepositoryId)
                .Index(t => t.ProductId)
                .Index(t => t.RepositoryId)
                .Index(t => t.BuySlipId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        QuantityPerUnit = c.String(nullable: false),
                        ProductCategoryId = c.Int(nullable: false),
                        ManufacturerId = c.Int(),
                        Manufacturer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Manufacturers", t => t.Manufacturer_Id)
                .ForeignKey("dbo.Manufacturers", t => t.ManufacturerId)
                .ForeignKey("dbo.ProductCategories", t => t.ProductCategoryId)
                .Index(t => t.ProductCategoryId)
                .Index(t => t.ManufacturerId)
                .Index(t => t.Manufacturer_Id);
            
            CreateTable(
                "dbo.BillItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Quantity = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        BillId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bills", t => t.BillId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.ProductId)
                .Index(t => t.BillId);
            
            CreateTable(
                "dbo.Bills",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedDate = c.String(nullable: false),
                        Description = c.String(maxLength: 500),
                        Reciver = c.String(),
                        PeriedId = c.Int(),
                        AppUserId = c.Int(),
                        PaymentTypeId = c.Int(),
                        BiilTypeId = c.Int(),
                        BillType_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.AppUserId)
                .ForeignKey("dbo.BillTypes", t => t.BillType_Id)
                .ForeignKey("dbo.BillTypes", t => t.BiilTypeId)
                .ForeignKey("dbo.PaymentTypes", t => t.PaymentTypeId)
                .ForeignKey("dbo.Periods", t => t.PeriedId)
                .Index(t => t.PeriedId)
                .Index(t => t.AppUserId)
                .Index(t => t.PaymentTypeId)
                .Index(t => t.BiilTypeId)
                .Index(t => t.BillType_Id);
            
            CreateTable(
                "dbo.BillTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(maxLength: 500),
                        IsRemoval = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedDate = c.String(nullable: false),
                        Description = c.String(maxLength: 500),
                        Reciver = c.String(),
                        PeriedId = c.Int(),
                        AppUserId = c.Int(),
                        PaymentTypeId = c.Int(),
                        BillTypeId = c.Int(),
                        PaymentType_Id = c.Int(),
                        BillType_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.AppUserId)
                .ForeignKey("dbo.BillTypes", t => t.BillTypeId)
                .ForeignKey("dbo.PaymentTypes", t => t.PaymentType_Id)
                .ForeignKey("dbo.PaymentTypes", t => t.PaymentTypeId)
                .ForeignKey("dbo.Periods", t => t.PeriedId)
                .ForeignKey("dbo.BillTypes", t => t.BillType_Id)
                .Index(t => t.PeriedId)
                .Index(t => t.AppUserId)
                .Index(t => t.PaymentTypeId)
                .Index(t => t.BillTypeId)
                .Index(t => t.PaymentType_Id)
                .Index(t => t.BillType_Id);
            
            CreateTable(
                "dbo.InvoiceItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(maxLength: 500),
                        Quantity = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        InvoiceId = c.Int(nullable: false),
                        RepositoryId = c.Int(),
                        Repository_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Invoices", t => t.InvoiceId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .ForeignKey("dbo.Repositories", t => t.Repository_Id)
                .ForeignKey("dbo.Repositories", t => t.RepositoryId)
                .Index(t => t.ProductId)
                .Index(t => t.InvoiceId)
                .Index(t => t.RepositoryId)
                .Index(t => t.Repository_Id);
            
            CreateTable(
                "dbo.Repositories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(nullable: false),
                        RepositoryTypeId = c.Int(),
                        PriceEstimateId = c.Int(nullable: false),
                        RepositoryType_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RepositoryTypes", t => t.RepositoryType_Id)
                .ForeignKey("dbo.RepositoryTypes", t => t.RepositoryTypeId)
                .Index(t => t.RepositoryTypeId)
                .Index(t => t.RepositoryType_Id);
            
            CreateTable(
                "dbo.EntrySlipItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        Description = c.String(maxLength: 500),
                        EntrySlipId = c.Int(nullable: false),
                        RpsId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EntrySlips", t => t.EntrySlipId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .ForeignKey("dbo.Repositories", t => t.RpsId)
                .Index(t => t.EntrySlipId)
                .Index(t => t.RpsId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.RepositoryTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PaymentTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Periods",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        StartDate = c.String(),
                        EndDate = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedDate = c.String(nullable: false),
                        UpdatedDate = c.String(nullable: false),
                        UnitId = c.Int(),
                        PeriodId = c.Int(),
                        Unit_Id = c.Int(),
                        Period_Id = c.Int(),
                        ApplicationUser_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Periods", t => t.PeriodId)
                .ForeignKey("dbo.Units", t => t.Unit_Id)
                .ForeignKey("dbo.Units", t => t.UnitId)
                .ForeignKey("dbo.Periods", t => t.Period_Id)
                .ForeignKey("dbo.Users", t => t.ApplicationUser_Id)
                .Index(t => t.UnitId)
                .Index(t => t.PeriodId)
                .Index(t => t.Unit_Id)
                .Index(t => t.Period_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        OrderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.Units",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Stocks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedDate = c.String(),
                        Description = c.String(),
                        Editable = c.Boolean(nullable: false),
                        PeriodId = c.Int(nullable: false),
                        SupplierId = c.Int(),
                        EntrySlipTypeId = c.Int(),
                        ApplicationUserId = c.Int(),
                        Period_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.ApplicationUserId)
                .ForeignKey("dbo.EntrySlipTypes", t => t.EntrySlipTypeId)
                .ForeignKey("dbo.Periods", t => t.PeriodId)
                .ForeignKey("dbo.Suppliers", t => t.SupplierId)
                .ForeignKey("dbo.Periods", t => t.Period_Id)
                .Index(t => t.PeriodId)
                .Index(t => t.SupplierId)
                .Index(t => t.EntrySlipTypeId)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.Period_Id);
            
            CreateTable(
                "dbo.StockItemListViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Stock_Id = c.Int(),
                        ProductCategory_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .ForeignKey("dbo.Stocks", t => t.Stock_Id)
                .ForeignKey("dbo.ProductCategories", t => t.ProductCategory_Id)
                .Index(t => t.ProductId)
                .Index(t => t.Stock_Id)
                .Index(t => t.ProductCategory_Id);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Address = c.String(nullable: false),
                        Description = c.String(maxLength: 1000),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Manufacturers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Address = c.String(maxLength: 1000),
                        Description = c.String(),
                        Tel = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductBills",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProrductId = c.Int(nullable: false),
                        BillId = c.Int(nullable: false),
                        Product_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bills", t => t.BillId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .Index(t => t.BillId)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.ProductCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        PackageCount = c.Decimal(precision: 18, scale: 2),
                        MinimumBalance = c.Decimal(precision: 18, scale: 2),
                        Fee = c.Decimal(precision: 18, scale: 2),
                        IsLastLevel = c.Boolean(nullable: false),
                        ParentId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductCategories", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.UserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AddressId = c.Int(),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.AddressId)
                .Index(t => t.AddressId);
            
            CreateTable(
                "dbo.DepositoryCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        Code = c.String(),
                        ParentCategoryId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DepositoryCategories", t => t.ParentCategoryId)
                .Index(t => t.ParentCategoryId);
            
            CreateTable(
                "dbo.OrderSlipTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.Sallers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.DepositoryCategories", "ParentCategoryId", "dbo.DepositoryCategories");
            DropForeignKey("dbo.Customers", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.Users", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.Orders", "ApplicationUser_Id", "dbo.Users");
            DropForeignKey("dbo.UserLogins", "UserId", "dbo.Users");
            DropForeignKey("dbo.EntrySlips", "PeriodId", "dbo.Periods");
            DropForeignKey("dbo.EntrySlips", "ESTypeId", "dbo.EntrySlipTypes");
            DropForeignKey("dbo.BuySlips", "EntrySlipType_Id", "dbo.EntrySlipTypes");
            DropForeignKey("dbo.BuySlips", "SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.BuySlips", "PeriodId", "dbo.Periods");
            DropForeignKey("dbo.BuySlips", "EntrySlipTypeId", "dbo.EntrySlipTypes");
            DropForeignKey("dbo.BuySlipItems", "RepositoryId", "dbo.Repositories");
            DropForeignKey("dbo.BuySlipItems", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "ProductCategoryId", "dbo.ProductCategories");
            DropForeignKey("dbo.StockItemListViewModels", "ProductCategory_Id", "dbo.ProductCategories");
            DropForeignKey("dbo.ProductCategories", "ParentId", "dbo.ProductCategories");
            DropForeignKey("dbo.ProductBills", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.ProductBills", "BillId", "dbo.Bills");
            DropForeignKey("dbo.Products", "ManufacturerId", "dbo.Manufacturers");
            DropForeignKey("dbo.Products", "Manufacturer_Id", "dbo.Manufacturers");
            DropForeignKey("dbo.BillItems", "ProductId", "dbo.Products");
            DropForeignKey("dbo.BillItems", "BillId", "dbo.Bills");
            DropForeignKey("dbo.Bills", "PeriedId", "dbo.Periods");
            DropForeignKey("dbo.Bills", "PaymentTypeId", "dbo.PaymentTypes");
            DropForeignKey("dbo.Bills", "BiilTypeId", "dbo.BillTypes");
            DropForeignKey("dbo.Invoices", "BillType_Id", "dbo.BillTypes");
            DropForeignKey("dbo.Invoices", "PeriedId", "dbo.Periods");
            DropForeignKey("dbo.Stocks", "Period_Id", "dbo.Periods");
            DropForeignKey("dbo.Stocks", "SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.BuySlips", "Supplier_Id", "dbo.Suppliers");
            DropForeignKey("dbo.StockItemListViewModels", "Stock_Id", "dbo.Stocks");
            DropForeignKey("dbo.StockItemListViewModels", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Stocks", "PeriodId", "dbo.Periods");
            DropForeignKey("dbo.Stocks", "EntrySlipTypeId", "dbo.EntrySlipTypes");
            DropForeignKey("dbo.Stocks", "ApplicationUserId", "dbo.Users");
            DropForeignKey("dbo.Orders", "Period_Id", "dbo.Periods");
            DropForeignKey("dbo.Orders", "UnitId", "dbo.Units");
            DropForeignKey("dbo.Orders", "Unit_Id", "dbo.Units");
            DropForeignKey("dbo.Orders", "PeriodId", "dbo.Periods");
            DropForeignKey("dbo.OrderItems", "ProductId", "dbo.Products");
            DropForeignKey("dbo.OrderItems", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Invoices", "PaymentTypeId", "dbo.PaymentTypes");
            DropForeignKey("dbo.Invoices", "PaymentType_Id", "dbo.PaymentTypes");
            DropForeignKey("dbo.InvoiceItems", "RepositoryId", "dbo.Repositories");
            DropForeignKey("dbo.Repositories", "RepositoryTypeId", "dbo.RepositoryTypes");
            DropForeignKey("dbo.Repositories", "RepositoryType_Id", "dbo.RepositoryTypes");
            DropForeignKey("dbo.InvoiceItems", "Repository_Id", "dbo.Repositories");
            DropForeignKey("dbo.EntrySlipItems", "RpsId", "dbo.Repositories");
            DropForeignKey("dbo.EntrySlipItems", "ProductId", "dbo.Products");
            DropForeignKey("dbo.EntrySlipItems", "EntrySlipId", "dbo.EntrySlips");
            DropForeignKey("dbo.InvoiceItems", "ProductId", "dbo.Products");
            DropForeignKey("dbo.InvoiceItems", "InvoiceId", "dbo.Invoices");
            DropForeignKey("dbo.Invoices", "BillTypeId", "dbo.BillTypes");
            DropForeignKey("dbo.Invoices", "AppUserId", "dbo.Users");
            DropForeignKey("dbo.Bills", "BillType_Id", "dbo.BillTypes");
            DropForeignKey("dbo.Bills", "AppUserId", "dbo.Users");
            DropForeignKey("dbo.BuySlipItems", "BuySlipId", "dbo.BuySlips");
            DropForeignKey("dbo.BuySlips", "AppUserId", "dbo.Users");
            DropForeignKey("dbo.EntrySlips", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserClaims", "UserId", "dbo.Users");
            DropIndex("dbo.Roles", "RoleNameIndex");
            DropIndex("dbo.DepositoryCategories", new[] { "ParentCategoryId" });
            DropIndex("dbo.Customers", new[] { "AddressId" });
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.UserLogins", new[] { "UserId" });
            DropIndex("dbo.ProductCategories", new[] { "ParentId" });
            DropIndex("dbo.ProductBills", new[] { "Product_Id" });
            DropIndex("dbo.ProductBills", new[] { "BillId" });
            DropIndex("dbo.StockItemListViewModels", new[] { "ProductCategory_Id" });
            DropIndex("dbo.StockItemListViewModels", new[] { "Stock_Id" });
            DropIndex("dbo.StockItemListViewModels", new[] { "ProductId" });
            DropIndex("dbo.Stocks", new[] { "Period_Id" });
            DropIndex("dbo.Stocks", new[] { "ApplicationUserId" });
            DropIndex("dbo.Stocks", new[] { "EntrySlipTypeId" });
            DropIndex("dbo.Stocks", new[] { "SupplierId" });
            DropIndex("dbo.Stocks", new[] { "PeriodId" });
            DropIndex("dbo.OrderItems", new[] { "OrderId" });
            DropIndex("dbo.OrderItems", new[] { "ProductId" });
            DropIndex("dbo.Orders", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Orders", new[] { "Period_Id" });
            DropIndex("dbo.Orders", new[] { "Unit_Id" });
            DropIndex("dbo.Orders", new[] { "PeriodId" });
            DropIndex("dbo.Orders", new[] { "UnitId" });
            DropIndex("dbo.EntrySlipItems", new[] { "ProductId" });
            DropIndex("dbo.EntrySlipItems", new[] { "RpsId" });
            DropIndex("dbo.EntrySlipItems", new[] { "EntrySlipId" });
            DropIndex("dbo.Repositories", new[] { "RepositoryType_Id" });
            DropIndex("dbo.Repositories", new[] { "RepositoryTypeId" });
            DropIndex("dbo.InvoiceItems", new[] { "Repository_Id" });
            DropIndex("dbo.InvoiceItems", new[] { "RepositoryId" });
            DropIndex("dbo.InvoiceItems", new[] { "InvoiceId" });
            DropIndex("dbo.InvoiceItems", new[] { "ProductId" });
            DropIndex("dbo.Invoices", new[] { "BillType_Id" });
            DropIndex("dbo.Invoices", new[] { "PaymentType_Id" });
            DropIndex("dbo.Invoices", new[] { "BillTypeId" });
            DropIndex("dbo.Invoices", new[] { "PaymentTypeId" });
            DropIndex("dbo.Invoices", new[] { "AppUserId" });
            DropIndex("dbo.Invoices", new[] { "PeriedId" });
            DropIndex("dbo.Bills", new[] { "BillType_Id" });
            DropIndex("dbo.Bills", new[] { "BiilTypeId" });
            DropIndex("dbo.Bills", new[] { "PaymentTypeId" });
            DropIndex("dbo.Bills", new[] { "AppUserId" });
            DropIndex("dbo.Bills", new[] { "PeriedId" });
            DropIndex("dbo.BillItems", new[] { "BillId" });
            DropIndex("dbo.BillItems", new[] { "ProductId" });
            DropIndex("dbo.Products", new[] { "Manufacturer_Id" });
            DropIndex("dbo.Products", new[] { "ManufacturerId" });
            DropIndex("dbo.Products", new[] { "ProductCategoryId" });
            DropIndex("dbo.BuySlipItems", new[] { "BuySlipId" });
            DropIndex("dbo.BuySlipItems", new[] { "RepositoryId" });
            DropIndex("dbo.BuySlipItems", new[] { "ProductId" });
            DropIndex("dbo.BuySlips", new[] { "EntrySlipType_Id" });
            DropIndex("dbo.BuySlips", new[] { "Supplier_Id" });
            DropIndex("dbo.BuySlips", new[] { "EntrySlipTypeId" });
            DropIndex("dbo.BuySlips", new[] { "AppUserId" });
            DropIndex("dbo.BuySlips", new[] { "PeriodId" });
            DropIndex("dbo.BuySlips", new[] { "SupplierId" });
            DropIndex("dbo.EntrySlips", new[] { "PeriodId" });
            DropIndex("dbo.EntrySlips", new[] { "ESTypeId" });
            DropIndex("dbo.EntrySlips", new[] { "UserId" });
            DropIndex("dbo.UserClaims", new[] { "UserId" });
            DropIndex("dbo.Users", "UserNameIndex");
            DropIndex("dbo.Users", new[] { "AddressId" });
            DropTable("dbo.Sallers");
            DropTable("dbo.Roles");
            DropTable("dbo.OrderSlipTypes");
            DropTable("dbo.DepositoryCategories");
            DropTable("dbo.Customers");
            DropTable("dbo.UserRoles");
            DropTable("dbo.UserLogins");
            DropTable("dbo.ProductCategories");
            DropTable("dbo.ProductBills");
            DropTable("dbo.Manufacturers");
            DropTable("dbo.Suppliers");
            DropTable("dbo.StockItemListViewModels");
            DropTable("dbo.Stocks");
            DropTable("dbo.Units");
            DropTable("dbo.OrderItems");
            DropTable("dbo.Orders");
            DropTable("dbo.Periods");
            DropTable("dbo.PaymentTypes");
            DropTable("dbo.RepositoryTypes");
            DropTable("dbo.EntrySlipItems");
            DropTable("dbo.Repositories");
            DropTable("dbo.InvoiceItems");
            DropTable("dbo.Invoices");
            DropTable("dbo.BillTypes");
            DropTable("dbo.Bills");
            DropTable("dbo.BillItems");
            DropTable("dbo.Products");
            DropTable("dbo.BuySlipItems");
            DropTable("dbo.BuySlips");
            DropTable("dbo.EntrySlipTypes");
            DropTable("dbo.EntrySlips");
            DropTable("dbo.UserClaims");
            DropTable("dbo.Users");
            DropTable("dbo.Addresses");
            DropTable("dbo.Abouts");
        }
    }
}

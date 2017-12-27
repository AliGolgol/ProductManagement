using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.AspNet.Identity.EntityFramework;
using Repository.DomainModel.AppUser;
using Repository.DomainModel.Catalog;
using Repository.DomainModel.Entry;
using Repository.DomainModel.Order;
using Repository.DomainModel.Period;
using Repository.DomainModel.Repository;
using Repository.DataLayer.Mapping.Order;
using Repository.DataLayer.Mapping.Common;
using Repository.DomainModel.Common;
using Repository.DataLayer.Mapping.Catalog;
using Repository.DataLayer.Mapping.Entry;
using Repository.DataLayer.Mapping.Repository;

namespace Repository.DataLayer.Context
{
    public class ApplicationDbContext :
     IdentityDbContext<ApplicationUser, CustomRole, int, CustomUserLogin, CustomUserRole,
         CustomUserClaim>,
     IUnitOfWork
    {

        #region Entties
        public DbSet<Bill> Bills { get; set; }
        public DbSet<DomainModel.Common.Customer> Customers { get; set; }
        public DbSet<DepositoryCategory> DepositoryCategories { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBill> ProductBills { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Saller> Sallers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Period> Periods { get; set; }
        public DbSet<DomainModel.Repository.Repository> Repositories { get; set; }
        public DbSet<RepositoryType> RepositoryTypes { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<StockItem> StockItems { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<EntrySlipType> EntrySlipTypes { get; set; }
        public DbSet<OrderSlipType> OrderSlipTypes { get; set; }
        public DbSet<EntrySlip> EntrySlips { get; set; }
        public DbSet<EntrySlipItem> EntrySlipItems { get; set; }
        public DbSet<BuySlip> BuySlips { get; set; }
        public DbSet<BuySlipItem> BuySlipItems { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<BillType> BillTypes { get; set; }
        public DbSet<BillItem> BillItems { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<CustomUserRole> UserRoles { get; set; }
        #endregion


        /// <summary>
        /// It looks for a connection string named connectionString in the web.config file.
        /// </summary>
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
            //this.Database.Log = data => System.Diagnostics.Debug.WriteLine(data);
        }

        /// <summary>
        /// To change the connection string at runtime. See the SmObjectFactory class for more info.
        /// </summary>
        public ApplicationDbContext(string connectionString)
            : base(connectionString)
        {
            //Note: defaultConnectionFactory in the web.config file should be set.
        }

        protected override void OnModelCreating(DbModelBuilder builder)
        {

            base.OnModelCreating(builder);

            #region config
            builder.Configurations.Add(new DepositoryCatConfig());
            builder.Configurations.Add(new ManufacturerConfig());
            builder.Configurations.Add(new ProductCategoryConfig());
            builder.Configurations.Add(new ProductConfig());
            builder.Configurations.Add(new AddressConfig());
            builder.Configurations.Add(new CustomerConfig());
            builder.Configurations.Add(new SupplierConfig());
            builder.Configurations.Add(new EntrySlipConfig());
            builder.Configurations.Add(new EntrySlipItemConfig());
            builder.Configurations.Add(new OrderConfig());
            builder.Configurations.Add(new OrderItemConfig());
            builder.Configurations.Add(new StockConfig());
            builder.Configurations.Add(new StockItemConfig());
            builder.Configurations.Add(new RepositoryConfig());
            builder.Configurations.Add(new RepositoryTypeConfig());
            builder.Configurations.Add(new BuySlipConfig());
            builder.Configurations.Add(new BuySlipItemConfig());
            builder.Configurations.Add(new InvoiceConfig());
            builder.Configurations.Add(new InvoiceItemConfig());
            builder.Configurations.Add(new PaymentTypeConfig());
            builder.Configurations.Add(new BillConfig());
            builder.Configurations.Add(new BillTypeConfig());
            builder.Configurations.Add(new BillItemConfig());
            builder.Configurations.Add(new AboutConfig());
            builder.Entity<Stock>();
            builder.Entity<DomainModel.Repository.Repository>();
            builder.Entity<DepositoryCategory>();
            #endregion

            builder.Entity<ApplicationUser>().ToTable("Users");
            builder.Entity<CustomRole>().ToTable("Roles");
            builder.Entity<CustomUserClaim>().ToTable("UserClaims");
            builder.Entity<CustomUserRole>().ToTable("UserRoles");
            builder.Entity<CustomUserLogin>().ToTable("UserLogins");
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public int SaveAllChanges()
        {
            return base.SaveChanges();
        }

        public IEnumerable<TEntity> AddThisRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            return ((DbSet<TEntity>)this.Set<TEntity>()).AddRange(entities);
        }

        public void MarkAsChanged<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Modified;
        }

        public IList<T> GetRows<T>(string sql, params object[] parameters) where T : class
        {
            return Database.SqlQuery<T>(sql, parameters).ToList();
        }

        public void ForceDatabaseInitialize()
        {
            this.Database.Initialize(true);
        }


    }

}

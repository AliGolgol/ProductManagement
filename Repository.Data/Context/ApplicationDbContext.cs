using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.AspNet.Identity.EntityFramework;
using Repository.DomainModel.AppUser;
using Repository.DomainModel.Catalog;
using Repository.DomainModel.Common;
using Repository.DomainModel.Order;
using Repository.DomainModel.Period;
using Repository.DomainModel.Repository;

namespace Repository.Data.Context
{
    public class ApplicationDbContext :
     IdentityDbContext<ApplicationUser, CustomRole, int, CustomUserLogin, CustomUserRole, 
         CustomUserClaim>,
     IUnitOfWork
    {

        public DbSet<Bill> Bills { get; set; }
        public DbSet<DomainModel.Common.Customer> Customers { get; set; }
        public DbSet<DepositoryCategory> DepositoryCategories { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBill> ProductBills { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<SaleSlip> SaleSlips { get; set; }
        public DbSet<Saller> Sallers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Period> Periods { get; set; }
        public DbSet<DomainModel.Repository.Repository> Repositories { get; set; }
        public DbSet<RepositoryType> RepositoryTypes { get; set; }

                 
        /// <summary>
        /// It looks for a connection string named connectionString1 in the web.config file.
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

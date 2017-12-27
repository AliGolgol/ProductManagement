using System.Data.Entity.Migrations;
using Repository.DataLayer.Context;
using Microsoft.AspNet.Identity;
using System.Linq;
using Repository.DomainModel.AppUser;
using System;

namespace Repository.DataLayer.Migrations
{
    public class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            var passwordHash = new PasswordHasher();
            var password = passwordHash.HashPassword("Aa1234@");
            if (!context.Users.Any(u => u.UserName == "fasl"))
            {
                context.Users.AddOrUpdate(x => x.Id,
                        new ApplicationUser()
                        { UserName = "fasl", PasswordHash = password ,SecurityStamp=Guid.NewGuid().ToString()});
            }
            //var manager=new UserManager<ApplicationUser>();
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                context.Roles.AddOrUpdate(x => x.Id,
                   new CustomRole("Admin")
                );
            }
            //context.UserRoles.Add(new CustomUserRole { RoleId = 1, UserId = 10 });

            base.Seed(context);
        }
    }
}

using Microsoft.AspNet.Identity.EntityFramework;

namespace Repository.DomainModel.AppUser
{
    public class CustomRole : IdentityRole<int, CustomUserRole>
    {
        public CustomRole() { }

        public CustomRole(string name)
        {
            Name = name;
        }
        
    }
}

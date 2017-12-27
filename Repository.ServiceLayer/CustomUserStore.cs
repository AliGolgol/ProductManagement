using Microsoft.AspNet.Identity;
using Repository.DomainModel.AppUser;
using Repository.ServiceLayer.Contracts;

namespace Repository.ServiceLayer
{
    public class CustomUserStore : ICustomUserStore
    {
        private readonly IUserStore<ApplicationUser, int> _userStore;

        public CustomUserStore(IUserStore<ApplicationUser, int> userStore)
        {
            _userStore = userStore;
        }


    }
}

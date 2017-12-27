using Microsoft.AspNet.Identity;
using Repository.DomainModel.AppUser;
using Repository.ServiceLayer.Contracts;

namespace Repository.ServiceLayer
{
    public class CustomRoleStore : ICustomRoleStore
    {
        private readonly IRoleStore<CustomRole, int> _roleStore;

        public CustomRoleStore(IRoleStore<CustomRole, int> roleStore)
        {
            _roleStore = roleStore;
        }
    }
}

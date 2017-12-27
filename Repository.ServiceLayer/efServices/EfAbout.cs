using Repository.DataLayer.Context;
using Repository.DomainModel.Common;
using Repository.ServiceLayer.Contracts;

namespace Repository.ServiceLayer.EfServices
{
    public class EfAbout:EfGenericService<About>,IAboutService
    {
        public EfAbout(IUnitOfWork uow):base(uow)
        {

        }
    }
}

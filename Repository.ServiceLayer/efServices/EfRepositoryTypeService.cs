using Repository.DataLayer.Context;
using Repository.DomainModel.Repository;
using Repository.ServiceLayer.Contracts;

namespace Repository.ServiceLayer.EfServices
{
    public class EfRepositoryTypeService:EfGenericService<RepositoryType>,IRepositoryTypeService
    {
        public EfRepositoryTypeService(IUnitOfWork uow):base(uow)
        { }
    }
}

using Repository.DataLayer.Context;
using Repository.ServiceLayer.Contracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ServiceLayer.EfServices
{
    public class EfGenericService<TEntity> : IGenericService<TEntity>
        where TEntity:class
    {

        IUnitOfWork _uow;
        IDbSet<TEntity> _tEntity;

        public EfGenericService(IUnitOfWork uow)
        {
            _uow = uow;
            _tEntity = _uow.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            _tEntity.Add(entity);
        }

        public void Delete(TEntity entity)
        {
            _tEntity.Remove(entity);
        }

        public void Dispose()
        {
            /*throw new NotImplementedException()*/;
        }

        public TEntity Find(Func<TEntity, bool> predicate)
        {
            return _tEntity.Where(predicate).FirstOrDefault();
        }

        public TEntity Get(int id)
        {
            return _tEntity.Find(id);
        }

        public IList<TEntity> GetAll()
        {
            return _tEntity.ToList();
        }

        public IList<TEntity> GetAll(Func<TEntity, bool> predicate)
        {
            return _tEntity.Where(predicate).ToList();
        }

        public void Update(TEntity entity)
        {
            _tEntity.Attach(entity);
            _uow.MarkAsChanged(entity);
            _uow.SaveAllChanges();
        }
    }
}

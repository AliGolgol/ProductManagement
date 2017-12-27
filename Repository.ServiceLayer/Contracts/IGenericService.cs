using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ServiceLayer.Contracts
{
    public interface IGenericService<T>:IDisposable where T:class
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        T Find(Func<T, bool> predicate);
        IList<T> GetAll();
        IList<T> GetAll(Func<T, bool> predicate);
        T Get(int id);
    }
}

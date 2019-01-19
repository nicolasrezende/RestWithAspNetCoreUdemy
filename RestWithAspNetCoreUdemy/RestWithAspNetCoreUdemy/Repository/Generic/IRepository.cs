using RestWithAspNetCoreUdemy.Models.Base;
using System.Collections.Generic;

namespace RestWithAspNetCoreUdemy.Repository.Generic
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Create(T entity);
        T Update(T entity);
        void Delete(long id);
        T FindById(long id);
        List<T> FindAll();
    }
}

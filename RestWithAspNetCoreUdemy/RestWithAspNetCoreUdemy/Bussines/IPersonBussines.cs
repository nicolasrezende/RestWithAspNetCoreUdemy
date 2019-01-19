using RestWithAspNetCoreUdemy.Data.VO;
using System.Collections.Generic;

namespace RestWithAspNetCoreUdemy.Bussines
{
    public interface IPersonBussines
    {
        PersonVO Create(PersonVO person);
        PersonVO Update(PersonVO person);
        void Delete(long id);
        PersonVO FindById(long id);
        List<PersonVO> FindAll();
    }
}

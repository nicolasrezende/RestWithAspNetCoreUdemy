using RestWithAspNetCoreUdemy.Data.VO;
using RestWithAspNetCoreUdemy.DTO;
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
        List<PersonVO> FindByName(string firstName, string lastName);
        PagedSearchDTO<PersonVO> FindWithPagedSearch(string name, string sorDirection, int pageSize, int page);
    }
}

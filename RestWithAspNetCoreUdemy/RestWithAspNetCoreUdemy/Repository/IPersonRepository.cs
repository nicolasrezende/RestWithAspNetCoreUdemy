using RestWithAspNetCoreUdemy.Models;
using RestWithAspNetCoreUdemy.Repository.Generic;
using System.Collections.Generic;

namespace RestWithAspNetCoreUdemy.Repository
{
    public interface IPersonRepository : IRepository<Person>
    {
        List<Person> FindByName(string firstName, string lastName);
    }
}

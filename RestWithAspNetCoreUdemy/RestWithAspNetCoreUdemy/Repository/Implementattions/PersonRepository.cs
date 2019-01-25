using RestWithAspNetCoreUdemy.Models;
using RestWithAspNetCoreUdemy.Models.Context;
using RestWithAspNetCoreUdemy.Repository.Generic;
using System.Collections.Generic;
using System.Linq;

namespace RestWithAspNetCoreUdemy.Repository.Implementattions
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(MysqlContext mysqlContext) : base(mysqlContext) { }

        public List<Person> FindByName(string firstName, string lastName)
        {
            if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
                return _context.Persons.Where(p => p.FirstName.Contains(firstName) && p.LastName.Contains(lastName)).ToList();
            else if (!string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(lastName))
                return _context.Persons.Where(p => p.FirstName.Contains(firstName)).ToList();
            else if (string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
                return _context.Persons.Where(p => p.LastName.Contains(lastName)).ToList();
            else
                return _context.Persons.ToList();
        }
    }
}

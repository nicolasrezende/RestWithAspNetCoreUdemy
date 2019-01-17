using System.Collections.Generic;
using RestWithAspNetCoreUdemy.Models;
using RestWithAspNetCoreUdemy.Repository;

namespace RestWithAspNetCoreUdemy.Bussines.Implementattions
{
    public class PersonBussinesImp : IPersonBussines
    {
        private readonly IPersonRepository _repository;

        public PersonBussinesImp(IPersonRepository repository)
        {
            _repository = repository;
        }

        public Person Create(Person person)
        {
            return _repository.Create(person);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public List<Person> FindAll()
        {
            return _repository.FindAll();
        }

        public Person FindById(long id)
        {
            return _repository.FindById(id);
        }

        public Person Update(Person person)
        {
            return _repository.Update(person);
        }
    }
}

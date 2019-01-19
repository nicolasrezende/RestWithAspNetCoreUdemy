using System.Collections.Generic;
using RestWithAspNetCoreUdemy.Data.Converters;
using RestWithAspNetCoreUdemy.Data.VO;
using RestWithAspNetCoreUdemy.Models;
using RestWithAspNetCoreUdemy.Repository.Generic;

namespace RestWithAspNetCoreUdemy.Bussines.Implementattions
{
    public class PersonBussinesImp : IPersonBussines
    {
        private readonly IRepository<Person> _repository;
        private readonly PersonConverter _personConverter; 

        public PersonBussinesImp(IRepository<Person> repository)
        {
            _repository = repository;
            _personConverter = new PersonConverter();
        }

        public PersonVO Create(PersonVO person)
        {
            var personEntity = _personConverter.Parse(person);
            personEntity = _repository.Create(personEntity);
            return _personConverter.Parse(personEntity);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public List<PersonVO> FindAll()
        {
            return _personConverter.ParseList(_repository.FindAll());
        }

        public PersonVO FindById(long id)
        {
            return _personConverter.Parse(_repository.FindById(id));
        }

        public PersonVO Update(PersonVO person)
        {
            var personEntity = _personConverter.Parse(person);
            personEntity = _repository.Update(personEntity);
            return _personConverter.Parse(personEntity);
        }
    }
}

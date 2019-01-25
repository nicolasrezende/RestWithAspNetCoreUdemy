using System.Collections.Generic;
using RestWithAspNetCoreUdemy.Data.Converters;
using RestWithAspNetCoreUdemy.Data.VO;
using RestWithAspNetCoreUdemy.DTO;
using RestWithAspNetCoreUdemy.Repository;

namespace RestWithAspNetCoreUdemy.Bussines.Implementattions
{
    public class PersonBussinesImp : IPersonBussines
    {
        private readonly IPersonRepository _repository;
        private readonly PersonConverter _personConverter; 

        public PersonBussinesImp(IPersonRepository repository)
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

        public void Delete(long id) => _repository.Delete(id);

        public List<PersonVO> FindAll() => _personConverter.ParseList(_repository.FindAll());

        public PersonVO FindById(long id) => _personConverter.Parse(_repository.FindById(id));

        public List<PersonVO> FindByName(string firstName, string lastName) => _personConverter.ParseList(_repository.FindByName(firstName, lastName));

        public PagedSearchDTO<PersonVO> FindWithPagedSearch(string name, string sorDirection, int pageSize, int page)
        {
            page = page > 0 ? page - 1 : 0;

            string whereName = "";
            if (!string.IsNullOrEmpty(name)) whereName = $" AND p.firstName LIKE '%{name}%'";

            var query = "SELECT * FROM Persons p WHERE 1 = 1";
            query += whereName;
            query += $" ORDER BY p.firstName {sorDirection} LIMIT {pageSize} OFFSET {page}";

            var countQuery = $"SELECT COUNT(*) FROM Persons p WHERE 1 = 1";
            countQuery += whereName;

            var personsVO = _personConverter.ParseList(_repository.FindWithPagedSearch(query));
            var countPersons = _repository.GetCount(countQuery);

            return new PagedSearchDTO<PersonVO>
            {
                SortDirection = sorDirection,
                PageSize = pageSize,
                CurrentPage = page + 1,
                TotalResults = countPersons,
                List = personsVO
            };
        }

        public PersonVO Update(PersonVO person)
        {
            var personEntity = _personConverter.Parse(person);
            personEntity = _repository.Update(personEntity);
            return _personConverter.Parse(personEntity);
        }
    }
}

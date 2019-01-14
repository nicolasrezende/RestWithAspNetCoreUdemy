using RestWithAspNetCoreUdemy.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace RestWithAspNetCoreUdemy.Services.Implementattions
{
    public class PersonServiceImp : IPersonService
    {
        private volatile int _count;
        private List<Person> _persons;

        public PersonServiceImp()
        {
            _persons = new List<Person>();

            for (int i = 0; i < 8; i++)
            {
                _persons.Add(MockPerson(i));
            }
        }

        private Person MockPerson(int i)
        {
            return new Person
            {
                Id = IncrementAndGet(),
                FirstName = "First Name " + i,
                LastName = "Last Name " + i,
                Address = "Address " + i,
                Gender = "Male"
            };
        }

        private long IncrementAndGet()
        {
            return Interlocked.Increment(ref _count);
        }

        public Person Create(Person person)
        {
            _persons.Add(person);
            return person;
        }

        public Person Update(Person person)
        {
            Delete(person.Id);
            Create(person);
            return person;
        }

        public void Delete(long id)
        {
            var person = FindById(id);
            _persons.Remove(person);
        }

        public Person FindById(long id)
        {
            return _persons.Where(p => p.Id == id).FirstOrDefault();
        }

        public List<Person> FindAll()
        {
            return _persons;
        }
    }
}

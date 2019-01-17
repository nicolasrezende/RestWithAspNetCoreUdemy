using RestWithAspNetCoreUdemy.Models;
using RestWithAspNetCoreUdemy.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestWithAspNetCoreUdemy.Repository.Implementattions
{
    public class PersonRepositoryImp : IPersonRepository
    { 
        private readonly MysqlContext _context;

        public PersonRepositoryImp(MysqlContext context)
        {
            _context = context;
        }

        public Person Create(Person person)
        {
            try
            {
                _context.Persons.Add(person);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return person;
        }

        public Person Update(Person person)
        {
            var result = FindById(person.Id);
            try
            {
                if (result != null)
                {
                    _context.Entry(result).CurrentValues.SetValues(person);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public void Delete(long id)
        {
            var result = FindById(id);
            try
            {
                if (result != null)
                {
                    _context.Persons.Remove(result);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Person FindById(long id)
        {
            return _context.Persons.Find(id);
        }

        public List<Person> FindAll()
        {
            return _context.Persons.ToList();
        }
    }
}

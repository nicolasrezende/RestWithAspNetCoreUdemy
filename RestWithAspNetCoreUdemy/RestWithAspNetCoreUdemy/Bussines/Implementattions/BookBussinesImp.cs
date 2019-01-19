using System.Collections.Generic;
using RestWithAspNetCoreUdemy.Models;
using RestWithAspNetCoreUdemy.Repository.Generic;

namespace RestWithAspNetCoreUdemy.Bussines.Implementattions
{
    public class BookBussinesImp : IBookBussines
    {
        private readonly IRepository<Book> _repository;

        public BookBussinesImp(IRepository<Book> repository)
        {
            _repository = repository;
        }

        public Book Create(Book book) => _repository.Create(book);

        public void Delete(long id) => _repository.Delete(id);

        public List<Book> FindAll() => _repository.FindAll();

        public Book FindById(long id) => _repository.FindById(id);

        public Book Update(Book book) => _repository.Update(book);
    }
}

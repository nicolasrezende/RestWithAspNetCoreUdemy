using System.Collections.Generic;
using RestWithAspNetCoreUdemy.Data.Converters;
using RestWithAspNetCoreUdemy.Data.VO;
using RestWithAspNetCoreUdemy.Models;
using RestWithAspNetCoreUdemy.Repository.Generic;

namespace RestWithAspNetCoreUdemy.Bussines.Implementattions
{
    public class BookBussinesImp : IBookBussines
    {
        private readonly IRepository<Book> _repository;
        private readonly BookConverter _bookConverter;

        public BookBussinesImp(IRepository<Book> repository)
        {
            _repository = repository;
            _bookConverter = new BookConverter();
        }

        public BookVO Create(BookVO book)
        {
            var bookEntity = _bookConverter.Parse(book);
            bookEntity = _repository.Create(bookEntity);
            return _bookConverter.Parse(bookEntity);
        }

        public void Delete(long id) => _repository.Delete(id);

        public List<BookVO> FindAll() => _bookConverter.ParseList(_repository.FindAll());

        public BookVO FindById(long id) => _bookConverter.Parse(_repository.FindById(id));

        public BookVO Update(BookVO book)
        {
            var bookEntity = _bookConverter.Parse(book);
            bookEntity = _repository.Update(bookEntity);
            return _bookConverter.Parse(bookEntity);
        }
    }
}

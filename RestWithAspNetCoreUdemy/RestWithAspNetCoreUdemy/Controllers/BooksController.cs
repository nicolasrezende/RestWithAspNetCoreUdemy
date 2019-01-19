using Microsoft.AspNetCore.Mvc;
using RestWithAspNetCoreUdemy.Bussines;
using RestWithAspNetCoreUdemy.Models;

namespace RestWithAspNetCoreUdemy.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookBussines _booksBussines;

        public BooksController(IBookBussines booksBussines)
        {
            _booksBussines = booksBussines;
        }

        // GET api/books
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(_booksBussines.FindAll());
        }

        // GET api/books/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            Book book = _booksBussines.FindById(id);
            if (book == null) return NotFound();
            return Ok(book);
        }

        // POST api/books
        [HttpPost]
        public ActionResult Post([FromBody] Book book)
        {
            if (book == null) BadRequest();
            return Ok(_booksBussines.Create(book));
        }

        // PUT api/books/5
        [HttpPut()]
        public ActionResult Put([FromBody] Book book)
        {
            if (book == null) BadRequest();
            var bookUpdate = _booksBussines.Update(book);
            if (bookUpdate == null) BadRequest();
            return Ok(bookUpdate);
        }

        // DELETE api/books/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _booksBussines.Delete(id);
            return NoContent();
        }
    }
}
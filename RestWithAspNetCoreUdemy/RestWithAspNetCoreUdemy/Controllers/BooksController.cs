using Microsoft.AspNetCore.Mvc;
using RestWithAspNetCoreUdemy.Bussines;
using RestWithAspNetCoreUdemy.Data.VO;
using System.Collections.Generic;

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
        [ProducesResponseType(200, Type = typeof(List<BookVO>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public ActionResult Get()
        {
            return Ok(_booksBussines.FindAll());
        }

        // GET api/books/5
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(BookVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public ActionResult Get(int id)
        {
            BookVO book = _booksBussines.FindById(id);
            if (book == null) return NotFound();
            return Ok(book);
        }

        // POST api/books
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public ActionResult Post([FromBody] BookVO book)
        {
            if (book == null) return BadRequest();
            return Ok(_booksBussines.Create(book));
        }

        // PUT api/books/5
        [HttpPut()]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public ActionResult Put([FromBody] BookVO book)
        {
            if (book == null) return BadRequest();
            var bookUpdate = _booksBussines.Update(book);
            if (bookUpdate == null) return BadRequest();
            return Ok(bookUpdate);
        }

        // DELETE api/books/5
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public ActionResult Delete(int id)
        {
            _booksBussines.Delete(id);
            return NoContent();
        }
    }
}
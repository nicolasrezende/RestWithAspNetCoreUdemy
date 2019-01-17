using Microsoft.AspNetCore.Mvc;
using RestWithAspNetCoreUdemy.Bussines;
using RestWithAspNetCoreUdemy.Models;

namespace RestWithAspNetCoreUdemy.Repository
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonBussines _personBussines;

        public PersonsController(IPersonBussines personBussines)
        {
            _personBussines = personBussines;
        }

        // GET api/persons
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(_personBussines.FindAll());
        }

        // GET api/persons/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            Person person = _personBussines.FindById(id);
            if (person == null) return NotFound();
            return Ok(person);
        }

        // POST api/persons
        [HttpPost]
        public ActionResult Post([FromBody] Person person)
        {
            if (person == null) BadRequest();
            return Ok(_personBussines.Create(person));
        }

        // PUT api/persons/5
        [HttpPut()]
        public ActionResult Put([FromBody] Person person)
        {
            if (person == null) BadRequest();
            return Ok(_personBussines.Update(person));
        }

        // DELETE api/persons/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _personBussines.Delete(id);
            return NoContent();
        }
    }
}

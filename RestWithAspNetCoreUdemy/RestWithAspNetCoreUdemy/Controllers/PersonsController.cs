using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestWithAspNetCoreUdemy.Bussines;
using RestWithAspNetCoreUdemy.Data.VO;
using RestWithAspNetCoreUdemy.DTO;
using RestWithAspNetCoreUdemy.HyperMedia;
using System.Collections.Generic;

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
        [HttpGet(Name = "GetAll")]
        [Authorize("Bearer")]
        [ProducesResponseType(200, Type = typeof(List<PersonVO>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public ActionResult GetAll()
        {
            List<PersonVO> persons = _personBussines.FindAll();
            return Ok(PersonLink.CreateLinksPersonVO(persons, this.Url));
        }

        // GET api/persons/5
        [HttpGet("{id}", Name = "GetPerson")]
        [Authorize("Bearer")]
        [ProducesResponseType(200, Type = typeof(PersonVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]       
        public ActionResult GetById(int id)
        {
            PersonVO person = _personBussines.FindById(id);
            if (person == null) return NotFound();
            return Ok(PersonLink.CreateLinksPersonVO(person, this.Url));
        }

        // GET api/persons/find-by-name?firstName=teste&lastName=teste
        [HttpGet("find-by-name", Name = "GetByName")]
        [Authorize("Bearer")]
        [ProducesResponseType(200, Type = typeof(PersonVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public ActionResult FindByName([FromQuery] string firstName, [FromQuery] string lastName)
        {
            return Ok(_personBussines.FindByName(firstName, lastName));
        }

        // GET api/persons/find-with-paged-search/asc/10/1?name=Teste
        [HttpGet("find-with-paged-search/{sortDirection}/{pageSize}/{page}", Name = "GetWithPagedSearch")]
        [Authorize("Bearer")]
        [ProducesResponseType(200, Type = typeof(PagedSearchDTO<PersonVO>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public ActionResult FindWithPagedSearch([FromQuery] string name, string sortDirection, int pageSize, int page)
        {
            return Ok(_personBussines.FindWithPagedSearch(name, sortDirection, pageSize, page));
        }

        // POST api/persons
        [HttpPost(Name = "CreatePerson")]
        [Authorize("Bearer")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public ActionResult Create([FromBody] PersonVO person)
        {
            if (person == null) BadRequest();
            return Ok(PersonLink.CreateLinksPersonVO(_personBussines.Create(person), this.Url));
        }

        // PUT api/persons/5
        [HttpPut("{id}", Name = "UpdatePerson")]
        [Authorize("Bearer")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public ActionResult Update(int id, [FromBody] PersonVO person)
        {
            if (person == null) BadRequest();
            var personUpdate = _personBussines.Update(person);
            if (personUpdate == null) BadRequest();
            return Ok(PersonLink.CreateLinksPersonVO(personUpdate, this.Url));
        }

        [HttpPatch("{id}", Name = "PartialUpdatePerson")]
        [Authorize("Bearer")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public ActionResult UpdatePartial(int id, [FromBody] PersonVO person)
        {
            if (person == null) BadRequest();
            var personUpdate = _personBussines.Update(person);
            if (personUpdate == null) BadRequest();
            return Ok(personUpdate);
        }

        // DELETE api/persons/5
        [HttpDelete("{id}", Name = "DeletePerson")]
        [Authorize("Bearer")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public ActionResult Delete(int id)
        {
            _personBussines.Delete(id);
            return NoContent();
        }
    }
}

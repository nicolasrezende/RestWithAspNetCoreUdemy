using Microsoft.AspNetCore.Mvc;
using RestWithAspNetCoreUdemy.Data.VO;
using System.Collections.Generic;
using System.Linq;

namespace RestWithAspNetCoreUdemy.HyperMedia
{
    public static class PersonLink
    {
        public static List<PersonVO> CreateLinksPersonVO(List<PersonVO> persons, IUrlHelper urlHelper)
        {
            persons.Select(person => CreateLinksPersonVO(person, urlHelper)).ToList();
            return persons;
        }

        public static PersonVO CreateLinksPersonVO(PersonVO personVO, IUrlHelper urlHelper)
        {
            personVO.Links.Add(new LinkVO
            {
                Rel = "self",
                Href = urlHelper.Link("GetPerson", new { id = personVO.Id }),
                Method = "GET"
            });
            personVO.Links.Add(new LinkVO
            {
                Rel = "update_person",
                Href = urlHelper.Link("UpdatePerson", new { id = personVO.Id }),
                Method = "PUT"
            });
            personVO.Links.Add(new LinkVO
            {
                Rel = "delete_person",
                Href = urlHelper.Link("DeletePerson", new { id = personVO.Id }),
                Method = "DELETE"
            });
            personVO.Links.Add(new LinkVO
            {
                Rel = "partial_update_person",
                Href = urlHelper.Link("PartialUpdatePerson", new { id = personVO.Id }),
                Method = "PATCH"
            });
            personVO.Links.Add(new LinkVO
            {
                Rel = "get_person_by_name",
                Href = urlHelper.Link("GetByName", new { firstName = personVO.FirstName, lastName = personVO.LastName }),
                Method = "GET"
            });
            return personVO;
        }
    }
}

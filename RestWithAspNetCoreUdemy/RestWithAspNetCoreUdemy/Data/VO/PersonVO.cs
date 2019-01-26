using System.Collections.Generic;

namespace RestWithAspNetCoreUdemy.Data.VO
{
    public class PersonVO
    {
        public PersonVO()
        {
            Links = new List<LinkVO>();
        }

        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public List<LinkVO> Links { get; set; }
    }
}

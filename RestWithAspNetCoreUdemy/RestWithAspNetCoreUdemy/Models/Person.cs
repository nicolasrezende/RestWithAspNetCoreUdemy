using RestWithAspNetCoreUdemy.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestWithAspNetCoreUdemy.Models
{
    [Table("persons")]
    public class Person : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
    }
}

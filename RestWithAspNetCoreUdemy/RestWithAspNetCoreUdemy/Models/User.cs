using System.ComponentModel.DataAnnotations.Schema;

namespace RestWithAspNetCoreUdemy.Models
{
    [Table("users")]
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string AccessKey { get; set; }
    }
}

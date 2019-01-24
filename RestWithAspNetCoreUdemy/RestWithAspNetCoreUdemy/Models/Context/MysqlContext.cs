using Microsoft.EntityFrameworkCore;

namespace RestWithAspNetCoreUdemy.Models.Context
{
    public class MysqlContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }

        public MysqlContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }
    }
}

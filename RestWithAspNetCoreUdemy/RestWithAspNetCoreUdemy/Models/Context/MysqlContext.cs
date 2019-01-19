using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithAspNetCoreUdemy.Models.Context
{
    public class MysqlContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Book> Books { get; set; }

        public MysqlContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }
    }
}

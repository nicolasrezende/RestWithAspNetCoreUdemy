using System.Linq;
using RestWithAspNetCoreUdemy.Models;
using RestWithAspNetCoreUdemy.Models.Context;

namespace RestWithAspNetCoreUdemy.Repository.Implementattions
{
    public class UserRepositoryImp : IUserRepository
    {
        private readonly MysqlContext _context;

        public UserRepositoryImp(MysqlContext context)
        {
            _context = context;
        }

        public User FindByLogin(string login) => _context.Users.SingleOrDefault(u => u.Login.Equals(login));
    }
}

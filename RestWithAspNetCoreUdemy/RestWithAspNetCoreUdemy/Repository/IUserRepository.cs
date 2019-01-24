using RestWithAspNetCoreUdemy.Models;

namespace RestWithAspNetCoreUdemy.Repository
{
    public interface IUserRepository
    {
        User FindByLogin(string login);
    }
}

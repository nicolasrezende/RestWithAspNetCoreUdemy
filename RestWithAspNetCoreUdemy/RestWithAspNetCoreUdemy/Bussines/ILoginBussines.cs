using RestWithAspNetCoreUdemy.Data.VO;
using RestWithAspNetCoreUdemy.Models;

namespace RestWithAspNetCoreUdemy.Bussines
{
    public interface ILoginBussines
    {
        object FindByLogin(UserVO user);
    }
}

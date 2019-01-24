using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestWithAspNetCoreUdemy.Bussines;
using RestWithAspNetCoreUdemy.Data.VO;

namespace RestWithAspNetCoreUdemy.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginBussines _loginBussines;

        public LoginController(ILoginBussines loginBussines)
        {
            _loginBussines = loginBussines;
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(object))]
        [ProducesResponseType(400)]
        public object Login(UserVO user)
        {
            if (user == null) return BadRequest();
            return _loginBussines.FindByLogin(user);
        }
    }
}
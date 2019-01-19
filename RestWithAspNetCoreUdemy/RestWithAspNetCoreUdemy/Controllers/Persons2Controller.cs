using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RestWithAspNetCoreUdemy.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/persons")]
    [ApiController]
    public class Persons2Controller : ControllerBase
    {
        public IActionResult Get(ApiVersion apiVersion)
        {
            return Ok(apiVersion.ToString());
        }

    }
}
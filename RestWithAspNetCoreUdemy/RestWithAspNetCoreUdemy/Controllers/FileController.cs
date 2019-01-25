using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestWithAspNetCoreUdemy.Bussines;

namespace RestWithAspNetCoreUdemy.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileBussines _fileBussines;

        public FileController(IFileBussines fileBussines)
        {
            _fileBussines = fileBussines;
        }

        // GET api/v1/file
        [HttpGet]
        [Authorize("Bearer")]
        [ProducesResponseType(200, Type = typeof(byte[]))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public IActionResult Get()
        {
            byte[] buffer = _fileBussines.GetPdfFile();
            if (buffer != null)
            {
                HttpContext.Response.ContentType = "application/pdf";
                HttpContext.Response.ContentLength = buffer.Length;
                HttpContext.Response.Body.Write(buffer, 0, buffer.Length);
            }

            return new ContentResult();
        }
    }
}
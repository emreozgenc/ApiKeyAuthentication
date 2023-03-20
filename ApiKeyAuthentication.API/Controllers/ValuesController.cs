using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiKeyAuthentication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet("/authorized")]
        [Authorize(Roles = "blogs.read")]
        public IActionResult GetAuthorized()
        {
            return Ok("Succeeded");
        }

        [HttpGet("/unauthorized")]
        [Authorize(Roles = "blogs.write")]
        public IActionResult GetUnauthorized()
        {
            return Ok("Succeeded");
        }
    }
}

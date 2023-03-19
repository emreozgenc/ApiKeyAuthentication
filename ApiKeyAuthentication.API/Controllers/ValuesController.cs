using ApiKeyAuthentication.API.Authentication.ApiKey;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiKeyAuthentication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        [Authorize(AuthenticationSchemes = $"{ApiKeyAuthenticationOptions.DefaultSchema}", Roles = "blogs.read")]
        public IActionResult Get()
        {
            return Ok("Succeeded");
        }
    }
}

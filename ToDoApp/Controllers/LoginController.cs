using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Utility;

namespace ToDoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly JwtTokenGenerator _jwtToken;

        public LoginController(JwtTokenGenerator jwtToken)
        {
            _jwtToken = jwtToken;
        }
        [HttpPost]
        public IActionResult Login([FromBody] Login request)
        {
            // Replace this with actual user validation logic
            if (request.Username == "string" && request.Password == "string")
            {
                var token = _jwtToken.GenerateToken(request.Username);
                return Ok(new { Token = token });
            }

            return Unauthorized("Invalid username or password.");
        }
    }
}

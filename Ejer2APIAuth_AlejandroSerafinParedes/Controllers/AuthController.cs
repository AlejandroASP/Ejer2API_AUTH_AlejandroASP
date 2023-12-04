using Ejer2API_AlejandroSerafinParedes.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ejer2API_AlejandroSerafinParedes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public AuthController
        (
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration
         )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Auth auth)
        {
            if (!ModelState.IsValid)
            {
                // si no es valido devuelve badrequest del modelstate
                return BadRequest(ModelState);
            }

            // se crea la variable usuario para recoger el correo del usuario

            var user = await _userManager.FindByNameAsync(auth.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, auth.Password))
            {
                return Unauthorized();
            }

            // obtenemos la key de nuestro appsetings
            var key = _configuration["Jwt:Key"];
            // si es vacio o nulo devuelve un error 500
            if (string.IsNullOrEmpty(key))
            {
                return StatusCode(500, "Internal Server Error: JWT secret key not configured.");
            }

            // devolver el metodo createtoken y por parametro el usuario
            var token = await CreateToken(user);
            return Ok(token);
        }
        /*[HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Auth auth)
        {
        }*/
        private async Task<string> CreateToken(IdentityUser user)
        {
            var claims = new List<Claim>
            {
                 //Identificador único del token
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                 //Fecha de emisión del token
                 new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                 //Usuario portador del token
                 new Claim(JwtRegisteredClaimNames.Sub, user.UserName)
        };
            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("Role", userRole));
            }
            var algo = _configuration["Jwt:Issuer"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],_configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(10), // modifiquen el tiempo de duración del token
            signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}

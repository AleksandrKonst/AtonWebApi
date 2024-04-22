using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AtonWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController(IConfiguration config) : ControllerBase
{
    [HttpPost]
    public IActionResult Post([FromBody] LoginDto loginRequest)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var sectoken = new JwtSecurityToken(config["Jwt:Issuer"],
            config["Jwt:Issuer"],
            new Claim[]{
                new Claim(ClaimTypes.Name, "alex"),
                new Claim(ClaimTypes.Role, "admin")
            },
            expires: DateTime.Now.AddMinutes(120),
            signingCredentials: credentials);

        return Ok(new JwtSecurityTokenHandler().WriteToken(sectoken));
    }
    
    public class LoginDto
    {
        public string username { get; set; }
        
        public string password { get; set; }
    }
}
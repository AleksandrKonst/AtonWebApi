using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.MediatR.Commands;
using AtonWebApi.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AtonWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[TypeFilter(typeof(ResponseExceptionFilter))]
public class LoginController(IConfiguration config, IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] LoginDto loginRequest, CancellationToken cancellationToken)
    {
        var command = new LoginUser.Command(loginRequest.Login, loginRequest.Password);
        var user = await mediator.Send(command, cancellationToken);

        var role = "user";
        if (user.Admin)
        {
            role = "admin";
        }
        
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var sectoken = new JwtSecurityToken(config["Jwt:Issuer"],
            config["Jwt:Issuer"],
            new Claim[]{
                new Claim(ClaimTypes.Name, loginRequest.Login),
                new Claim(ClaimTypes.Role, role)
            },
            expires: DateTime.Now.AddMinutes(120),
            signingCredentials: credentials);

        return Ok(new JwtSecurityTokenHandler().WriteToken(sectoken));
    }

    public record LoginDto(string Login, string Password);
}
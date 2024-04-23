using System.Dynamic;
using System.Security.Claims;
using Application.DTO;
using Application.MediatR.Commands;
using Application.MediatR.Queries;
using AtonWebApi.Filters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AtonWebApi.Controllers;

// <summary>
/// Контролер пользователей
/// </summary>
[Authorize]
[ApiController]
[TypeFilter(typeof(ResponseExceptionFilter))]
[Route("v{version:apiVersion}/[controller]")]
public class UsersController(IMediator mediator) : ControllerBase
{
    [HttpPost("create")]
    [Authorize(Roles = "admin")]
    [RequestSizeLimit(2 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> CreateUser([FromBody] NewUserDto newUserDto, CancellationToken cancellationToken)
    {
        dynamic response = new ExpandoObject();
    
        var command = new CreateUser.Command(newUserDto, User.Claims.First(x => x.Type == ClaimTypes.Name).Value);
        var result = await mediator.Send(command, cancellationToken);
            
        if (result.Result)
        {
            response.service_data = new
            {
                response_datetime = DateTime.Now,
                mesaage = "Пользователь создан"
            };
            return Ok(response);
        }
        throw new Exception("Ошибка добавления пользователя");
    }
    
    [HttpPut("update/data")]
    [RequestSizeLimit(2 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> UpdateUser([FromBody] NewUserDataDto newUserDataDto, CancellationToken cancellationToken)
    {
        dynamic response = new ExpandoObject();
    
        var command = new UpdateUserData.Command(newUserDataDto, User.Claims.First(x => x.Type == ClaimTypes.Name).Value);
        var result = await mediator.Send(command, cancellationToken);
            
        if (result.Result)
        {
            response.service_data = new
            {
                response_datetime = DateTime.Now,
                mesaage = "Пользователь изменен"
            };
            return Ok(response);
        }
        throw new Exception("Ошибка изменения пользователя");
    }
    
    [HttpPut("update/password")]
    [RequestSizeLimit(2 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> UpdateUserPassword([FromBody] ChangePasswordDto changePasswordDto, CancellationToken cancellationToken)
    {
        dynamic response = new ExpandoObject();
    
        var command = new ChangePasswordUser.Command(changePasswordDto, User.Claims.First(x => x.Type == ClaimTypes.Name).Value);
        var result = await mediator.Send(command, cancellationToken);
            
        if (result.Result)
        {
            response.service_data = new
            {
                response_datetime = DateTime.Now,
                mesaage = "Пользователь изменен"
            };
            return Ok(response);
        }
        throw new Exception("Ошибка изменения пользователя");
    }
    
    [HttpPut("update/login")]
    [RequestSizeLimit(2 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> UpdateUserLogin(string login, string newLogin, CancellationToken cancellationToken)
    {
        dynamic response = new ExpandoObject();
    
        var command = new ChangeLoginUser.Command(login, newLogin, User.Claims.First(x => x.Type == ClaimTypes.Name).Value);
        var result = await mediator.Send(command, cancellationToken);
            
        if (result.Result)
        {
            response.service_data = new
            {
                response_datetime = DateTime.Now,
                mesaage = "Пользователь изменен"
            };
            return Ok(response);
        }
        throw new Exception("Ошибка изменения пользователя");
    }
    
    [HttpGet("get/active")]
    [Authorize(Roles = "admin")]
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> GetActiveUser(CancellationToken cancellationToken)
    {
        dynamic response = new ExpandoObject();
    
        var query = new GetAllRevokedOnUsers.Query();
        var result = await mediator.Send(query, cancellationToken);
        
        response.service_data = new
        {
            response_datetime = DateTime.Now,
            mesaage = "список активных пользователей"
        };
        response.result = result.Result;
        return Ok(response);
    }
    
    [HttpGet("get/{login}")]
    [Authorize(Roles = "admin")]
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> GetUserByLogin(string login, CancellationToken cancellationToken)
    {
        dynamic response = new ExpandoObject();

        var query = new GetUserByLogin.Query(login);
        var result = await mediator.Send(query, cancellationToken);
        
        response.service_data = new
        {
            response_datetime = DateTime.Now,
            mesaage = $"пользователь {login}"
        };
        response.result = result.Result;
        return Ok(response);
    }
    
    [HttpGet("get/self/{login}/{password}")]
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> GetSelfUser(string login, string password, CancellationToken cancellationToken)
    {
        dynamic response = new ExpandoObject();
    
        var query = new GetUserByLoginAndPassword.Query(login, password, User.Claims.First(x => x.Type == ClaimTypes.Name).Value);
        var result = await mediator.Send(query, cancellationToken);
        
        response.service_data = new
        {
            response_datetime = DateTime.Now,
            mesaage = "персональные данные"
        };
        response.result = result.Result;
        return Ok(response);
    }
    
    [HttpGet("get/age/{age}")]
    [Authorize(Roles = "admin")]
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> GetUserByAge(int age, CancellationToken cancellationToken)
    {
        dynamic response = new ExpandoObject();
    
        var query = new GetUserByAge.Query(age);
        var result = await mediator.Send(query, cancellationToken);
        
        response.service_data = new
        {
            response_datetime = DateTime.Now,
            mesaage = $"список пользователей старше {age}"
        };
        response.result = result.Result;
        return Ok(response);
    }
    
    [HttpDelete("delete")]
    [Authorize(Roles = "admin")]
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> DeleteUser(string login, bool soft, CancellationToken cancellationToken)
    {
        dynamic response = new ExpandoObject();
    
        var command = new DeleteUser.Command(login, User.Claims.First(x => x.Type == ClaimTypes.Name).Value, soft);
        var result = await mediator.Send(command, cancellationToken);
            
        if (result.Result)
        {
            response.service_data = new
            {
                response_datetime = DateTime.Now,
                mesaage = "Пользователь удален"
            };
            return Ok(response);
        }
        throw new Exception("Ошибка удаления пользователя");
    }
    
    [HttpPost("restore/{login}")]
    [Authorize(Roles = "admin")]
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> RestoreUser(string login, CancellationToken cancellationToken)
    {
        dynamic response = new ExpandoObject();
    
        var command = new RestoreUser.Command(login, User.Claims.First(x => x.Type == ClaimTypes.Name).Value);
        var result = await mediator.Send(command, cancellationToken);
            
        if (result.Result)
        {
            response.service_data = new
            {
                response_datetime = DateTime.Now,
                mesaage = "Пользователь востановлен"
            };
            return Ok(response);
        }
        throw new Exception("Ошибка востановления пользователя");
    }
}
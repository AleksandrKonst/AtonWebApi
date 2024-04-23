using Application.AutoMapperProfiles;
using Application.DTO;
using Application.MediatR.Commands;
using Application.MediatR.Queries;
using AutoMapper;
using Domain.Entity;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using UnitTests.Mock;
using Xunit;

namespace UnitTests;

/// <summary>
/// Пользовательские тесты
/// </summary>
public class UserTest
{
    [Fact]
    public async void ChangeLoginUser()
    {
        var mockRepo = new Mock<IUserRepository>();
        mockRepo.Setup(repo => repo.GetAsync("aleksandr"))
            .ReturnsAsync(UserMocks.GetAdminUser());
        mockRepo.Setup(repo => repo.UpdateAsync(It.IsAny<User>()))
            .ReturnsAsync(true);

        var mockLogger = new Mock<ILogger<ChangeLoginUser.Handler>>();
        var handler = new ChangeLoginUser.Handler(mockRepo.Object, mockLogger.Object);
        
        var queryResult = await handler.Handle(new ChangeLoginUser.Command("aleksandr", "tom", "aleksandr"), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = true;
        
        Assert.Equal(JsonConvert.SerializeObject(trueResult), JsonConvert.SerializeObject(result));
    }
    
    [Fact]
    public async void ChangePasswordUser()
    {
        var mockRepo = new Mock<IUserRepository>();
        mockRepo.Setup(repo => repo.GetAsync("aleksandr"))
            .ReturnsAsync(UserMocks.GetAdminUser());
        mockRepo.Setup(repo => repo.UpdateAsync(It.IsAny<User>()))
            .ReturnsAsync(true);

        var mockLogger = new Mock<ILogger<ChangePasswordUser.Handler>>();
        var handler = new ChangePasswordUser.Handler(mockRepo.Object, mockLogger.Object);
        
        var queryResult = await handler.Handle(new ChangePasswordUser.Command(new ChangePasswordDto()
        {
            Login = "aleksandr",
            Password = "12345678"
        }, "aleksandr"), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = true;
        
        Assert.Equal(JsonConvert.SerializeObject(trueResult), JsonConvert.SerializeObject(result));
    }
    
    [Fact]
    public async void CreateUser()
    {
        var mockRepo = new Mock<IUserRepository>();
        mockRepo.Setup(repo => repo.AddAsync(It.IsAny<User>()))
            .ReturnsAsync(true);

        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApplicationProfile());
        });
        
        var mockLogger = new Mock<ILogger<CreateUser.Handler>>();
        
        var handler = new CreateUser.Handler(mockRepo.Object, mockMapper.CreateMapper(), mockLogger.Object);
        
        var queryResult = await handler.Handle(new CreateUser.Command(UserMocks.GetCreateUser(), "aleksandr"), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = true;
        
        Assert.Equal(JsonConvert.SerializeObject(trueResult), JsonConvert.SerializeObject(result));
    }
    
    [Fact]
    public async void DeleteUser()
    {
        var mockRepo = new Mock<IUserRepository>();
        mockRepo.Setup(repo => repo.GetAsync("aleksandr"))
            .ReturnsAsync(UserMocks.GetAdminUser());
        mockRepo.Setup(repo => repo.UpdateAsync(It.IsAny<User>()))
            .ReturnsAsync(true);

        var mockLogger = new Mock<ILogger<DeleteUser.Handler>>();
        var handler = new DeleteUser.Handler(mockRepo.Object, mockLogger.Object);
        
        var queryResult = await handler.Handle(new DeleteUser.Command("aleksandr", "tom", true), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = true;
        
        Assert.Equal(JsonConvert.SerializeObject(trueResult), JsonConvert.SerializeObject(result));
    }
    
    [Fact]
    public async void RestoreUser()
    {
        var mockRepo = new Mock<IUserRepository>();
        mockRepo.Setup(repo => repo.GetAsync("aleksandr"))
            .ReturnsAsync(UserMocks.GetAdminUser());
        mockRepo.Setup(repo => repo.UpdateAsync(It.IsAny<User>()))
            .ReturnsAsync(true);

        var mockLogger = new Mock<ILogger<RestoreUser.Handler>>();
        var handler = new RestoreUser.Handler(mockRepo.Object, mockLogger.Object);
        
        var queryResult = await handler.Handle(new RestoreUser.Command("aleksandr", "aleksandr"), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = true;
        
        Assert.Equal(JsonConvert.SerializeObject(trueResult), JsonConvert.SerializeObject(result));
    }
    
    [Fact]
    public async void UpdateUserData()
    {
        var mockRepo = new Mock<IUserRepository>();
        mockRepo.Setup(repo => repo.GetAsync("aleksandr"))
            .ReturnsAsync(UserMocks.GetAdminUser());
        mockRepo.Setup(repo => repo.UpdateAsync(It.IsAny<User>()))
            .ReturnsAsync(true);

        var mockLogger = new Mock<ILogger<RestoreUser.Handler>>();
        var handler = new RestoreUser.Handler(mockRepo.Object, mockLogger.Object);
        
        var queryResult = await handler.Handle(new RestoreUser.Command("aleksandr", "aleksandr"), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = true;
        
        Assert.Equal(JsonConvert.SerializeObject(trueResult), JsonConvert.SerializeObject(result));
    }
    
    [Fact]
    public async void GetAllRevokedOnUsers()
    {
        var mockRepo = new Mock<IUserRepository>();
        mockRepo.Setup(repo => repo.GetUsersByRevokedOnAsync())
            .ReturnsAsync(UserMocks.GetAllUser);

        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApplicationProfile());
        });
        
        var handler = new GetAllRevokedOnUsers.Handler(mockRepo.Object, mockMapper.CreateMapper());
        
        var queryResult = await handler.Handle(new GetAllRevokedOnUsers.Query(), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = UserMocks.GetAllUser();
        
        Assert.Equal(JsonConvert.SerializeObject(trueResult), JsonConvert.SerializeObject(result));
    }
    
    [Fact]
    public async void GetUserByAge()
    {
        var mockRepo = new Mock<IUserRepository>();
        mockRepo.Setup(repo => repo.GetUsersByAgeAsync(18))
            .ReturnsAsync(UserMocks.GetAllUser);

        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApplicationProfile());
        });
        
        var handler = new GetUserByAge.Handler(mockRepo.Object, mockMapper.CreateMapper());
        
        var queryResult = await handler.Handle(new GetUserByAge.Query(18), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = UserMocks.GetAllUser();
        
        Assert.Equal(JsonConvert.SerializeObject(trueResult), JsonConvert.SerializeObject(result));
    }
    
    [Fact]
    public async void GetUserByLogin()
    {
        var mockRepo = new Mock<IUserRepository>();
        mockRepo.Setup(repo => repo.GetAsync("aleksandr"))
            .ReturnsAsync(UserMocks.GetAdminUser());

        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApplicationProfile());
        });
        
        var handler = new GetUserByLogin.Handler(mockRepo.Object, mockMapper.CreateMapper());
        
        var queryResult = await handler.Handle(new GetUserByLogin.Query("aleksandr"), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = new UserByLoginDto()
        {
            Name = "aleksandr",
            Gender = 1,
            Birthday = new DateTime(2001, 7, 8),
            Status = true
        };
        
        Assert.Equal(JsonConvert.SerializeObject(trueResult), JsonConvert.SerializeObject(result));
    }
    
    [Fact]
    public async void GetUserByLoginAndPassword()
    {
        var mockRepo = new Mock<IUserRepository>();
        mockRepo.Setup(repo => repo.CheckUserPasswordAsync("aleksandr", "1243"))
            .ReturnsAsync(UserMocks.GetAdminUser());
        mockRepo.Setup(repo => repo.GetAsync("aleksandr"))
            .ReturnsAsync(UserMocks.GetAdminUser());

        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApplicationProfile());
        });
        
        var handler = new GetUserByLoginAndPassword.Handler(mockRepo.Object, mockMapper.CreateMapper());
        
        var queryResult = await handler.Handle(new GetUserByLoginAndPassword.Query("aleksandr", "1243", "aleksandr"), new CancellationToken());
        var result = queryResult.Result;
        var trueResult = new UserByLoginDto()
        {
            Name = "aleksandr",
            Gender = 1,
            Birthday = new DateTime(2001, 7, 8),
            Status = true
        };
        
        Assert.Equal(JsonConvert.SerializeObject(trueResult), JsonConvert.SerializeObject(result));
    }
}
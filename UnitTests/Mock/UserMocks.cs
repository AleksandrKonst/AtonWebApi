using Application.DTO;
using Domain.Entity;

namespace UnitTests.Mock;

public static class UserMocks
{
    public static User GetAdminUser()
    {
        var result = new User()
        {
            Guid = new Guid("f5566449-6227-4a53-9839-af03ac9554ec"),
            Login = "aleksandr",
            Password = "1243",
            Name = "aleksandr",
            Gender = 1,
            Birthday = new DateTime(2001, 7, 8),
            Admin = true,
            CreatedOn = new DateTime(2023, 7, 8).ToUniversalTime(),
            CreatedBy = "alex",
            ModifiedOn = null,
            ModifiedBy = null,
            RevokedOn = null,
            RevokedBy = null
        };
        return result;
    }
    
    public static NewUserDto GetCreateUser()
    {
        var result = new NewUserDto()
        {
            Login = "aleksandr",
            Password = "1243",
            Name = "aleksandr",
            Gender = 1,
            Birthday = null,
            Admin = true,
        };
        return result;
    }
    
    public static IEnumerable<User> GetAllUser()
    {
        var result = new User()
        {
            Guid = new Guid("f5566449-6227-4a53-9839-af03ac9554ec"),
            Login = "aleksandr",
            Password = "1243",
            Name = "aleksandr",
            Gender = 1,
            Birthday = new DateTime(2001, 7, 8),
            Admin = true,
            CreatedOn = new DateTime(2023, 7, 8).ToUniversalTime(),
            CreatedBy = "alex",
            ModifiedOn = null,
            ModifiedBy = null,
            RevokedOn = null,
            RevokedBy = null
        };
        return new List<User>() {result};
    }
}
using Domain.Entity;

namespace Domain.Interfaces;

public interface IUserRepository  : ICrudRepository<User, string>
{
    
    Task<User?> CheckUserPasswordAsync(string login, string password);
    Task<IEnumerable<User>> GetUsersByRevokedOnAsync();
    Task<IEnumerable<User>> GetUsersByAgeAsync(int age);
}
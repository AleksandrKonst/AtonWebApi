using Domain.Entity;
using Domain.Interfaces;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository(AtonDataContext context) : IUserRepository
{
    public async Task<bool> AddAsync(User obj)
    {
        await context.Users.AddAsync(obj);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateAsync(User obj)
    {
        context.Users.Update(obj);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<User?> GetAsync(string identifier) =>
        await context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Login == identifier);

    public async Task<bool> DeleteAsync(string identifier)
    {
        var user = await context.Users.Where(u => u.Login == identifier).FirstOrDefaultAsync();
        if (user == null) return false;
        
        context.Users.Remove(user);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(User obj)
    {
        context.Users.Remove(obj);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<User?> CheckUserPasswordAsync(string login, string password)
    {
        var result = await context.Users.FirstOrDefaultAsync(u => u.Login == login && u.Password == password);
        return result;
    }

    public async Task<IEnumerable<User>> GetUsersByRevokedOnAsync() =>
        await context.Users.Where(u => u.RevokedOn != null).OrderBy(u => u.CreatedOn).AsNoTracking().ToListAsync();

    public async Task<IEnumerable<User>> GetUsersByAgeAsync(int age) =>
        await context.Users.Where(u => (DateTime.Now.Date - u.Birthday!.Value).TotalDays / 365 >= age).AsNoTracking().ToListAsync();
}
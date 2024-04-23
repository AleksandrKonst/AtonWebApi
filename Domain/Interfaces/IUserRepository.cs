using Domain.Entity;

namespace Domain.Interfaces;

/// <summary>
/// Пользовательский репозиторий
/// </summary>
public interface IUserRepository  : ICrudRepository<User, string>
{
    /// <summary>
    /// Проверка пароля
    /// </summary>
    /// <param name="login">Логин.</param>
    /// <param name="password">Пароль.</param>
    Task<User?> CheckUserPasswordAsync(string login, string password);
    /// <summary>
    /// Получение действующих пользователей
    /// </summary>
    Task<IEnumerable<User>> GetUsersByRevokedOnAsync();
    /// <summary>
    /// Получение пользователей старше age
    /// </summary>
    /// <param name="age">Минимальный возраст.</param>
    Task<IEnumerable<User>> GetUsersByAgeAsync(int age);
}
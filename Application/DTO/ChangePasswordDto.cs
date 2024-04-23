namespace Application.DTO;

/// <summary>
/// DTO для изменения пароля
/// </summary>
public class ChangePasswordDto
{
    /// <summary>
    /// Логин
    /// </summary>
    public string Login { get; set; }
    /// <summary>
    /// пароль
    /// </summary>
    public string Password { get; set; }
}
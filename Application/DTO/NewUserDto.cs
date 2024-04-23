namespace Application.DTO;

/// <summary>
/// DTO для создания нового пользователя
/// </summary>
public class NewUserDto
{
    /// <summary>
    /// Уникальный Логин (запрещены все символы кроме латинских букв и цифр)
    /// </summary>
    public string Login { get; set; }
    /// <summary>
    /// Пароль(запрещены все символы кроме латинских букв и цифр)
    /// </summary>
    public string Password { get; set; }
    /// <summary>
    /// Имя (запрещены все символы кроме латинских и русских букв)
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Пол 0 - женщина, 1 - мужчина, 2 - неизвестно
    /// </summary>
    public int Gender { get; set; }
    /// <summary>
    /// Поле даты рождения может быть Null
    /// </summary>
    public DateTime? Birthday { get; set; }
    /// <summary>
    /// Указание - является ли пользователь админом
    /// </summary>
    public bool Admin { get; set; }
}
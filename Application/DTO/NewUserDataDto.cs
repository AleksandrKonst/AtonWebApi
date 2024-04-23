namespace Application.DTO;

/// <summary>
/// DTO для изменения пользовательских данных
/// </summary>
public class NewUserDataDto
{
    /// <summary>
    /// Уникальный Логин (запрещены все символы кроме латинских букв и цифр)
    /// </summary>
    public string Login { get; set; }
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
}
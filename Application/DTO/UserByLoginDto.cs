namespace Application.DTO;

/// <summary>
/// DTO - сведения о пользователе
/// </summary>
public class UserByLoginDto
{
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
    /// Если пользователь активнен статус равен true
    /// </summary>
    public bool Status { get; set; }
}
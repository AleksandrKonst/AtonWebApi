namespace Domain.Interfaces;

/// <summary>
/// CRUD репозиторий
/// </summary>
public interface ICrudRepository<T, C>
{
    /// <summary>
    /// Добавить
    /// </summary>
    /// <param name="obj">Сущность.</param>
    Task<bool> AddAsync(T obj);
    /// <summary>
    /// Получить
    /// </summary>
    /// <param name="identifier">Идентификатор.</param>
    Task<T?> GetAsync(C identifier);
    /// <summary>
    /// Обновить
    /// </summary>
    /// <param name="obj">Сущность.</param>
    Task<bool> UpdateAsync(T obj);
    /// <summary>
    /// Удалить
    /// </summary>
    /// <param name="obj">Сущность.</param>
    Task<bool> DeleteAsync(T obj);
}
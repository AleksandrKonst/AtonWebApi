namespace Domain.Interfaces;

public interface ICrudRepository<T, C>
{
    Task<bool> AddAsync(T obj);
    Task<T?> GetAsync(C identifier);
    Task<bool> UpdateAsync(T obj);
    Task<bool> DeleteAsync(T obj);
}
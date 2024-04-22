namespace Domain.Interfaces;

public interface ICrudRepository<T, C>
{
    Task<bool> AddAsync(T obj);

    Task<bool> UpdateAsync(T obj);

    Task<T?> GetAsync(C identifier);
    
    Task<bool> DeleteAsync(C identifier);
    
    Task<bool> DeleteAsync(T obj);
}
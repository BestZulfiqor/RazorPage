namespace Infrastructure.Interfaces;

public interface IBaseRepository<T1, T2>
{
    public Task<IQueryable<T1>> GetAllAsync();
    public Task<T1?> GetById(int id);
    public Task<T2> AddAsync(T1 entity);
    public Task<T2> UpdateAsync(T1 entity);
    public Task<T2> DeleteAsync(T1 entity);
}
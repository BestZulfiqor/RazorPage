using Infrastructure.Data;
using Infrastructure.Interfaces;
using Table = Domain.Entities.Table;

namespace Infrastructure.Repositories;

public class TableRepository(DataContext context) : IBaseRepository<Domain.Entities.Table, int>
{
    public Task<IQueryable<Table>> GetAllAsync()
    {
        var tables = context.Tables.AsQueryable();
        return Task.FromResult(tables);
    }

    public async Task<Table?> GetById(int id)
    {
        var table = await context.Tables.FindAsync(id);
        return table;
    }

    public async Task<int> AddAsync(Table entity)
    {
        await context.Tables.AddAsync(entity);
        return await context.SaveChangesAsync();
    }

    public async Task<int> UpdateAsync(Table entity)
    {
        context.Tables.Update(entity);
        return await context.SaveChangesAsync();
    }

    public async Task<int> DeleteAsync(Table entity)
    {
        context.Tables.Remove(entity);
        return await context.SaveChangesAsync();
    }
}
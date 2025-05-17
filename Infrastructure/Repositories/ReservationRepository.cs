using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories;

public class ReservationRepository(DataContext context) : IBaseRepository<Reservation, int>
{
    public Task<IQueryable<Reservation>> GetAllAsync()
    {
        var tables = context.Reservations.AsQueryable();
        return Task.FromResult(tables);
    }

    public async Task<Reservation?> GetById(int id)
    {
        var table = await context.Reservations.FindAsync(id);
        return table;
    }

    public async Task<int> AddAsync(Reservation entity)
    {
        await context.Reservations.AddAsync(entity);
        return await context.SaveChangesAsync();
    }

    public async Task<int> UpdateAsync(Reservation entity)
    {
        context.Reservations.Update(entity);
        return await context.SaveChangesAsync();
    }

    public async Task<int> DeleteAsync(Reservation entity)
    {
        context.Reservations.Remove(entity);
        return await context.SaveChangesAsync();
    }
}
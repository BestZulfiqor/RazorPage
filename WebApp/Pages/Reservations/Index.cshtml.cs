using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.Reservations;

public class Index(DataContext context) : PageModel
{
    [BindProperty]
    public List<Reservation> Reservations { get; set; } = [];

    public async Task OnGetAsync(int? searchTableNumber, DateTime? searchDate)
    {
        var query = context.Reservations
            .Include(r => r.Table)
            .Include(r => r.Customer).AsQueryable();

        if (searchTableNumber.HasValue)
        {
            query = query.Where(r => r.Table.Number == searchTableNumber.Value)
                .Include(r => r.Table)
                .Include(r => r.Customer);
        }

        if (searchDate.HasValue)
        {
            query = query.Where(r => r.ReservationDate.Date == searchDate.Value.Date)
                .Include(r => r.Table)
                .Include(r => r.Customer);
        }

        Reservations = await query.ToListAsync();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var reservation = await context.Reservations.FindAsync(id);

        if (reservation == null) return RedirectToPage();

        var table = await context.Tables.FindAsync(reservation.TableId);
        if (table != null)
        {
            table.IsReserved = false;
        }
        context.Reservations.Remove(reservation);
        await context.SaveChangesAsync();

        return RedirectToPage();
    }
}

using Domain.DTOs.Reservations;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.Reservations;

public class Create(DataContext context) : PageModel
{
    [BindProperty]
    public GetReservationDto Reservations { get; set; } = new();

    [BindProperty]
    public List<Table> Tables { get; set; } = [];
    [BindProperty]
    public List<Customer> Customers { get; set; } = [];

    public async Task<IActionResult> OnGetAsync(int tableId){
        Tables = await context.Tables.Where(n => n.Id == tableId && !n.IsReserved).ToListAsync();
        Customers = await context.Customers.ToListAsync();

        Reservations.TableId = tableId;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(){
        var table = await context.Tables.FirstOrDefaultAsync(t => t.Id == Reservations.TableId);

        if (table == null || table.IsReserved)
        {
            ModelState.AddModelError("", "Этот столик уже забронирован.");
            Tables = await context.Tables.Where(t => !t.IsReserved).ToListAsync();
            Customers = await context.Customers.ToListAsync();
            return Page();
        }
        
        var customer = await context.Customers.FirstOrDefaultAsync(t => t.Id == Reservations.CustomerId);
        if (customer == null)
        {
            ModelState.AddModelError("", "Этот столик уже забронирован.");
            Tables = await context.Tables.Where(t => !t.IsReserved).ToListAsync();
            Customers = await context.Customers.ToListAsync();
            return Page();
        }

        var reservation = new Reservation
        {
            CustomerId = Reservations.CustomerId,
            ReservationDate = Reservations.ReservationDate.ToUniversalTime(),
            TableId = Reservations.TableId
        };

        await context.Reservations.AddAsync(reservation);
        table.IsReserved = true;

        await context.SaveChangesAsync();
        return Redirect("/Reservations/Index");
    }
}

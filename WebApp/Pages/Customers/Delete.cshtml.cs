using Domain.DTOs.Customers;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.Customers;

public class Delete(DataContext context) : PageModel
{
    [BindProperty]
    public GetCustomerDto Customer { get; set; } = new();


    public async Task<IActionResult> OnGetAsync(int id)
    {
        var customer = await context.Customers.FindAsync(id);
        if (customer == null)
        {
            return RedirectToPage("./Index");
        }

        Customer.Id = customer.Id;
        Customer.FullName = customer.FullName;
        Customer.Phone = customer.Phone;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        // 1. Найти все резервации этого клиента
        var reservations = await context.Reservations
            .Where(r => r.CustomerId == Customer.Id)
            .ToListAsync();

        // 2. Для каждой — освободить стол и удалить резервацию
        foreach (var res in reservations)
        {
            var table = await context.Tables.FindAsync(res.TableId);
            if (table != null)
                table.IsReserved = false;

            context.Reservations.Remove(res);
        }

        // 3. Удалить самого клиента
        var customer = await context.Customers.FindAsync(Customer.Id);
        if (customer != null)
            context.Customers.Remove(customer);

        // 4. Сохранить всё одним SaveChanges
        await context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}

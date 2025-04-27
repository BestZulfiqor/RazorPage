using Domain.DTOs.Tables;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Tables;

public class Edit(DataContext context) : PageModel
{
    [BindProperty]
    public GetTableDto Table { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var table = await context.Tables.FindAsync(id);
        if (table == null)
        {
            return RedirectToPage("./Index");
        }

        Table.Id = table.Id;
        Table.IsReserved = table.IsReserved;
        Table.Number = table.Number;
        Table.Seats = table.Seats;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var table = await context.Tables.FindAsync(Table.Id);
        if (table == null)
        {
            return RedirectToPage("./Index");
        }

        table.IsReserved = Table.IsReserved;
        table.Number = Table.Number;
        table.Seats = Table.Seats;

        await context.SaveChangesAsync();
        return RedirectToPage("./Index");
    }
}

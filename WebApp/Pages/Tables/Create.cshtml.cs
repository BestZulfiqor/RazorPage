using Domain.DTOs.Tables;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Tables;

public class Create(DataContext context) : PageModel
{
    [BindProperty]
    public GetTableDto Tables { get; set; } = new();

    public IActionResult OnGetAsync()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var table = new Table
        {
            Number = Tables.Number,
            Seats = Tables.Seats,
            IsReserved = false
        };


        await context.Tables.AddAsync(table);
        await context.SaveChangesAsync();

        return RedirectToPage("/Tables/Index");
    }
}

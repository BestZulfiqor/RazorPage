using AutoMapper;
using Domain.DTOs.Tables;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.Tables;

public class Delete(DataContext context, IMapper mapper) : PageModel
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

        Table = mapper.Map<GetTableDto>(table);

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var table = await context.Tables.FindAsync(Table.Id);
        if (table == null)
        {
            return RedirectToPage("./Index");
        }

        context.Tables.Remove(table);
        await context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}

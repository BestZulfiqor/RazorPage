using AutoMapper;
using Domain.DTOs.Customers;
using Domain.DTOs.Tables;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.Tables;

public class Index(DataContext context, IMapper mapper) : PageModel
{
    [BindProperty]
    public List<GetTableDto> Tables { get; set; } = [];

    public async Task<IActionResult> OnGetAsync()
    {
        var tables1 = await context.Tables.ToListAsync();
        Tables = mapper.Map<List<GetTableDto>>(tables1);
        return Page();
    }
}

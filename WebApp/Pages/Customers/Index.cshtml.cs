using AutoMapper;
using Domain.DTOs.Customers;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.Customers;

public class Index(DataContext context, IMapper mapper) : PageModel
{
    [BindProperty]
    public List<GetCustomerDto> Customers { get; set; } = [];

    public async Task<IActionResult> OnGetAsync()
    {
        var customers = await context.Customers.ToListAsync();
        Customers = mapper.Map<List<GetCustomerDto>>(customers);
        return Page();
    }
}

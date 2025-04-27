using AutoMapper;
using Domain.DTOs.Customers;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Customers;

public class Create(DataContext context) : PageModel
{
    [BindProperty]
    public GetCustomerDto Customer { get; set; } = new();

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(){
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var customer = new Customer(){
            FullName = Customer.FullName,
            Phone = Customer.Phone
        };

        await context.Customers.AddAsync(customer);
        await context.SaveChangesAsync();

        return RedirectToPage("/Customers/Index");
    }
}

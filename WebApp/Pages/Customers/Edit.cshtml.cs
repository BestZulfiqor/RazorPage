using Domain.DTOs.Customers;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Customers;

public class Edit(DataContext context) : PageModel
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
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var customer = await context.Customers.FindAsync(Customer.Id);
        if (customer == null)
        {
            return RedirectToPage("./Index");
        }
        
        customer.FullName = Customer.FullName;
        customer.Phone = Customer.Phone;

        await context.SaveChangesAsync();
        return RedirectToPage("./Index");
    }
}

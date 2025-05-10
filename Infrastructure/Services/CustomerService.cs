using System.Net;
using AutoMapper;
using Domain.DTOs.Customers;
using Domain.Entities;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;
[Authorize]
public class CustomerService(DataContext context, IMapper mapper) : ICustomerService
{
    public async Task<Response<GetCustomerDto>> CreateCustomerAsync(CreateCustomerDto customerDto)
    {
        var customer = mapper.Map<Customer>(customerDto);
        await context.Customers.AddAsync(customer);
        var result = await context.SaveChangesAsync();
        var dto = mapper.Map<GetCustomerDto>(customer);
        return result == 0
            ? new Response<GetCustomerDto>(HttpStatusCode.BadRequest, "Customer not add!")
            : new Response<GetCustomerDto>(dto);
    }

    public async Task<Response<string>> DeleteCustomerAsync(int id)
    {
        var exist = await context.Customers.FindAsync(id);
        if (exist == null)
        {
            return new Response<string>(HttpStatusCode.NotFound, "Customer not found");
        }
        context.Customers.Remove(exist);
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>(HttpStatusCode.BadRequest, "Customer not deleted")
            : new Response<string>("Customer deleted!");
    }

    public async Task<Response<GetCustomerDto>> GetCustomerByIdAsync(int id)
    {
        var exist = await context.Customers.FindAsync(id);
        if (exist == null)
        {
            return new Response<GetCustomerDto>(HttpStatusCode.NotFound, "Customer not found");
        }
        var dto = mapper.Map<GetCustomerDto>(exist);
        return new Response<GetCustomerDto>(dto);
    }

    public async Task<Response<List<GetCustomerDto>>> GetCustomers()
    {
        var customers = await context.Customers.ToListAsync();
        var data = mapper.Map<List<GetCustomerDto>>(customers);
        return new Response<List<GetCustomerDto>>(data);
    }

    public async Task<Response<GetCustomerDto>> UpdateCustomerAsync(int id, UpdateCustomerDto customerDto)
    {
        var exist = await context.Customers.FindAsync(id);
        if (exist == null)
        {
            return new Response<GetCustomerDto>(HttpStatusCode.NotFound, "Customer not found");
        }
        exist.FullName = customerDto.FullName;
        exist.Phone = customerDto.Phone;
        var result = await context.SaveChangesAsync();
        var dto = mapper.Map<GetCustomerDto>(exist);
        return result == 0
            ? new Response<GetCustomerDto>(HttpStatusCode.BadRequest, "Customer not updated")
            : new Response<GetCustomerDto>(dto);
    }

}

using Domain.DTOs.Customers;
using Domain.Responses;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController(ICustomerService service) : ControllerBase
{
    [HttpGet]
    public async Task<Response<List<GetCustomerDto>>> GetAllCustomer() => await service.GetCustomers();

    [HttpGet("{id:int}")]
    public async Task<Response<GetCustomerDto>> GetCustomer(int id) => await service.GetCustomerByIdAsync(id);

    [HttpPost]
    public async Task<Response<GetCustomerDto>> CreateCustomer(CreateCustomerDto customerDto) =>
        await service.CreateCustomerAsync(customerDto);

    [HttpPut("{id:int}")]
    public async Task<Response<GetCustomerDto>> UpdateCustomer(int id, UpdateCustomerDto customerDto) =>
        await service.UpdateCustomerAsync(id, customerDto);

    [HttpDelete("{id:int}")]
    public async Task<Response<string>> DeleteCustomer(int id) =>
        await service.DeleteCustomerAsync(id);
}
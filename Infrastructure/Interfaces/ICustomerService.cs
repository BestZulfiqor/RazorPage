using Domain.DTOs.Customers;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface ICustomerService
{
    Task<Response<List<GetCustomerDto>>> GetCustomers();
    Task<Response<GetCustomerDto>> GetCustomerByIdAsync(int id);
    Task<Response<GetCustomerDto>> CreateCustomerAsync(CreateCustomerDto customerDto);
    Task<Response<GetCustomerDto>> UpdateCustomerAsync(int id, UpdateCustomerDto customerDto);
    Task<Response<string>> DeleteCustomerAsync(int id);
}

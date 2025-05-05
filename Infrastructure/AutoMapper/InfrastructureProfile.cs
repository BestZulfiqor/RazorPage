using AutoMapper;
using Domain.Entities;
using Domain.DTOs.Customers;
using Domain.DTOs.Tables;
using Domain.DTOs.Reservations;
namespace Infrastructure.AutoMapper;
public class InfrastructureProfile : Profile
{
    public InfrastructureProfile()
    {
        CreateMap<Customer, GetCustomerDto>();
        CreateMap<CreateCustomerDto, Customer>();
        CreateMap<UpdateCustomerDto, Customer>();

        CreateMap<Table, GetTableDto>();
        CreateMap<CreateTableDto, Table>();
        CreateMap<UpdateTableDto, Table>();
        CreateMap<GetTableDto, Table>();

        CreateMap<Reservation, GetReservationDto>();
        CreateMap<CreateReservationDto, Reservation>();
        CreateMap<UpdateReservationDto, Reservation>();
    }
}
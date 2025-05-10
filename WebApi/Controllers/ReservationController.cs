using Domain.DTOs.Reservations;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationController(IReservationService service) : ControllerBase
{
    [HttpGet]
    public async Task<Response<List<GetReservationDto>>> GetAllReservation() => await service.GetReservations();

    [HttpGet("{id:int}")]
    public async Task<Response<GetReservationDto>> GetReservation(int id) => await service.GetReservationByIdAsync(id);

    [HttpPost]
    public async Task<Response<GetReservationDto>> CreateReservation(CreateReservationDto reservationDto) =>
        await service.CreateReservationAsync(reservationDto);

    [HttpPut("{id:int}")]
    public async Task<Response<GetReservationDto>> UpdateReservation(int id, UpdateReservationDto reservationDto) =>
        await service.UpdateReservationAsync(id, reservationDto);

    [HttpDelete("{id:int}")]
    public async Task<Response<string>> DeleteReservation(int id) =>
        await service.DeleteReservationAsync(id);
}
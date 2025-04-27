using System.Net;
using AutoMapper;
using Domain.DTOs.Reservations;
using Domain.Entities;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class ReservationService(DataContext context, IMapper mapper) : IReservationService
{
    public async Task<Response<GetReservationDto>> CreateReservationAsync(CreateReservationDto reservationDto)
    {
        var reservation = mapper.Map<Reservation>(reservationDto);
        await context.Reservations.AddAsync(reservation);
        var result = await context.SaveChangesAsync();
        var dto = mapper.Map<GetReservationDto>(reservation);
        return result == 0
            ? new Response<GetReservationDto>(HttpStatusCode.BadRequest, "Reservation not add!")
            : new Response<GetReservationDto>(dto);
    }

    public async Task<Response<string>> DeleteReservationAsync(int id)
    {
        var exist = await context.Reservations.FindAsync(id);
        if (exist == null)
        {
            return new Response<string>(HttpStatusCode.NotFound, "Reservation not found");
        }
        context.Reservations.Remove(exist);
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>(HttpStatusCode.BadRequest, "Reservation not deleted")
            : new Response<string>("Reservation deleted!");
    }

    public async Task<Response<GetReservationDto>> GetReservationByIdAsync(int id)
    {
        var exist = await context.Reservations.FindAsync(id);
        if (exist == null)
        {
            return new Response<GetReservationDto>(HttpStatusCode.NotFound, "Reservation not found");
        }
        var dto = mapper.Map<GetReservationDto>(exist);
        return new Response<GetReservationDto>(dto);
    }

    public async Task<Response<List<GetReservationDto>>> GetReservations()
    {
        var Reservations = await context.Reservations.ToListAsync();
        var data = mapper.Map<List<GetReservationDto>>(Reservations);
        return new Response<List<GetReservationDto>>(data);
    }

    public async Task<Response<GetReservationDto>> UpdateReservationAsync(int id, UpdateReservationDto reservationDto)
    {
        var exist = await context.Reservations.FindAsync(id);
        if (exist == null)
        {
            return new Response<GetReservationDto>(HttpStatusCode.NotFound, "Reservation not found");
        }
        exist.CustomerId = reservationDto.CustomerId;
        exist.ReservationDate = reservationDto.ReservationDate;
        exist.TableId = reservationDto.TableId;
        var result = await context.SaveChangesAsync();
        var dto = mapper.Map<GetReservationDto>(exist);
        return result == 0
            ? new Response<GetReservationDto>(HttpStatusCode.BadRequest, "Reservation not updated")
            : new Response<GetReservationDto>(dto);
    }
}

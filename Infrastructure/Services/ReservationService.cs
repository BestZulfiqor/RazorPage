using System.Net;
using AutoMapper;
using Domain.DTOs.Reservations;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;
[Authorize]
public class ReservationService(IBaseRepository<Reservation, int> repository, DataContext context, IMapper mapper) : IReservationService
{
    public async Task<Response<GetReservationDto>> CreateReservationAsync(CreateReservationDto reservationDto)
    {
        try
        {
            var table = await context.Tables
                .Include(t => t.Reservations)
                .FirstOrDefaultAsync(t => t.Id == reservationDto.TableId);

            if (table == null)
                return new Response<GetReservationDto>(HttpStatusCode.NotFound, "Table not found!");

            if (reservationDto.ReservationDateStart <= reservationDto.ReservationDateEnd)
                return new Response<GetReservationDto>(HttpStatusCode.BadRequest, "Дата начало должен быть выше дата окончания");

            bool any = table.Reservations.Any(r =>
                reservationDto.ReservationDateStart < r.ReservationDateEnd &&
                reservationDto.ReservationDateEnd > r.ReservationDateStart);

            if (any)
                return new Response<GetReservationDto>(HttpStatusCode.Conflict, "This time is already reserved!");

            var reservation = mapper.Map<Reservation>(reservationDto);
            reservation.Table = table;

            var result = await repository.AddAsync(reservation);

            var dto = mapper.Map<GetReservationDto>(reservation);
            return result == 0
                ? new Response<GetReservationDto>(HttpStatusCode.BadRequest, "Reservation not add!")
                : new Response<GetReservationDto>(dto);
        }
        catch (Exception ex)
        {
            return new Response<GetReservationDto>(HttpStatusCode.BadRequest,
                $"Reservation not created! Error: {ex.Message}");
        }
    }


    public async Task<Response<string>> DeleteReservationAsync(int id)
    {
        var exist = await context.Reservations.FindAsync(id);
        if (exist == null)
        {
            return new Response<string>(HttpStatusCode.NotFound, "Reservation not found");
        }

        var result = await repository.DeleteAsync(exist); 
        return result == 0
            ? new Response<string>(HttpStatusCode.BadRequest, "Reservation not deleted")
            : new Response<string>("Reservation deleted!");
    }

    public async Task<Response<GetReservationDto>> GetReservationByIdAsync(int id)
    {
        var exist = await repository.GetById(id);
        if (exist == null)
        {
            return new Response<GetReservationDto>(HttpStatusCode.NotFound, "Reservation not found");
        }
        
        var dto = mapper.Map<GetReservationDto>(exist);
        return new Response<GetReservationDto>(dto);
    }

    public async Task<Response<List<GetReservationDto>>> GetReservations()
    {
        var reservations = await repository.GetAllAsync();
        var data = mapper.Map<List<GetReservationDto>>(reservations);
        return new Response<List<GetReservationDto>>(data);
    }

    public async Task<Response<GetReservationDto>> UpdateReservationAsync(int id, UpdateReservationDto reservationDto)
    {
        try
        {
            var existingReservation = await context.Reservations
                .Include(r => r.Table)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (existingReservation == null)
                return new Response<GetReservationDto>(HttpStatusCode.NotFound, "Reservation not found");

            var table = await context.Tables.FindAsync(reservationDto.TableId);
            if (table == null)
                return new Response<GetReservationDto>(HttpStatusCode.NotFound, "Table not found");

            if (reservationDto.ReservationDateStart <= reservationDto.ReservationDateEnd)
                return new Response<GetReservationDto>(HttpStatusCode.BadRequest, "Change time!");

            bool hasOverlap = await context.Reservations
                .AnyAsync(r => r.TableId == reservationDto.TableId &&
                               r.Id != id &&
                               reservationDto.ReservationDateStart < r.ReservationDateEnd &&
                               reservationDto.ReservationDateEnd > r.ReservationDateStart);

            if (hasOverlap)
                return new Response<GetReservationDto>(HttpStatusCode.Conflict, "This time slot is already booked");

            existingReservation.CustomerId = reservationDto.CustomerId;
            existingReservation.ReservationDateStart = reservationDto.ReservationDateStart;
            existingReservation.ReservationDateEnd = reservationDto.ReservationDateEnd;
            existingReservation.TableId = reservationDto.TableId;
            var result = await repository.UpdateAsync(existingReservation);

            var dto = mapper.Map<GetReservationDto>(existingReservation);
            return result == 0
                ? new Response<GetReservationDto>(HttpStatusCode.BadRequest, "Reservation not update")
                : new Response<GetReservationDto>(dto);
        }
        catch (Exception ex)
        {
            return new Response<GetReservationDto>(HttpStatusCode.InternalServerError,
                $"Error updating reservation. Error: {ex.Message}");
        }
    }
}
using Domain.DTOs.Reservations;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface IReservationService
{
    Task<Response<List<GetReservationDto>>> GetReservations();
    Task<Response<GetReservationDto>> GetReservationByIdAsync(int id);
    Task<Response<GetReservationDto>> CreateReservationAsync(CreateReservationDto reservationDto);
    Task<Response<GetReservationDto>> UpdateReservationAsync(int id, UpdateReservationDto reservationDto);
    Task<Response<string>> DeleteReservationAsync(int id);
}

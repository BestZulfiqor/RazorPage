using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Reservations;

public class CreateReservationDto
{
    public int TableId { get; set; }
    public int CustomerId { get; set; }
    
    [Required]
    public DateTimeOffset ReservationDateEnd { get; set; }
    [Required]
    public DateTimeOffset ReservationDateStart { get; set; }
}

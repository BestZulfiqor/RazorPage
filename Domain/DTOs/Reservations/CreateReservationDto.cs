namespace Domain.DTOs.Reservations;

public class CreateReservationDto
{
    public int TableId { get; set; }
    public int CustomerId { get; set; }
    public DateTimeOffset ReservationDate { get; set; }
}

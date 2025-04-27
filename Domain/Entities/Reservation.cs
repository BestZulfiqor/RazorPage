namespace Domain.Entities;

public class Reservation
{
    public int Id { get; set; }
    public int TableId { get; set; }
    public int CustomerId { get; set; }
    public DateTimeOffset ReservationDate { get; set; }

    public Table Table { get; set; }
    public Customer Customer { get; set; }
}

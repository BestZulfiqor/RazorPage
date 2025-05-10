namespace Domain.Entities;

public class Table
{
    public int Id { get; set; }
    public int Number { get; set; }
    public int Seats { get; set; }
    
    public List<Reservation> Reservations { get; set; }
    
}

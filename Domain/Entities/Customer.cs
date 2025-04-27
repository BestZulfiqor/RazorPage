namespace Domain.Entities;

public class Customer
{
    public int Id { get; set; }
    public string FullName { get; set; } = null!;
    public string Phone { get; set; }
    
    public List<Reservation> Reservations { get; set; } = [];
}

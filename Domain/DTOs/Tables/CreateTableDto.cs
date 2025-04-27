namespace Domain.DTOs.Tables;

public class CreateTableDto
{
    public int Number { get; set; }
    public int Seats { get; set; }
    public bool IsReserved { get; set; }
}

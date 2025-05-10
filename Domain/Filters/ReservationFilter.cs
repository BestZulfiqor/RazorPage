namespace Domain.Filters;

public class ReservationFilter
{
    public DateTimeOffset? ReservationDateEnd { get; set; }
    public DateTimeOffset? ReservationDateStart { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
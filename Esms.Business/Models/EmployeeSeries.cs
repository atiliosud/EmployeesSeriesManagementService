namespace Esms.Business.Models;

public class EmployeeSeries
{
    public int EmployeesExternalId { get; set; }
    public int SeriesCode { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public Employee Employee { get; set; } = null!;
    public Series Series { get; set; } = null!;
}

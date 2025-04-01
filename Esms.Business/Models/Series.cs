namespace Esms.Business.Models;

public class Series
{
    public int Code { get; set; }
    public int ExternalId { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    //public ICollection<EmployeeSeries> EmployeeSeries { get; set; } = new List<EmployeeSeries>();
}

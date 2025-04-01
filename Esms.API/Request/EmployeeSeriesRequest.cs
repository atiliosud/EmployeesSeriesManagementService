using Esms.Business.Models;

namespace Esms.API.Request;

public class EmployeeSeriesRequest
{
    public int ExternalId { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public Series MapModel()
    {

        var newSeries = new Series
        {
            ExternalId = ExternalId,
            Name = Name,
            StartDate = StartDate,
            EndDate = EndDate
        };
        return newSeries;
    }
}

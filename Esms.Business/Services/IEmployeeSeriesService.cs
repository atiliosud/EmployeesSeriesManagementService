using Esms.Business.Models;

namespace Esms.Business.Services;

public interface IEmployeeSeriesService : IServiceBase<EmployeeSeries>
{
    Task<EmployeeSeries> AddSerieAsync(Series newSeries);
    Task<IEnumerable<EmployeeSeries>> GetSeriesByPeriodAsync(int employeeId, DateTime startDate, DateTime endDate);
}
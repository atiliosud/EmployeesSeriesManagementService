using Esms.Business.Models;
using Esms.Business.Repositories;

namespace Esms.Business.Services.V1;

public class EmployeeSeriesService : ServiceBase<EmployeeSeries>, IEmployeeSeriesService
{
    private readonly IRepositoryBase<EmployeeSeries> repository;
    private readonly IRepositoryBase<Series> repositorySeries;

    public EmployeeSeriesService(IRepositoryBase<EmployeeSeries> repository, IRepositoryBase<Series> repositorySeries) : base(repository)
    {
        this.repository = repository;
        this.repositorySeries = repositorySeries;
    }

    public async Task<EmployeeSeries> AddSerieAsync(Series newSeries)
    {
        await repositorySeries.AddAsync(newSeries);
        var employeeSeries = new EmployeeSeries()
        {
            EmployeesExternalId = newSeries.ExternalId,
            SeriesCode = newSeries.Code,
            StartDate = newSeries.StartDate,
            EndDate = newSeries.EndDate,
            Series = newSeries
        };
        await repository.AddAsync(employeeSeries);

        return employeeSeries;
    }

    public async Task<IEnumerable<EmployeeSeries>> GetSeriesByPeriodAsync(int employeeId, DateTime startDate, DateTime endDate)
    {
        return await repository
            .FindAsync(es => es.EmployeesExternalId == employeeId && es.StartDate >= startDate && es.EndDate <= endDate,
                "Series"
            );
    }

}

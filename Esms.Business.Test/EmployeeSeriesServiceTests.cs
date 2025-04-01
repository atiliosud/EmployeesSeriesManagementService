using Esms.Business.Models;
using Esms.Business.Repositories;
using Esms.Business.Services.V1;
using Moq;

namespace Esms.Business.Test;

public class EmployeeSeriesServiceTests
{
    private readonly Mock<IRepositoryBase<EmployeeSeries>> _employeeSeriesRepositoryMock;
    private readonly Mock<IRepositoryBase<Series>> _seriesRepositoryMock;
    private readonly EmployeeSeriesService _employeeSeriesService;

    public EmployeeSeriesServiceTests()
    {
        _employeeSeriesRepositoryMock = new Mock<IRepositoryBase<EmployeeSeries>>();
        _seriesRepositoryMock = new Mock<IRepositoryBase<Series>>();
        _employeeSeriesService = new EmployeeSeriesService(_employeeSeriesRepositoryMock.Object, _seriesRepositoryMock.Object);
    }

    [Fact]
    public async Task AddSerieAsync_ShouldAddSerieAndEmployeeSeries()
    {
        // Arrange
        var newSeries = new Series
        {
            ExternalId = 1,
            Code = 123,
            StartDate = DateTime.Now.AddDays(-10),
            EndDate = DateTime.Now.AddDays(10)
        };

        _seriesRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Series>())).Returns(Task.CompletedTask);
        _employeeSeriesRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<EmployeeSeries>())).Returns(Task.CompletedTask);

        // Act
        var result = await _employeeSeriesService.AddSerieAsync(newSeries);

        // Assert
        _seriesRepositoryMock.Verify(repo => repo.AddAsync(newSeries), Times.Once);
        _employeeSeriesRepositoryMock.Verify(repo => repo.AddAsync(It.Is<EmployeeSeries>(es =>
            es.EmployeesExternalId == newSeries.ExternalId &&
            es.SeriesCode == newSeries.Code &&
            es.StartDate == newSeries.StartDate &&
            es.EndDate == newSeries.EndDate &&
            es.Series == newSeries
        )), Times.Once);

        Assert.Equal(newSeries.ExternalId, result.EmployeesExternalId);
        Assert.Equal(newSeries.Code, result.SeriesCode);
        Assert.Equal(newSeries.StartDate, result.StartDate);
        Assert.Equal(newSeries.EndDate, result.EndDate);
        Assert.Equal(newSeries, result.Series);
    }

    [Fact]
    public async Task GetSeriesByPeriodAsync_ShouldReturnSeriesInPeriod()
    {
        // Arrange
        int employeeId = 1;
        DateTime startDate = DateTime.Now.AddDays(-5);
        DateTime endDate = DateTime.Now.AddDays(5);

        var expectedSeries = new List<EmployeeSeries>
        {
            new EmployeeSeries
            {
                EmployeesExternalId = employeeId,
                SeriesCode = 123,
                StartDate = DateTime.Now.AddDays(-2),
                EndDate = DateTime.Now.AddDays(2),
                Series = new Series { Code = 123 }
            },
            new EmployeeSeries
            {
                EmployeesExternalId = employeeId,
                SeriesCode = 456,
                StartDate = DateTime.Now.AddDays(-1),
                EndDate = DateTime.Now.AddDays(4),
                Series = new Series { Code = 456 }
            }
        };

        _employeeSeriesRepositoryMock.Setup(repo => repo.FindAsync(
            It.IsAny<System.Linq.Expressions.Expression<Func<EmployeeSeries, bool>>>(),
            It.IsAny<string>()
        )).ReturnsAsync(expectedSeries);

        // Act
        var result = await _employeeSeriesService.GetSeriesByPeriodAsync(employeeId, startDate, endDate);

        // Assert
        _employeeSeriesRepositoryMock.Verify(repo => repo.FindAsync(
            It.Is<System.Linq.Expressions.Expression<Func<EmployeeSeries, bool>>>(
                e => e.Compile().Invoke(new EmployeeSeries { EmployeesExternalId = employeeId, StartDate = startDate, EndDate = endDate })
            ),
            "Series"
        ), Times.Once);

        Assert.Equal(expectedSeries.Count, result.Count());
        Assert.Equal(expectedSeries[0].SeriesCode, result.ElementAt(0).SeriesCode);
        Assert.Equal(expectedSeries[1].SeriesCode, result.ElementAt(1).SeriesCode);
    }

    [Fact]
    public async Task GetSeriesByPeriodAsync_ShouldReturnEmptyList_WhenNoSeriesFound()
    {
        // Arrange
        int employeeId = 1;
        DateTime startDate = DateTime.Now.AddDays(-5);
        DateTime endDate = DateTime.Now.AddDays(5);

        _employeeSeriesRepositoryMock.Setup(repo => repo.FindAsync(
            It.IsAny<System.Linq.Expressions.Expression<Func<EmployeeSeries, bool>>>(),
            It.IsAny<string>()
        )).ReturnsAsync(new List<EmployeeSeries>());

        // Act
        var result = await _employeeSeriesService.GetSeriesByPeriodAsync(employeeId, startDate, endDate);

        // Assert
        Assert.Empty(result);
    }
}

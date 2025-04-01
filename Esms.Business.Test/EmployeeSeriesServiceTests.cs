using System.Linq.Expressions;
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

        Expression<Func<EmployeeSeries, bool>>? capturedFilter = null;
        string? capturedInclude1 = null;
        string? capturedInclude2 = null;

        _employeeSeriesRepositoryMock.Setup(repo => repo.FindAsync(
            It.IsAny<Expression<Func<EmployeeSeries, bool>>>(),
            It.IsAny<string>(),
            It.IsAny<string>()
        )).Callback<Expression<Func<EmployeeSeries, bool>>, string, string>((f, inc1, inc2) =>
        {
            capturedFilter = f;
            capturedInclude1 = inc1;
            capturedInclude2 = inc2;
        }).ReturnsAsync(expectedSeries);

        // Act
        var result = await _employeeSeriesService.GetSeriesByPeriodAsync(employeeId, startDate, endDate);

        // Assert
        Assert.NotNull(capturedFilter);
        Assert.Equal("Series", capturedInclude1);
        Assert.Empty(capturedInclude2);

        var predicate = capturedFilter!.Compile();
        var matches = predicate(new EmployeeSeries
        {
            EmployeesExternalId = employeeId,
            StartDate = startDate,
            EndDate = endDate
        });

        Assert.True(matches);
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
            null, null
        )).ReturnsAsync(new List<EmployeeSeries>());

        // Act
        var result = await _employeeSeriesService.GetSeriesByPeriodAsync(employeeId, startDate, endDate);

        // Assert
        Assert.Null(result);
    }
}

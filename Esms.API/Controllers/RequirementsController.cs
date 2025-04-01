using Esms.API.Request;
using Esms.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Esms.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RequirementsController : ControllerBase
    {
        private readonly IEmployeesAddressesService _addressService;
        private readonly IEmployeeSeriesService _employeeSeriesService;
        private readonly ILogger<RequirementsController> _logger;

        public RequirementsController(
            IEmployeesAddressesService addressService,
            IEmployeeSeriesService employeeSeriesService,
            ILogger<RequirementsController> logger)
        {
            _addressService = addressService;
            _employeeSeriesService = employeeSeriesService;
            _logger = logger;
        }

        /// <summary>
        /// Get addresses of a specific employee.
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpGet("{employeeId}/addresses")]
        public async Task<IActionResult> GetEmployeeAddresses(int employeeId)
        {
            _logger.LogInformation("Fetching addresses for EmployeeId: {EmployeeId}", employeeId);

            var addresses = await _addressService.GetAddressesByEmployeeIdAsync(employeeId);

            if (!addresses.Any())
            {
                _logger.LogWarning("No addresses found for EmployeeId: {EmployeeId}", employeeId);
                return NotFound("No addresses found.");
            }

            return Ok(addresses);
        }

        /// <summary>
        /// Get personal address of all employees where work a city.
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpGet("addresses/workcity/{city}")]
        public async Task<IActionResult> GetEmployeesInBrussels(string city)
        {
            _logger.LogInformation("Fetching addresses of employees working in {city}.", city);

            var addresses = await _addressService.GetEmployeesInCityAsync(city);

            if (!addresses.Any())
            {
                _logger.LogWarning("No employees found in {city}.", city);
                return NotFound($"No employees found in {city}.");
            }

            return Ok(addresses);
        }

        /// <summary>
        /// Get Series of an employee for a specific period.
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpGet("{employeeId}/series")]
        public async Task<IActionResult> GetEmployeeSeries(int employeeId, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            _logger.LogInformation("Fetching series for EmployeeId: {EmployeeId} from {StartDate} to {EndDate}", employeeId, startDate, endDate);

            var series = await _employeeSeriesService.GetSeriesByPeriodAsync(employeeId, startDate, endDate);

            if (!series.Any())
            {
                _logger.LogWarning("No series found for EmployeeId: {EmployeeId} in period {StartDate} - {EndDate}", employeeId, startDate, endDate);
                return NotFound("No series found for this period.");
            }

            return Ok(series);
        }

        /// <summary>
        /// Save a new series of an employee.
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPost("{employeeId}/series")]
        public async Task<IActionResult> SaveEmployeeSeries(int employeeId, [FromBody] EmployeeSeriesRequest request)
        {
            if (employeeId != request.ExternalId)
            {
                _logger.LogWarning("EmployeeId in route ({EmployeeId}) does not match request body ({RequestEmployeeId}).", employeeId, request.ExternalId);
                return BadRequest("Employee ID mismatch.");
            }

            _logger.LogInformation("Saving new series for EmployeeId: {EmployeeId}", employeeId);

            var newSeries = request.MapModel();

            var createdSeries = await _employeeSeriesService.AddSerieAsync(newSeries);

            _logger.LogInformation("New series created successfully for EmployeeId: {EmployeeId}", employeeId);
            return CreatedAtAction(nameof(GetEmployeeSeries), new { employeeId, startDate = request.StartDate, endDate = request.EndDate }, createdSeries);
        }
    }
}

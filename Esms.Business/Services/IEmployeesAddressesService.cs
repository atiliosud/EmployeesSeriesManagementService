using Esms.Business.Models;

namespace Esms.Business.Services;

public interface IEmployeesAddressesService : IServiceBase<EmployeesAddresses>
{
    Task<IEnumerable<Address>> GetAddressesByEmployeeIdAsync(int employeeId);
    Task<IEnumerable<Address>> GetEmployeesInCityAsync(string city);
    Task<IEnumerable<String>> GetAllCitiesAsync();
    Task<IEnumerable<EmployeesAddresses>> GetEmployeeAddressesByCityAsync(string city);

    
}

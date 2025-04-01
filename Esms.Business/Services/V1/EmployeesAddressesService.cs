using Esms.Business.Models;
using Esms.Business.Repositories;

namespace Esms.Business.Services.V1;

public class EmployeesAddressesService : ServiceBase<EmployeesAddresses>, IEmployeesAddressesService
{
    private readonly IRepositoryBase<EmployeesAddresses> repository;

    public EmployeesAddressesService(IRepositoryBase<EmployeesAddresses> repository) : base(repository)
    {
        this.repository = repository;
    }

    public async Task<IEnumerable<Address>> GetAddressesByEmployeeIdAsync(int employeeId)
    {
        var addresses = await repository
            .FindAsync(ea => ea.EmployeesExternalId == employeeId, "Address.AddressType");
        return addresses.Select(ea => ea.Address)
            .ToList();
    }


    public async Task<IEnumerable<Address>> GetEmployeesInCityAsync(string city)
    {
        var addresses = await repository
            .FindAsync(ea => ea.Address.City == city, "Address.AddressType");
        return addresses.Select(ea => ea.Address)
            .ToList();
    }

    public async Task<IEnumerable<string>> GetAllCitiesAsync()
    {
        var addresses = await repository
            .FindAsync(ea => true, "Address.AddressType");
        return addresses.Select(ea => ea.Address.City)
            .ToList();
    }

    public async Task<IEnumerable<EmployeesAddresses>> GetEmployeeAddressesByCityAsync(string city)
    {
        var addresses = await repository
            .FindAsync(ea => ea.Address.City == city, "Address.AddressType", "Employee");
        return addresses.ToList();
    }
}
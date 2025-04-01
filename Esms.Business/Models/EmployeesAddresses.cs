namespace Esms.Business.Models;

public class EmployeesAddresses
{
    public int EmployeesExternalId { get; set; }
    public int AddressesId { get; set; }

    public Employee Employee { get; set; } = null!;
    public Address Address { get; set; } = null!;
}

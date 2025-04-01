namespace Esms.Business.Models;

public class Employee
{
    public int ExternalId { get; set; }
    public string UserId { get; set; } = string.Empty;
    public int Number { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? SecondName { get; set; }
    public string Language { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public string BirthPlace { get; set; } = string.Empty;
    public byte[]? ProfileImage { get; set; }
    public string Nationality { get; set; } = string.Empty;
    public DateTime? ExitDate { get; set; }
    public string EmailAddress { get; set; } = string.Empty;
    public int OrganizationalUnit { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public ICollection<EmployeeSeries> EmployeeSeries { get; set; } = new List<EmployeeSeries>();

    public string FullName()
    {
        if (string.IsNullOrEmpty(SecondName))
        {
            return $"{FirstName} {LastName}";
        }
        return $"{FirstName} {SecondName} {LastName}";
    }
}

namespace Esms.Business.Models;

public class Address
{
    public int Id { get; set; }
    public int AddressTypeId { get; set; }
    public string Country { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public string MailboxNumber { get; set; } = string.Empty;
    public string Building { get; set; } = string.Empty;
    public string Floor { get; set; } = string.Empty;

    public AddressType AddressType { get; set; } = null!;
}

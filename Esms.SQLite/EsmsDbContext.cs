using Esms.Business.Models;
using Microsoft.EntityFrameworkCore;

namespace Esms.SQLite;

public class EsmsDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<AddressType> AddressTypes { get; set; }
    public DbSet<EmployeesAddresses> EmployeesAddresses { get; set; }
    public DbSet<Series> Series { get; set; }
    public DbSet<EmployeeSeries> EmployeeSeries { get; set; }

    public EsmsDbContext(DbContextOptions<EsmsDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.ExternalId);
            entity.Property(e => e.UserId).HasMaxLength(10).IsRequired();
            entity.Property(e => e.FirstName).HasMaxLength(50).IsRequired();
            entity.Property(e => e.LastName).HasMaxLength(50).IsRequired();
            entity.Property(e => e.SecondName);
            entity.Property(e => e.Language).HasMaxLength(2).IsRequired();
            entity.Property(e => e.BirthPlace).HasMaxLength(50).IsRequired();
            entity.Property(e => e.Nationality).HasMaxLength(3).IsRequired();
            entity.Property(e => e.EmailAddress).HasMaxLength(250).IsRequired();
            entity.Property(e => e.PhoneNumber).HasMaxLength(30).IsRequired();
        });

        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(a => a.Id);
            entity.Property(a => a.Country).HasMaxLength(50).IsRequired();
            entity.Property(a => a.City).HasMaxLength(50).IsRequired();
            entity.Property(a => a.ZipCode).HasMaxLength(5).IsRequired();
            entity.Property(a => a.Street).HasMaxLength(60).IsRequired();
            entity.Property(a => a.Number).HasMaxLength(10).IsRequired();
            entity.Property(a => a.MailboxNumber).HasMaxLength(10);
            entity.Property(a => a.Building).HasMaxLength(40);
            entity.Property(a => a.Floor).HasMaxLength(10);
        });

        modelBuilder.Entity<AddressType>(entity =>
        {
            entity.HasKey(at => at.Id);
            entity.Property(at => at.Description).HasMaxLength(10).IsRequired();
        });

        modelBuilder.Entity<EmployeesAddresses>(entity =>
        {
            entity.HasKey(ea => new { ea.EmployeesExternalId, ea.AddressesId });
            entity.HasOne(ea => ea.Employee).WithMany().HasForeignKey(ea => ea.EmployeesExternalId);
            entity.HasOne(ea => ea.Address).WithMany().HasForeignKey(ea => ea.AddressesId);
        });

        modelBuilder.Entity<Series>(entity =>
        {
            entity.HasKey(s => s.Code);
            entity.Property(s => s.Name).HasMaxLength(255).IsRequired();
        });

        modelBuilder.Entity<EmployeeSeries>(entity =>
        {
            entity.HasKey(es => new { es.EmployeesExternalId, es.SeriesCode });
            entity.HasOne(es => es.Employee).WithMany(e => e.EmployeeSeries).HasForeignKey(es => es.EmployeesExternalId);
            entity.HasOne(es => es.Series).WithMany().HasForeignKey(es => es.SeriesCode);
        });

        modelBuilder.Entity<AddressType>().HasData(
            new AddressType { Id = 1, Description = "Personal" },
            new AddressType { Id = 2, Description = "Work" }
        );
    }
}

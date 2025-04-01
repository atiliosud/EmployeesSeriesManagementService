﻿// <auto-generated />
using System;
using Esms.SQLite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Esms.SQLite.Migrations
{
    [DbContext(typeof(EsmsDbContext))]
    partial class EsmsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.3");

            modelBuilder.Entity("Esms.Business.Models.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AddressTypeId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Building")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("TEXT");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Floor")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("TEXT");

                    b.Property<string>("MailboxNumber")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("TEXT");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("TEXT");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("TEXT");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AddressTypeId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("Esms.Business.Models.AddressType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("AddressTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Personal"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Work"
                        });
                });

            modelBuilder.Entity("Esms.Business.Models.Employee", b =>
                {
                    b.Property<int>("ExternalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("BirthPlace")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("ExitDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Nationality")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("TEXT");

                    b.Property<int>("Number")
                        .HasColumnType("INTEGER");

                    b.Property<int>("OrganizationalUnit")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("ProfileImage")
                        .HasColumnType("BLOB");

                    b.Property<string>("SecondName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("TEXT");

                    b.HasKey("ExternalId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Esms.Business.Models.EmployeeSeries", b =>
                {
                    b.Property<int>("EmployeesExternalId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SeriesCode")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TEXT");

                    b.HasKey("EmployeesExternalId", "SeriesCode");

                    b.HasIndex("SeriesCode");

                    b.ToTable("EmployeeSeries");
                });

            modelBuilder.Entity("Esms.Business.Models.EmployeesAddresses", b =>
                {
                    b.Property<int>("EmployeesExternalId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("AddressesId")
                        .HasColumnType("INTEGER");

                    b.HasKey("EmployeesExternalId", "AddressesId");

                    b.HasIndex("AddressesId");

                    b.ToTable("EmployeesAddresses");
                });

            modelBuilder.Entity("Esms.Business.Models.Series", b =>
                {
                    b.Property<int>("Code")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("ExternalId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Code");

                    b.ToTable("Series");
                });

            modelBuilder.Entity("Esms.Business.Models.Address", b =>
                {
                    b.HasOne("Esms.Business.Models.AddressType", "AddressType")
                        .WithMany("Addresses")
                        .HasForeignKey("AddressTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AddressType");
                });

            modelBuilder.Entity("Esms.Business.Models.EmployeeSeries", b =>
                {
                    b.HasOne("Esms.Business.Models.Employee", "Employee")
                        .WithMany("EmployeeSeries")
                        .HasForeignKey("EmployeesExternalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Esms.Business.Models.Series", "Series")
                        .WithMany("EmployeeSeries")
                        .HasForeignKey("SeriesCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Series");
                });

            modelBuilder.Entity("Esms.Business.Models.EmployeesAddresses", b =>
                {
                    b.HasOne("Esms.Business.Models.Address", "Address")
                        .WithMany("EmployeesAddresses")
                        .HasForeignKey("AddressesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Esms.Business.Models.Employee", "Employee")
                        .WithMany("EmployeesAddresses")
                        .HasForeignKey("EmployeesExternalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("Esms.Business.Models.Address", b =>
                {
                    b.Navigation("EmployeesAddresses");
                });

            modelBuilder.Entity("Esms.Business.Models.AddressType", b =>
                {
                    b.Navigation("Addresses");
                });

            modelBuilder.Entity("Esms.Business.Models.Employee", b =>
                {
                    b.Navigation("EmployeeSeries");

                    b.Navigation("EmployeesAddresses");
                });

            modelBuilder.Entity("Esms.Business.Models.Series", b =>
                {
                    b.Navigation("EmployeeSeries");
                });
#pragma warning restore 612, 618
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Esms.SQLite.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AddressTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    ExternalId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Number = table.Column<int>(type: "INTEGER", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    SecondName = table.Column<string>(type: "TEXT", nullable: true),
                    Language = table.Column<string>(type: "TEXT", maxLength: 2, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    BirthPlace = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    ProfileImage = table.Column<byte[]>(type: "BLOB", nullable: true),
                    Nationality = table.Column<string>(type: "TEXT", maxLength: 3, nullable: false),
                    ExitDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    EmailAddress = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    OrganizationalUnit = table.Column<int>(type: "INTEGER", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.ExternalId);
                });

            migrationBuilder.CreateTable(
                name: "Series",
                columns: table => new
                {
                    Code = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ExternalId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Series", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AddressTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    Country = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    City = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    ZipCode = table.Column<string>(type: "TEXT", maxLength: 5, nullable: false),
                    Street = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false),
                    Number = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    MailboxNumber = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Building = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false),
                    Floor = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_AddressTypes_AddressTypeId",
                        column: x => x.AddressTypeId,
                        principalTable: "AddressTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeSeries",
                columns: table => new
                {
                    EmployeesExternalId = table.Column<int>(type: "INTEGER", nullable: false),
                    SeriesCode = table.Column<int>(type: "INTEGER", nullable: false),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeSeries", x => new { x.EmployeesExternalId, x.SeriesCode });
                    table.ForeignKey(
                        name: "FK_EmployeeSeries_Employees_EmployeesExternalId",
                        column: x => x.EmployeesExternalId,
                        principalTable: "Employees",
                        principalColumn: "ExternalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeSeries_Series_SeriesCode",
                        column: x => x.SeriesCode,
                        principalTable: "Series",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeesAddresses",
                columns: table => new
                {
                    EmployeesExternalId = table.Column<int>(type: "INTEGER", nullable: false),
                    AddressesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeesAddresses", x => new { x.EmployeesExternalId, x.AddressesId });
                    table.ForeignKey(
                        name: "FK_EmployeesAddresses_Addresses_AddressesId",
                        column: x => x.AddressesId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeesAddresses_Employees_EmployeesExternalId",
                        column: x => x.EmployeesExternalId,
                        principalTable: "Employees",
                        principalColumn: "ExternalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_AddressTypeId",
                table: "Addresses",
                column: "AddressTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSeries_SeriesCode",
                table: "EmployeeSeries",
                column: "SeriesCode");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeesAddresses_AddressesId",
                table: "EmployeesAddresses",
                column: "AddressesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeSeries");

            migrationBuilder.DropTable(
                name: "EmployeesAddresses");

            migrationBuilder.DropTable(
                name: "Series");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "AddressTypes");
        }
    }
}

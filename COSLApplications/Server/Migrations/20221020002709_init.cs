using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace COSLApplications.Server.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "HSEIncidentsModel",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    IncidentID = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    IncidentVersion = table.Column<int>(type: "INTEGER", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Abstract = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    LastUpdateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastUpdateBy = table.Column<string>(type: "TEXT", maxLength: 30, nullable: true),
                    ReportPerson = table.Column<string>(type: "TEXT", maxLength: 30, nullable: true),
                    IncidentTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ReportTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FinalLevel = table.Column<int>(type: "INTEGER", nullable: false),
                    FinalIncidentClass = table.Column<int>(type: "INTEGER", nullable: false),
                    CurrentStatus = table.Column<int>(type: "INTEGER", nullable: false),
                    Customer = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    CustomerRep = table.Column<string>(type: "TEXT", maxLength: 30, nullable: true),
                    CustomerRepContact = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Contractor = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    ContractorManager = table.Column<string>(type: "TEXT", maxLength: 30, nullable: true),
                    ContractorManagerContact = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Country = table.Column<string>(type: "TEXT", maxLength: 30, nullable: true),
                    Field = table.Column<string>(type: "TEXT", nullable: true),
                    OpsSite = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    FieldManagers = table.Column<string>(type: "TEXT", maxLength: 30, nullable: true),
                    FieldManagerContact = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    FieldHSE = table.Column<string>(type: "TEXT", maxLength: 30, nullable: true),
                    FieldHSEContact = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    NPTLost = table.Column<int>(type: "INTEGER", nullable: false),
                    NPTCost = table.Column<int>(type: "INTEGER", nullable: false),
                    WeatherCondition = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    SiteCondition = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HSEIncidentsModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserModel",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    UserID = table.Column<int>(type: "INTEGER", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Company = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: true),
                    City = table.Column<string>(type: "TEXT", nullable: true),
                    Region = table.Column<string>(type: "TEXT", nullable: true),
                    Department = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Position = table.Column<string>(type: "TEXT", nullable: true),
                    ItGroup = table.Column<int>(type: "INTEGER", nullable: true),
                    OfficePosition = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EvidenceDoc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    IncidentID = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    IncidentVersion = table.Column<int>(type: "INTEGER", nullable: false),
                    LinkAddress = table.Column<string>(type: "TEXT", maxLength: 150, nullable: true),
                    FileOnServer = table.Column<string>(type: "TEXT", maxLength: 150, nullable: true),
                    description = table.Column<string>(type: "TEXT", maxLength: 150, nullable: true),
                    FileName = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    Supplier = table.Column<string>(type: "TEXT", maxLength: 150, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    HSEIncidentsModelId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvidenceDoc", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EvidenceDoc_HSEIncidentsModel_HSEIncidentsModelId",
                        column: x => x.HSEIncidentsModelId,
                        principalSchema: "dbo",
                        principalTable: "HSEIncidentsModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IncidentsDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    IncidentID = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    IncidentVersion = table.Column<int>(type: "INTEGER", nullable: false),
                    LastUpdateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateBy = table.Column<string>(type: "TEXT", maxLength: 30, nullable: true),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    Discription = table.Column<string>(type: "TEXT", nullable: false),
                    ImmediateAction = table.Column<string>(type: "TEXT", nullable: false),
                    Analyzation = table.Column<string>(type: "TEXT", nullable: true),
                    RootCause = table.Column<string>(type: "TEXT", nullable: true),
                    Correction = table.Column<string>(type: "TEXT", nullable: true),
                    FellowUp = table.Column<string>(type: "TEXT", nullable: true),
                    Approved = table.Column<string>(type: "TEXT", nullable: true),
                    HSEIncidentsModelId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncidentsDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IncidentsDetails_HSEIncidentsModel_HSEIncidentsModelId",
                        column: x => x.HSEIncidentsModelId,
                        principalSchema: "dbo",
                        principalTable: "HSEIncidentsModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Training",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    CertNo = table.Column<string>(type: "TEXT", nullable: false),
                    Result = table.Column<string>(type: "TEXT", nullable: false),
                    IssueDate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    Expir = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    TrainingCenter = table.Column<string>(type: "TEXT", nullable: false),
                    UserModelId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Training", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Training_UserModel_UserModelId",
                        column: x => x.UserModelId,
                        principalSchema: "dbo",
                        principalTable: "UserModel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EvidenceDoc_HSEIncidentsModelId",
                table: "EvidenceDoc",
                column: "HSEIncidentsModelId");

            migrationBuilder.CreateIndex(
                name: "IX_HSEIncidentsModel_IncidentID_IncidentVersion",
                schema: "dbo",
                table: "HSEIncidentsModel",
                columns: new[] { "IncidentID", "IncidentVersion" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IncidentsDetails_HSEIncidentsModelId",
                table: "IncidentsDetails",
                column: "HSEIncidentsModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Training_UserModelId",
                schema: "dbo",
                table: "Training",
                column: "UserModelId");

            migrationBuilder.CreateIndex(
                name: "IX_UserModel_Email",
                schema: "dbo",
                table: "UserModel",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserModel_UserID",
                schema: "dbo",
                table: "UserModel",
                column: "UserID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EvidenceDoc");

            migrationBuilder.DropTable(
                name: "IncidentsDetails");

            migrationBuilder.DropTable(
                name: "Training",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "HSEIncidentsModel",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "UserModel",
                schema: "dbo");
        }
    }
}

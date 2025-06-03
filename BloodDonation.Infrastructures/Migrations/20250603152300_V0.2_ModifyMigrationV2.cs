using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodDonation.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class V02_ModifyMigrationV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BackUrlIdentity",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FrontUrlIdentity",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "HealthCheckForms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    VolumeBloodDonated = table.Column<double>(type: "float", nullable: false),
                    Hemoglobin = table.Column<double>(type: "float", nullable: false),
                    IsInfectiousDisease = table.Column<bool>(type: "bit", nullable: false),
                    IsPregnant = table.Column<bool>(type: "bit", nullable: false),
                    IsUsedAlcoholRecently = table.Column<bool>(type: "bit", nullable: false),
                    HasChronicDisease = table.Column<bool>(type: "bit", nullable: false),
                    HasUnsafeSexualBehaviourOrSameSexSexualContact = table.Column<bool>(type: "bit", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BloodDonateRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthCheckForms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HealthCheckForms_BloodDonationRequests_BloodDonateRequestId",
                        column: x => x.BloodDonateRequestId,
                        principalTable: "BloodDonationRequests",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b77"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 3, 22, 22, 59, 359, DateTimeKind.Local).AddTicks(2253));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b78"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 3, 22, 22, 59, 359, DateTimeKind.Local).AddTicks(2251));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b79"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 3, 22, 22, 59, 359, DateTimeKind.Local).AddTicks(2248));

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b80"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 3, 22, 22, 59, 359, DateTimeKind.Local).AddTicks(2218));

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b81"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 3, 22, 22, 59, 359, DateTimeKind.Local).AddTicks(2203));

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b82"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 3, 22, 22, 59, 359, DateTimeKind.Local).AddTicks(2201));

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b83"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 3, 22, 22, 59, 359, DateTimeKind.Local).AddTicks(2199));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b84"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 3, 22, 22, 59, 359, DateTimeKind.Local).AddTicks(2005));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b85"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 3, 22, 22, 59, 359, DateTimeKind.Local).AddTicks(2002));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b86"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 3, 22, 22, 59, 359, DateTimeKind.Local).AddTicks(1977));

            migrationBuilder.CreateIndex(
                name: "IX_HealthCheckForms_BloodDonateRequestId",
                table: "HealthCheckForms",
                column: "BloodDonateRequestId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HealthCheckForms");

            migrationBuilder.DropColumn(
                name: "BackUrlIdentity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FrontUrlIdentity",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b77"),
                column: "CreatedDate",
                value: new DateTime(2025, 5, 27, 22, 12, 28, 150, DateTimeKind.Local).AddTicks(3636));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b78"),
                column: "CreatedDate",
                value: new DateTime(2025, 5, 27, 22, 12, 28, 150, DateTimeKind.Local).AddTicks(3633));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b79"),
                column: "CreatedDate",
                value: new DateTime(2025, 5, 27, 22, 12, 28, 150, DateTimeKind.Local).AddTicks(3629));

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b80"),
                column: "CreatedDate",
                value: new DateTime(2025, 5, 27, 22, 12, 28, 150, DateTimeKind.Local).AddTicks(3559));

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b81"),
                column: "CreatedDate",
                value: new DateTime(2025, 5, 27, 22, 12, 28, 150, DateTimeKind.Local).AddTicks(3556));

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b82"),
                column: "CreatedDate",
                value: new DateTime(2025, 5, 27, 22, 12, 28, 150, DateTimeKind.Local).AddTicks(3552));

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b83"),
                column: "CreatedDate",
                value: new DateTime(2025, 5, 27, 22, 12, 28, 150, DateTimeKind.Local).AddTicks(3541));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b84"),
                column: "CreatedDate",
                value: new DateTime(2025, 5, 27, 22, 12, 28, 150, DateTimeKind.Local).AddTicks(3196));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b85"),
                column: "CreatedDate",
                value: new DateTime(2025, 5, 27, 22, 12, 28, 150, DateTimeKind.Local).AddTicks(3193));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b86"),
                column: "CreatedDate",
                value: new DateTime(2025, 5, 27, 22, 12, 28, 150, DateTimeKind.Local).AddTicks(3152));
        }
    }
}

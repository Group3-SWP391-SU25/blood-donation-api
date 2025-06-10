using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodDonation.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFieldHealthCheck : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAntiHCVPositive",
                table: "BloodChecks");

            migrationBuilder.DropColumn(
                name: "IsHBsAgPositive",
                table: "BloodChecks");

            migrationBuilder.DropColumn(
                name: "IsHIVPositive",
                table: "BloodChecks");

            migrationBuilder.DropColumn(
                name: "IsOtherInfectionsPositive",
                table: "BloodChecks");

            migrationBuilder.AddColumn<double>(
                name: "HCT",
                table: "BloodChecks",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "HGB",
                table: "BloodChecks",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MCH",
                table: "BloodChecks",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MCHC",
                table: "BloodChecks",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MCV",
                table: "BloodChecks",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MPV",
                table: "BloodChecks",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PLT",
                table: "BloodChecks",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "RBC",
                table: "BloodChecks",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "WBC",
                table: "BloodChecks",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b63"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 11, 2, 39, 34, 646, DateTimeKind.Local).AddTicks(2901));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b64"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 11, 2, 39, 34, 646, DateTimeKind.Local).AddTicks(2900));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b65"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 11, 2, 39, 34, 646, DateTimeKind.Local).AddTicks(2898));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b66"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 11, 2, 39, 34, 646, DateTimeKind.Local).AddTicks(2897));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b67"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 11, 2, 39, 34, 646, DateTimeKind.Local).AddTicks(2895));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b68"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 11, 2, 39, 34, 646, DateTimeKind.Local).AddTicks(2894));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b69"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 11, 2, 39, 34, 646, DateTimeKind.Local).AddTicks(2893));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b70"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 11, 2, 39, 34, 646, DateTimeKind.Local).AddTicks(2891));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b71"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 11, 2, 39, 34, 646, DateTimeKind.Local).AddTicks(2890));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b72"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 11, 2, 39, 34, 646, DateTimeKind.Local).AddTicks(2888));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b73"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 11, 2, 39, 34, 646, DateTimeKind.Local).AddTicks(2887));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b74"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 11, 2, 39, 34, 646, DateTimeKind.Local).AddTicks(2884));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b75"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 11, 2, 39, 34, 646, DateTimeKind.Local).AddTicks(2883));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b76"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 11, 2, 39, 34, 646, DateTimeKind.Local).AddTicks(2881));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b77"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 11, 2, 39, 34, 646, DateTimeKind.Local).AddTicks(2880));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b78"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 11, 2, 39, 34, 646, DateTimeKind.Local).AddTicks(2878));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b79"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 11, 2, 39, 34, 646, DateTimeKind.Local).AddTicks(2874));

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b7c"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 11, 2, 39, 34, 646, DateTimeKind.Local).AddTicks(2843));

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b7d"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 11, 2, 39, 34, 646, DateTimeKind.Local).AddTicks(2812));

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b7e"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 11, 2, 39, 34, 646, DateTimeKind.Local).AddTicks(2810));

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b7f"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 11, 2, 39, 34, 646, DateTimeKind.Local).AddTicks(2808));

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b80"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 11, 2, 39, 34, 646, DateTimeKind.Local).AddTicks(2806));

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b81"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 11, 2, 39, 34, 646, DateTimeKind.Local).AddTicks(2805));

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b82"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 11, 2, 39, 34, 646, DateTimeKind.Local).AddTicks(2803));

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b83"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 11, 2, 39, 34, 646, DateTimeKind.Local).AddTicks(2799));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b84"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 11, 2, 39, 34, 646, DateTimeKind.Local).AddTicks(2653));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b85"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 11, 2, 39, 34, 646, DateTimeKind.Local).AddTicks(2651));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b86"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 11, 2, 39, 34, 646, DateTimeKind.Local).AddTicks(2628));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HCT",
                table: "BloodChecks");

            migrationBuilder.DropColumn(
                name: "HGB",
                table: "BloodChecks");

            migrationBuilder.DropColumn(
                name: "MCH",
                table: "BloodChecks");

            migrationBuilder.DropColumn(
                name: "MCHC",
                table: "BloodChecks");

            migrationBuilder.DropColumn(
                name: "MCV",
                table: "BloodChecks");

            migrationBuilder.DropColumn(
                name: "MPV",
                table: "BloodChecks");

            migrationBuilder.DropColumn(
                name: "PLT",
                table: "BloodChecks");

            migrationBuilder.DropColumn(
                name: "RBC",
                table: "BloodChecks");

            migrationBuilder.DropColumn(
                name: "WBC",
                table: "BloodChecks");

            migrationBuilder.AddColumn<bool>(
                name: "IsAntiHCVPositive",
                table: "BloodChecks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsHBsAgPositive",
                table: "BloodChecks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsHIVPositive",
                table: "BloodChecks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsOtherInfectionsPositive",
                table: "BloodChecks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b63"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 11, 0, 33, 3, 124, DateTimeKind.Local).AddTicks(4471));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b64"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 11, 0, 33, 3, 124, DateTimeKind.Local).AddTicks(4469));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b65"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 11, 0, 33, 3, 124, DateTimeKind.Local).AddTicks(4467));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b66"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 11, 0, 33, 3, 124, DateTimeKind.Local).AddTicks(4465));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b67"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 11, 0, 33, 3, 124, DateTimeKind.Local).AddTicks(4463));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b68"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 11, 0, 33, 3, 124, DateTimeKind.Local).AddTicks(4460));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b69"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 11, 0, 33, 3, 124, DateTimeKind.Local).AddTicks(4458));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b70"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 11, 0, 33, 3, 124, DateTimeKind.Local).AddTicks(4456));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b71"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 11, 0, 33, 3, 124, DateTimeKind.Local).AddTicks(4451));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b72"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 11, 0, 33, 3, 124, DateTimeKind.Local).AddTicks(4449));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b73"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 11, 0, 33, 3, 124, DateTimeKind.Local).AddTicks(4447));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b74"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 11, 0, 33, 3, 124, DateTimeKind.Local).AddTicks(4444));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b75"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 11, 0, 33, 3, 124, DateTimeKind.Local).AddTicks(4442));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b76"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 11, 0, 33, 3, 124, DateTimeKind.Local).AddTicks(4439));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b77"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 11, 0, 33, 3, 124, DateTimeKind.Local).AddTicks(4437));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b78"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 11, 0, 33, 3, 124, DateTimeKind.Local).AddTicks(4435));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b79"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 11, 0, 33, 3, 124, DateTimeKind.Local).AddTicks(4428));

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b7c"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 11, 0, 33, 3, 124, DateTimeKind.Local).AddTicks(4366));

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b7d"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 11, 0, 33, 3, 124, DateTimeKind.Local).AddTicks(4364));

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b7e"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 11, 0, 33, 3, 124, DateTimeKind.Local).AddTicks(4361));

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b7f"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 11, 0, 33, 3, 124, DateTimeKind.Local).AddTicks(4359));

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b80"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 11, 0, 33, 3, 124, DateTimeKind.Local).AddTicks(4356));

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b81"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 11, 0, 33, 3, 124, DateTimeKind.Local).AddTicks(4353));

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b82"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 11, 0, 33, 3, 124, DateTimeKind.Local).AddTicks(4349));

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b83"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 11, 0, 33, 3, 124, DateTimeKind.Local).AddTicks(4344));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b84"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 11, 0, 33, 3, 124, DateTimeKind.Local).AddTicks(4038));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b85"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 11, 0, 33, 3, 124, DateTimeKind.Local).AddTicks(4035));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b86"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 11, 0, 33, 3, 124, DateTimeKind.Local).AddTicks(3918));
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodDonation.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class V06_ModifyBloodIssue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BloodIssues_EmergencyBloodRequests_EmergencyBloodRequestId",
                table: "BloodIssues");

            migrationBuilder.DropIndex(
                name: "IX_BloodIssues_EmergencyBloodRequestId",
                table: "BloodIssues");

            migrationBuilder.DropColumn(
                name: "EmergencyBloodRequestId",
                table: "BloodIssues");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "EmergencyBloodRequests",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "BloodIssueId",
                table: "EmergencyBloodRequests",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ParentStorageId",
                table: "BloodStorages",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "BloodIssues",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Reason",
                table: "BloodIssues",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ReceiverName",
                table: "BloodIssues",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b63"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 22, 0, 28, 33, 871, DateTimeKind.Local).AddTicks(8517));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b64"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 22, 0, 28, 33, 871, DateTimeKind.Local).AddTicks(8515));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b65"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 22, 0, 28, 33, 871, DateTimeKind.Local).AddTicks(8514));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b66"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 22, 0, 28, 33, 871, DateTimeKind.Local).AddTicks(8512));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b67"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 22, 0, 28, 33, 871, DateTimeKind.Local).AddTicks(8511));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b68"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 22, 0, 28, 33, 871, DateTimeKind.Local).AddTicks(8509));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b69"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 22, 0, 28, 33, 871, DateTimeKind.Local).AddTicks(8508));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b70"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 22, 0, 28, 33, 871, DateTimeKind.Local).AddTicks(8506));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b71"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 22, 0, 28, 33, 871, DateTimeKind.Local).AddTicks(8505));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b72"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 22, 0, 28, 33, 871, DateTimeKind.Local).AddTicks(8504));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b73"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 22, 0, 28, 33, 871, DateTimeKind.Local).AddTicks(8502));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b74"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 22, 0, 28, 33, 871, DateTimeKind.Local).AddTicks(8501));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b75"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 22, 0, 28, 33, 871, DateTimeKind.Local).AddTicks(8499));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b76"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 22, 0, 28, 33, 871, DateTimeKind.Local).AddTicks(8498));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b77"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 22, 0, 28, 33, 871, DateTimeKind.Local).AddTicks(8496));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b78"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 22, 0, 28, 33, 871, DateTimeKind.Local).AddTicks(8495));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b79"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 22, 0, 28, 33, 871, DateTimeKind.Local).AddTicks(8489));

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b7c"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 22, 0, 28, 33, 871, DateTimeKind.Local).AddTicks(8441));

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b7d"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 22, 0, 28, 33, 871, DateTimeKind.Local).AddTicks(8440));

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b7e"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 22, 0, 28, 33, 871, DateTimeKind.Local).AddTicks(8438));

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b7f"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 22, 0, 28, 33, 871, DateTimeKind.Local).AddTicks(8436));

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b80"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 22, 0, 28, 33, 871, DateTimeKind.Local).AddTicks(8434));

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b81"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 22, 0, 28, 33, 871, DateTimeKind.Local).AddTicks(8395));

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b82"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 22, 0, 28, 33, 871, DateTimeKind.Local).AddTicks(8393));

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b83"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 22, 0, 28, 33, 871, DateTimeKind.Local).AddTicks(8388));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b84"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 22, 0, 28, 33, 871, DateTimeKind.Local).AddTicks(8174));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b85"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 22, 0, 28, 33, 871, DateTimeKind.Local).AddTicks(8172));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b86"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 22, 0, 28, 33, 871, DateTimeKind.Local).AddTicks(8143));

            migrationBuilder.CreateIndex(
                name: "IX_EmergencyBloodRequests_BloodIssueId",
                table: "EmergencyBloodRequests",
                column: "BloodIssueId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BloodStorages_ParentStorageId",
                table: "BloodStorages",
                column: "ParentStorageId");

            migrationBuilder.AddForeignKey(
                name: "FK_BloodStorages_BloodStorages_ParentStorageId",
                table: "BloodStorages",
                column: "ParentStorageId",
                principalTable: "BloodStorages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmergencyBloodRequests_BloodIssues_BloodIssueId",
                table: "EmergencyBloodRequests",
                column: "BloodIssueId",
                principalTable: "BloodIssues",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BloodStorages_BloodStorages_ParentStorageId",
                table: "BloodStorages");

            migrationBuilder.DropForeignKey(
                name: "FK_EmergencyBloodRequests_BloodIssues_BloodIssueId",
                table: "EmergencyBloodRequests");

            migrationBuilder.DropIndex(
                name: "IX_EmergencyBloodRequests_BloodIssueId",
                table: "EmergencyBloodRequests");

            migrationBuilder.DropIndex(
                name: "IX_BloodStorages_ParentStorageId",
                table: "BloodStorages");

            migrationBuilder.DropColumn(
                name: "BloodIssueId",
                table: "EmergencyBloodRequests");

            migrationBuilder.DropColumn(
                name: "ParentStorageId",
                table: "BloodStorages");

            migrationBuilder.DropColumn(
                name: "Reason",
                table: "BloodIssues");

            migrationBuilder.DropColumn(
                name: "ReceiverName",
                table: "BloodIssues");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "EmergencyBloodRequests",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "BloodIssues",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<Guid>(
                name: "EmergencyBloodRequestId",
                table: "BloodIssues",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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

            migrationBuilder.CreateIndex(
                name: "IX_BloodIssues_EmergencyBloodRequestId",
                table: "BloodIssues",
                column: "EmergencyBloodRequestId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BloodIssues_EmergencyBloodRequests_EmergencyBloodRequestId",
                table: "BloodIssues",
                column: "EmergencyBloodRequestId",
                principalTable: "EmergencyBloodRequests",
                principalColumn: "Id");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodDonation.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class ChangeBloodStorageFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "EmergencyBloodRequests",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "BloodComponentId",
                table: "EmergencyBloodRequests",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BloodGroupId",
                table: "EmergencyBloodRequests",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b63"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 28, 11, 11, 14, 228, DateTimeKind.Local).AddTicks(5663));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b64"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 28, 11, 11, 14, 228, DateTimeKind.Local).AddTicks(5662));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b65"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 28, 11, 11, 14, 228, DateTimeKind.Local).AddTicks(5660));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b66"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 28, 11, 11, 14, 228, DateTimeKind.Local).AddTicks(5658));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b67"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 28, 11, 11, 14, 228, DateTimeKind.Local).AddTicks(5655));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b68"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 28, 11, 11, 14, 228, DateTimeKind.Local).AddTicks(5653));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b69"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 28, 11, 11, 14, 228, DateTimeKind.Local).AddTicks(5651));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b70"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 28, 11, 11, 14, 228, DateTimeKind.Local).AddTicks(5649));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b71"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 28, 11, 11, 14, 228, DateTimeKind.Local).AddTicks(5647));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b72"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 28, 11, 11, 14, 228, DateTimeKind.Local).AddTicks(5646));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b73"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 28, 11, 11, 14, 228, DateTimeKind.Local).AddTicks(5640));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b74"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 28, 11, 11, 14, 228, DateTimeKind.Local).AddTicks(5637));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b75"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 28, 11, 11, 14, 228, DateTimeKind.Local).AddTicks(5635));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b76"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 28, 11, 11, 14, 228, DateTimeKind.Local).AddTicks(5633));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b77"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 28, 11, 11, 14, 228, DateTimeKind.Local).AddTicks(5631));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b78"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 28, 11, 11, 14, 228, DateTimeKind.Local).AddTicks(5628));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b79"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 28, 11, 11, 14, 228, DateTimeKind.Local).AddTicks(5618));

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b7c"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 28, 11, 11, 14, 228, DateTimeKind.Local).AddTicks(5535));

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b7d"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 28, 11, 11, 14, 228, DateTimeKind.Local).AddTicks(5533));

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b7e"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 28, 11, 11, 14, 228, DateTimeKind.Local).AddTicks(5530));

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b7f"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 28, 11, 11, 14, 228, DateTimeKind.Local).AddTicks(5528));

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b80"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 28, 11, 11, 14, 228, DateTimeKind.Local).AddTicks(5526));

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b81"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 28, 11, 11, 14, 228, DateTimeKind.Local).AddTicks(5524));

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b82"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 28, 11, 11, 14, 228, DateTimeKind.Local).AddTicks(5521));

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b83"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 28, 11, 11, 14, 228, DateTimeKind.Local).AddTicks(5453));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b84"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 28, 11, 11, 14, 228, DateTimeKind.Local).AddTicks(5161));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b85"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 28, 11, 11, 14, 228, DateTimeKind.Local).AddTicks(5158));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b86"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 28, 11, 11, 14, 228, DateTimeKind.Local).AddTicks(5115));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b87"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 28, 11, 11, 14, 228, DateTimeKind.Local).AddTicks(5163));

            migrationBuilder.CreateIndex(
                name: "IX_EmergencyBloodRequests_BloodComponentId",
                table: "EmergencyBloodRequests",
                column: "BloodComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_EmergencyBloodRequests_BloodGroupId",
                table: "EmergencyBloodRequests",
                column: "BloodGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmergencyBloodRequests_BloodComponents_BloodComponentId",
                table: "EmergencyBloodRequests",
                column: "BloodComponentId",
                principalTable: "BloodComponents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmergencyBloodRequests_BloodGroups_BloodGroupId",
                table: "EmergencyBloodRequests",
                column: "BloodGroupId",
                principalTable: "BloodGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmergencyBloodRequests_BloodComponents_BloodComponentId",
                table: "EmergencyBloodRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_EmergencyBloodRequests_BloodGroups_BloodGroupId",
                table: "EmergencyBloodRequests");

            migrationBuilder.DropIndex(
                name: "IX_EmergencyBloodRequests_BloodComponentId",
                table: "EmergencyBloodRequests");

            migrationBuilder.DropIndex(
                name: "IX_EmergencyBloodRequests_BloodGroupId",
                table: "EmergencyBloodRequests");

            migrationBuilder.DropColumn(
                name: "BloodComponentId",
                table: "EmergencyBloodRequests");

            migrationBuilder.DropColumn(
                name: "BloodGroupId",
                table: "EmergencyBloodRequests");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "EmergencyBloodRequests",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b63"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 26, 9, 40, 30, 298, DateTimeKind.Local).AddTicks(8022));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b64"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 26, 9, 40, 30, 298, DateTimeKind.Local).AddTicks(8012));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b65"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 26, 9, 40, 30, 298, DateTimeKind.Local).AddTicks(8003));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b66"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 26, 9, 40, 30, 298, DateTimeKind.Local).AddTicks(7994));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b67"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 26, 9, 40, 30, 298, DateTimeKind.Local).AddTicks(7985));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b68"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 26, 9, 40, 30, 298, DateTimeKind.Local).AddTicks(7977));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b69"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 26, 9, 40, 30, 298, DateTimeKind.Local).AddTicks(7967));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b70"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 26, 9, 40, 30, 298, DateTimeKind.Local).AddTicks(7958));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b71"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 26, 9, 40, 30, 298, DateTimeKind.Local).AddTicks(7950));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b72"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 26, 9, 40, 30, 298, DateTimeKind.Local).AddTicks(7941));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b73"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 26, 9, 40, 30, 298, DateTimeKind.Local).AddTicks(7933));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b74"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 26, 9, 40, 30, 298, DateTimeKind.Local).AddTicks(7924));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b75"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 26, 9, 40, 30, 298, DateTimeKind.Local).AddTicks(7914));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b76"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 26, 9, 40, 30, 298, DateTimeKind.Local).AddTicks(7906));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b77"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 26, 9, 40, 30, 298, DateTimeKind.Local).AddTicks(7897));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b78"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 26, 9, 40, 30, 298, DateTimeKind.Local).AddTicks(7886));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b79"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 26, 9, 40, 30, 298, DateTimeKind.Local).AddTicks(7868));

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b7c"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 26, 9, 40, 30, 298, DateTimeKind.Local).AddTicks(7670));

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b7d"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 26, 9, 40, 30, 298, DateTimeKind.Local).AddTicks(7660));

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b7e"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 26, 9, 40, 30, 298, DateTimeKind.Local).AddTicks(7649));

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b7f"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 26, 9, 40, 30, 298, DateTimeKind.Local).AddTicks(7638));

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b80"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 26, 9, 40, 30, 298, DateTimeKind.Local).AddTicks(7627));

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b81"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 26, 9, 40, 30, 298, DateTimeKind.Local).AddTicks(7613));

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b82"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 26, 9, 40, 30, 298, DateTimeKind.Local).AddTicks(7601));

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b83"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 26, 9, 40, 30, 298, DateTimeKind.Local).AddTicks(7491));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b84"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 26, 9, 40, 30, 298, DateTimeKind.Local).AddTicks(6338));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b85"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 26, 9, 40, 30, 298, DateTimeKind.Local).AddTicks(6325));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b86"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 26, 9, 40, 30, 298, DateTimeKind.Local).AddTicks(6218));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b87"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 26, 9, 40, 30, 298, DateTimeKind.Local).AddTicks(6348));
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodDonation.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class Change_BloodDonation_BloodStorage_To_OneToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BloodStorages_BloodDonationId",
                table: "BloodStorages");

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b63"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 23, 21, 4, 2, 42, DateTimeKind.Local).AddTicks(6495));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b64"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 23, 21, 4, 2, 42, DateTimeKind.Local).AddTicks(6493));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b65"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 23, 21, 4, 2, 42, DateTimeKind.Local).AddTicks(6492));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b66"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 23, 21, 4, 2, 42, DateTimeKind.Local).AddTicks(6490));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b67"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 23, 21, 4, 2, 42, DateTimeKind.Local).AddTicks(6489));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b68"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 23, 21, 4, 2, 42, DateTimeKind.Local).AddTicks(6450));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b69"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 23, 21, 4, 2, 42, DateTimeKind.Local).AddTicks(6449));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b70"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 23, 21, 4, 2, 42, DateTimeKind.Local).AddTicks(6447));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b71"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 23, 21, 4, 2, 42, DateTimeKind.Local).AddTicks(6446));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b72"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 23, 21, 4, 2, 42, DateTimeKind.Local).AddTicks(6444));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b73"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 23, 21, 4, 2, 42, DateTimeKind.Local).AddTicks(6443));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b74"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 23, 21, 4, 2, 42, DateTimeKind.Local).AddTicks(6441));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b75"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 23, 21, 4, 2, 42, DateTimeKind.Local).AddTicks(6439));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b76"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 23, 21, 4, 2, 42, DateTimeKind.Local).AddTicks(6438));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b77"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 23, 21, 4, 2, 42, DateTimeKind.Local).AddTicks(6436));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b78"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 23, 21, 4, 2, 42, DateTimeKind.Local).AddTicks(6434));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b79"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 23, 21, 4, 2, 42, DateTimeKind.Local).AddTicks(6429));

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b7c"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 23, 21, 4, 2, 42, DateTimeKind.Local).AddTicks(6399));

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b7d"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 23, 21, 4, 2, 42, DateTimeKind.Local).AddTicks(6397));

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b7e"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 23, 21, 4, 2, 42, DateTimeKind.Local).AddTicks(6395));

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b7f"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 23, 21, 4, 2, 42, DateTimeKind.Local).AddTicks(6393));

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b80"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 23, 21, 4, 2, 42, DateTimeKind.Local).AddTicks(6391));

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b81"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 23, 21, 4, 2, 42, DateTimeKind.Local).AddTicks(6385));

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b82"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 23, 21, 4, 2, 42, DateTimeKind.Local).AddTicks(6383));

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b83"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 23, 21, 4, 2, 42, DateTimeKind.Local).AddTicks(6379));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b84"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 23, 21, 4, 2, 42, DateTimeKind.Local).AddTicks(6225));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b85"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 23, 21, 4, 2, 42, DateTimeKind.Local).AddTicks(6223));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b86"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 23, 21, 4, 2, 42, DateTimeKind.Local).AddTicks(6189));

            migrationBuilder.CreateIndex(
                name: "IX_BloodStorages_BloodDonationId",
                table: "BloodStorages",
                column: "BloodDonationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BloodStorages_BloodDonationId",
                table: "BloodStorages");

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
                name: "IX_BloodStorages_BloodDonationId",
                table: "BloodStorages",
                column: "BloodDonationId",
                unique: true);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodDonation.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class V0_3_ModifyDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BloodStorages_BloodDonationRequests_BloodDonateId",
                table: "BloodStorages");

            migrationBuilder.DropTable(
                name: "BloodDonationRequirement");

            migrationBuilder.DropIndex(
                name: "IX_BloodStorages_BloodDonateId",
                table: "BloodStorages");

            migrationBuilder.RenameColumn(
                name: "BloodDonateId",
                table: "BloodStorages",
                newName: "BloodDonationId");

            migrationBuilder.CreateTable(
                name: "BloodDonation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BloodType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Volume = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DonationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BloodDonationRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodDonation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BloodDonation_BloodDonationRequests_BloodDonationRequestId",
                        column: x => x.BloodDonationRequestId,
                        principalTable: "BloodDonationRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b77"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 10, 0, 30, 17, 51, DateTimeKind.Local).AddTicks(4375));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b78"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 10, 0, 30, 17, 51, DateTimeKind.Local).AddTicks(4374));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b79"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 10, 0, 30, 17, 51, DateTimeKind.Local).AddTicks(4371));

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b80"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 10, 0, 30, 17, 51, DateTimeKind.Local).AddTicks(4342));

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b81"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 10, 0, 30, 17, 51, DateTimeKind.Local).AddTicks(4340));

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b82"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 10, 0, 30, 17, 51, DateTimeKind.Local).AddTicks(4339));

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b83"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 10, 0, 30, 17, 51, DateTimeKind.Local).AddTicks(4334));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b84"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 10, 0, 30, 17, 51, DateTimeKind.Local).AddTicks(4111));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b85"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 10, 0, 30, 17, 51, DateTimeKind.Local).AddTicks(4107));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b86"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 10, 0, 30, 17, 51, DateTimeKind.Local).AddTicks(4080));

            migrationBuilder.CreateIndex(
                name: "IX_BloodStorages_BloodDonationId",
                table: "BloodStorages",
                column: "BloodDonationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BloodDonation_BloodDonationRequestId",
                table: "BloodDonation",
                column: "BloodDonationRequestId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BloodStorages_BloodDonation_BloodDonationId",
                table: "BloodStorages",
                column: "BloodDonationId",
                principalTable: "BloodDonation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BloodStorages_BloodDonation_BloodDonationId",
                table: "BloodStorages");

            migrationBuilder.DropTable(
                name: "BloodDonation");

            migrationBuilder.DropIndex(
                name: "IX_BloodStorages_BloodDonationId",
                table: "BloodStorages");

            migrationBuilder.RenameColumn(
                name: "BloodDonationId",
                table: "BloodStorages",
                newName: "BloodDonateId");

            migrationBuilder.CreateTable(
                name: "BloodDonationRequirement",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Actual = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BloodDonationRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsPass = table.Column<bool>(type: "bit", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Requirement = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodDonationRequirement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BloodDonationRequirement_BloodDonationRequests_BloodDonationRequestId",
                        column: x => x.BloodDonationRequestId,
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
                value: new DateTime(2025, 6, 4, 18, 13, 58, 188, DateTimeKind.Local).AddTicks(7003));

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
                name: "IX_BloodStorages_BloodDonateId",
                table: "BloodStorages",
                column: "BloodDonateId");

            migrationBuilder.CreateIndex(
                name: "IX_BloodDonationRequirement_BloodDonationRequestId",
                table: "BloodDonationRequirement",
                column: "BloodDonationRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_BloodStorages_BloodDonationRequests_BloodDonateId",
                table: "BloodStorages",
                column: "BloodDonateId",
                principalTable: "BloodDonationRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

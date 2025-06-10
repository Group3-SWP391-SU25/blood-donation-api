using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BloodDonation.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class AddTableBloodCheck : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StorageTemerature",
                table: "BloodComponents",
                newName: "MinStorageTemerature");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "BloodStorages",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<Guid>(
                name: "BloodGroupId",
                table: "BloodStorages",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "RhFactor",
                table: "BloodGroups",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<double>(
                name: "ShelfLifeInDay",
                table: "BloodComponents",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<double>(
                name: "MaxStorageTemerature",
                table: "BloodComponents",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "BloodChecks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsHIVPositive = table.Column<bool>(type: "bit", nullable: false),
                    IsHBsAgPositive = table.Column<bool>(type: "bit", nullable: false),
                    IsAntiHCVPositive = table.Column<bool>(type: "bit", nullable: false),
                    IsOtherInfectionsPositive = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CheckedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BloodGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BloodDonationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodChecks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BloodChecks_BloodDonation_BloodDonationId",
                        column: x => x.BloodDonationId,
                        principalTable: "BloodDonation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BloodChecks_BloodGroups_BloodGroupId",
                        column: x => x.BloodGroupId,
                        principalTable: "BloodGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b77"),
                columns: new[] { "CreatedDate", "MaxStorageTemerature", "MinStorageTemerature", "Name", "ShelfLifeInDay" },
                values: new object[] { new DateTime(2025, 6, 11, 0, 33, 3, 124, DateTimeKind.Local).AddTicks(4437), 6.0, 2.0, "Khối hồng cầu rửa (xử lý trong hệ thống hở)", 1.0 });

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b78"),
                columns: new[] { "CreatedDate", "MaxStorageTemerature", "MinStorageTemerature", "Name", "ShelfLifeInDay" },
                values: new object[] { new DateTime(2025, 6, 11, 0, 33, 3, 124, DateTimeKind.Local).AddTicks(4435), 6.0, 2.0, "Khối hồng cầu có dung dịch bảo quản", 35.0 });

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b79"),
                columns: new[] { "CreatedDate", "MaxStorageTemerature", "MinStorageTemerature", "Name", "ShelfLifeInDay" },
                values: new object[] { new DateTime(2025, 6, 11, 0, 33, 3, 124, DateTimeKind.Local).AddTicks(4428), 6.0, 2.0, "Hồng cầu lắng", 35.0 });

            migrationBuilder.InsertData(
                table: "BloodComponents",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "IsDeleted", "MaxStorageTemerature", "MinStorageTemerature", "Name", "ShelfLifeInDay", "Status", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("859a4997-1ffa-4915-b50e-9a99e4147b63"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 6, 11, 0, 33, 3, 124, DateTimeKind.Local).AddTicks(4471), false, 24.0, 20.0, "Máu toàn phần", 1.0, "", null, null },
                    { new Guid("859a4997-1ffa-4915-b50e-9a99e4147b64"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 6, 11, 0, 33, 3, 124, DateTimeKind.Local).AddTicks(4469), false, 6.0, 2.0, "Máu toàn phần (Bảo quản)", 35.0, "", null, null },
                    { new Guid("859a4997-1ffa-4915-b50e-9a99e4147b65"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 6, 11, 0, 33, 3, 124, DateTimeKind.Local).AddTicks(4467), false, 24.0, 20.0, "Khối bạch cầu hạt trung tính", 1.0, "", null, null },
                    { new Guid("859a4997-1ffa-4915-b50e-9a99e4147b66"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 6, 11, 0, 33, 3, 124, DateTimeKind.Local).AddTicks(4465), false, 6.0, 2.0, "Tủa lạnh (Xử lí hở)", 1.0, "", null, null },
                    { new Guid("859a4997-1ffa-4915-b50e-9a99e4147b67"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 6, 11, 0, 33, 3, 124, DateTimeKind.Local).AddTicks(4463), false, 6.0, 2.0, "Tủa lạnh (Xử lí kín)", 14.0, "", null, null },
                    { new Guid("859a4997-1ffa-4915-b50e-9a99e4147b68"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 6, 11, 0, 33, 3, 124, DateTimeKind.Local).AddTicks(4460), false, 6.0, 2.0, "Huyết tương (Xử lí hở)", 1.0, "", null, null },
                    { new Guid("859a4997-1ffa-4915-b50e-9a99e4147b69"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 6, 11, 0, 33, 3, 124, DateTimeKind.Local).AddTicks(4458), false, 6.0, 2.0, "Huyết tương (Xử lí kín)", 14.0, "", null, null },
                    { new Guid("859a4997-1ffa-4915-b50e-9a99e4147b70"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 6, 11, 0, 33, 3, 124, DateTimeKind.Local).AddTicks(4456), false, -25.0, -272.0, "Huyết tương đông lạnh", 2.0, "", null, null },
                    { new Guid("859a4997-1ffa-4915-b50e-9a99e4147b71"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 6, 11, 0, 33, 3, 124, DateTimeKind.Local).AddTicks(4451), false, 24.0, 20.0, "Khối tiểu cầu lọc bạch cầu (Xử lí hở)", 0.25, "", null, null },
                    { new Guid("859a4997-1ffa-4915-b50e-9a99e4147b72"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 6, 11, 0, 33, 3, 124, DateTimeKind.Local).AddTicks(4449), false, 24.0, 20.0, "Khối tiểu cầu lọc bạch cầu (Xử lí kín)", 5.0, "", null, null },
                    { new Guid("859a4997-1ffa-4915-b50e-9a99e4147b73"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 6, 11, 0, 33, 3, 124, DateTimeKind.Local).AddTicks(4447), false, 24.0, 20.0, "Khối tiểu cầu (Xử lí hở)", 0.25, "", null, null },
                    { new Guid("859a4997-1ffa-4915-b50e-9a99e4147b74"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 6, 11, 0, 33, 3, 124, DateTimeKind.Local).AddTicks(4444), false, 24.0, 20.0, "Khối tiểu cầu (Xử lí kín)", 5.0, "", null, null },
                    { new Guid("859a4997-1ffa-4915-b50e-9a99e4147b75"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 6, 11, 0, 33, 3, 124, DateTimeKind.Local).AddTicks(4442), false, -60.0, -80.0, "Khối hồng cầu đông lạnh", 365242199.0, "", null, null },
                    { new Guid("859a4997-1ffa-4915-b50e-9a99e4147b76"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 6, 11, 0, 33, 3, 124, DateTimeKind.Local).AddTicks(4439), false, 6.0, 2.0, "Khối hồng cầu rửa (rửa trong hệ thống kín và có bổ sung dung dịch bảo quản hồng cầu)", 14.0, "", null, null }
                });

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b80"),
                columns: new[] { "CreatedDate", "RhFactor", "Type" },
                values: new object[] { new DateTime(2025, 6, 11, 0, 33, 3, 124, DateTimeKind.Local).AddTicks(4356), "-", "B" });

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b81"),
                columns: new[] { "CreatedDate", "RhFactor", "Type" },
                values: new object[] { new DateTime(2025, 6, 11, 0, 33, 3, 124, DateTimeKind.Local).AddTicks(4353), "+", "B" });

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b82"),
                columns: new[] { "CreatedDate", "RhFactor", "Type" },
                values: new object[] { new DateTime(2025, 6, 11, 0, 33, 3, 124, DateTimeKind.Local).AddTicks(4349), "-", "A" });

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b83"),
                columns: new[] { "CreatedDate", "RhFactor" },
                values: new object[] { new DateTime(2025, 6, 11, 0, 33, 3, 124, DateTimeKind.Local).AddTicks(4344), "+" });

            migrationBuilder.InsertData(
                table: "BloodGroups",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "IsDeleted", "RhFactor", "Type", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("859a4997-1ffa-4915-b50e-9a99e4147b7c"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 6, 11, 0, 33, 3, 124, DateTimeKind.Local).AddTicks(4366), false, "-", "O", null, null },
                    { new Guid("859a4997-1ffa-4915-b50e-9a99e4147b7d"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 6, 11, 0, 33, 3, 124, DateTimeKind.Local).AddTicks(4364), false, "+", "O", null, null },
                    { new Guid("859a4997-1ffa-4915-b50e-9a99e4147b7e"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 6, 11, 0, 33, 3, 124, DateTimeKind.Local).AddTicks(4361), false, "-", "AB", null, null },
                    { new Guid("859a4997-1ffa-4915-b50e-9a99e4147b7f"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 6, 11, 0, 33, 3, 124, DateTimeKind.Local).AddTicks(4359), false, "+", "AB", null, null }
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_BloodChecks_BloodDonationId",
                table: "BloodChecks",
                column: "BloodDonationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BloodChecks_BloodGroupId",
                table: "BloodChecks",
                column: "BloodGroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BloodChecks");

            migrationBuilder.DeleteData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b63"));

            migrationBuilder.DeleteData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b64"));

            migrationBuilder.DeleteData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b65"));

            migrationBuilder.DeleteData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b66"));

            migrationBuilder.DeleteData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b67"));

            migrationBuilder.DeleteData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b68"));

            migrationBuilder.DeleteData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b69"));

            migrationBuilder.DeleteData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b70"));

            migrationBuilder.DeleteData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b71"));

            migrationBuilder.DeleteData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b72"));

            migrationBuilder.DeleteData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b73"));

            migrationBuilder.DeleteData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b74"));

            migrationBuilder.DeleteData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b75"));

            migrationBuilder.DeleteData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b76"));

            migrationBuilder.DeleteData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b7c"));

            migrationBuilder.DeleteData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b7d"));

            migrationBuilder.DeleteData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b7e"));

            migrationBuilder.DeleteData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b7f"));

            migrationBuilder.DropColumn(
                name: "RhFactor",
                table: "BloodGroups");

            migrationBuilder.DropColumn(
                name: "MaxStorageTemerature",
                table: "BloodComponents");

            migrationBuilder.RenameColumn(
                name: "MinStorageTemerature",
                table: "BloodComponents",
                newName: "StorageTemerature");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "BloodStorages",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "BloodGroupId",
                table: "BloodStorages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ShelfLifeInDay",
                table: "BloodComponents",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b77"),
                columns: new[] { "CreatedDate", "Name", "ShelfLifeInDay", "StorageTemerature" },
                values: new object[] { new DateTime(2025, 6, 10, 17, 46, 15, 976, DateTimeKind.Local).AddTicks(1211), "Platelets", 0, 0.0 });

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b78"),
                columns: new[] { "CreatedDate", "Name", "ShelfLifeInDay", "StorageTemerature" },
                values: new object[] { new DateTime(2025, 6, 10, 17, 46, 15, 976, DateTimeKind.Local).AddTicks(1210), "Plasma", 0, 0.0 });

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b79"),
                columns: new[] { "CreatedDate", "Name", "ShelfLifeInDay", "StorageTemerature" },
                values: new object[] { new DateTime(2025, 6, 10, 17, 46, 15, 976, DateTimeKind.Local).AddTicks(1208), "Red Blood Cells", 0, 0.0 });

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b80"),
                columns: new[] { "CreatedDate", "Type" },
                values: new object[] { new DateTime(2025, 6, 10, 17, 46, 15, 976, DateTimeKind.Local).AddTicks(1150), "AB" });

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b81"),
                columns: new[] { "CreatedDate", "Type" },
                values: new object[] { new DateTime(2025, 6, 10, 17, 46, 15, 976, DateTimeKind.Local).AddTicks(1148), "O" });

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b82"),
                columns: new[] { "CreatedDate", "Type" },
                values: new object[] { new DateTime(2025, 6, 10, 17, 46, 15, 976, DateTimeKind.Local).AddTicks(1146), "B" });

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b83"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 10, 17, 46, 15, 976, DateTimeKind.Local).AddTicks(1142));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b84"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 10, 17, 46, 15, 976, DateTimeKind.Local).AddTicks(885));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b85"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 10, 17, 46, 15, 976, DateTimeKind.Local).AddTicks(882));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b86"),
                column: "CreatedDate",
                value: new DateTime(2025, 6, 10, 17, 46, 15, 976, DateTimeKind.Local).AddTicks(847));
        }
    }
}

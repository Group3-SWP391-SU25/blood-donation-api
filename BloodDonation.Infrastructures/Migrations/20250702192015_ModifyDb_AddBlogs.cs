using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodDonation.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class ModifyDb_AddBlogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Users_UserId",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "Html",
                table: "Blogs");

            migrationBuilder.RenameColumn(
                name: "Slug",
                table: "Blogs",
                newName: "Summary");

            migrationBuilder.RenameColumn(
                name: "Imageurls",
                table: "Blogs",
                newName: "Content");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Blogs",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b63"),
                column: "CreatedDate",
                value: new DateTime(2025, 7, 3, 2, 20, 14, 355, DateTimeKind.Local).AddTicks(6007));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b64"),
                column: "CreatedDate",
                value: new DateTime(2025, 7, 3, 2, 20, 14, 355, DateTimeKind.Local).AddTicks(6005));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b65"),
                column: "CreatedDate",
                value: new DateTime(2025, 7, 3, 2, 20, 14, 355, DateTimeKind.Local).AddTicks(6004));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b66"),
                column: "CreatedDate",
                value: new DateTime(2025, 7, 3, 2, 20, 14, 355, DateTimeKind.Local).AddTicks(6002));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b67"),
                column: "CreatedDate",
                value: new DateTime(2025, 7, 3, 2, 20, 14, 355, DateTimeKind.Local).AddTicks(6001));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b68"),
                column: "CreatedDate",
                value: new DateTime(2025, 7, 3, 2, 20, 14, 355, DateTimeKind.Local).AddTicks(5999));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b69"),
                column: "CreatedDate",
                value: new DateTime(2025, 7, 3, 2, 20, 14, 355, DateTimeKind.Local).AddTicks(5998));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b70"),
                column: "CreatedDate",
                value: new DateTime(2025, 7, 3, 2, 20, 14, 355, DateTimeKind.Local).AddTicks(5996));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b71"),
                column: "CreatedDate",
                value: new DateTime(2025, 7, 3, 2, 20, 14, 355, DateTimeKind.Local).AddTicks(5995));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b72"),
                column: "CreatedDate",
                value: new DateTime(2025, 7, 3, 2, 20, 14, 355, DateTimeKind.Local).AddTicks(5994));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b73"),
                column: "CreatedDate",
                value: new DateTime(2025, 7, 3, 2, 20, 14, 355, DateTimeKind.Local).AddTicks(5992));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b74"),
                column: "CreatedDate",
                value: new DateTime(2025, 7, 3, 2, 20, 14, 355, DateTimeKind.Local).AddTicks(5991));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b75"),
                column: "CreatedDate",
                value: new DateTime(2025, 7, 3, 2, 20, 14, 355, DateTimeKind.Local).AddTicks(5989));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b76"),
                column: "CreatedDate",
                value: new DateTime(2025, 7, 3, 2, 20, 14, 355, DateTimeKind.Local).AddTicks(5988));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b77"),
                column: "CreatedDate",
                value: new DateTime(2025, 7, 3, 2, 20, 14, 355, DateTimeKind.Local).AddTicks(5985));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b78"),
                column: "CreatedDate",
                value: new DateTime(2025, 7, 3, 2, 20, 14, 355, DateTimeKind.Local).AddTicks(5983));

            migrationBuilder.UpdateData(
                table: "BloodComponents",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b79"),
                column: "CreatedDate",
                value: new DateTime(2025, 7, 3, 2, 20, 14, 355, DateTimeKind.Local).AddTicks(5977));

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b7c"),
                column: "CreatedDate",
                value: new DateTime(2025, 7, 3, 2, 20, 14, 355, DateTimeKind.Local).AddTicks(5932));

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b7d"),
                column: "CreatedDate",
                value: new DateTime(2025, 7, 3, 2, 20, 14, 355, DateTimeKind.Local).AddTicks(5931));

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b7e"),
                column: "CreatedDate",
                value: new DateTime(2025, 7, 3, 2, 20, 14, 355, DateTimeKind.Local).AddTicks(5928));

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b7f"),
                column: "CreatedDate",
                value: new DateTime(2025, 7, 3, 2, 20, 14, 355, DateTimeKind.Local).AddTicks(5886));

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b80"),
                column: "CreatedDate",
                value: new DateTime(2025, 7, 3, 2, 20, 14, 355, DateTimeKind.Local).AddTicks(5884));

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b81"),
                column: "CreatedDate",
                value: new DateTime(2025, 7, 3, 2, 20, 14, 355, DateTimeKind.Local).AddTicks(5882));

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b82"),
                column: "CreatedDate",
                value: new DateTime(2025, 7, 3, 2, 20, 14, 355, DateTimeKind.Local).AddTicks(5880));

            migrationBuilder.UpdateData(
                table: "BloodGroups",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b83"),
                column: "CreatedDate",
                value: new DateTime(2025, 7, 3, 2, 20, 14, 355, DateTimeKind.Local).AddTicks(5876));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b84"),
                column: "CreatedDate",
                value: new DateTime(2025, 7, 3, 2, 20, 14, 355, DateTimeKind.Local).AddTicks(5682));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b85"),
                column: "CreatedDate",
                value: new DateTime(2025, 7, 3, 2, 20, 14, 355, DateTimeKind.Local).AddTicks(5680));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b86"),
                column: "CreatedDate",
                value: new DateTime(2025, 7, 3, 2, 20, 14, 355, DateTimeKind.Local).AddTicks(5655));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b87"),
                column: "CreatedDate",
                value: new DateTime(2025, 7, 3, 2, 20, 14, 355, DateTimeKind.Local).AddTicks(5684));

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Users_UserId",
                table: "Blogs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Users_UserId",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Blogs");

            migrationBuilder.RenameColumn(
                name: "Summary",
                table: "Blogs",
                newName: "Slug");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Blogs",
                newName: "Imageurls");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Blogs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Html",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Users_UserId",
                table: "Blogs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

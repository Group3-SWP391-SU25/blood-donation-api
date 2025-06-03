using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodDonation.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class V01_modified_fields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blog_User_UserId",
                table: "Blog");

            migrationBuilder.DropForeignKey(
                name: "FK_BloodIssue_BlooodStorage_BloodStorageId",
                table: "BloodIssue");

            migrationBuilder.DropForeignKey(
                name: "FK_BloodIssue_EmergencyBloodRequest_EmergencyBloodRequestId",
                table: "BloodIssue");

            migrationBuilder.DropForeignKey(
                name: "FK_BlooodStorage_BloodComponent_BloodComponentId",
                table: "BlooodStorage");

            migrationBuilder.DropForeignKey(
                name: "FK_BlooodStorage_BloodDonate_BloodDonateId",
                table: "BlooodStorage");

            migrationBuilder.DropForeignKey(
                name: "FK_BlooodStorage_BloodGroup_BloodGroupId",
                table: "BlooodStorage");

            migrationBuilder.DropForeignKey(
                name: "FK_EmergencyBloodRequest_User_UserId",
                table: "EmergencyBloodRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_User_BloodGroup_BloodGroupId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Role_RoleId",
                table: "User");

            migrationBuilder.DropTable(
                name: "BloodDonate");

            migrationBuilder.DropTable(
                name: "BloodRequest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Role",
                table: "Role");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmergencyBloodRequest",
                table: "EmergencyBloodRequest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlooodStorage",
                table: "BlooodStorage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BloodIssue",
                table: "BloodIssue");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BloodGroup",
                table: "BloodGroup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BloodComponent",
                table: "BloodComponent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Blog",
                table: "Blog");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Role",
                newName: "Roles");

            migrationBuilder.RenameTable(
                name: "EmergencyBloodRequest",
                newName: "EmergencyBloodRequests");

            migrationBuilder.RenameTable(
                name: "BlooodStorage",
                newName: "BloodStorages");

            migrationBuilder.RenameTable(
                name: "BloodIssue",
                newName: "BloodIssues");

            migrationBuilder.RenameTable(
                name: "BloodGroup",
                newName: "BloodGroups");

            migrationBuilder.RenameTable(
                name: "BloodComponent",
                newName: "BloodComponents");

            migrationBuilder.RenameTable(
                name: "Blog",
                newName: "Blogs");

            migrationBuilder.RenameIndex(
                name: "IX_User_RoleId",
                table: "Users",
                newName: "IX_Users_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_User_BloodGroupId",
                table: "Users",
                newName: "IX_Users_BloodGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_EmergencyBloodRequest_UserId",
                table: "EmergencyBloodRequests",
                newName: "IX_EmergencyBloodRequests_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_BlooodStorage_BloodGroupId",
                table: "BloodStorages",
                newName: "IX_BloodStorages_BloodGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_BlooodStorage_BloodDonateId",
                table: "BloodStorages",
                newName: "IX_BloodStorages_BloodDonateId");

            migrationBuilder.RenameIndex(
                name: "IX_BlooodStorage_BloodComponentId",
                table: "BloodStorages",
                newName: "IX_BloodStorages_BloodComponentId");

            migrationBuilder.RenameIndex(
                name: "IX_BloodIssue_EmergencyBloodRequestId",
                table: "BloodIssues",
                newName: "IX_BloodIssues_EmergencyBloodRequestId");

            migrationBuilder.RenameIndex(
                name: "IX_BloodIssue_BloodStorageId",
                table: "BloodIssues",
                newName: "IX_BloodIssues_BloodStorageId");

            migrationBuilder.RenameIndex(
                name: "IX_Blog_UserId",
                table: "Blogs",
                newName: "IX_Blogs_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmergencyBloodRequests",
                table: "EmergencyBloodRequests",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BloodStorages",
                table: "BloodStorages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BloodIssues",
                table: "BloodIssues",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BloodGroups",
                table: "BloodGroups",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BloodComponents",
                table: "BloodComponents",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Blogs",
                table: "Blogs",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "BloodDonationRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BloodType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReasonReject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DonatedDateRequest = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Volume = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodDonationRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BloodDonationRequests_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BloodDonationRequirement",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Requirement = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Actual = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPass = table.Column<bool>(type: "bit", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BloodDonationRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
                columns: new[] { "CreatedDate", "Name" },
                values: new object[] { new DateTime(2025, 5, 27, 22, 12, 28, 150, DateTimeKind.Local).AddTicks(3196), "NURSE" });

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

            migrationBuilder.CreateIndex(
                name: "IX_BloodDonationRequests_UserId",
                table: "BloodDonationRequests",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BloodDonationRequirement_BloodDonationRequestId",
                table: "BloodDonationRequirement",
                column: "BloodDonationRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Users_UserId",
                table: "Blogs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BloodIssues_BloodStorages_BloodStorageId",
                table: "BloodIssues",
                column: "BloodStorageId",
                principalTable: "BloodStorages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BloodIssues_EmergencyBloodRequests_EmergencyBloodRequestId",
                table: "BloodIssues",
                column: "EmergencyBloodRequestId",
                principalTable: "EmergencyBloodRequests",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BloodStorages_BloodComponents_BloodComponentId",
                table: "BloodStorages",
                column: "BloodComponentId",
                principalTable: "BloodComponents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BloodStorages_BloodDonationRequests_BloodDonateId",
                table: "BloodStorages",
                column: "BloodDonateId",
                principalTable: "BloodDonationRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BloodStorages_BloodGroups_BloodGroupId",
                table: "BloodStorages",
                column: "BloodGroupId",
                principalTable: "BloodGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmergencyBloodRequests_Users_UserId",
                table: "EmergencyBloodRequests",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_BloodGroups_BloodGroupId",
                table: "Users",
                column: "BloodGroupId",
                principalTable: "BloodGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Users_UserId",
                table: "Blogs");

            migrationBuilder.DropForeignKey(
                name: "FK_BloodIssues_BloodStorages_BloodStorageId",
                table: "BloodIssues");

            migrationBuilder.DropForeignKey(
                name: "FK_BloodIssues_EmergencyBloodRequests_EmergencyBloodRequestId",
                table: "BloodIssues");

            migrationBuilder.DropForeignKey(
                name: "FK_BloodStorages_BloodComponents_BloodComponentId",
                table: "BloodStorages");

            migrationBuilder.DropForeignKey(
                name: "FK_BloodStorages_BloodDonationRequests_BloodDonateId",
                table: "BloodStorages");

            migrationBuilder.DropForeignKey(
                name: "FK_BloodStorages_BloodGroups_BloodGroupId",
                table: "BloodStorages");

            migrationBuilder.DropForeignKey(
                name: "FK_EmergencyBloodRequests_Users_UserId",
                table: "EmergencyBloodRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_BloodGroups_BloodGroupId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "BloodDonationRequirement");

            migrationBuilder.DropTable(
                name: "BloodDonationRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmergencyBloodRequests",
                table: "EmergencyBloodRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BloodStorages",
                table: "BloodStorages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BloodIssues",
                table: "BloodIssues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BloodGroups",
                table: "BloodGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BloodComponents",
                table: "BloodComponents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Blogs",
                table: "Blogs");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "Role");

            migrationBuilder.RenameTable(
                name: "EmergencyBloodRequests",
                newName: "EmergencyBloodRequest");

            migrationBuilder.RenameTable(
                name: "BloodStorages",
                newName: "BlooodStorage");

            migrationBuilder.RenameTable(
                name: "BloodIssues",
                newName: "BloodIssue");

            migrationBuilder.RenameTable(
                name: "BloodGroups",
                newName: "BloodGroup");

            migrationBuilder.RenameTable(
                name: "BloodComponents",
                newName: "BloodComponent");

            migrationBuilder.RenameTable(
                name: "Blogs",
                newName: "Blog");

            migrationBuilder.RenameIndex(
                name: "IX_Users_RoleId",
                table: "User",
                newName: "IX_User_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_BloodGroupId",
                table: "User",
                newName: "IX_User_BloodGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_EmergencyBloodRequests_UserId",
                table: "EmergencyBloodRequest",
                newName: "IX_EmergencyBloodRequest_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_BloodStorages_BloodGroupId",
                table: "BlooodStorage",
                newName: "IX_BlooodStorage_BloodGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_BloodStorages_BloodDonateId",
                table: "BlooodStorage",
                newName: "IX_BlooodStorage_BloodDonateId");

            migrationBuilder.RenameIndex(
                name: "IX_BloodStorages_BloodComponentId",
                table: "BlooodStorage",
                newName: "IX_BlooodStorage_BloodComponentId");

            migrationBuilder.RenameIndex(
                name: "IX_BloodIssues_EmergencyBloodRequestId",
                table: "BloodIssue",
                newName: "IX_BloodIssue_EmergencyBloodRequestId");

            migrationBuilder.RenameIndex(
                name: "IX_BloodIssues_BloodStorageId",
                table: "BloodIssue",
                newName: "IX_BloodIssue_BloodStorageId");

            migrationBuilder.RenameIndex(
                name: "IX_Blogs_UserId",
                table: "Blog",
                newName: "IX_Blog_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Role",
                table: "Role",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmergencyBloodRequest",
                table: "EmergencyBloodRequest",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlooodStorage",
                table: "BlooodStorage",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BloodIssue",
                table: "BloodIssue",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BloodGroup",
                table: "BloodGroup",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BloodComponent",
                table: "BloodComponent",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Blog",
                table: "Blog",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "BloodRequest",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BloodType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DonatedDateRequest = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ReasonReject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BloodRequest_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BloodDonate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BloodRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Volume = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodDonate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BloodDonate_BloodRequest_BloodRequestId",
                        column: x => x.BloodRequestId,
                        principalTable: "BloodRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "BloodComponent",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b77"),
                column: "CreatedDate",
                value: new DateTime(2025, 5, 24, 20, 15, 26, 646, DateTimeKind.Local).AddTicks(7058));

            migrationBuilder.UpdateData(
                table: "BloodComponent",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b78"),
                column: "CreatedDate",
                value: new DateTime(2025, 5, 24, 20, 15, 26, 646, DateTimeKind.Local).AddTicks(7055));

            migrationBuilder.UpdateData(
                table: "BloodComponent",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b79"),
                column: "CreatedDate",
                value: new DateTime(2025, 5, 24, 20, 15, 26, 646, DateTimeKind.Local).AddTicks(7051));

            migrationBuilder.UpdateData(
                table: "BloodGroup",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b80"),
                column: "CreatedDate",
                value: new DateTime(2025, 5, 24, 20, 15, 26, 646, DateTimeKind.Local).AddTicks(6984));

            migrationBuilder.UpdateData(
                table: "BloodGroup",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b81"),
                column: "CreatedDate",
                value: new DateTime(2025, 5, 24, 20, 15, 26, 646, DateTimeKind.Local).AddTicks(6982));

            migrationBuilder.UpdateData(
                table: "BloodGroup",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b82"),
                column: "CreatedDate",
                value: new DateTime(2025, 5, 24, 20, 15, 26, 646, DateTimeKind.Local).AddTicks(6980));

            migrationBuilder.UpdateData(
                table: "BloodGroup",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b83"),
                column: "CreatedDate",
                value: new DateTime(2025, 5, 24, 20, 15, 26, 646, DateTimeKind.Local).AddTicks(6972));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b84"),
                columns: new[] { "CreatedDate", "Name" },
                values: new object[] { new DateTime(2025, 5, 24, 20, 15, 26, 646, DateTimeKind.Local).AddTicks(6631), "STAFF" });

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b85"),
                column: "CreatedDate",
                value: new DateTime(2025, 5, 24, 20, 15, 26, 646, DateTimeKind.Local).AddTicks(6629));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("859a4997-1ffa-4915-b50e-9a99e4147b86"),
                column: "CreatedDate",
                value: new DateTime(2025, 5, 24, 20, 15, 26, 646, DateTimeKind.Local).AddTicks(6590));

            migrationBuilder.CreateIndex(
                name: "IX_BloodDonate_BloodRequestId",
                table: "BloodDonate",
                column: "BloodRequestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BloodRequest_UserId",
                table: "BloodRequest",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blog_User_UserId",
                table: "Blog",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BloodIssue_BlooodStorage_BloodStorageId",
                table: "BloodIssue",
                column: "BloodStorageId",
                principalTable: "BlooodStorage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BloodIssue_EmergencyBloodRequest_EmergencyBloodRequestId",
                table: "BloodIssue",
                column: "EmergencyBloodRequestId",
                principalTable: "EmergencyBloodRequest",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BlooodStorage_BloodComponent_BloodComponentId",
                table: "BlooodStorage",
                column: "BloodComponentId",
                principalTable: "BloodComponent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlooodStorage_BloodDonate_BloodDonateId",
                table: "BlooodStorage",
                column: "BloodDonateId",
                principalTable: "BloodDonate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlooodStorage_BloodGroup_BloodGroupId",
                table: "BlooodStorage",
                column: "BloodGroupId",
                principalTable: "BloodGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmergencyBloodRequest_User_UserId",
                table: "EmergencyBloodRequest",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_BloodGroup_BloodGroupId",
                table: "User",
                column: "BloodGroupId",
                principalTable: "BloodGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Role_RoleId",
                table: "User",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

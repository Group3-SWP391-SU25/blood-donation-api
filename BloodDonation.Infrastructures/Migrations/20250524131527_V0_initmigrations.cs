using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BloodDonation.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class V0_initmigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BloodComponent",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StorageTemerature = table.Column<double>(type: "float", nullable: false),
                    ShelfLifeInDay = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodComponent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BloodGroup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdentityId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HashPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<bool>(type: "bit", nullable: false),
                    Addresss = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BloodGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_BloodGroup_BloodGroupId",
                        column: x => x.BloodGroupId,
                        principalTable: "BloodGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Blog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Html = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Imageurls = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blog_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BloodRequest",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BloodType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReasonReject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DonatedDateRequest = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
                name: "EmergencyBloodRequest",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Volume = table.Column<double>(type: "float", nullable: false),
                    ReasonReject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmergencyBloodRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmergencyBloodRequest_User_UserId",
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
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Volume = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BloodRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "BlooodStorage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Volume = table.Column<double>(type: "float", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiredDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BloodDonateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BloodComponentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BloodGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlooodStorage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlooodStorage_BloodComponent_BloodComponentId",
                        column: x => x.BloodComponentId,
                        principalTable: "BloodComponent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlooodStorage_BloodDonate_BloodDonateId",
                        column: x => x.BloodDonateId,
                        principalTable: "BloodDonate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlooodStorage_BloodGroup_BloodGroupId",
                        column: x => x.BloodGroupId,
                        principalTable: "BloodGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BloodIssue",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Volume = table.Column<double>(type: "float", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateIssue = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BloodStorageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmergencyBloodRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodIssue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BloodIssue_BlooodStorage_BloodStorageId",
                        column: x => x.BloodStorageId,
                        principalTable: "BlooodStorage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BloodIssue_EmergencyBloodRequest_EmergencyBloodRequestId",
                        column: x => x.EmergencyBloodRequestId,
                        principalTable: "EmergencyBloodRequest",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "BloodComponent",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "IsDeleted", "Name", "ShelfLifeInDay", "Status", "StorageTemerature", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("859a4997-1ffa-4915-b50e-9a99e4147b77"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 5, 24, 20, 15, 26, 646, DateTimeKind.Local).AddTicks(7058), false, "Platelets", 0, "", 0.0, null, null },
                    { new Guid("859a4997-1ffa-4915-b50e-9a99e4147b78"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 5, 24, 20, 15, 26, 646, DateTimeKind.Local).AddTicks(7055), false, "Plasma", 0, "", 0.0, null, null },
                    { new Guid("859a4997-1ffa-4915-b50e-9a99e4147b79"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 5, 24, 20, 15, 26, 646, DateTimeKind.Local).AddTicks(7051), false, "Red Blood Cells", 0, "", 0.0, null, null }
                });

            migrationBuilder.InsertData(
                table: "BloodGroup",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "IsDeleted", "Type", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("859a4997-1ffa-4915-b50e-9a99e4147b80"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 5, 24, 20, 15, 26, 646, DateTimeKind.Local).AddTicks(6984), false, "AB", null, null },
                    { new Guid("859a4997-1ffa-4915-b50e-9a99e4147b81"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 5, 24, 20, 15, 26, 646, DateTimeKind.Local).AddTicks(6982), false, "O", null, null },
                    { new Guid("859a4997-1ffa-4915-b50e-9a99e4147b82"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 5, 24, 20, 15, 26, 646, DateTimeKind.Local).AddTicks(6980), false, "B", null, null },
                    { new Guid("859a4997-1ffa-4915-b50e-9a99e4147b83"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 5, 24, 20, 15, 26, 646, DateTimeKind.Local).AddTicks(6972), false, "A", null, null }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "IsDeleted", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("859a4997-1ffa-4915-b50e-9a99e4147b84"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 5, 24, 20, 15, 26, 646, DateTimeKind.Local).AddTicks(6631), false, "STAFF", null, null },
                    { new Guid("859a4997-1ffa-4915-b50e-9a99e4147b85"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 5, 24, 20, 15, 26, 646, DateTimeKind.Local).AddTicks(6629), false, "MEMBER", null, null },
                    { new Guid("859a4997-1ffa-4915-b50e-9a99e4147b86"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 5, 24, 20, 15, 26, 646, DateTimeKind.Local).AddTicks(6590), false, "ADMIN", null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Blog_UserId",
                table: "Blog",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BloodDonate_BloodRequestId",
                table: "BloodDonate",
                column: "BloodRequestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BloodIssue_BloodStorageId",
                table: "BloodIssue",
                column: "BloodStorageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BloodIssue_EmergencyBloodRequestId",
                table: "BloodIssue",
                column: "EmergencyBloodRequestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BloodRequest_UserId",
                table: "BloodRequest",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BlooodStorage_BloodComponentId",
                table: "BlooodStorage",
                column: "BloodComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_BlooodStorage_BloodDonateId",
                table: "BlooodStorage",
                column: "BloodDonateId");

            migrationBuilder.CreateIndex(
                name: "IX_BlooodStorage_BloodGroupId",
                table: "BlooodStorage",
                column: "BloodGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_EmergencyBloodRequest_UserId",
                table: "EmergencyBloodRequest",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_BloodGroupId",
                table: "User",
                column: "BloodGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blog");

            migrationBuilder.DropTable(
                name: "BloodIssue");

            migrationBuilder.DropTable(
                name: "BlooodStorage");

            migrationBuilder.DropTable(
                name: "EmergencyBloodRequest");

            migrationBuilder.DropTable(
                name: "BloodComponent");

            migrationBuilder.DropTable(
                name: "BloodDonate");

            migrationBuilder.DropTable(
                name: "BloodRequest");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "BloodGroup");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}

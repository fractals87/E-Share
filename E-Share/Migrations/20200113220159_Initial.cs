using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace E_Share.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    Credit = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: false),
                    Cost = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Provinces",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Abbreviation = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provinces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RechargeStories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    Credit = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RechargeStories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RechargeStories_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Cap = table.Column<int>(nullable: false),
                    ProvinceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Avalailable",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DayOfWeek = table.Column<int>(nullable: false),
                    EndService = table.Column<TimeSpan>(nullable: false),
                    StartService = table.Column<TimeSpan>(nullable: false),
                    CityId = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avalailable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Avalailable_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Avalailable_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: false),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    BatteryResidue = table.Column<int>(nullable: false),
                    StatusId = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false),
                    CityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicles_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vehicles_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vehicles_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rides",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    VehicleId = table.Column<int>(nullable: false),
                    DateStart = table.Column<DateTime>(nullable: false),
                    DateStop = table.Column<DateTime>(nullable: false),
                    Price = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rides", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rides_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rides_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3f24398b-2651-449f-a9cb-5e3085746795", "c30e81ef-fb83-42ea-b793-ce9d31b13999", "Admin", "ADMIN" },
                    { "19d7d7b6-10f3-455c-8f40-010bf5970143", "abc5370d-5a74-4496-bd60-0efb4ebdd201", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Credit", "DateOfBirth", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Surname", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "4585db44-9cde-45fb-8777-4df85f8ca7b6", 0, "65e4c76c-d8e9-41a0-a3d1-9a3ad1359fde", 100.0, new DateTime(2020, 1, 13, 23, 1, 59, 649, DateTimeKind.Local).AddTicks(2434), "admin@test.it", true, false, null, "admin", "ADMIN@TEST.IT", "ADMIN@TEST.IT", "AQAAAAEAACcQAAAAEEcZps9iPvMzEqYyE0QHSBEdGVxJpSRToYehtZrPi4ov5CJ2xG8yd7GqaAtuA/go5g==", null, false, "", "test", false, "admin@test.it" },
                    { "445f85b8c-71fb-43f8-b025-080888bee57d", 0, "9ab9f454-c0e4-4792-b0f7-dfbc6ebdfad5", 100.0, new DateTime(2020, 1, 13, 23, 1, 59, 659, DateTimeKind.Local).AddTicks(3232), "user@test.it", true, false, null, "user", "USER@TEST.IT", "USER@TEST.IT", "AQAAAAEAACcQAAAAENQ6F15PQQD7spqeRADZR2i6LoSixSg07AIJ5arlcwFPyQf2SHH3PgmQG7NarSgB4Q==", null, false, "", "test", false, "user@test.it" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Cost", "Description" },
                values: new object[,]
                {
                    { 1, 0.5, "e-scooter" },
                    { 2, 1.0, "e-bike" },
                    { 3, 1.5, "e-car" }
                });

            migrationBuilder.InsertData(
                table: "Provinces",
                columns: new[] { "Id", "Abbreviation", "Name" },
                values: new object[,]
                {
                    { 1, "NO", "Novara" },
                    { 2, "Vercelli", "Vercelli" }
                });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "Description" },
                values: new object[,]
                {
                    { 1, "Disponibile" },
                    { 2, "In uso" },
                    { 3, "Manutenzione" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[,]
                {
                    { "4585db44-9cde-45fb-8777-4df85f8ca7b6", "3f24398b-2651-449f-a9cb-5e3085746795" },
                    { "445f85b8c-71fb-43f8-b025-080888bee57d", "19d7d7b6-10f3-455c-8f40-010bf5970143" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Cap", "Name", "ProvinceId" },
                values: new object[,]
                {
                    { 1, 0, "Novara", 1 },
                    { 2, 0, "Vercelli", 2 }
                });

            migrationBuilder.InsertData(
                table: "Avalailable",
                columns: new[] { "Id", "CategoryId", "CityId", "DayOfWeek", "EndService", "StartService" },
                values: new object[,]
                {
                    { 1, 1, 1, 1, new TimeSpan(0, 23, 0, 0, 0), new TimeSpan(0, 6, 0, 0, 0) },
                    { 41, 3, 2, 6, new TimeSpan(0, 23, 0, 0, 0), new TimeSpan(0, 6, 0, 0, 0) },
                    { 22, 1, 2, 1, new TimeSpan(0, 23, 0, 0, 0), new TimeSpan(0, 6, 0, 0, 0) },
                    { 23, 1, 2, 2, new TimeSpan(0, 23, 0, 0, 0), new TimeSpan(0, 6, 0, 0, 0) },
                    { 24, 1, 2, 3, new TimeSpan(0, 23, 0, 0, 0), new TimeSpan(0, 6, 0, 0, 0) },
                    { 25, 1, 2, 4, new TimeSpan(0, 23, 0, 0, 0), new TimeSpan(0, 6, 0, 0, 0) },
                    { 26, 1, 2, 5, new TimeSpan(0, 23, 0, 0, 0), new TimeSpan(0, 6, 0, 0, 0) },
                    { 27, 1, 2, 6, new TimeSpan(0, 23, 0, 0, 0), new TimeSpan(0, 6, 0, 0, 0) },
                    { 28, 1, 2, 0, new TimeSpan(0, 23, 0, 0, 0), new TimeSpan(0, 6, 0, 0, 0) },
                    { 42, 3, 2, 0, new TimeSpan(0, 23, 0, 0, 0), new TimeSpan(0, 6, 0, 0, 0) },
                    { 29, 2, 2, 1, new TimeSpan(0, 23, 0, 0, 0), new TimeSpan(0, 6, 0, 0, 0) },
                    { 31, 2, 2, 3, new TimeSpan(0, 23, 0, 0, 0), new TimeSpan(0, 6, 0, 0, 0) },
                    { 32, 2, 2, 4, new TimeSpan(0, 23, 0, 0, 0), new TimeSpan(0, 6, 0, 0, 0) },
                    { 33, 2, 2, 5, new TimeSpan(0, 23, 0, 0, 0), new TimeSpan(0, 6, 0, 0, 0) },
                    { 34, 2, 2, 6, new TimeSpan(0, 23, 0, 0, 0), new TimeSpan(0, 6, 0, 0, 0) },
                    { 35, 2, 2, 0, new TimeSpan(0, 23, 0, 0, 0), new TimeSpan(0, 6, 0, 0, 0) },
                    { 36, 3, 2, 1, new TimeSpan(0, 23, 0, 0, 0), new TimeSpan(0, 6, 0, 0, 0) },
                    { 37, 3, 2, 2, new TimeSpan(0, 23, 0, 0, 0), new TimeSpan(0, 6, 0, 0, 0) },
                    { 38, 3, 2, 3, new TimeSpan(0, 23, 0, 0, 0), new TimeSpan(0, 6, 0, 0, 0) },
                    { 30, 2, 2, 2, new TimeSpan(0, 23, 0, 0, 0), new TimeSpan(0, 6, 0, 0, 0) },
                    { 39, 3, 2, 4, new TimeSpan(0, 23, 0, 0, 0), new TimeSpan(0, 6, 0, 0, 0) },
                    { 40, 3, 2, 5, new TimeSpan(0, 23, 0, 0, 0), new TimeSpan(0, 6, 0, 0, 0) },
                    { 20, 3, 1, 6, new TimeSpan(0, 23, 0, 0, 0), new TimeSpan(0, 6, 0, 0, 0) },
                    { 2, 1, 1, 2, new TimeSpan(0, 23, 0, 0, 0), new TimeSpan(0, 6, 0, 0, 0) },
                    { 3, 1, 1, 3, new TimeSpan(0, 23, 0, 0, 0), new TimeSpan(0, 6, 0, 0, 0) },
                    { 4, 1, 1, 4, new TimeSpan(0, 23, 0, 0, 0), new TimeSpan(0, 6, 0, 0, 0) },
                    { 5, 1, 1, 5, new TimeSpan(0, 23, 0, 0, 0), new TimeSpan(0, 6, 0, 0, 0) },
                    { 6, 1, 1, 6, new TimeSpan(0, 23, 0, 0, 0), new TimeSpan(0, 6, 0, 0, 0) },
                    { 7, 1, 1, 0, new TimeSpan(0, 23, 0, 0, 0), new TimeSpan(0, 6, 0, 0, 0) },
                    { 8, 2, 1, 1, new TimeSpan(0, 23, 0, 0, 0), new TimeSpan(0, 6, 0, 0, 0) },
                    { 9, 2, 1, 2, new TimeSpan(0, 23, 0, 0, 0), new TimeSpan(0, 6, 0, 0, 0) },
                    { 21, 3, 1, 0, new TimeSpan(0, 23, 0, 0, 0), new TimeSpan(0, 6, 0, 0, 0) },
                    { 10, 2, 1, 3, new TimeSpan(0, 23, 0, 0, 0), new TimeSpan(0, 6, 0, 0, 0) },
                    { 12, 2, 1, 5, new TimeSpan(0, 23, 0, 0, 0), new TimeSpan(0, 6, 0, 0, 0) },
                    { 13, 2, 1, 6, new TimeSpan(0, 23, 0, 0, 0), new TimeSpan(0, 6, 0, 0, 0) },
                    { 14, 2, 1, 0, new TimeSpan(0, 23, 0, 0, 0), new TimeSpan(0, 6, 0, 0, 0) },
                    { 15, 3, 1, 1, new TimeSpan(0, 23, 0, 0, 0), new TimeSpan(0, 6, 0, 0, 0) },
                    { 16, 3, 1, 2, new TimeSpan(0, 23, 0, 0, 0), new TimeSpan(0, 6, 0, 0, 0) },
                    { 17, 3, 1, 3, new TimeSpan(0, 23, 0, 0, 0), new TimeSpan(0, 6, 0, 0, 0) },
                    { 18, 3, 1, 4, new TimeSpan(0, 23, 0, 0, 0), new TimeSpan(0, 6, 0, 0, 0) },
                    { 19, 3, 1, 5, new TimeSpan(0, 23, 0, 0, 0), new TimeSpan(0, 6, 0, 0, 0) },
                    { 11, 2, 1, 4, new TimeSpan(0, 23, 0, 0, 0), new TimeSpan(0, 6, 0, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "BatteryResidue", "CategoryId", "CityId", "Code", "Latitude", "Longitude", "StatusId" },
                values: new object[,]
                {
                    { 4, 100, 1, 2, "A2", 45.330682000000003, 8.4220570000000006, 1 },
                    { 3, 100, 3, 1, "C1", 45.44811, 8.6259420000000002, 1 },
                    { 2, 100, 2, 1, "B1", 45.452404000000001, 8.6213200000000008, 1 },
                    { 1, 100, 1, 1, "A1", 45.451097699999998, 8.6223253, 1 },
                    { 5, 100, 2, 2, "B2", 45.330514999999998, 8.4233569999999993, 1 },
                    { 6, 100, 3, 2, "C2", 45.330052000000002, 8.4214020000000005, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Avalailable_CategoryId",
                table: "Avalailable",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Avalailable_CityId",
                table: "Avalailable",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_ProvinceId",
                table: "Cities",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_RechargeStories_UserId",
                table: "RechargeStories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Rides_UserId",
                table: "Rides",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Rides_VehicleId",
                table: "Rides",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_CategoryId",
                table: "Vehicles",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_CityId",
                table: "Vehicles",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_StatusId",
                table: "Vehicles",
                column: "StatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Avalailable");

            migrationBuilder.DropTable(
                name: "RechargeStories");

            migrationBuilder.DropTable(
                name: "Rides");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropTable(
                name: "Provinces");
        }
    }
}

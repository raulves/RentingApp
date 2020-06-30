using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DAL.App.EF.Migrations
{
    public partial class InitialDbCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    DisplayName = table.Column<string>(maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LangStrs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 256, nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LangStrs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RentalPeriods",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 256, nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(maxLength: 64, nullable: false),
                    PeriodStart = table.Column<int>(nullable: false),
                    PeriodEnd = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalPeriods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<Guid>(nullable: false),
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
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 256, nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: false),
                    DescriptionId = table.Column<Guid>(nullable: false),
                    ParentCategoryId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_LangStrs_DescriptionId",
                        column: x => x.DescriptionId,
                        principalTable: "LangStrs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LangStrTranslation",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 256, nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: false),
                    Culture = table.Column<string>(maxLength: 5, nullable: false),
                    Value = table.Column<string>(maxLength: 10240, nullable: false),
                    LangStrId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LangStrTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LangStrTranslation_LangStrs_LangStrId",
                        column: x => x.LangStrId,
                        principalTable: "LangStrs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 256, nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: false),
                    DescriptionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentTypes_LangStrs_DescriptionId",
                        column: x => x.DescriptionId,
                        principalTable: "LangStrs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductDescriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 256, nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: false),
                    DescriptionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDescriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductDescriptions_LangStrs_DescriptionId",
                        column: x => x.DescriptionId,
                        principalTable: "LangStrs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppUserCompanies",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 256, nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: false),
                    AppUserId = table.Column<Guid>(nullable: false),
                    CompanyId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserCompanies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 256, nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: false),
                    AppUserId = table.Column<Guid>(nullable: false),
                    BookingNumber = table.Column<string>(maxLength: 64, nullable: false),
                    BookingDate = table.Column<DateTime>(nullable: false),
                    BookingStartDay = table.Column<DateTime>(nullable: false),
                    BookingEndDay = table.Column<DateTime>(nullable: false),
                    BookingPeriodDays = table.Column<int>(nullable: false),
                    PricePerDay = table.Column<decimal>(nullable: false),
                    VatPercent = table.Column<decimal>(nullable: false),
                    Vat = table.Column<decimal>(nullable: false),
                    BookingWithoutVat = table.Column<decimal>(nullable: false),
                    BookingTotal = table.Column<decimal>(nullable: false),
                    ItemId = table.Column<Guid>(nullable: false),
                    ItemOwnerId = table.Column<Guid>(nullable: true),
                    RenterId = table.Column<Guid>(nullable: true),
                    ItemOwnerCompanyId = table.Column<Guid>(nullable: true),
                    RenterCompanyId = table.Column<Guid>(nullable: true),
                    InvoiceId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 256, nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: false),
                    AppUserId = table.Column<Guid>(nullable: false),
                    CompanyName = table.Column<string>(maxLength: 64, nullable: false),
                    RegisterCode = table.Column<string>(maxLength: 64, nullable: false),
                    VatNumber = table.Column<string>(maxLength: 64, nullable: true),
                    Email = table.Column<string>(maxLength: 64, nullable: true),
                    Phone = table.Column<string>(maxLength: 64, nullable: true),
                    LocationId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 256, nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: false),
                    AppUserId = table.Column<Guid>(nullable: false),
                    Picture = table.Column<byte[]>(maxLength: 4096, nullable: true),
                    ItemId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 256, nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: false),
                    AppUserId = table.Column<Guid>(nullable: false),
                    InvoiceNumber = table.Column<string>(maxLength: 64, nullable: false),
                    InvoiceDate = table.Column<DateTime>(nullable: false),
                    VatPercent = table.Column<decimal>(nullable: false),
                    Vat = table.Column<decimal>(nullable: false),
                    InvoiceWithoutVat = table.Column<decimal>(nullable: false),
                    InvoiceTotal = table.Column<decimal>(nullable: false),
                    CompanyId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoices_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 256, nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: false),
                    AppUserId = table.Column<Guid>(nullable: false),
                    CategoryId = table.Column<Guid>(nullable: false),
                    ItemId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemDescriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 256, nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: false),
                    AppUserId = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(maxLength: 64, nullable: false),
                    ProductDescriptionId = table.Column<Guid>(nullable: false),
                    ItemId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemDescriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemDescriptions_ProductDescriptions_ProductDescriptionId",
                        column: x => x.ProductDescriptionId,
                        principalTable: "ProductDescriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemOwnerships",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 256, nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: false),
                    AppUserId = table.Column<Guid>(nullable: false),
                    CompanyId = table.Column<Guid>(nullable: true),
                    ItemId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemOwnerships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemOwnerships_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 256, nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: false),
                    AppUserId = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(maxLength: 64, nullable: false),
                    Brand = table.Column<string>(maxLength: 64, nullable: false),
                    Model = table.Column<string>(maxLength: 64, nullable: false),
                    LocationId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemsBooked",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 256, nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: false),
                    AppUserId = table.Column<Guid>(nullable: false),
                    DateFrom = table.Column<DateTime>(nullable: false),
                    DateTo = table.Column<DateTime>(nullable: false),
                    ItemId = table.Column<Guid>(nullable: false),
                    BookingId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemsBooked", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemsBooked_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemsBooked_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 256, nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: false),
                    AppUserId = table.Column<Guid>(nullable: false),
                    AddressLine = table.Column<string>(maxLength: 64, nullable: false),
                    City = table.Column<string>(maxLength: 64, nullable: false),
                    County = table.Column<string>(maxLength: 64, nullable: false),
                    Country = table.Column<string>(maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
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
                    FirstName = table.Column<string>(maxLength: 64, nullable: false),
                    LastName = table.Column<string>(maxLength: 64, nullable: false),
                    Phone = table.Column<string>(maxLength: 64, nullable: true),
                    LocationId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 256, nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: false),
                    AppUserId = table.Column<Guid>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    PaymentDate = table.Column<DateTime>(nullable: false),
                    PaymentTypeId = table.Column<Guid>(nullable: false),
                    InvoiceId = table.Column<Guid>(nullable: false),
                    CompanyId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Payments_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Payments_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payments_PaymentTypes_PaymentTypeId",
                        column: x => x.PaymentTypeId,
                        principalTable: "PaymentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Prices",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 256, nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: false),
                    AppUserId = table.Column<Guid>(nullable: false),
                    ItemPrice = table.Column<decimal>(nullable: false),
                    ItemId = table.Column<Guid>(nullable: false),
                    RentalPeriodId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prices_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Prices_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prices_RentalPeriods_RentalPeriodId",
                        column: x => x.RentalPeriodId,
                        principalTable: "RentalPeriods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserCompanies_AppUserId",
                table: "AppUserCompanies",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserCompanies_CompanyId",
                table: "AppUserCompanies",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

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
                name: "IX_AspNetUsers_LocationId",
                table: "AspNetUsers",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_AppUserId",
                table: "Bookings",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_InvoiceId",
                table: "Bookings",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ItemId",
                table: "Bookings",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ItemOwnerCompanyId",
                table: "Bookings",
                column: "ItemOwnerCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ItemOwnerId",
                table: "Bookings",
                column: "ItemOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_RenterCompanyId",
                table: "Bookings",
                column: "RenterCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_RenterId",
                table: "Bookings",
                column: "RenterId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_DescriptionId",
                table: "Categories",
                column: "DescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentCategoryId",
                table: "Categories",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_AppUserId",
                table: "Companies",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_LocationId",
                table: "Companies",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_AppUserId",
                table: "Images",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_ItemId",
                table: "Images",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_AppUserId",
                table: "Invoices",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_CompanyId",
                table: "Invoices",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemCategories_AppUserId",
                table: "ItemCategories",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemCategories_CategoryId",
                table: "ItemCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemCategories_ItemId",
                table: "ItemCategories",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemDescriptions_AppUserId",
                table: "ItemDescriptions",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemDescriptions_ItemId",
                table: "ItemDescriptions",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemDescriptions_ProductDescriptionId",
                table: "ItemDescriptions",
                column: "ProductDescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemOwnerships_AppUserId",
                table: "ItemOwnerships",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemOwnerships_CompanyId",
                table: "ItemOwnerships",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemOwnerships_ItemId",
                table: "ItemOwnerships",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_AppUserId",
                table: "Items",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_LocationId",
                table: "Items",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsBooked_AppUserId",
                table: "ItemsBooked",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsBooked_BookingId",
                table: "ItemsBooked",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsBooked_ItemId",
                table: "ItemsBooked",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_LangStrTranslation_LangStrId",
                table: "LangStrTranslation",
                column: "LangStrId");

            migrationBuilder.CreateIndex(
                name: "IX_LangStrTranslation_Culture_LangStrId",
                table: "LangStrTranslation",
                columns: new[] { "Culture", "LangStrId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Locations_AppUserId",
                table: "Locations",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_AppUserId",
                table: "Payments",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_CompanyId",
                table: "Payments",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_InvoiceId",
                table: "Payments",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PaymentTypeId",
                table: "Payments",
                column: "PaymentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTypes_DescriptionId",
                table: "PaymentTypes",
                column: "DescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Prices_AppUserId",
                table: "Prices",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Prices_ItemId",
                table: "Prices",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Prices_RentalPeriodId",
                table: "Prices",
                column: "RentalPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDescriptions_DescriptionId",
                table: "ProductDescriptions",
                column: "DescriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserCompanies_AspNetUsers_AppUserId",
                table: "AppUserCompanies",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserCompanies_Companies_CompanyId",
                table: "AppUserCompanies",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_AspNetUsers_AppUserId",
                table: "Bookings",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_AspNetUsers_ItemOwnerId",
                table: "Bookings",
                column: "ItemOwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_AspNetUsers_RenterId",
                table: "Bookings",
                column: "RenterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Companies_ItemOwnerCompanyId",
                table: "Bookings",
                column: "ItemOwnerCompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Companies_RenterCompanyId",
                table: "Bookings",
                column: "RenterCompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Invoices_InvoiceId",
                table: "Bookings",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Items_ItemId",
                table: "Bookings",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_AspNetUsers_AppUserId",
                table: "Companies",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Locations_LocationId",
                table: "Companies",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_AspNetUsers_AppUserId",
                table: "Images",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Items_ItemId",
                table: "Images",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_AspNetUsers_AppUserId",
                table: "Invoices",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemCategories_AspNetUsers_AppUserId",
                table: "ItemCategories",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemCategories_Items_ItemId",
                table: "ItemCategories",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemDescriptions_AspNetUsers_AppUserId",
                table: "ItemDescriptions",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemDescriptions_Items_ItemId",
                table: "ItemDescriptions",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemOwnerships_AspNetUsers_AppUserId",
                table: "ItemOwnerships",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemOwnerships_Items_ItemId",
                table: "ItemOwnerships",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_AspNetUsers_AppUserId",
                table: "Items",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Locations_LocationId",
                table: "Items",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemsBooked_AspNetUsers_AppUserId",
                table: "ItemsBooked",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_AspNetUsers_AppUserId",
                table: "Locations",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_AspNetUsers_AppUserId",
                table: "Locations");

            migrationBuilder.DropTable(
                name: "AppUserCompanies");

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
                name: "Images");

            migrationBuilder.DropTable(
                name: "ItemCategories");

            migrationBuilder.DropTable(
                name: "ItemDescriptions");

            migrationBuilder.DropTable(
                name: "ItemOwnerships");

            migrationBuilder.DropTable(
                name: "ItemsBooked");

            migrationBuilder.DropTable(
                name: "LangStrTranslation");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Prices");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "ProductDescriptions");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "PaymentTypes");

            migrationBuilder.DropTable(
                name: "RentalPeriods");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "LangStrs");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Locations");
        }
    }
}

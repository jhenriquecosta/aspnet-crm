using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SolidCRM.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AddressType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppSetting",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AppName = table.Column<string>(maxLength: 100, nullable: false),
                    AppShortName = table.Column<string>(maxLength: 50, nullable: true),
                    AppVersion = table.Column<string>(maxLength: 15, nullable: true),
                    IsToggleSidebar = table.Column<bool>(nullable: false),
                    IsBoxedLayout = table.Column<bool>(nullable: false),
                    IsFixedLayout = table.Column<bool>(nullable: false),
                    IsToggleRightSidebar = table.Column<bool>(nullable: false),
                    Skin = table.Column<string>(maxLength: 20, nullable: true),
                    FooterText = table.Column<string>(maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSetting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Blog",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 500, nullable: false),
                    Details = table.Column<string>(nullable: false),
                    AddedBy = table.Column<int>(nullable: true),
                    DateAdded = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<int>(nullable: true),
                    DateModied = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsPublished = table.Column<bool>(nullable: false),
                    ViewCount = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 120, nullable: false),
                    Details = table.Column<string>(maxLength: 300, nullable: false),
                    CategoryImage = table.Column<string>(maxLength: 200, nullable: true),
                    ParentId = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Category_Category_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    About = table.Column<string>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Website = table.Column<string>(maxLength: 100, nullable: true),
                    Phone = table.Column<string>(maxLength: 13, nullable: true),
                    Fax = table.Column<string>(maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContractStatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContractType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currency",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 30, nullable: true),
                    Code = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Direction",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Direction", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GeneralSetting",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SettingKey = table.Column<string>(maxLength: 50, nullable: false),
                    SettingValue = table.Column<string>(maxLength: 2000, nullable: false),
                    Description = table.Column<string>(maxLength: 100, nullable: true),
                    SettingGroup = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralSetting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LeadStatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MenuText = table.Column<string>(maxLength: 100, nullable: false),
                    MenuURL = table.Column<string>(maxLength: 400, nullable: false),
                    ParentId = table.Column<int>(nullable: true),
                    SortOrder = table.Column<int>(nullable: true),
                    MenuIcon = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Menu_Menu_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMode",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMode", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Priority",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 100, nullable: false),
                    ColorName = table.Column<string>(maxLength: 100, nullable: false),
                    SortOrder = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Priority", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuantityUnit",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuantityUnit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleName = table.Column<string>(maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Source",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Source", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 100, nullable: false),
                    ColorName = table.Column<string>(maxLength: 100, nullable: false),
                    SortOrder = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NewsMedia",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BlogId = table.Column<int>(nullable: false),
                    MediaFile = table.Column<string>(maxLength: 100, nullable: true),
                    DisplaySortOrder = table.Column<int>(nullable: true),
                    AddedBy = table.Column<int>(nullable: true),
                    DateAdded = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    DateModied = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsMedia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NewsMedia_Blog_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BlogCategory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BlogId = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false),
                    AddedBy = table.Column<int>(nullable: true),
                    DateAdded = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    DateModied = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogCategory_Blog_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlogCategory_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LedgerAccount",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    CurrencyId = table.Column<int>(nullable: false),
                    AccountCode = table.Column<string>(maxLength: 50, nullable: true),
                    AccountColor = table.Column<string>(maxLength: 10, nullable: true),
                    ParentId = table.Column<int>(nullable: true),
                    CompanyId = table.Column<int>(nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime", nullable: true),
                    AddedBy = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LedgerAccount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LedgerAccount_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LedgerAccount_LedgerAccount_ParentId",
                        column: x => x.ParentId,
                        principalTable: "LedgerAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClientAddress",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AddressTypeId = table.Column<int>(nullable: true),
                    Street = table.Column<string>(maxLength: 100, nullable: false),
                    City = table.Column<string>(maxLength: 100, nullable: false),
                    State = table.Column<string>(maxLength: 100, nullable: true),
                    ZipCode = table.Column<string>(maxLength: 10, nullable: true),
                    CountryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientAddress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientAddress_AddressType_AddressTypeId",
                        column: x => x.AddressTypeId,
                        principalTable: "AddressType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClientAddress_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(maxLength: 100, nullable: false),
                    Password = table.Column<string>(maxLength: 100, nullable: false),
                    FirstName = table.Column<string>(maxLength: 100, nullable: false),
                    LastName = table.Column<string>(maxLength: 100, nullable: true),
                    ProfilePicture = table.Column<string>(maxLength: 200, nullable: true),
                    Email = table.Column<string>(maxLength: 50, nullable: true),
                    Facebook = table.Column<string>(maxLength: 100, nullable: true),
                    LinkedIn = table.Column<string>(maxLength: 100, nullable: true),
                    Skype = table.Column<string>(maxLength: 100, nullable: true),
                    EmailSignature = table.Column<string>(maxLength: 500, nullable: true),
                    LanguageId = table.Column<int>(nullable: true),
                    DirectionId = table.Column<int>(nullable: true),
                    Phone = table.Column<string>(maxLength: 15, nullable: true),
                    ChangePasswordCode = table.Column<string>(maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Direction_DirectionId",
                        column: x => x.DirectionId,
                        principalTable: "Direction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_Language_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 200, nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime", nullable: false),
                    AddedBy = table.Column<int>(nullable: false),
                    CompanyId = table.Column<int>(nullable: false),
                    DebitLedgerAccountId = table.Column<int>(nullable: false),
                    DebitAmount = table.Column<decimal>(nullable: false),
                    CreditLedgerAccountId = table.Column<int>(nullable: false),
                    CreditAmount = table.Column<decimal>(nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<int>(nullable: true),
                    DateModied = table.Column<DateTime>(type: "datetime", nullable: true),
                    Attachment = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaction_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transaction_LedgerAccount_CreditLedgerAccountId",
                        column: x => x.CreditLedgerAccountId,
                        principalTable: "LedgerAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transaction_LedgerAccount_DebitLedgerAccountId",
                        column: x => x.DebitLedgerAccountId,
                        principalTable: "LedgerAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BillDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    PaymentModeId = table.Column<int>(nullable: false),
                    To = table.Column<string>(maxLength: 100, nullable: false),
                    OtherInvoiceCode = table.Column<string>(maxLength: 50, nullable: true),
                    Address = table.Column<string>(maxLength: 200, nullable: true),
                    CreatedBy = table.Column<int>(nullable: false),
                    CompanyId = table.Column<int>(nullable: true),
                    CountryId = table.Column<int>(nullable: false),
                    ZipCode = table.Column<string>(maxLength: 10, nullable: true),
                    Email = table.Column<string>(maxLength: 40, nullable: true),
                    Mobile = table.Column<string>(maxLength: 15, nullable: true),
                    ClientAddressId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoice_ClientAddress_ClientAddressId",
                        column: x => x.ClientAddressId,
                        principalTable: "ClientAddress",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoice_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoice_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invoice_PaymentMode_PaymentModeId",
                        column: x => x.PaymentModeId,
                        principalTable: "PaymentMode",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyClient",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CompanyId = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Email = table.Column<string>(maxLength: 50, nullable: true),
                    Phone = table.Column<string>(maxLength: 15, nullable: true),
                    VATNumber = table.Column<string>(maxLength: 50, nullable: true),
                    Latitude = table.Column<string>(maxLength: 50, nullable: true),
                    Longitude = table.Column<string>(maxLength: 100, nullable: true),
                    Address = table.Column<string>(maxLength: 100, nullable: true),
                    CountryId = table.Column<int>(nullable: true),
                    UserId = table.Column<int>(nullable: true),
                    CurrencyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyClient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyClient_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyClient_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyClient_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyClient_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FileManager",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: false),
                    Tags = table.Column<string>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    FileUrl = table.Column<string>(maxLength: 1000, nullable: false),
                    FileExtension = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileManager", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileManager_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lead",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Position = table.Column<string>(maxLength: 100, nullable: true),
                    Email = table.Column<string>(maxLength: 22, nullable: true),
                    Website = table.Column<string>(maxLength: 100, nullable: true),
                    Phone = table.Column<string>(maxLength: 15, nullable: true),
                    Company = table.Column<string>(maxLength: 100, nullable: true),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    LeadStatusId = table.Column<int>(nullable: false),
                    SourceId = table.Column<int>(nullable: false),
                    AssignedToUserId = table.Column<int>(nullable: true),
                    OnDated = table.Column<DateTime>(type: "datetime", nullable: false),
                    Address = table.Column<string>(maxLength: 500, nullable: true),
                    CountryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lead", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lead_User_AssignedToUserId",
                        column: x => x.AssignedToUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lead_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lead_LeadStatus_LeadStatusId",
                        column: x => x.LeadStatusId,
                        principalTable: "LeadStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lead_Source_SourceId",
                        column: x => x.SourceId,
                        principalTable: "Source",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MenuPermission",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MenuId = table.Column<int>(nullable: true),
                    RoleId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: true),
                    SortOrder = table.Column<int>(nullable: true),
                    IsCreate = table.Column<bool>(nullable: false),
                    IsRead = table.Column<bool>(nullable: false),
                    IsUpdate = table.Column<bool>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuPermission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuPermission_Menu_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuPermission_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuPermission_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Note = table.Column<string>(maxLength: 1000, nullable: false),
                    AddedByUserId = table.Column<int>(nullable: true),
                    DateAdded = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    DateModied = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notes_User_AddedByUserId",
                        column: x => x.AddedByUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleUser",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleUser_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleUser_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Todo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 200, nullable: false),
                    UserId = table.Column<int>(nullable: true),
                    DateAdded = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    DateModied = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Todo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Todo_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    InvoiceId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 200, nullable: true),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    Quantity = table.Column<decimal>(nullable: false),
                    UnitPrice = table.Column<decimal>(nullable: false),
                    QuantityUnitId = table.Column<int>(nullable: false),
                    Total = table.Column<decimal>(nullable: false),
                    Tax = table.Column<decimal>(nullable: true),
                    Discount = table.Column<decimal>(nullable: true),
                    Adjustment = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceItem_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceItem_QuantityUnit_QuantityUnitId",
                        column: x => x.QuantityUnitId,
                        principalTable: "QuantityUnit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contract",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Subject = table.Column<string>(maxLength: 100, nullable: false),
                    ContractValue = table.Column<decimal>(nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    CompanyClientId = table.Column<int>(nullable: false),
                    ContractTypeId = table.Column<int>(nullable: false),
                    ContractStatusId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contract", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contract_CompanyClient_CompanyClientId",
                        column: x => x.CompanyClientId,
                        principalTable: "CompanyClient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contract_ContractStatus_ContractStatusId",
                        column: x => x.ContractStatusId,
                        principalTable: "ContractStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contract_ContractType_ContractTypeId",
                        column: x => x.ContractTypeId,
                        principalTable: "ContractType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    AddedBy = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CompanyOfficeId = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: false),
                    CompanyClientId = table.Column<int>(nullable: true),
                    Details = table.Column<string>(maxLength: 2000, nullable: true),
                    TargetDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    DateAdded = table.Column<DateTime>(type: "datetime", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    StatusId = table.Column<int>(nullable: false),
                    PriorityId = table.Column<int>(nullable: false),
                    Tags = table.Column<string>(maxLength: 100, nullable: true),
                    Amount = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Project_CompanyClient_CompanyClientId",
                        column: x => x.CompanyClientId,
                        principalTable: "CompanyClient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Project_Priority_PriorityId",
                        column: x => x.PriorityId,
                        principalTable: "Priority",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Project_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 150, nullable: false),
                    TicketDetail = table.Column<string>(nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CompanyClientId = table.Column<int>(nullable: false),
                    StatusId = table.Column<int>(nullable: false),
                    PriorityId = table.Column<int>(nullable: false),
                    CompanyId = table.Column<int>(nullable: false),
                    AddedBy = table.Column<int>(nullable: true),
                    DateAdded = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    DateModied = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsDelete = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ticket_CompanyClient_CompanyClientId",
                        column: x => x.CompanyClientId,
                        principalTable: "CompanyClient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ticket_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ticket_Priority_PriorityId",
                        column: x => x.PriorityId,
                        principalTable: "Priority",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ticket_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectFile",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProjectId = table.Column<int>(nullable: false),
                    FileManagerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectFile_FileManager_FileManagerId",
                        column: x => x.FileManagerId,
                        principalTable: "FileManager",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectFile_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectMember",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProjectId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectMember", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectMember_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectMember_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectMileStone",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProjectId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: true),
                    Amount = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectMileStone", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectMileStone_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TicketTask",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TicketId = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    TaskDetail = table.Column<string>(maxLength: 1000, nullable: false),
                    StatusId = table.Column<int>(nullable: false),
                    PriorityId = table.Column<int>(nullable: false),
                    AddedBy = table.Column<int>(nullable: true),
                    DateAdded = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    DateModied = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsDelete = table.Column<bool>(nullable: true),
                    TakeTimeInHour = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketTask_Priority_PriorityId",
                        column: x => x.PriorityId,
                        principalTable: "Priority",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketTask_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketTask_Ticket_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Ticket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectTask",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProjectId = table.Column<int>(nullable: false),
                    TaskNames = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    DateAdded = table.Column<DateTime>(type: "datetime", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime", nullable: true),
                    AddedBy = table.Column<int>(nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    StatusId = table.Column<int>(nullable: false),
                    PriorityId = table.Column<int>(nullable: false),
                    ParentId = table.Column<int>(nullable: true),
                    ProjectMileStoneId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectTask_ProjectTask_ParentId",
                        column: x => x.ParentId,
                        principalTable: "ProjectTask",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectTask_Priority_PriorityId",
                        column: x => x.PriorityId,
                        principalTable: "Priority",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectTask_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectTask_ProjectMileStone_ProjectMileStoneId",
                        column: x => x.ProjectMileStoneId,
                        principalTable: "ProjectMileStone",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectTask_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogCategory_BlogId",
                table: "BlogCategory",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogCategory_CategoryId",
                table: "BlogCategory",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_ParentId",
                table: "Category",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientAddress_AddressTypeId",
                table: "ClientAddress",
                column: "AddressTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientAddress_CountryId",
                table: "ClientAddress",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyClient_CompanyId",
                table: "CompanyClient",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyClient_CountryId",
                table: "CompanyClient",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyClient_CurrencyId",
                table: "CompanyClient",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyClient_UserId",
                table: "CompanyClient",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_CompanyClientId",
                table: "Contract",
                column: "CompanyClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_ContractStatusId",
                table: "Contract",
                column: "ContractStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_ContractTypeId",
                table: "Contract",
                column: "ContractTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FileManager_UserId",
                table: "FileManager",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_ClientAddressId",
                table: "Invoice",
                column: "ClientAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_CompanyId",
                table: "Invoice",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_CountryId",
                table: "Invoice",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_PaymentModeId",
                table: "Invoice",
                column: "PaymentModeId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItem_InvoiceId",
                table: "InvoiceItem",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItem_QuantityUnitId",
                table: "InvoiceItem",
                column: "QuantityUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Lead_AssignedToUserId",
                table: "Lead",
                column: "AssignedToUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Lead_CountryId",
                table: "Lead",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Lead_LeadStatusId",
                table: "Lead",
                column: "LeadStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Lead_SourceId",
                table: "Lead",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_LedgerAccount_CompanyId",
                table: "LedgerAccount",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_LedgerAccount_ParentId",
                table: "LedgerAccount",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Menu_ParentId",
                table: "Menu",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuPermission_MenuId",
                table: "MenuPermission",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuPermission_RoleId",
                table: "MenuPermission",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuPermission_UserId",
                table: "MenuPermission",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsMedia_BlogId",
                table: "NewsMedia",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_AddedByUserId",
                table: "Notes",
                column: "AddedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_CompanyClientId",
                table: "Project",
                column: "CompanyClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_PriorityId",
                table: "Project",
                column: "PriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_StatusId",
                table: "Project",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectFile_FileManagerId",
                table: "ProjectFile",
                column: "FileManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectFile_ProjectId",
                table: "ProjectFile",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMember_ProjectId",
                table: "ProjectMember",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMember_UserId",
                table: "ProjectMember",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMileStone_ProjectId",
                table: "ProjectMileStone",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTask_ParentId",
                table: "ProjectTask",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTask_PriorityId",
                table: "ProjectTask",
                column: "PriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTask_ProjectId",
                table: "ProjectTask",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTask_ProjectMileStoneId",
                table: "ProjectTask",
                column: "ProjectMileStoneId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTask_StatusId",
                table: "ProjectTask",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleUser_RoleId",
                table: "RoleUser",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleUser_UserId",
                table: "RoleUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_CompanyClientId",
                table: "Ticket",
                column: "CompanyClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_CompanyId",
                table: "Ticket",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_PriorityId",
                table: "Ticket",
                column: "PriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_StatusId",
                table: "Ticket",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTask_PriorityId",
                table: "TicketTask",
                column: "PriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTask_StatusId",
                table: "TicketTask",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTask_TicketId",
                table: "TicketTask",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_Todo_UserId",
                table: "Todo",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_CompanyId",
                table: "Transaction",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_CreditLedgerAccountId",
                table: "Transaction",
                column: "CreditLedgerAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_DebitLedgerAccountId",
                table: "Transaction",
                column: "DebitLedgerAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_User_DirectionId",
                table: "User",
                column: "DirectionId");

            migrationBuilder.CreateIndex(
                name: "IX_User_LanguageId",
                table: "User",
                column: "LanguageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppSetting");

            migrationBuilder.DropTable(
                name: "BlogCategory");

            migrationBuilder.DropTable(
                name: "Contract");

            migrationBuilder.DropTable(
                name: "GeneralSetting");

            migrationBuilder.DropTable(
                name: "InvoiceItem");

            migrationBuilder.DropTable(
                name: "Lead");

            migrationBuilder.DropTable(
                name: "MenuPermission");

            migrationBuilder.DropTable(
                name: "NewsMedia");

            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "ProjectFile");

            migrationBuilder.DropTable(
                name: "ProjectMember");

            migrationBuilder.DropTable(
                name: "ProjectTask");

            migrationBuilder.DropTable(
                name: "RoleUser");

            migrationBuilder.DropTable(
                name: "TicketTask");

            migrationBuilder.DropTable(
                name: "Todo");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "ContractStatus");

            migrationBuilder.DropTable(
                name: "ContractType");

            migrationBuilder.DropTable(
                name: "Invoice");

            migrationBuilder.DropTable(
                name: "QuantityUnit");

            migrationBuilder.DropTable(
                name: "LeadStatus");

            migrationBuilder.DropTable(
                name: "Source");

            migrationBuilder.DropTable(
                name: "Menu");

            migrationBuilder.DropTable(
                name: "Blog");

            migrationBuilder.DropTable(
                name: "FileManager");

            migrationBuilder.DropTable(
                name: "ProjectMileStone");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Ticket");

            migrationBuilder.DropTable(
                name: "LedgerAccount");

            migrationBuilder.DropTable(
                name: "ClientAddress");

            migrationBuilder.DropTable(
                name: "PaymentMode");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "AddressType");

            migrationBuilder.DropTable(
                name: "CompanyClient");

            migrationBuilder.DropTable(
                name: "Priority");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "Currency");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Direction");

            migrationBuilder.DropTable(
                name: "Language");
        }
    }
}

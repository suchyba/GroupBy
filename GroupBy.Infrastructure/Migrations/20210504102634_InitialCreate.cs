using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupBy.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agreements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agreements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InventoryItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Descryption = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InventoryItemSources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryItemSources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HigherPositionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Positions_Positions_HigherPositionId",
                        column: x => x.HigherPositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ranks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HigherRankId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ranks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ranks_Ranks_HigherRankId",
                        column: x => x.HigherRankId,
                        principalTable: "Ranks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "Volunteers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstNames = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Confirmed = table.Column<bool>(type: "bit", nullable: false),
                    RankId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Volunteers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Volunteers_Ranks_RankId",
                        column: x => x.RankId,
                        principalTable: "Ranks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AgreementVolunteer",
                columns: table => new
                {
                    AgreementsId = table.Column<int>(type: "int", nullable: false),
                    VolunteersAcceptedId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgreementVolunteer", x => new { x.AgreementsId, x.VolunteersAcceptedId });
                    table.ForeignKey(
                        name: "FK_AgreementVolunteer_Agreements_AgreementsId",
                        column: x => x.AgreementsId,
                        principalTable: "Agreements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AgreementVolunteer_Volunteers_VolunteersAcceptedId",
                        column: x => x.VolunteersAcceptedId,
                        principalTable: "Volunteers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VolunteerId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Volunteers_VolunteerId",
                        column: x => x.VolunteerId,
                        principalTable: "Volunteers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "FinancialRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    BookOrderNumberId = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RelatedProjectId = table.Column<int>(type: "int", nullable: true),
                    RelatedDocumentId = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MembershipFee = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    ProgramFee = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Dotation = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    EarningAction = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    OnePercent = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    FinancialIncomeRecord_Other = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Inventory = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Material = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Food = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Service = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Transport = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Insurance = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Accommodation = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Salary = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Other = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialRecords", x => new { x.BookId, x.BookOrderNumberId, x.Id });
                });

            migrationBuilder.CreateTable(
                name: "RemainderVolunteer",
                columns: table => new
                {
                    RemaindersId = table.Column<int>(type: "int", nullable: false),
                    VolunteersAcceptedId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RemainderVolunteer", x => new { x.RemaindersId, x.VolunteersAcceptedId });
                    table.ForeignKey(
                        name: "FK_RemainderVolunteer_Volunteers_VolunteersAcceptedId",
                        column: x => x.VolunteersAcceptedId,
                        principalTable: "Volunteers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TODOListElements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Task = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderNumber = table.Column<int>(type: "int", nullable: false),
                    AsigneeId = table.Column<int>(type: "int", nullable: true),
                    Deadline = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TODOListElementId = table.Column<int>(type: "int", nullable: true),
                    TODOListId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TODOListElements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TODOListElements_TODOListElements_TODOListElementId",
                        column: x => x.TODOListElementId,
                        principalTable: "TODOListElements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TODOListElements_Volunteers_AsigneeId",
                        column: x => x.AsigneeId,
                        principalTable: "Volunteers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccountingBooks",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false),
                    BookOrderNumberId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Locked = table.Column<bool>(type: "bit", nullable: false),
                    RelatedGroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountingBooks", x => new { x.BookId, x.BookOrderNumberId });
                });

            migrationBuilder.CreateTable(
                name: "Elements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RelatedProjectId = table.Column<int>(type: "int", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Elements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupVolunteer",
                columns: table => new
                {
                    GroupsId = table.Column<int>(type: "int", nullable: false),
                    MembersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupVolunteer", x => new { x.GroupsId, x.MembersId });
                    table.ForeignKey(
                        name: "FK_GroupVolunteer_Volunteers_MembersId",
                        column: x => x.MembersId,
                        principalTable: "Volunteers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryBooks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryBooks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InventoryBookRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    InventoryBookId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Document = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Income = table.Column<bool>(type: "bit", nullable: false),
                    OrderNumber = table.Column<int>(type: "int", nullable: false),
                    SourceId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryBookRecords", x => new { x.InventoryBookId, x.Id });
                    table.ForeignKey(
                        name: "FK_InventoryBookRecords_InventoryBooks_InventoryBookId",
                        column: x => x.InventoryBookId,
                        principalTable: "InventoryBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryBookRecords_InventoryItems_ItemId",
                        column: x => x.ItemId,
                        principalTable: "InventoryItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryBookRecords_InventoryItemSources_SourceId",
                        column: x => x.SourceId,
                        principalTable: "InventoryItemSources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InvitationToGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TargetGroupId = table.Column<int>(type: "int", nullable: true),
                    InviterId = table.Column<int>(type: "int", nullable: true),
                    InvitedId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvitationToGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvitationToGroups_Volunteers_InvitedId",
                        column: x => x.InvitedId,
                        principalTable: "Volunteers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InvitationToGroups_Volunteers_InviterId",
                        column: x => x.InviterId,
                        principalTable: "Volunteers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    PositionId = table.Column<int>(type: "int", nullable: false),
                    Invitation = table.Column<bool>(type: "bit", nullable: true),
                    SeeElements = table.Column<bool>(type: "bit", nullable: true),
                    EditElements = table.Column<bool>(type: "bit", nullable: true),
                    Books = table.Column<bool>(type: "bit", nullable: true),
                    EditMembers = table.Column<bool>(type: "bit", nullable: true),
                    Resolutions = table.Column<bool>(type: "bit", nullable: true),
                    Subgroups = table.Column<bool>(type: "bit", nullable: true),
                    EditPosition = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => new { x.GroupId, x.PositionId });
                    table.ForeignKey(
                        name: "FK_Permissions_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PositionRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    VolunteerId = table.Column<int>(type: "int", nullable: false),
                    PositionId = table.Column<int>(type: "int", nullable: true),
                    AppointmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DismissDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AppointingResolutionGroupId = table.Column<int>(type: "int", nullable: true),
                    AppointingResolutionId = table.Column<int>(type: "int", nullable: true),
                    DismissingResolutionGroupId = table.Column<int>(type: "int", nullable: true),
                    DismissingResolutionId = table.Column<int>(type: "int", nullable: true),
                    RelatedGroupId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PositionRecords", x => new { x.VolunteerId, x.Id });
                    table.ForeignKey(
                        name: "FK_PositionRecords_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PositionRecords_Volunteers_VolunteerId",
                        column: x => x.VolunteerId,
                        principalTable: "Volunteers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BeginDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OwnerId = table.Column<int>(type: "int", nullable: false),
                    ParentGroupId = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Independent = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_Volunteers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Volunteers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentGroupId = table.Column<int>(type: "int", nullable: true),
                    OwnerId = table.Column<int>(type: "int", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Groups_Groups_ParentGroupId",
                        column: x => x.ParentGroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Groups_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Groups_Volunteers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Volunteers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RegistrationCodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TargetGroupId = table.Column<int>(type: "int", nullable: true),
                    TargetRankId = table.Column<int>(type: "int", nullable: true),
                    OwnerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrationCodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegistrationCodes_Groups_TargetGroupId",
                        column: x => x.TargetGroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RegistrationCodes_Ranks_TargetRankId",
                        column: x => x.TargetRankId,
                        principalTable: "Ranks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RegistrationCodes_Volunteers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Volunteers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Resolutions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LegislatorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resolutions", x => new { x.GroupId, x.Id });
                    table.ForeignKey(
                        name: "FK_Resolutions_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Resolutions_Volunteers_LegislatorId",
                        column: x => x.LegislatorId,
                        principalTable: "Volunteers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountingBooks_RelatedGroupId",
                table: "AccountingBooks",
                column: "RelatedGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_AgreementVolunteer_VolunteersAcceptedId",
                table: "AgreementVolunteer",
                column: "VolunteersAcceptedId");

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
                name: "IX_AspNetUsers_VolunteerId",
                table: "AspNetUsers",
                column: "VolunteerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Elements_GroupId",
                table: "Elements",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Elements_RelatedProjectId",
                table: "Elements",
                column: "RelatedProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialRecords_RelatedDocumentId",
                table: "FinancialRecords",
                column: "RelatedDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialRecords_RelatedProjectId",
                table: "FinancialRecords",
                column: "RelatedProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_OwnerId",
                table: "Groups",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_ParentGroupId",
                table: "Groups",
                column: "ParentGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_ProjectId",
                table: "Groups",
                column: "ProjectId",
                unique: true,
                filter: "[ProjectId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_GroupVolunteer_MembersId",
                table: "GroupVolunteer",
                column: "MembersId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryBookRecords_ItemId",
                table: "InventoryBookRecords",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryBookRecords_SourceId",
                table: "InventoryBookRecords",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryBooks_GroupId",
                table: "InventoryBooks",
                column: "GroupId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvitationToGroups_InvitedId",
                table: "InvitationToGroups",
                column: "InvitedId");

            migrationBuilder.CreateIndex(
                name: "IX_InvitationToGroups_InviterId",
                table: "InvitationToGroups",
                column: "InviterId");

            migrationBuilder.CreateIndex(
                name: "IX_InvitationToGroups_TargetGroupId",
                table: "InvitationToGroups",
                column: "TargetGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_PositionId",
                table: "Permissions",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_PositionRecords_AppointingResolutionGroupId_AppointingResolutionId",
                table: "PositionRecords",
                columns: new[] { "AppointingResolutionGroupId", "AppointingResolutionId" });

            migrationBuilder.CreateIndex(
                name: "IX_PositionRecords_DismissingResolutionGroupId_DismissingResolutionId",
                table: "PositionRecords",
                columns: new[] { "DismissingResolutionGroupId", "DismissingResolutionId" });

            migrationBuilder.CreateIndex(
                name: "IX_PositionRecords_PositionId",
                table: "PositionRecords",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_PositionRecords_RelatedGroupId",
                table: "PositionRecords",
                column: "RelatedGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_HigherPositionId",
                table: "Positions",
                column: "HigherPositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_OwnerId",
                table: "Projects",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ParentGroupId",
                table: "Projects",
                column: "ParentGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Ranks_HigherRankId",
                table: "Ranks",
                column: "HigherRankId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationCodes_OwnerId",
                table: "RegistrationCodes",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationCodes_TargetGroupId",
                table: "RegistrationCodes",
                column: "TargetGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationCodes_TargetRankId",
                table: "RegistrationCodes",
                column: "TargetRankId");

            migrationBuilder.CreateIndex(
                name: "IX_RemainderVolunteer_VolunteersAcceptedId",
                table: "RemainderVolunteer",
                column: "VolunteersAcceptedId");

            migrationBuilder.CreateIndex(
                name: "IX_Resolutions_LegislatorId",
                table: "Resolutions",
                column: "LegislatorId");

            migrationBuilder.CreateIndex(
                name: "IX_TODOListElements_AsigneeId",
                table: "TODOListElements",
                column: "AsigneeId");

            migrationBuilder.CreateIndex(
                name: "IX_TODOListElements_TODOListElementId",
                table: "TODOListElements",
                column: "TODOListElementId");

            migrationBuilder.CreateIndex(
                name: "IX_TODOListElements_TODOListId",
                table: "TODOListElements",
                column: "TODOListId");

            migrationBuilder.CreateIndex(
                name: "IX_Volunteers_RankId",
                table: "Volunteers",
                column: "RankId");

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialRecords_AccountingBooks_BookId_BookOrderNumberId",
                table: "FinancialRecords",
                columns: new[] { "BookId", "BookOrderNumberId" },
                principalTable: "AccountingBooks",
                principalColumns: new[] { "BookId", "BookOrderNumberId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialRecords_Elements_RelatedDocumentId",
                table: "FinancialRecords",
                column: "RelatedDocumentId",
                principalTable: "Elements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialRecords_Projects_RelatedProjectId",
                table: "FinancialRecords",
                column: "RelatedProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RemainderVolunteer_Elements_RemaindersId",
                table: "RemainderVolunteer",
                column: "RemaindersId",
                principalTable: "Elements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TODOListElements_Elements_TODOListId",
                table: "TODOListElements",
                column: "TODOListId",
                principalTable: "Elements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountingBooks_Groups_RelatedGroupId",
                table: "AccountingBooks",
                column: "RelatedGroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Elements_Groups_GroupId",
                table: "Elements",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Elements_Projects_RelatedProjectId",
                table: "Elements",
                column: "RelatedProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupVolunteer_Groups_GroupsId",
                table: "GroupVolunteer",
                column: "GroupsId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryBooks_Groups_GroupId",
                table: "InventoryBooks",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvitationToGroups_Groups_TargetGroupId",
                table: "InvitationToGroups",
                column: "TargetGroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_Groups_GroupId",
                table: "Permissions",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PositionRecords_Groups_RelatedGroupId",
                table: "PositionRecords",
                column: "RelatedGroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PositionRecords_Resolutions_AppointingResolutionGroupId_AppointingResolutionId",
                table: "PositionRecords",
                columns: new[] { "AppointingResolutionGroupId", "AppointingResolutionId" },
                principalTable: "Resolutions",
                principalColumns: new[] { "GroupId", "Id" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PositionRecords_Resolutions_DismissingResolutionGroupId_DismissingResolutionId",
                table: "PositionRecords",
                columns: new[] { "DismissingResolutionGroupId", "DismissingResolutionId" },
                principalTable: "Resolutions",
                principalColumns: new[] { "GroupId", "Id" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Groups_ParentGroupId",
                table: "Projects",
                column: "ParentGroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Groups_ParentGroupId",
                table: "Projects");

            migrationBuilder.DropTable(
                name: "AgreementVolunteer");

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
                name: "FinancialRecords");

            migrationBuilder.DropTable(
                name: "GroupVolunteer");

            migrationBuilder.DropTable(
                name: "InventoryBookRecords");

            migrationBuilder.DropTable(
                name: "InvitationToGroups");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "PositionRecords");

            migrationBuilder.DropTable(
                name: "RegistrationCodes");

            migrationBuilder.DropTable(
                name: "RemainderVolunteer");

            migrationBuilder.DropTable(
                name: "TODOListElements");

            migrationBuilder.DropTable(
                name: "Agreements");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "AccountingBooks");

            migrationBuilder.DropTable(
                name: "InventoryBooks");

            migrationBuilder.DropTable(
                name: "InventoryItems");

            migrationBuilder.DropTable(
                name: "InventoryItemSources");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "Resolutions");

            migrationBuilder.DropTable(
                name: "Elements");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Volunteers");

            migrationBuilder.DropTable(
                name: "Ranks");
        }
    }
}

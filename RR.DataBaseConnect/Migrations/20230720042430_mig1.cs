using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RR.DataBaseConnect.Migrations
{
    /// <inheritdoc />
    public partial class mig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RewardType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RewardTypes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RewardType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserNamePassword",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    employeeId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserNamePassword", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AwardCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdOfReward = table.Column<int>(type: "int", nullable: false),
                    RewardId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AwardCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AwardCategory_RewardType_RewardId",
                        column: x => x.RewardId,
                        principalTable: "RewardType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Campaigns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CampaignName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    votingDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RewardId = table.Column<int>(type: "int", nullable: false),
                    RewardTypesId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campaigns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Campaigns_RewardType_RewardTypesId",
                        column: x => x.RewardTypesId,
                        principalTable: "RewardType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Designation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserPassId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employee_UserNamePassword_UserPassId",
                        column: x => x.UserPassId,
                        principalTable: "UserNamePassword",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LeadCitation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NominatorId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Citation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CampaignId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadCitation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeadCitation_Campaigns_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "Campaigns",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmployeeRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdOfRole = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeRoles_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployeeRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PeerToPeerResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomineeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NominatorId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    awardCategory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Citation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdOfCampaign = table.Column<int>(type: "int", nullable: false),
                    IdOfNominee = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeerToPeerResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PeerToPeerResults_Campaigns_IdOfCampaign",
                        column: x => x.IdOfCampaign,
                        principalTable: "Campaigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PeerToPeerResults_Employee_IdOfNominee",
                        column: x => x.IdOfNominee,
                        principalTable: "Employee",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LeadCitationReplies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CampaignId = table.Column<int>(type: "int", nullable: false),
                    NominatorId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReplierId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReplyCitation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LeadCitationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadCitationReplies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeadCitationReplies_Campaigns_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "Campaigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LeadCitationReplies_LeadCitation_LeadCitationId",
                        column: x => x.LeadCitationId,
                        principalTable: "LeadCitation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OtherRewards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RewardId = table.Column<int>(type: "int", nullable: false),
                    CampaignId = table.Column<int>(type: "int", nullable: false),
                    NominatorId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomineeId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AwardCategory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    IdOFCampaign = table.Column<int>(type: "int", nullable: false),
                    IdOfCitation = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtherRewards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OtherRewards_Campaigns_IdOFCampaign",
                        column: x => x.IdOFCampaign,
                        principalTable: "Campaigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OtherRewards_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OtherRewards_LeadCitation_IdOfCitation",
                        column: x => x.IdOfCitation,
                        principalTable: "LeadCitation",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PeerToPeer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NominatorId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AwardCategory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Month = table.Column<int>(type: "int", nullable: true),
                    Citation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NomineeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CampaignId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    PeerToPeerResultsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeerToPeer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PeerToPeer_Campaigns_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "Campaigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PeerToPeer_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PeerToPeer_PeerToPeerResults_PeerToPeerResultsId",
                        column: x => x.PeerToPeerResultsId,
                        principalTable: "PeerToPeerResults",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OtherRewardResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RewardId = table.Column<int>(type: "int", nullable: false),
                    CampaignId = table.Column<int>(type: "int", nullable: false),
                    VoterId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NominatorId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomineeId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AwardCategory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Stars = table.Column<int>(type: "int", nullable: false),
                    CampaignName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomineeEmpId = table.Column<int>(type: "int", nullable: true),
                    CampId = table.Column<int>(type: "int", nullable: true),
                    IfOfNomination = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtherRewardResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OtherRewardResults_Campaigns_CampId",
                        column: x => x.CampId,
                        principalTable: "Campaigns",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OtherRewardResults_Employee_NomineeEmpId",
                        column: x => x.NomineeEmpId,
                        principalTable: "Employee",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OtherRewardResults_OtherRewards_IfOfNomination",
                        column: x => x.IfOfNomination,
                        principalTable: "OtherRewards",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AwardCategory_RewardId",
                table: "AwardCategory",
                column: "RewardId");

            migrationBuilder.CreateIndex(
                name: "IX_Campaigns_RewardTypesId",
                table: "Campaigns",
                column: "RewardTypesId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_EmployeeId",
                table: "Employee",
                column: "EmployeeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employee_UserPassId",
                table: "Employee",
                column: "UserPassId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeRoles_EmployeeId",
                table: "EmployeeRoles",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeRoles_RoleId",
                table: "EmployeeRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_LeadCitation_CampaignId",
                table: "LeadCitation",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_LeadCitationReplies_CampaignId",
                table: "LeadCitationReplies",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_LeadCitationReplies_LeadCitationId",
                table: "LeadCitationReplies",
                column: "LeadCitationId");

            migrationBuilder.CreateIndex(
                name: "IX_OtherRewardResults_CampId",
                table: "OtherRewardResults",
                column: "CampId");

            migrationBuilder.CreateIndex(
                name: "IX_OtherRewardResults_IfOfNomination",
                table: "OtherRewardResults",
                column: "IfOfNomination");

            migrationBuilder.CreateIndex(
                name: "IX_OtherRewardResults_NomineeEmpId",
                table: "OtherRewardResults",
                column: "NomineeEmpId");

            migrationBuilder.CreateIndex(
                name: "IX_OtherRewards_EmployeeId",
                table: "OtherRewards",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_OtherRewards_IdOFCampaign",
                table: "OtherRewards",
                column: "IdOFCampaign");

            migrationBuilder.CreateIndex(
                name: "IX_OtherRewards_IdOfCitation",
                table: "OtherRewards",
                column: "IdOfCitation");

            migrationBuilder.CreateIndex(
                name: "IX_PeerToPeer_CampaignId",
                table: "PeerToPeer",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_PeerToPeer_EmployeeId",
                table: "PeerToPeer",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_PeerToPeer_PeerToPeerResultsId",
                table: "PeerToPeer",
                column: "PeerToPeerResultsId");

            migrationBuilder.CreateIndex(
                name: "IX_PeerToPeerResults_IdOfCampaign",
                table: "PeerToPeerResults",
                column: "IdOfCampaign");

            migrationBuilder.CreateIndex(
                name: "IX_PeerToPeerResults_IdOfNominee",
                table: "PeerToPeerResults",
                column: "IdOfNominee");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AwardCategory");

            migrationBuilder.DropTable(
                name: "EmployeeRoles");

            migrationBuilder.DropTable(
                name: "LeadCitationReplies");

            migrationBuilder.DropTable(
                name: "OtherRewardResults");

            migrationBuilder.DropTable(
                name: "PeerToPeer");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "OtherRewards");

            migrationBuilder.DropTable(
                name: "PeerToPeerResults");

            migrationBuilder.DropTable(
                name: "LeadCitation");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Campaigns");

            migrationBuilder.DropTable(
                name: "UserNamePassword");

            migrationBuilder.DropTable(
                name: "RewardType");
        }
    }
}

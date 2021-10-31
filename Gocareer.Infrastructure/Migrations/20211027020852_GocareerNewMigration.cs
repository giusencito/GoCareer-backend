using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gocareer.Infrastructure.Migrations
{
    public partial class GocareerNewMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Career",
                columns: table => new
                {
                    Careerid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CareerName = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    CareerDescription = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Career", x => x.Careerid);
                });

            migrationBuilder.CreateTable(
                name: "Especialist",
                columns: table => new
                {
                    EspecialistId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EspecialistName = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    EspecialistLastName = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    EspecialistEmail = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    EspecialistPassword = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    EspecialistInformation = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Especialist", x => x.EspecialistId);
                });

            migrationBuilder.CreateTable(
                name: "Estudiante",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    UserLastname = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    Useremail = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    UserPassword = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estudiante", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Article",
                columns: table => new
                {
                    Articleid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticleName = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    ArticleDescription = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    Careerid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Article", x => x.Articleid);
                    table.ForeignKey(
                        name: "FK_Article_Career_Careerid",
                        column: x => x.Careerid,
                        principalTable: "Career",
                        principalColumn: "Careerid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Work",
                columns: table => new
                {
                    Workid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkName = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    WorkDescription = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    Careerid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Work", x => x.Workid);
                    table.ForeignKey(
                        name: "FK_Work_Career_Careerid",
                        column: x => x.Careerid,
                        principalTable: "Career",
                        principalColumn: "Careerid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Test",
                columns: table => new
                {
                    Testid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Personalized = table.Column<bool>(type: "bit", nullable: false),
                    EspecialistId = table.Column<int>(type: "int", nullable: false),
                    Testname = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Test", x => x.Testid);
                    table.ForeignKey(
                        name: "FK_Test_Especialist_EspecialistId",
                        column: x => x.EspecialistId,
                        principalTable: "Especialist",
                        principalColumn: "EspecialistId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Meeting",
                columns: table => new
                {
                    MeetingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Hour = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    EspecialistId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meeting", x => x.MeetingId);
                    table.ForeignKey(
                        name: "FK_Meeting_Especialist_EspecialistId",
                        column: x => x.EspecialistId,
                        principalTable: "Especialist",
                        principalColumn: "EspecialistId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Meeting_Estudiante_UserId",
                        column: x => x.UserId,
                        principalTable: "Estudiante",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    Messageid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MessageDescription = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    answer = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    EspecialistId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.Messageid);
                    table.ForeignKey(
                        name: "FK_Message_Especialist_EspecialistId",
                        column: x => x.EspecialistId,
                        principalTable: "Especialist",
                        principalColumn: "EspecialistId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Message_Estudiante_UserId",
                        column: x => x.UserId,
                        principalTable: "Estudiante",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    QuestionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    Testid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.QuestionId);
                    table.ForeignKey(
                        name: "FK_Question_Test_Testid",
                        column: x => x.Testid,
                        principalTable: "Test",
                        principalColumn: "Testid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User_Test",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Testid = table.Column<int>(type: "int", nullable: false),
                    Careerid = table.Column<int>(type: "int", nullable: false),
                    Releasedate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Test", x => new { x.UserId, x.Testid, x.Careerid });
                    table.ForeignKey(
                        name: "FK_User_Test_Career_Careerid",
                        column: x => x.Careerid,
                        principalTable: "Career",
                        principalColumn: "Careerid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_Test_Estudiante_UserId",
                        column: x => x.UserId,
                        principalTable: "Estudiante",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_Test_Test_Testid",
                        column: x => x.Testid,
                        principalTable: "Test",
                        principalColumn: "Testid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Option",
                columns: table => new
                {
                    Optionid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OptionName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Option", x => x.Optionid);
                    table.ForeignKey(
                        name: "FK_Option_Question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Article_Careerid",
                table: "Article",
                column: "Careerid");

            migrationBuilder.CreateIndex(
                name: "IX_Meeting_EspecialistId",
                table: "Meeting",
                column: "EspecialistId");

            migrationBuilder.CreateIndex(
                name: "IX_Meeting_UserId",
                table: "Meeting",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_EspecialistId",
                table: "Message",
                column: "EspecialistId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_UserId",
                table: "Message",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Option_QuestionId",
                table: "Option",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_Testid",
                table: "Question",
                column: "Testid");

            migrationBuilder.CreateIndex(
                name: "IX_Test_EspecialistId",
                table: "Test",
                column: "EspecialistId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Test_Careerid",
                table: "User_Test",
                column: "Careerid");

            migrationBuilder.CreateIndex(
                name: "IX_User_Test_Testid",
                table: "User_Test",
                column: "Testid");

            migrationBuilder.CreateIndex(
                name: "IX_Work_Careerid",
                table: "Work",
                column: "Careerid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Article");

            migrationBuilder.DropTable(
                name: "Meeting");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "Option");

            migrationBuilder.DropTable(
                name: "User_Test");

            migrationBuilder.DropTable(
                name: "Work");

            migrationBuilder.DropTable(
                name: "Question");

            migrationBuilder.DropTable(
                name: "Estudiante");

            migrationBuilder.DropTable(
                name: "Career");

            migrationBuilder.DropTable(
                name: "Test");

            migrationBuilder.DropTable(
                name: "Especialist");
        }
    }
}

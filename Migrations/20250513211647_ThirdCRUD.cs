using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GIF_S.Migrations
{
    /// <inheritdoc />
    public partial class ThirdCRUD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserCourses");

            migrationBuilder.AlterColumn<string>(
                name: "Review",
                table: "Rates",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "UserCourseId",
                table: "Rates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "UserCourseEnrolls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnrollmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Blocked = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CrsId = table.Column<int>(type: "int", nullable: false),
                    ProgId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCourseEnrolls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserCourseEnrolls_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCourseEnrolls_Courses_CrsId",
                        column: x => x.CrsId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCourseEnrolls_Progresses_ProgId",
                        column: x => x.ProgId,
                        principalTable: "Progresses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserCourseFavourites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CrsId = table.Column<int>(type: "int", nullable: false),
                    Favourite = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCourseFavourites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserCourseFavourites_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCourseFavourites_Courses_CrsId",
                        column: x => x.CrsId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rates_UserCourseId",
                table: "Rates",
                column: "UserCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCourseEnrolls_CrsId",
                table: "UserCourseEnrolls",
                column: "CrsId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCourseEnrolls_ProgId",
                table: "UserCourseEnrolls",
                column: "ProgId",
                unique: true,
                filter: "[ProgId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserCourseEnrolls_UserId",
                table: "UserCourseEnrolls",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCourseFavourites_CrsId",
                table: "UserCourseFavourites",
                column: "CrsId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCourseFavourites_UserId",
                table: "UserCourseFavourites",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rates_UserCourseEnrolls_UserCourseId",
                table: "Rates",
                column: "UserCourseId",
                principalTable: "UserCourseEnrolls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rates_UserCourseEnrolls_UserCourseId",
                table: "Rates");

            migrationBuilder.DropTable(
                name: "UserCourseEnrolls");

            migrationBuilder.DropTable(
                name: "UserCourseFavourites");

            migrationBuilder.DropIndex(
                name: "IX_Rates_UserCourseId",
                table: "Rates");

            migrationBuilder.DropColumn(
                name: "UserCourseId",
                table: "Rates");

            migrationBuilder.AlterColumn<string>(
                name: "Review",
                table: "Rates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "UserCourses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CrsId = table.Column<int>(type: "int", nullable: false),
                    ProgId = table.Column<int>(type: "int", nullable: false),
                    RateId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Blocked = table.Column<bool>(type: "bit", nullable: false),
                    EnrollmentDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCourses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserCourses_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCourses_Courses_CrsId",
                        column: x => x.CrsId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCourses_Progresses_ProgId",
                        column: x => x.ProgId,
                        principalTable: "Progresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCourses_Rates_RateId",
                        column: x => x.RateId,
                        principalTable: "Rates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserCourses_CrsId",
                table: "UserCourses",
                column: "CrsId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCourses_ProgId",
                table: "UserCourses",
                column: "ProgId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserCourses_RateId",
                table: "UserCourses",
                column: "RateId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCourses_UserId",
                table: "UserCourses",
                column: "UserId");
        }
    }
}

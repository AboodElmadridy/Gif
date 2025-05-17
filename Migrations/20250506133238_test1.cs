using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GIF_S.Migrations
{
    /// <inheritdoc />
    public partial class test1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isTrue",
                table: "Answers",
                newName: "IsTrue");

            migrationBuilder.AddColumn<DateTime>(
                name: "Duration",
                table: "SFiles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Duration",
                table: "Sections",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Quizzes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "Courses",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<short>(
                name: "Difficulty",
                table: "Courses",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Duration",
                table: "Courses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeCreated",
                table: "Courses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "RegisterationDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Courses_AuthorId",
                table: "Courses",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_AspNetUsers_AuthorId",
                table: "Courses",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_AspNetUsers_AuthorId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_AuthorId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "SFiles");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Difficulty",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "TimeCreated",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "RegisterationDate",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "IsTrue",
                table: "Answers",
                newName: "isTrue");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GIF_S.Migrations
{
    /// <inheritdoc />
    public partial class repos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "Rates",
                newName: "Id");

            migrationBuilder.AddColumn<bool>(
                name: "Blocked",
                table: "WishLists",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Blocked",
                table: "UserCourses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Blocked",
                table: "SFiles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Blocked",
                table: "Sections",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Blocked",
                table: "RoadMaps",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Blocked",
                table: "Rates",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Blocked",
                table: "Quizzes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Blocked",
                table: "Questions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Blocked",
                table: "Progresses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Blocked",
                table: "Courses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Blocked",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Blocked",
                table: "Answers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Blocked",
                table: "WishLists");

            migrationBuilder.DropColumn(
                name: "Blocked",
                table: "UserCourses");

            migrationBuilder.DropColumn(
                name: "Blocked",
                table: "SFiles");

            migrationBuilder.DropColumn(
                name: "Blocked",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "Blocked",
                table: "RoadMaps");

            migrationBuilder.DropColumn(
                name: "Blocked",
                table: "Rates");

            migrationBuilder.DropColumn(
                name: "Blocked",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "Blocked",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "Blocked",
                table: "Progresses");

            migrationBuilder.DropColumn(
                name: "Blocked",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Blocked",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Blocked",
                table: "Answers");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Rates",
                newName: "id");
        }
    }
}

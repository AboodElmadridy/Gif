using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GIF_S.Migrations
{
    /// <inheritdoc />
    public partial class SecondCRUD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Quizzes_QuizzesId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_QuizzesId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "QuizzesId",
                table: "Questions");

            migrationBuilder.AddColumn<int>(
                name: "QuizId",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Bio",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_QuizId",
                table: "Questions",
                column: "QuizId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Quizzes_QuizId",
                table: "Questions",
                column: "QuizId",
                principalTable: "Quizzes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Quizzes_QuizId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_QuizId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "QuizId",
                table: "Questions");

            migrationBuilder.AddColumn<int>(
                name: "QuizzesId",
                table: "Questions",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Bio",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Questions_QuizzesId",
                table: "Questions",
                column: "QuizzesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Quizzes_QuizzesId",
                table: "Questions",
                column: "QuizzesId",
                principalTable: "Quizzes",
                principalColumn: "Id");
        }
    }
}

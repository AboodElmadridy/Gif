using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GIF_S.Migrations
{
    /// <inheritdoc />
    public partial class fifthCRUD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TimeCreated",
                table: "Courses",
                newName: "TimeCreate");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdate",
                table: "Courses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "InstructorForms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExperienceQuestion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TopicQuestion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyQuestion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkedIn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CV = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InstructorId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstructorForms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstructorForms_AspNetUsers_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InstructorForms_InstructorId",
                table: "InstructorForms",
                column: "InstructorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InstructorForms");

            migrationBuilder.DropColumn(
                name: "LastUpdate",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "TimeCreate",
                table: "Courses",
                newName: "TimeCreated");
        }
    }
}

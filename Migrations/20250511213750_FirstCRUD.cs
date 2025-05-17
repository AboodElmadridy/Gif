using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GIF_S.Migrations
{
    /// <inheritdoc />
    public partial class FirstCRUD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_RoadMaps_RoadMapId",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "Image_Description",
                table: "RoadMaps",
                newName: "Image");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "RoadMaps",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "RoadMapId",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Bio",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CourseId",
                table: "Categories",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_RoadMaps_RoadMapId",
                table: "Courses",
                column: "RoadMapId",
                principalTable: "RoadMaps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_RoadMaps_RoadMapId",
                table: "Courses");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "RoadMaps");

            migrationBuilder.DropColumn(
                name: "Bio",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "RoadMaps",
                newName: "Image_Description");

            migrationBuilder.AlterColumn<int>(
                name: "RoadMapId",
                table: "Courses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_RoadMaps_RoadMapId",
                table: "Courses",
                column: "RoadMapId",
                principalTable: "RoadMaps",
                principalColumn: "Id");
        }
    }
}

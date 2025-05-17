using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GIF_S.Migrations
{
    /// <inheritdoc />
    public partial class fourthCRUD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_RoadMaps_RoadMapId",
                table: "Courses");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_RoadMaps_RoadMapId",
                table: "Courses");

            migrationBuilder.AlterColumn<int>(
                name: "RoadMapId",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_RoadMaps_RoadMapId",
                table: "Courses",
                column: "RoadMapId",
                principalTable: "RoadMaps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

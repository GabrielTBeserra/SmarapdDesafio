using Microsoft.EntityFrameworkCore.Migrations;

namespace SMARAPDDesafio.Migrations
{
    public partial class TituloPorAgendamento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Rooms");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Schedulings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Schedulings");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Rooms",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Api.Migrations
{
    public partial class Update_user : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Eliminado",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Genero",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "NombreCompleto",
                table: "AspNetUsers",
                newName: "Nationality");

            migrationBuilder.RenameColumn(
                name: "Nacionalidad",
                table: "AspNetUsers",
                newName: "CompletName");

            migrationBuilder.RenameColumn(
                name: "Edad",
                table: "AspNetUsers",
                newName: "Age");

            migrationBuilder.AddColumn<decimal>(
                name: "Balance",
                table: "AspNetUsers",
                type: "Money",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Balance",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Nationality",
                table: "AspNetUsers",
                newName: "NombreCompleto");

            migrationBuilder.RenameColumn(
                name: "CompletName",
                table: "AspNetUsers",
                newName: "Nacionalidad");

            migrationBuilder.RenameColumn(
                name: "Age",
                table: "AspNetUsers",
                newName: "Edad");

            migrationBuilder.AddColumn<bool>(
                name: "Eliminado",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Genero",
                table: "AspNetUsers",
                nullable: true);
        }
    }
}

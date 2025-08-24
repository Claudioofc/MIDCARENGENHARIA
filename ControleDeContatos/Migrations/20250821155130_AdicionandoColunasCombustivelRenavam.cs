using Microsoft.EntityFrameworkCore.Migrations;

namespace ControleDeContatos.Migrations
{
    public partial class AdicionandoColunasCombustivelRenavam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Combustivel",
                table: "Veiculos",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Renavam",
                table: "Veiculos",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Mecanico",
                table: "OrdensServico",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Combustivel",
                table: "Veiculos");

            migrationBuilder.DropColumn(
                name: "Renavam",
                table: "Veiculos");

            migrationBuilder.DropColumn(
                name: "Mecanico",
                table: "OrdensServico");
        }
    }
}

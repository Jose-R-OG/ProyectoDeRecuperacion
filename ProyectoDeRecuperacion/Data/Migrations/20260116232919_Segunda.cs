using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoDeRecuperacion.Migrations
{
    /// <inheritdoc />
    public partial class Segunda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_detalles_EntradaId",
                table: "detalles",
                column: "EntradaId");

            migrationBuilder.AddForeignKey(
                name: "FK_detalles_entradas_EntradaId",
                table: "detalles",
                column: "EntradaId",
                principalTable: "entradas",
                principalColumn: "EntradaId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_detalles_entradas_EntradaId",
                table: "detalles");

            migrationBuilder.DropIndex(
                name: "IX_detalles_EntradaId",
                table: "detalles");
        }
    }
}

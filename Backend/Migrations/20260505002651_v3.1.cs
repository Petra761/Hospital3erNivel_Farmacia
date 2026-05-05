using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class v31 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_posologias_tipos_unidad_medida_unidad_id",
                table: "posologias");

            migrationBuilder.DropIndex(
                name: "IX_posologias_unidad_id",
                table: "posologias");

            migrationBuilder.RenameColumn(
                name: "unidad_id",
                table: "posologias",
                newName: "unidad_medida");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "unidad_medida",
                table: "posologias",
                newName: "unidad_id");

            migrationBuilder.CreateIndex(
                name: "IX_posologias_unidad_id",
                table: "posologias",
                column: "unidad_id");

            migrationBuilder.AddForeignKey(
                name: "FK_posologias_tipos_unidad_medida_unidad_id",
                table: "posologias",
                column: "unidad_id",
                principalTable: "tipos_unidad_medida",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

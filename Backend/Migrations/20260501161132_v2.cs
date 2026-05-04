using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "recepciones",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    codigo = table.Column<string>(type: "text", nullable: false),
                    fecha_recepcion = table.Column<DateOnly>(type: "date", nullable: false),
                    recibido_por_codigo = table.Column<string>(type: "text", nullable: false),
                    estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recepciones", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "detalle_recepcion",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    recepcion_id = table.Column<int>(type: "integer", nullable: false),
                    medicamento_id = table.Column<int>(type: "integer", nullable: false),
                    cantidad_recibida = table.Column<int>(type: "integer", nullable: false),
                    estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_detalle_recepcion", x => x.id);
                    table.ForeignKey(
                        name: "FK_detalle_recepcion_medicamentos_medicamento_id",
                        column: x => x.medicamento_id,
                        principalTable: "medicamentos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_detalle_recepcion_recepciones_recepcion_id",
                        column: x => x.recepcion_id,
                        principalTable: "recepciones",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_lotes_detalle_recepcion_id",
                table: "lotes",
                column: "detalle_recepcion_id");

            migrationBuilder.CreateIndex(
                name: "IX_detalle_recepcion_medicamento_id",
                table: "detalle_recepcion",
                column: "medicamento_id");

            migrationBuilder.CreateIndex(
                name: "IX_detalle_recepcion_recepcion_id",
                table: "detalle_recepcion",
                column: "recepcion_id");

            migrationBuilder.AddForeignKey(
                name: "FK_lotes_detalle_recepcion_detalle_recepcion_id",
                table: "lotes",
                column: "detalle_recepcion_id",
                principalTable: "detalle_recepcion",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_lotes_detalle_recepcion_detalle_recepcion_id",
                table: "lotes");

            migrationBuilder.DropTable(
                name: "detalle_recepcion");

            migrationBuilder.DropTable(
                name: "recepciones");

            migrationBuilder.DropIndex(
                name: "IX_lotes_detalle_recepcion_id",
                table: "lotes");
        }
    }
}

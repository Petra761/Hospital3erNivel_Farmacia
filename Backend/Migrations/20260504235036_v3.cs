using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "recetas",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    codigo = table.Column<string>(type: "text", nullable: false),
                    paciente_codigo = table.Column<string>(type: "text", nullable: false),
                    medico_codigo = table.Column<string>(type: "text", nullable: false),
                    fecha_solicitud = table.Column<DateOnly>(type: "date", nullable: false),
                    estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recetas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "detalle_receta",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    receta_id = table.Column<int>(type: "integer", nullable: false),
                    medicamento_id = table.Column<int>(type: "integer", nullable: false),
                    cantidad_solicitada = table.Column<int>(type: "integer", nullable: false),
                    estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_detalle_receta", x => x.id);
                    table.ForeignKey(
                        name: "FK_detalle_receta_medicamentos_medicamento_id",
                        column: x => x.medicamento_id,
                        principalTable: "medicamentos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_detalle_receta_recetas_receta_id",
                        column: x => x.receta_id,
                        principalTable: "recetas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "dispensacion",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    codigo = table.Column<string>(type: "text", nullable: false),
                    receta_id = table.Column<int>(type: "integer", nullable: false),
                    farmaceutico_codigo = table.Column<string>(type: "text", nullable: false),
                    quien_recoge = table.Column<string>(type: "text", nullable: true),
                    fecha = table.Column<DateOnly>(type: "date", nullable: false),
                    estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dispensacion", x => x.id);
                    table.ForeignKey(
                        name: "FK_dispensacion_recetas_receta_id",
                        column: x => x.receta_id,
                        principalTable: "recetas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "posologias",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    codigo = table.Column<string>(type: "text", nullable: false),
                    detalle_receta_id = table.Column<int>(type: "integer", nullable: false),
                    dosis = table.Column<decimal>(type: "numeric", nullable: false),
                    unidad_id = table.Column<int>(type: "integer", nullable: false),
                    via_administracion = table.Column<string>(type: "text", nullable: false),
                    frecuencia = table.Column<string>(type: "text", nullable: false),
                    frecuencia_valor = table.Column<int>(type: "integer", nullable: false),
                    duracion = table.Column<string>(type: "text", nullable: false),
                    indicaciones_adicionales = table.Column<string>(type: "text", nullable: false),
                    estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_posologias", x => x.id);
                    table.ForeignKey(
                        name: "FK_posologias_detalle_receta_detalle_receta_id",
                        column: x => x.detalle_receta_id,
                        principalTable: "detalle_receta",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_posologias_tipos_unidad_medida_unidad_id",
                        column: x => x.unidad_id,
                        principalTable: "tipos_unidad_medida",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "dispensacion_lote",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    dispensacion_id = table.Column<int>(type: "integer", nullable: false),
                    stock_actual_id = table.Column<int>(type: "integer", nullable: false),
                    detalle_receta_id = table.Column<int>(type: "integer", nullable: false),
                    cantidad_entregada = table.Column<int>(type: "integer", nullable: false),
                    estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dispensacion_lote", x => x.id);
                    table.ForeignKey(
                        name: "FK_dispensacion_lote_detalle_receta_detalle_receta_id",
                        column: x => x.detalle_receta_id,
                        principalTable: "detalle_receta",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dispensacion_lote_dispensacion_dispensacion_id",
                        column: x => x.dispensacion_id,
                        principalTable: "dispensacion",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dispensacion_lote_stock_actual_stock_actual_id",
                        column: x => x.stock_actual_id,
                        principalTable: "stock_actual",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_detalle_receta_medicamento_id",
                table: "detalle_receta",
                column: "medicamento_id");

            migrationBuilder.CreateIndex(
                name: "IX_detalle_receta_receta_id",
                table: "detalle_receta",
                column: "receta_id");

            migrationBuilder.CreateIndex(
                name: "IX_dispensacion_receta_id",
                table: "dispensacion",
                column: "receta_id");

            migrationBuilder.CreateIndex(
                name: "IX_dispensacion_lote_detalle_receta_id",
                table: "dispensacion_lote",
                column: "detalle_receta_id");

            migrationBuilder.CreateIndex(
                name: "IX_dispensacion_lote_dispensacion_id",
                table: "dispensacion_lote",
                column: "dispensacion_id");

            migrationBuilder.CreateIndex(
                name: "IX_dispensacion_lote_stock_actual_id",
                table: "dispensacion_lote",
                column: "stock_actual_id");

            migrationBuilder.CreateIndex(
                name: "IX_posologias_detalle_receta_id",
                table: "posologias",
                column: "detalle_receta_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_posologias_unidad_id",
                table: "posologias",
                column: "unidad_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dispensacion_lote");

            migrationBuilder.DropTable(
                name: "posologias");

            migrationBuilder.DropTable(
                name: "dispensacion");

            migrationBuilder.DropTable(
                name: "detalle_receta");

            migrationBuilder.DropTable(
                name: "recetas");
        }
    }
}

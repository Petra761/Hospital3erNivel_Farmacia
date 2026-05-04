using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class v0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "formas_farmaceuticas",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "text", nullable: false),
                    estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_formas_farmaceuticas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tipos_medicamentos",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    codigo = table.Column<string>(type: "text", nullable: false),
                    nombre_generico = table.Column<string>(type: "text", nullable: false),
                    nombre_comercial = table.Column<string>(type: "text", nullable: false),
                    es_controlado = table.Column<bool>(type: "boolean", nullable: false),
                    requiere_refrigeracion = table.Column<bool>(type: "boolean", nullable: false),
                    stock_minimo_alerta = table.Column<int>(type: "integer", nullable: false),
                    estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tipos_medicamentos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tipos_unidad_medida",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "text", nullable: false),
                    abreviatura = table.Column<string>(type: "text", nullable: false),
                    estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tipos_unidad_medida", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "medicamentos",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    codigo = table.Column<string>(type: "text", nullable: false),
                    medicamento_id = table.Column<int>(type: "integer", nullable: false),
                    unidad_medida_id = table.Column<int>(type: "integer", nullable: false),
                    forma_id = table.Column<int>(type: "integer", nullable: false),
                    valor_concentracion = table.Column<decimal>(type: "numeric", nullable: false),
                    estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medicamentos", x => x.id);
                    table.ForeignKey(
                        name: "FK_medicamentos_formas_farmaceuticas_forma_id",
                        column: x => x.forma_id,
                        principalTable: "formas_farmaceuticas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_medicamentos_tipos_medicamentos_medicamento_id",
                        column: x => x.medicamento_id,
                        principalTable: "tipos_medicamentos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_medicamentos_tipos_unidad_medida_unidad_medida_id",
                        column: x => x.unidad_medida_id,
                        principalTable: "tipos_unidad_medida",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_medicamentos_forma_id",
                table: "medicamentos",
                column: "forma_id");

            migrationBuilder.CreateIndex(
                name: "IX_medicamentos_medicamento_id",
                table: "medicamentos",
                column: "medicamento_id");

            migrationBuilder.CreateIndex(
                name: "IX_medicamentos_unidad_medida_id",
                table: "medicamentos",
                column: "unidad_medida_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "medicamentos");

            migrationBuilder.DropTable(
                name: "formas_farmaceuticas");

            migrationBuilder.DropTable(
                name: "tipos_medicamentos");

            migrationBuilder.DropTable(
                name: "tipos_unidad_medida");
        }
    }
}

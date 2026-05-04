using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class v21 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fecha_vencimiento",
                table: "lotes");

            migrationBuilder.AddColumn<DateOnly>(
                name: "fecha_vencimiento",
                table: "detalle_recepcion",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fecha_vencimiento",
                table: "detalle_recepcion");

            migrationBuilder.AddColumn<DateOnly>(
                name: "fecha_vencimiento",
                table: "lotes",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CongresoAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Participantes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    Apellidos = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Twitter = table.Column<string>(type: "TEXT", nullable: false),
                    Ocupacion = table.Column<string>(type: "TEXT", nullable: false),
                    Avatar = table.Column<string>(type: "TEXT", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participantes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Participantes",
                columns: new[] { "Id", "Apellidos", "Avatar", "Email", "FechaRegistro", "Nombre", "Ocupacion", "Twitter" },
                values: new object[,]
                {
                    { 1, "Pérez", "https://i.pravatar.cc/150?img=1", "carlos.perez@example.com", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Carlos", "Desarrollador", "@carlosp" },
                    { 2, "Gómez", "https://i.pravatar.cc/150?img=2", "ana.gomez@example.com", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ana", "Diseñadora", "@anagomez" },
                    { 3, "Martínez", "https://i.pravatar.cc/150?img=3", "luis.martinez@example.com", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Luis", "Estudiante", "@luism" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Participantes");
        }
    }
}

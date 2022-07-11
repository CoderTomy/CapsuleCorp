using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CapsuleCorp.Migrations
{
    public partial class CapsuleCorpContextCapsuleCorpDatabaseContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Administradores",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    mail = table.Column<string>(nullable: false),
                    contrasenia = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administradores", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Pacientes",
                columns: table => new
                {
                    pacienteID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DNI = table.Column<int>(nullable: false),
                    nombre = table.Column<string>(maxLength: 20, nullable: false),
                    apellido = table.Column<string>(maxLength: 20, nullable: false),
                    mail = table.Column<string>(nullable: false),
                    contrasenia = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacientes", x => x.pacienteID);
                });

            migrationBuilder.CreateTable(
                name: "Turnos",
                columns: table => new
                {
                    turnoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fecha = table.Column<DateTime>(nullable: false),
                    especialidad = table.Column<int>(nullable: false),
                    pacienteID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turnos", x => x.turnoID);
                    table.ForeignKey(
                        name: "FK_Turnos_Pacientes_pacienteID",
                        column: x => x.pacienteID,
                        principalTable: "Pacientes",
                        principalColumn: "pacienteID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Turnos_pacienteID",
                table: "Turnos",
                column: "pacienteID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Administradores");

            migrationBuilder.DropTable(
                name: "Turnos");

            migrationBuilder.DropTable(
                name: "Pacientes");
        }
    }
}
using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace eCommerce.Office.Migrations
{
    /// <inheritdoc />
    public partial class BancoInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Colaboradores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colaboradores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Setores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Turmas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turmas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Veiculos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Placa = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veiculos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ColaboradoresSetores",
                columns: table => new
                {
                    ColaboradorId = table.Column<int>(type: "int", nullable: false),
                    SetorId = table.Column<int>(type: "int", nullable: false),
                    Criado = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColaboradoresSetores", x => new { x.SetorId, x.ColaboradorId });
                    table.ForeignKey(
                        name: "FK_ColaboradoresSetores_Colaboradores_ColaboradorId",
                        column: x => x.ColaboradorId,
                        principalTable: "Colaboradores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ColaboradoresSetores_Setores_SetorId",
                        column: x => x.SetorId,
                        principalTable: "Setores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ColaboradorTurma",
                columns: table => new
                {
                    ColaboradoresId = table.Column<int>(type: "int", nullable: false),
                    TurmasId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColaboradorTurma", x => new { x.ColaboradoresId, x.TurmasId });
                    table.ForeignKey(
                        name: "FK_ColaboradorTurma_Colaboradores_ColaboradoresId",
                        column: x => x.ColaboradoresId,
                        principalTable: "Colaboradores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ColaboradorTurma_Turmas_TurmasId",
                        column: x => x.TurmasId,
                        principalTable: "Turmas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ColaboradorVeiculo",
                columns: table => new
                {
                    ColaboradoresId = table.Column<int>(type: "int", nullable: false),
                    VeiculosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColaboradorVeiculo", x => new { x.ColaboradoresId, x.VeiculosId });
                    table.ForeignKey(
                        name: "FK_ColaboradorVeiculo_Colaboradores_ColaboradoresId",
                        column: x => x.ColaboradoresId,
                        principalTable: "Colaboradores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ColaboradorVeiculo_Veiculos_VeiculosId",
                        column: x => x.VeiculosId,
                        principalTable: "Veiculos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Colaboradores",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { 1, "Joel" },
                    { 2, "Maria" },
                    { 3, "Felipe" },
                    { 4, "Bento" },
                    { 5, "Endrick" },
                    { 6, "Gabriel" },
                    { 7, "Erika" }
                });

            migrationBuilder.InsertData(
                table: "Setores",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { 1, "Logística" },
                    { 2, "Separação" },
                    { 3, "Financeiro" },
                    { 4, "Administrativo" }
                });

            migrationBuilder.InsertData(
                table: "ColaboradoresSetores",
                columns: new[] { "ColaboradorId", "SetorId", "Criado" },
                values: new object[,]
                {
                    { 1, 1, new DateTimeOffset(new DateTime(2023, 11, 21, 7, 6, 35, 944, DateTimeKind.Unspecified).AddTicks(7900), new TimeSpan(0, -3, 0, 0, 0)) },
                    { 6, 1, new DateTimeOffset(new DateTime(2023, 11, 21, 7, 6, 35, 944, DateTimeKind.Unspecified).AddTicks(7935), new TimeSpan(0, -3, 0, 0, 0)) },
                    { 4, 2, new DateTimeOffset(new DateTime(2023, 11, 21, 7, 6, 35, 944, DateTimeKind.Unspecified).AddTicks(7938), new TimeSpan(0, -3, 0, 0, 0)) },
                    { 5, 2, new DateTimeOffset(new DateTime(2023, 11, 21, 7, 6, 35, 944, DateTimeKind.Unspecified).AddTicks(7936), new TimeSpan(0, -3, 0, 0, 0)) },
                    { 7, 3, new DateTimeOffset(new DateTime(2023, 11, 21, 7, 6, 35, 944, DateTimeKind.Unspecified).AddTicks(7939), new TimeSpan(0, -3, 0, 0, 0)) },
                    { 2, 4, new DateTimeOffset(new DateTime(2023, 11, 21, 7, 6, 35, 944, DateTimeKind.Unspecified).AddTicks(7941), new TimeSpan(0, -3, 0, 0, 0)) },
                    { 3, 4, new DateTimeOffset(new DateTime(2023, 11, 21, 7, 6, 35, 944, DateTimeKind.Unspecified).AddTicks(7942), new TimeSpan(0, -3, 0, 0, 0)) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ColaboradoresSetores_ColaboradorId",
                table: "ColaboradoresSetores",
                column: "ColaboradorId");

            migrationBuilder.CreateIndex(
                name: "IX_ColaboradorTurma_TurmasId",
                table: "ColaboradorTurma",
                column: "TurmasId");

            migrationBuilder.CreateIndex(
                name: "IX_ColaboradorVeiculo_VeiculosId",
                table: "ColaboradorVeiculo",
                column: "VeiculosId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ColaboradoresSetores");

            migrationBuilder.DropTable(
                name: "ColaboradorTurma");

            migrationBuilder.DropTable(
                name: "ColaboradorVeiculo");

            migrationBuilder.DropTable(
                name: "Setores");

            migrationBuilder.DropTable(
                name: "Turmas");

            migrationBuilder.DropTable(
                name: "Colaboradores");

            migrationBuilder.DropTable(
                name: "Veiculos");
        }
    }
}

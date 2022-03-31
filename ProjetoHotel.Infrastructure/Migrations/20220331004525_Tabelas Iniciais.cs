using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjetoHotel.Infrastructure.Migrations
{
    public partial class TabelasIniciais : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "hotel",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(nullable: true),
                    cnpj = table.Column<string>(nullable: true),
                    endereco = table.Column<string>(nullable: true),
                    descricao = table.Column<string>(nullable: true),
                    image_url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hotel", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "hotel_imagem",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    image_url = table.Column<string>(nullable: true),
                    hotel_id = table.Column<long>(nullable: false),
                    hotel_id1 = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hotel_imagem", x => x.id);
                    table.ForeignKey(
                        name: "FK_hotel_imagem_hotel_hotel_id1",
                        column: x => x.hotel_id1,
                        principalTable: "hotel",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "quarto",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(nullable: true),
                    numero_ocupantes = table.Column<int>(nullable: false),
                    numero_adultos = table.Column<int>(nullable: false),
                    numero_criancas = table.Column<string>(nullable: true),
                    preco = table.Column<decimal>(nullable: false),
                    hotel_id = table.Column<long>(nullable: false),
                    hotel_id1 = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_quarto", x => x.id);
                    table.ForeignKey(
                        name: "FK_quarto_hotel_hotel_id1",
                        column: x => x.hotel_id1,
                        principalTable: "hotel",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "quarto_imagem",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    image_url = table.Column<string>(nullable: true),
                    quarto_id = table.Column<long>(nullable: false),
                    quarto_id1 = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_quarto_imagem", x => x.id);
                    table.ForeignKey(
                        name: "FK_quarto_imagem_quarto_quarto_id1",
                        column: x => x.quarto_id1,
                        principalTable: "quarto",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_hotel_imagem_hotel_id1",
                table: "hotel_imagem",
                column: "hotel_id1");

            migrationBuilder.CreateIndex(
                name: "IX_quarto_hotel_id1",
                table: "quarto",
                column: "hotel_id1");

            migrationBuilder.CreateIndex(
                name: "IX_quarto_imagem_quarto_id1",
                table: "quarto_imagem",
                column: "quarto_id1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "hotel_imagem");

            migrationBuilder.DropTable(
                name: "quarto_imagem");

            migrationBuilder.DropTable(
                name: "quarto");

            migrationBuilder.DropTable(
                name: "hotel");
        }
    }
}

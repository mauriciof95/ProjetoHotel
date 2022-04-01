using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjetoHotel.Infrastructure.Migrations
{
    public partial class migracaoinicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "hotel",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(maxLength: 50, nullable: false),
                    cnpj = table.Column<string>(maxLength: 14, nullable: false),
                    endereco = table.Column<string>(maxLength: 100, nullable: false),
                    descricao = table.Column<string>(nullable: true)
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
                    image_url = table.Column<string>(nullable: false),
                    hotel_id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hotel_imagem", x => x.id);
                    table.ForeignKey(
                        name: "FK_hotel_imagem_hotel_hotel_id",
                        column: x => x.hotel_id,
                        principalTable: "hotel",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
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
                    numero_criancas = table.Column<int>(nullable: false),
                    preco = table.Column<decimal>(nullable: false),
                    hotel_id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_quarto", x => x.id);
                    table.ForeignKey(
                        name: "FK_quarto_hotel_hotel_id",
                        column: x => x.hotel_id,
                        principalTable: "hotel",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "quarto_imagem",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    image_url = table.Column<string>(nullable: false),
                    quarto_id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_quarto_imagem", x => x.id);
                    table.ForeignKey(
                        name: "FK_quarto_imagem_quarto_quarto_id",
                        column: x => x.quarto_id,
                        principalTable: "quarto",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_hotel_imagem_hotel_id",
                table: "hotel_imagem",
                column: "hotel_id");

            migrationBuilder.CreateIndex(
                name: "IX_quarto_hotel_id",
                table: "quarto",
                column: "hotel_id");

            migrationBuilder.CreateIndex(
                name: "IX_quarto_imagem_quarto_id",
                table: "quarto_imagem",
                column: "quarto_id");
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

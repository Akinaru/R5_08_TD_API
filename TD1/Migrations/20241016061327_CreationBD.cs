using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TD1.Migrations
{
    /// <inheritdoc />
    public partial class CreationBD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "marque",
                columns: table => new
                {
                    idmarque = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nommarque = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_marque", x => x.idmarque);
                });

            migrationBuilder.CreateTable(
                name: "typeproduit",
                columns: table => new
                {
                    idtypeproduit = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nomtypeproduit = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_typeproduit", x => x.idtypeproduit);
                });

            migrationBuilder.CreateTable(
                name: "produit",
                columns: table => new
                {
                    idproduit = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nomproduit = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    nomphoto = table.Column<string>(type: "text", nullable: true),
                    uriphoto = table.Column<string>(type: "text", nullable: true),
                    idtypeproduit = table.Column<int>(type: "integer", nullable: false),
                    idmarque = table.Column<int>(type: "integer", nullable: false),
                    stockreel = table.Column<int>(type: "integer", nullable: false),
                    stockmin = table.Column<int>(type: "integer", nullable: false),
                    stockmax = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_produit", x => x.idproduit);
                    table.ForeignKey(
                        name: "fk_produit_marque",
                        column: x => x.idmarque,
                        principalTable: "marque",
                        principalColumn: "idmarque");
                    table.ForeignKey(
                        name: "fk_produit_typeproduit",
                        column: x => x.idtypeproduit,
                        principalTable: "typeproduit",
                        principalColumn: "idtypeproduit");
                });

            migrationBuilder.InsertData(
                table: "marque",
                columns: new[] { "idmarque", "nommarque" },
                values: new object[] { 1, "Apple" });

            migrationBuilder.InsertData(
                table: "typeproduit",
                columns: new[] { "idtypeproduit", "nomtypeproduit" },
                values: new object[,]
                {
                    { 1, "Téléphone" },
                    { 2, "Montre connecté" }
                });

            migrationBuilder.InsertData(
                table: "produit",
                columns: new[] { "idproduit", "description", "idmarque", "idtypeproduit", "nomphoto", "nomproduit", "stockmax", "stockmin", "stockreel", "uriphoto" },
                values: new object[,]
                {
                    { 1, "Iphone dernière génération.", 1, 1, "photo1.jpg", "Iphone 16 Pro Max", 200, 10, 100, "https://www.cdiscount.com/pdt2/a/t/u/1/700x700/ip16prom512natu/rw/apple-iphone-16-pro-max-512gb-natural-titanium.jpg" },
                    { 2, "Iphone avant dernière génération.", 1, 1, "photo2.jpg", "Iphone 15 Pro Max", 100, 5, 50, "https://static.fnac-static.com/multimedia/Images/FR/MDM/34/f1/52/22212916/1540-1/tsp20240918073442/Apple-iPhone-15-Pro-Max-6-7-5G-Double-SIM-256-Go-Bleu-Titanium.jpg" },
                    { 3, "Iphone avant avant dernière génération.", 1, 1, "photo2.jpg", "Iphone 14 Pro Max", 100, 5, 50, "https://m.media-amazon.com/images/I/71yzJoE7WlL.__AC_SX300_SY300_QL70_ML2_.jpg" },
                    { 4, "Apple watch dernière génération.", 1, 2, "photo2.jpg", "Apple Watch S10", 100, 5, 50, "https://www.cdiscount.com/pdt2/l/s/m/1/700x700/wat10cel46silsm/rw/apple-watch-series-10-gps-cellular-46mm-boit.jpg" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_produit_idmarque",
                table: "produit",
                column: "idmarque");

            migrationBuilder.CreateIndex(
                name: "IX_produit_idtypeproduit",
                table: "produit",
                column: "idtypeproduit");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "produit");

            migrationBuilder.DropTable(
                name: "marque");

            migrationBuilder.DropTable(
                name: "typeproduit");
        }
    }
}

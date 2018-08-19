using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace InternetShopParser.Model.Database.Migrations
{
    public partial class _initCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    ImageSource = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(type: "Money", nullable: false),
                    Currency = table.Column<string>(maxLength: 20, nullable: true),
                    Descriptions = table.Column<string>(nullable: true),
                    DateCreate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductUpdetePrices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    DateUpdate = table.Column<DateTime>(nullable: false),
                    PriceUpdate = table.Column<decimal>(type: "Money", nullable: false),
                    ProductId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductUpdetePrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductUpdetePrices_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductUpdetePrices_ProductId",
                table: "ProductUpdetePrices",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductUpdetePrices");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace MicroserviceShoppingCart.Migrations
{
    public partial class Initial_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SessionCarts",
                columns: table => new
                {
                    SessionCartID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CreateAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionCarts", x => x.SessionCartID);
                });

            migrationBuilder.CreateTable(
                name: "SessionCartDetails",
                columns: table => new
                {
                    SessionCartDetailID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CreateAt = table.Column<DateTime>(nullable: false),
                    SelecctedItemID = table.Column<string>(nullable: true),
                    SessionCartID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionCartDetails", x => x.SessionCartDetailID);
                    table.ForeignKey(
                        name: "FK_SessionCartDetails_SessionCarts_SessionCartID",
                        column: x => x.SessionCartID,
                        principalTable: "SessionCarts",
                        principalColumn: "SessionCartID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SessionCartDetails_SessionCartID",
                table: "SessionCartDetails",
                column: "SessionCartID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SessionCartDetails");

            migrationBuilder.DropTable(
                name: "SessionCarts");
        }
    }
}

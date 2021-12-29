using Microsoft.EntityFrameworkCore.Migrations;

namespace RestApi2.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RestItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa_produktu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Ilosc = table.Column<byte>(type: "tinyint", nullable: false),
                    Zakupiony = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestItems", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RestItems");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;
//Klasa stworzona po tym jak :* dodałam klase Category -> Dodałam nuget packages: EntitieFrameworkCore.SqlServer i EntityFrameworkCiore.Tool
// -> Dodałam klasę (w folderze Data) ApplicationDbContext oraz connection string w appsettings.json -> 
//uruchomienie dwóch instrukcji w Package Manager Console: add-migration addCategoryToDatabe a później update-database (stworzenie bazy w sql server)
namespace FirstAspCoreProject.Migrations
{
    public partial class addCategoryToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    DisplayOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}

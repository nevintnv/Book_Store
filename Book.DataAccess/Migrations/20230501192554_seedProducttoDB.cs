using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Book.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class seedProducttoDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ISBN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListPrice = table.Column<double>(type: "float", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Price50 = table.Column<double>(type: "float", nullable: false),
                    Price100 = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Author", "Description", "ISBN", "ListPrice", "Price", "Price100", "Price50", "Title" },
                values: new object[,]
                {
                    { 1, "Mark Twain", "In the adventures of Huckleberry Finn, a sequel to the adventures of Tom Sawyer, Huck escapes from the clutches of his abusive drunk father ‘pap’, and the ‘sivilizing’ guardian widow Douglas", "SWD0009998888", 99.0, 90.0, 80.0, 85.0, "The Adventures of Huckleberry Finn" },
                    { 2, "F. Scott Fitzgerald ", "Fingerprint! Pocket Classics are perfect pocket-sized editions with complete original content.", "FFDS00998886655", 90.0, 87.0, 80.0, 85.0, "The Great Gatsby" },
                    { 3, "George Orwell", "1984: A Novel, unleashes a unique plot as per which No One is Safe or Free. No place is safe to run or even hide from a dominating party leader, Big Brother, who is considered equal to God.", "ASD23445544", 100.0, 90.0, 82.0, 87.0, "1984" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}

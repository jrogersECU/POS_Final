using Microsoft.EntityFrameworkCore.Migrations;

public partial class InitialCreate : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Products",
            columns: table => new
            {
                Product_Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(maxLength: 100, nullable: false),
                Price = table.Column<decimal>(nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Products", x => x.Product_Id);
            });

        migrationBuilder.CreateTable(
            name: "Inventories",
            columns: table => new
            {
                InventoryId = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                QuantityOnHand = table.Column<int>(nullable: false),
                ReorderPoint = table.Column<int>(nullable: false),
                Supplier = table.Column<string>(nullable: true),
                Product_Id = table.Column<int>(nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Inventories", x => x.InventoryId);
                table.ForeignKey(
                    name: "FK_Inventories_Products_Product_Id",
                    column: x => x.Product_Id,
                    principalTable: "Products",
                    principalColumn: "Product_Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "TransactionItems",
            columns: table => new
            {
                TransactionItemId = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_TransactionItems", x => x.TransactionItemId);
            });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "TransactionItems");

        migrationBuilder.DropTable(
            name: "Inventories");

        migrationBuilder.DropTable(
            name: "Products");
    }
}

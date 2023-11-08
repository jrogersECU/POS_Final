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
                    name: "FK_Inventories_Products_ProductId",
                    column: x => x.Product_Id,
                    principalTable: "Products",
                    principalColumn: "ProductId",
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

             
        // Insert predefined users with roles into the Users table
        migrationBuilder.InsertData(
            table: "Users",
            columns: new[] { "Username", "Email", "PasswordHash", "PasswordSalt", "Role" },
            values: new object[] 
            {

                "user1", "user1@example.com", "hashed_password_user1", "salt_user1", "User"
            
            });

        migrationBuilder.InsertData(
            table: "Users",
            columns: new[] { "Username", "Email", "PasswordHash", "PasswordSalt", "Role" },
            values: new object[] 
            { 

            "admin1", "admin1@example.com", "hashed_password_admin1", "salt_admin1", "Admin"

            });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData( 
            table: "Users",
            keyColumn: "Username",
            keyValues: new string[]{ "user1" });
        
        migrationBuilder.DeleteData(
            table: "Users", 
            keyColumn: "Username", 
            keyValues: new[] { "admin1" });
        
        migrationBuilder.DropTable(
            name: "TransactionItems");

        migrationBuilder.DropTable(
            name: "Inventories");

        migrationBuilder.DropTable(
            name: "Products");
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShopeeApp.Migrations
{
    /// <inheritdoc />
    public partial class AddFileNameToProductImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "ProductImages",
                newName: "FileName");

            migrationBuilder.UpdateData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 1,
                column: "FileName",
                value: "iPhone17_1.jpg");

            migrationBuilder.UpdateData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FileName", "ProductId" },
                values: new object[] { "iPhone17_2.jpg", 1 });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "Id", "CategoryId", "FileName", "ProductId" },
                values: new object[,]
                {
                    { 3, null, "iPhone17_3.jpg", 1 },
                    { 4, null, "oppo_Reno14_1.jpg", 2 },
                    { 5, null, "oppo_Reno14_2.jpg", 2 }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "Price" },
                values: new object[] { "iPhone 17 Pro Max APPLE (6.9'' - 256 GB)", 14999.00m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CategoryId", "Description", "Name", "Price" },
                values: new object[] { 1, "Новий SAMSUNG", "Smartphone SAMSUNG Galaxy S25 FE (6.7'' - 8 GB - 512 GB)", 1082.10m });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 3, 1, "Потужний ноутбук", "Smartphone OPPO Reno 14 (6.59'' - 12 GB - 512 GB)", 649.99m },
                    { 4, 2, "Потужний ноутбук", "Portátil HP Laptop 15-fd0019np (15.6\" - Intel Core i3 N305 - RAM:8 GB - 256 GB SSD - Intel UHD)", 549.99m },
                    { 5, 2, "Потужний ноутбук", "Portátil HP OmniBook Ultra Flip 14-fh0005np (14'' - Intel Core Ultra 7 256V - RAM: 16 GB - 1 TB SSD - Intel Arc Graphics)", 1899.99m },
                    { 6, 2, "Portátil ASUS Vivobook M1502YA (15.6\" - RYZEN 7 7730U - Ram: 16GB - 1TB)", "MacBook Pro", 749.99m }
                });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "Id", "CategoryId", "FileName", "ProductId" },
                values: new object[,]
                {
                    { 6, null, "samsung_phone_1.jpg", 3 },
                    { 7, null, "samsung_phone_2.jpg", 3 },
                    { 8, null, "samsung_phone_3.jpg", 3 },
                    { 9, null, "asus_1.jpg", 4 },
                    { 10, null, "hp_1.jpg", 5 },
                    { 11, null, "hp_2.jpg", 6 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "ProductImages",
                newName: "ImageUrl");

            migrationBuilder.UpdateData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://example.com/iphone.jpg");

            migrationBuilder.UpdateData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ImageUrl", "ProductId" },
                values: new object[] { "https://example.com/macbook.jpg", 2 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "Price" },
                values: new object[] { "iPhone 15", 999.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CategoryId", "Description", "Name", "Price" },
                values: new object[] { 2, "Потужний ноутбук", "MacBook Pro", 1999.99m });
        }
    }
}

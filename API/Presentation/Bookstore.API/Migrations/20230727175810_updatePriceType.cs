using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookstore.API.Migrations
{
    /// <inheritdoc />
    public partial class updatePriceType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Books",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,0)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Books",
                type: "decimal(18,0)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}

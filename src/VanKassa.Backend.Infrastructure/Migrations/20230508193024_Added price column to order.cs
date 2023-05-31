using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VanKassa.Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Addedpricecolumntoorder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "price",
                schema: "dbo",
                table: "order",
                type: "numeric(10)",
                precision: 10,
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "price",
                schema: "dbo",
                table: "order");
        }
    }
}

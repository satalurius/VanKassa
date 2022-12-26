using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VanKassa.Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangedOutletStreetType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "street_number",
                table: "outlet",
                type: "VARCHAR",
                maxLength: 15,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(10,5)",
                oldPrecision: 10,
                oldScale: 5,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "street_number",
                table: "outlet",
                type: "numeric(10,5)",
                precision: 10,
                scale: 5,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR",
                oldMaxLength: 15,
                oldNullable: true);
        }
    }
}

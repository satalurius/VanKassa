using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace VanKassa.Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderOutletLink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "outlet_id",
                schema: "dbo",
                table: "order",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "ix_orders_outlet_id",
                schema: "dbo",
                table: "order",
                column: "outlet_id");

            migrationBuilder.AddForeignKey(
                name: "fk_orders_outlets_outlet_id",
                schema: "dbo",
                table: "order",
                column: "outlet_id",
                principalSchema: "dbo",
                principalTable: "outlet",
                principalColumn: "outlet_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_orders_outlets_outlet_id",
                schema: "dbo",
                table: "order");

            migrationBuilder.DropIndex(
                name: "ix_orders_outlet_id",
                schema: "dbo",
                table: "order");

            migrationBuilder.DropColumn(
                name: "outlet_id",
                schema: "dbo",
                table: "order");

            migrationBuilder.AlterColumn<int>(
                name: "order_id",
                schema: "dbo",
                table: "order_product",
                type: "integer",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<int>(
                name: "order_id",
                schema: "dbo",
                table: "order",
                type: "integer",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);
        }
    }
}

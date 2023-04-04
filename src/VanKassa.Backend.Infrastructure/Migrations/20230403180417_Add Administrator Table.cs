using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace VanKassa.Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAdministratorTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "administrator",
                schema: "dbo",
                columns: table => new
                {
                    userid = table.Column<int>(name: "user_id", type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    phone = table.Column<string>(type: "VARCHAR", maxLength: 15, nullable: false),
                    lastname = table.Column<string>(name: "last_name", type: "VARCHAR", maxLength: 64, nullable: false),
                    fistname = table.Column<string>(name: "fist_name", type: "VARCHAR", maxLength: 64, nullable: false),
                    patronymic = table.Column<string>(type: "VARCHAR", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("administrator_id", x => x.userid);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "administrator",
                schema: "dbo");
        }
    }
}

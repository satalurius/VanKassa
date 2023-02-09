using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace VanKassa.Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIdentityAndFixTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_product_order_OrderId",
                table: "order_product");

            migrationBuilder.DropForeignKey(
                name: "FK_order_product_product_ProductId",
                table: "order_product");

            migrationBuilder.DropForeignKey(
                name: "FK_product_category_CategoryId",
                table: "product");

            migrationBuilder.DropForeignKey(
                name: "FK_user_credentials_user_UserId",
                table: "user_credentials");

            migrationBuilder.DropTable(
                name: "user_outlet");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.RenameTable(
                name: "user_credentials",
                newName: "user_credentials",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "role",
                newName: "role",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "product",
                newName: "product",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "outlet",
                newName: "outlet",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "order_product",
                newName: "order_product",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "order",
                newName: "order",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "category",
                newName: "category",
                newSchema: "dbo");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "dbo",
                table: "user_credentials",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                schema: "dbo",
                table: "user_credentials",
                newName: "user_id");

            migrationBuilder.RenameIndex(
                name: "IX_user_credentials_UserId",
                schema: "dbo",
                table: "user_credentials",
                newName: "IX_user_credentials_user_id");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                schema: "dbo",
                table: "role",
                newName: "role_id");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                schema: "dbo",
                table: "product",
                newName: "category_id");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                schema: "dbo",
                table: "product",
                newName: "product_id");

            migrationBuilder.RenameIndex(
                name: "IX_product_CategoryId",
                schema: "dbo",
                table: "product",
                newName: "ix_products_category_id");

            migrationBuilder.RenameColumn(
                name: "OutletId",
                schema: "dbo",
                table: "outlet",
                newName: "outlet_id");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                schema: "dbo",
                table: "order_product",
                newName: "product_id");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                schema: "dbo",
                table: "order_product",
                newName: "order_id");

            migrationBuilder.RenameIndex(
                name: "IX_order_product_ProductId",
                schema: "dbo",
                table: "order_product",
                newName: "ix_order_products_product_id");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                schema: "dbo",
                table: "order",
                newName: "order_id");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                schema: "dbo",
                table: "category",
                newName: "category_id");

            migrationBuilder.CreateTable(
                name: "asp_net_roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalizedname = table.Column<string>(name: "normalized_name", type: "character varying(256)", maxLength: 256, nullable: true),
                    concurrencystamp = table.Column<string>(name: "concurrency_stamp", type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "asp_net_users",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    username = table.Column<string>(name: "user_name", type: "character varying(256)", maxLength: 256, nullable: true),
                    normalizedusername = table.Column<string>(name: "normalized_user_name", type: "character varying(256)", maxLength: 256, nullable: true),
                    email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalizedemail = table.Column<string>(name: "normalized_email", type: "character varying(256)", maxLength: 256, nullable: true),
                    emailconfirmed = table.Column<bool>(name: "email_confirmed", type: "boolean", nullable: false),
                    passwordhash = table.Column<string>(name: "password_hash", type: "text", nullable: true),
                    securitystamp = table.Column<string>(name: "security_stamp", type: "text", nullable: true),
                    concurrencystamp = table.Column<string>(name: "concurrency_stamp", type: "text", nullable: true),
                    phonenumber = table.Column<string>(name: "phone_number", type: "text", nullable: true),
                    phonenumberconfirmed = table.Column<bool>(name: "phone_number_confirmed", type: "boolean", nullable: false),
                    twofactorenabled = table.Column<bool>(name: "two_factor_enabled", type: "boolean", nullable: false),
                    lockoutend = table.Column<DateTimeOffset>(name: "lockout_end", type: "timestamp with time zone", nullable: true),
                    lockoutenabled = table.Column<bool>(name: "lockout_enabled", type: "boolean", nullable: false),
                    accessfailedcount = table.Column<int>(name: "access_failed_count", type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "employee",
                schema: "dbo",
                columns: table => new
                {
                    userid = table.Column<int>(name: "user_id", type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    lastname = table.Column<string>(name: "last_name", type: "VARCHAR", maxLength: 64, nullable: false),
                    fistname = table.Column<string>(name: "fist_name", type: "VARCHAR", maxLength: 64, nullable: false),
                    patronymic = table.Column<string>(type: "VARCHAR", maxLength: 64, nullable: false),
                    photo = table.Column<string>(type: "TEXT", nullable: false),
                    fired = table.Column<bool>(type: "BOOLEAN", nullable: false),
                    roleid = table.Column<int>(name: "role_id", type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("employee_id", x => x.userid);
                    table.ForeignKey(
                        name: "fk_employees_employees_roles_role_id",
                        column: x => x.roleid,
                        principalSchema: "dbo",
                        principalTable: "role",
                        principalColumn: "role_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "asp_net_role_claims",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    roleid = table.Column<int>(name: "role_id", type: "integer", nullable: false),
                    claimtype = table.Column<string>(name: "claim_type", type: "text", nullable: true),
                    claimvalue = table.Column<string>(name: "claim_value", type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_role_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_asp_net_role_claims_asp_net_roles_role_id",
                        column: x => x.roleid,
                        principalTable: "asp_net_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "asp_net_user_claims",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    userid = table.Column<int>(name: "user_id", type: "integer", nullable: false),
                    claimtype = table.Column<string>(name: "claim_type", type: "text", nullable: true),
                    claimvalue = table.Column<string>(name: "claim_value", type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_asp_net_user_claims_asp_net_users_user_id",
                        column: x => x.userid,
                        principalTable: "asp_net_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "asp_net_user_logins",
                columns: table => new
                {
                    loginprovider = table.Column<string>(name: "login_provider", type: "text", nullable: false),
                    providerkey = table.Column<string>(name: "provider_key", type: "text", nullable: false),
                    providerdisplayname = table.Column<string>(name: "provider_display_name", type: "text", nullable: true),
                    userid = table.Column<int>(name: "user_id", type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_logins", x => new { x.loginprovider, x.providerkey });
                    table.ForeignKey(
                        name: "fk_asp_net_user_logins_asp_net_users_user_id",
                        column: x => x.userid,
                        principalTable: "asp_net_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "asp_net_user_roles",
                columns: table => new
                {
                    userid = table.Column<int>(name: "user_id", type: "integer", nullable: false),
                    roleid = table.Column<int>(name: "role_id", type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_roles", x => new { x.userid, x.roleid });
                    table.ForeignKey(
                        name: "fk_asp_net_user_roles_asp_net_roles_role_id",
                        column: x => x.roleid,
                        principalTable: "asp_net_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_asp_net_user_roles_asp_net_users_user_id",
                        column: x => x.userid,
                        principalTable: "asp_net_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "asp_net_user_tokens",
                columns: table => new
                {
                    userid = table.Column<int>(name: "user_id", type: "integer", nullable: false),
                    loginprovider = table.Column<string>(name: "login_provider", type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_tokens", x => new { x.userid, x.loginprovider, x.name });
                    table.ForeignKey(
                        name: "fk_asp_net_user_tokens_asp_net_users_user_id",
                        column: x => x.userid,
                        principalTable: "asp_net_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "refresh_tokens",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    userid = table.Column<int>(name: "user_id", type: "integer", nullable: false),
                    token = table.Column<string>(type: "text", nullable: false),
                    expireddate = table.Column<DateTime>(name: "expired_date", type: "timestamp with time zone", nullable: false),
                    createddate = table.Column<DateTime>(name: "created_date", type: "timestamp with time zone", nullable: false),
                    revokeddate = table.Column<DateTime>(name: "revoked_date", type: "timestamp with time zone", nullable: true),
                    replacedbytoken = table.Column<string>(name: "replaced_by_token", type: "text", nullable: false),
                    revokedreason = table.Column<string>(name: "revoked_reason", type: "text", nullable: false),
                    loginuserid = table.Column<int>(name: "login_user_id", type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_refresh_tokens", x => x.id);
                    table.ForeignKey(
                        name: "fk_refresh_tokens_asp_net_users_login_user_id",
                        column: x => x.loginuserid,
                        principalTable: "asp_net_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "employee_outlet",
                schema: "dbo",
                columns: table => new
                {
                    userid = table.Column<int>(name: "user_id", type: "integer", nullable: false),
                    outletid = table.Column<int>(name: "outlet_id", type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employee_outlet", x => new { x.userid, x.outletid });
                    table.ForeignKey(
                        name: "fk_employee_outlets_employees_employee_temp_id",
                        column: x => x.outletid,
                        principalSchema: "dbo",
                        principalTable: "employee",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "fk_employee_outlets_outlets_outlet_id",
                        column: x => x.outletid,
                        principalSchema: "dbo",
                        principalTable: "outlet",
                        principalColumn: "outlet_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_role_claims_role_id",
                table: "asp_net_role_claims",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "role_name_index",
                table: "asp_net_roles",
                column: "normalized_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_user_claims_user_id",
                table: "asp_net_user_claims",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_user_logins_user_id",
                table: "asp_net_user_logins",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_user_roles_role_id",
                table: "asp_net_user_roles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "email_index",
                table: "asp_net_users",
                column: "normalized_email");

            migrationBuilder.CreateIndex(
                name: "user_name_index",
                table: "asp_net_users",
                column: "normalized_user_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_employees_role_id",
                schema: "dbo",
                table: "employee",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_employee_outlets_outlet_id",
                schema: "dbo",
                table: "employee_outlet",
                column: "outlet_id");

            migrationBuilder.CreateIndex(
                name: "ix_refresh_tokens_login_user_id",
                table: "refresh_tokens",
                column: "login_user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_order_products_orders_order_id",
                schema: "dbo",
                table: "order_product",
                column: "order_id",
                principalSchema: "dbo",
                principalTable: "order",
                principalColumn: "order_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_order_products_products_product_id",
                schema: "dbo",
                table: "order_product",
                column: "product_id",
                principalSchema: "dbo",
                principalTable: "product",
                principalColumn: "product_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_products_categories_category_id",
                schema: "dbo",
                table: "product",
                column: "category_id",
                principalSchema: "dbo",
                principalTable: "category",
                principalColumn: "category_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_employees_user_credentials_user_credentials_id",
                schema: "dbo",
                table: "user_credentials",
                column: "user_id",
                principalSchema: "dbo",
                principalTable: "employee",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_order_products_orders_order_id",
                schema: "dbo",
                table: "order_product");

            migrationBuilder.DropForeignKey(
                name: "fk_order_products_products_product_id",
                schema: "dbo",
                table: "order_product");

            migrationBuilder.DropForeignKey(
                name: "fk_products_categories_category_id",
                schema: "dbo",
                table: "product");

            migrationBuilder.DropForeignKey(
                name: "fk_employees_user_credentials_user_credentials_id",
                schema: "dbo",
                table: "user_credentials");

            migrationBuilder.DropTable(
                name: "asp_net_role_claims");

            migrationBuilder.DropTable(
                name: "asp_net_user_claims");

            migrationBuilder.DropTable(
                name: "asp_net_user_logins");

            migrationBuilder.DropTable(
                name: "asp_net_user_roles");

            migrationBuilder.DropTable(
                name: "asp_net_user_tokens");

            migrationBuilder.DropTable(
                name: "employee_outlet",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "refresh_tokens");

            migrationBuilder.DropTable(
                name: "asp_net_roles");

            migrationBuilder.DropTable(
                name: "employee",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "asp_net_users");

            migrationBuilder.RenameTable(
                name: "user_credentials",
                schema: "dbo",
                newName: "user_credentials");

            migrationBuilder.RenameTable(
                name: "role",
                schema: "dbo",
                newName: "role");

            migrationBuilder.RenameTable(
                name: "product",
                schema: "dbo",
                newName: "product");

            migrationBuilder.RenameTable(
                name: "outlet",
                schema: "dbo",
                newName: "outlet");

            migrationBuilder.RenameTable(
                name: "order_product",
                schema: "dbo",
                newName: "order_product");

            migrationBuilder.RenameTable(
                name: "order",
                schema: "dbo",
                newName: "order");

            migrationBuilder.RenameTable(
                name: "category",
                schema: "dbo",
                newName: "category");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "user_credentials",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "user_credentials",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_user_credentials_user_id",
                table: "user_credentials",
                newName: "IX_user_credentials_UserId");

            migrationBuilder.RenameColumn(
                name: "role_id",
                table: "role",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "category_id",
                table: "product",
                newName: "CategoryId");

            migrationBuilder.RenameColumn(
                name: "product_id",
                table: "product",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "ix_products_category_id",
                table: "product",
                newName: "IX_product_CategoryId");

            migrationBuilder.RenameColumn(
                name: "outlet_id",
                table: "outlet",
                newName: "OutletId");

            migrationBuilder.RenameColumn(
                name: "product_id",
                table: "order_product",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "order_id",
                table: "order_product",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "ix_order_products_product_id",
                table: "order_product",
                newName: "IX_order_product_ProductId");

            migrationBuilder.RenameColumn(
                name: "order_id",
                table: "order",
                newName: "OrderId");

            migrationBuilder.RenameColumn(
                name: "category_id",
                table: "category",
                newName: "CategoryId");

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    fired = table.Column<bool>(type: "BOOLEAN", nullable: false),
                    fistname = table.Column<string>(name: "fist_name", type: "VARCHAR", maxLength: 64, nullable: false),
                    lastname = table.Column<string>(name: "last_name", type: "VARCHAR", maxLength: 64, nullable: false),
                    patronymic = table.Column<string>(type: "VARCHAR", maxLength: 64, nullable: false),
                    photo = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("user_id", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_user_role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "role",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_outlet",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    OutletId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_outlet", x => new { x.UserId, x.OutletId });
                    table.ForeignKey(
                        name: "FK_user_outlet_outlet_OutletId",
                        column: x => x.OutletId,
                        principalTable: "outlet",
                        principalColumn: "OutletId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_outlet_user_OutletId",
                        column: x => x.OutletId,
                        principalTable: "user",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_RoleId",
                table: "user",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_user_outlet_OutletId",
                table: "user_outlet",
                column: "OutletId");

            migrationBuilder.AddForeignKey(
                name: "FK_order_product_order_OrderId",
                table: "order_product",
                column: "OrderId",
                principalTable: "order",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_order_product_product_ProductId",
                table: "order_product",
                column: "ProductId",
                principalTable: "product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_product_category_CategoryId",
                table: "product",
                column: "CategoryId",
                principalTable: "category",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_credentials_user_UserId",
                table: "user_credentials",
                column: "UserId",
                principalTable: "user",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

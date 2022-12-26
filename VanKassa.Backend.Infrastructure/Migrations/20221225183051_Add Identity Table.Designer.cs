﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using VanKassa.Backend.Infrastructure.Data;

#nullable disable

namespace VanKassa.Backend.Infrastructure.Migrations
{
    [DbContext(typeof(VanKassaDbContext))]
    [Migration("20221225183051_Add Identity Table")]
    partial class AddIdentityTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("VanKassa.Domain.Dtos.EmployeesDbDto", b =>
                {
                    b.Property<string>("Addresses")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Patronymic")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Photo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.ToTable("EmployeesDbDtos");
                });

            modelBuilder.Entity("VanKassa.Domain.Entities.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("name");

                    b.HasKey("CategoryId")
                        .HasName("category_id");

                    b.ToTable("category", (string)null);
                });

            modelBuilder.Entity("VanKassa.Domain.Entities.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("OrderId"));

                    b.Property<bool>("Canceled")
                        .HasColumnType("BOOLEAN")
                        .HasColumnName("canceled");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TIMESTAMP")
                        .HasColumnName("date");

                    b.HasKey("OrderId")
                        .HasName("order_id");

                    b.ToTable("order", (string)null);
                });

            modelBuilder.Entity("VanKassa.Domain.Entities.OrderProduct", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("integer");

                    b.Property<int>("ProductId")
                        .HasColumnType("integer");

                    b.HasKey("OrderId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("order_product", (string)null);
                });

            modelBuilder.Entity("VanKassa.Domain.Entities.Outlet", b =>
                {
                    b.Property<int>("OutletId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("OutletId"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("VARCHAR(25)")
                        .HasColumnName("city");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("street");

                    b.Property<string>("StreetNumber")
                        .HasMaxLength(15)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("street_number");

                    b.HasKey("OutletId")
                        .HasName("outlet_id");

                    b.ToTable("outlet", (string)null);
                });

            modelBuilder.Entity("VanKassa.Domain.Entities.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ProductId"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("name");

                    b.Property<decimal>("Price")
                        .HasPrecision(10)
                        .HasColumnType("DECIMAL")
                        .HasColumnName("price");

                    b.HasKey("ProductId")
                        .HasName("product_id");

                    b.HasIndex("CategoryId");

                    b.ToTable("product", (string)null);
                });

            modelBuilder.Entity("VanKassa.Domain.Entities.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("RoleId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("name");

                    b.HasKey("RoleId")
                        .HasName("role_id");

                    b.ToTable("role", (string)null);
                });

            modelBuilder.Entity("VanKassa.Domain.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserId"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("fist_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("last_name");

                    b.Property<string>("Patronymic")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("patronymic");

                    b.Property<string>("Photo")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("photo");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.HasKey("UserId")
                        .HasName("user_id");

                    b.HasIndex("RoleId");

                    b.ToTable("user", (string)null);
                });

            modelBuilder.Entity("VanKassa.Domain.Entities.UserCredentials", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("password");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("user_name");

                    b.HasKey("Id")
                        .HasName("id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("user_credentials", (string)null);
                });

            modelBuilder.Entity("VanKassa.Domain.Entities.UserOutlet", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("OutletId")
                        .HasColumnType("integer");

                    b.HasKey("UserId", "OutletId");

                    b.HasIndex("OutletId");

                    b.ToTable("user_outlet", (string)null);
                });

            modelBuilder.Entity("VanKassa.Domain.Entities.OrderProduct", b =>
                {
                    b.HasOne("VanKassa.Domain.Entities.Order", "Order")
                        .WithMany("OrderProducts")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VanKassa.Domain.Entities.Product", "Product")
                        .WithMany("OrderProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("VanKassa.Domain.Entities.Product", b =>
                {
                    b.HasOne("VanKassa.Domain.Entities.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("VanKassa.Domain.Entities.User", b =>
                {
                    b.HasOne("VanKassa.Domain.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("VanKassa.Domain.Entities.UserCredentials", b =>
                {
                    b.HasOne("VanKassa.Domain.Entities.User", "User")
                        .WithOne("UserCredentials")
                        .HasForeignKey("VanKassa.Domain.Entities.UserCredentials", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("VanKassa.Domain.Entities.UserOutlet", b =>
                {
                    b.HasOne("VanKassa.Domain.Entities.Outlet", "Outlet")
                        .WithMany("UserOutlets")
                        .HasForeignKey("OutletId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VanKassa.Domain.Entities.User", "User")
                        .WithMany("UserOutlets")
                        .HasForeignKey("OutletId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Outlet");

                    b.Navigation("User");
                });

            modelBuilder.Entity("VanKassa.Domain.Entities.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("VanKassa.Domain.Entities.Order", b =>
                {
                    b.Navigation("OrderProducts");
                });

            modelBuilder.Entity("VanKassa.Domain.Entities.Outlet", b =>
                {
                    b.Navigation("UserOutlets");
                });

            modelBuilder.Entity("VanKassa.Domain.Entities.Product", b =>
                {
                    b.Navigation("OrderProducts");
                });

            modelBuilder.Entity("VanKassa.Domain.Entities.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("VanKassa.Domain.Entities.User", b =>
                {
                    b.Navigation("UserCredentials")
                        .IsRequired();

                    b.Navigation("UserOutlets");
                });
#pragma warning restore 612, 618
        }
    }
}

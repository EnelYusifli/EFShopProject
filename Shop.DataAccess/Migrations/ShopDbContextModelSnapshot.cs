﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shop.DataAccess;

#nullable disable

namespace Shop.DataAccess.Migrations
{
    [DbContext(typeof(ShopDbContext))]
    partial class ShopDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.26")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Shop.Core.Entities.Cart", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeactive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("Shop.Core.Entities.CartProduct", b =>
                {
                    b.Property<int>("CartId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeactive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProductCountInCart")
                        .HasColumnType("int");

                    b.HasKey("CartId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("CartProduct");
                });

            modelBuilder.Entity("Shop.Core.Entities.Invoice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeactive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedTime")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("WalletId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WalletId");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("Shop.Core.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AvailableCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeactive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Shop.Core.Entities.ProductInvoice", b =>
                {
                    b.Property<int>("InvoiceId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeactive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProductCountInInvoice")
                        .HasColumnType("int");

                    b.HasKey("InvoiceId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductInvoice");
                });

            modelBuilder.Entity("Shop.Core.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Age")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeactive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Shop.Core.Entities.Wallet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("CVC")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ExpirationDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeactive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Wallets");
                });

            modelBuilder.Entity("Shop.Core.Entities.Cart", b =>
                {
                    b.HasOne("Shop.Core.Entities.User", "User")
                        .WithOne("Cart")
                        .HasForeignKey("Shop.Core.Entities.Cart", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Shop.Core.Entities.CartProduct", b =>
                {
                    b.HasOne("Shop.Core.Entities.Cart", "Cart")
                        .WithMany("CartProducts")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shop.Core.Entities.Product", "Product")
                        .WithMany("CartProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cart");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Shop.Core.Entities.Invoice", b =>
                {
                    b.HasOne("Shop.Core.Entities.Wallet", "Wallet")
                        .WithMany("Invoices")
                        .HasForeignKey("WalletId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Wallet");
                });

            modelBuilder.Entity("Shop.Core.Entities.ProductInvoice", b =>
                {
                    b.HasOne("Shop.Core.Entities.Invoice", "Invoice")
                        .WithMany("ProductInvoices")
                        .HasForeignKey("InvoiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shop.Core.Entities.Product", "Product")
                        .WithMany("ProductInvoices")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Invoice");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Shop.Core.Entities.Wallet", b =>
                {
                    b.HasOne("Shop.Core.Entities.User", "User")
                        .WithMany("Wallets")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Shop.Core.Entities.Cart", b =>
                {
                    b.Navigation("CartProducts");
                });

            modelBuilder.Entity("Shop.Core.Entities.Invoice", b =>
                {
                    b.Navigation("ProductInvoices");
                });

            modelBuilder.Entity("Shop.Core.Entities.Product", b =>
                {
                    b.Navigation("CartProducts");

                    b.Navigation("ProductInvoices");
                });

            modelBuilder.Entity("Shop.Core.Entities.User", b =>
                {
                    b.Navigation("Cart")
                        .IsRequired();

                    b.Navigation("Wallets");
                });

            modelBuilder.Entity("Shop.Core.Entities.Wallet", b =>
                {
                    b.Navigation("Invoices");
                });
#pragma warning restore 612, 618
        }
    }
}
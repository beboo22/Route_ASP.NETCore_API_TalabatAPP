﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Talabat.Repository.Data;

#nullable disable

namespace Talabat.Repository.Data.migrations
{
    [DbContext(typeof(StoreDbContext))]
    [Migration("20241207203832_updateForErrorIndexer")]
    partial class updateForErrorIndexer
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Talabat.Core.Entity.Order_Aggregrate.DeliveryMethod", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<decimal>("Cost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeliveryTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("DeliveryMethod");
                });

            modelBuilder.Entity("Talabat.Core.Entity.Order_Aggregrate.Order", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("BuyerEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTimeOffset>("DateTimeOffset")
                        .HasColumnType("datetimeoffset");

                    b.Property<int?>("DeliveryMethodId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("SubTotal")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ID");

                    b.HasIndex("DeliveryMethodId")
                        .IsUnique()
                        .HasFilter("[DeliveryMethodId] IS NOT NULL");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("Talabat.Core.Entity.Order_Aggregrate.OrderItem", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("OrderID")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quntity")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("OrderID");

                    b.ToTable("OrderItem");
                });

            modelBuilder.Entity("Talabat.Core.Entity.Product", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int?>("BrandId")
                        .HasColumnType("int")
                        .HasColumnName("CategoryId");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int")
                        .HasColumnName("BrandId");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("VARCHAR");

                    b.Property<string>("PictureUrl")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(max)");

                    b.Property<decimal>("price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ID");

                    b.HasIndex("BrandId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Talabat.Core.Entity.ProductBrand", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("VARCHAR");

                    b.HasKey("ID");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("Talabat.Core.Entity.ProductCategory", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("VARCHAR");

                    b.HasKey("ID");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("Talabat.Core.Entity.Order_Aggregrate.Order", b =>
                {
                    b.HasOne("Talabat.Core.Entity.Order_Aggregrate.DeliveryMethod", "DeliveryMethod")
                        .WithOne()
                        .HasForeignKey("Talabat.Core.Entity.Order_Aggregrate.Order", "DeliveryMethodId");

                    b.OwnsOne("Talabat.Core.Entity.Order_Aggregrate.Address", "Address", b1 =>
                        {
                            b1.Property<int>("OrderID")
                                .HasColumnType("int");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Fname")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Lname")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("OrderID");

                            b1.ToTable("Order");

                            b1.WithOwner()
                                .HasForeignKey("OrderID");
                        });

                    b.Navigation("Address")
                        .IsRequired();

                    b.Navigation("DeliveryMethod");
                });

            modelBuilder.Entity("Talabat.Core.Entity.Order_Aggregrate.OrderItem", b =>
                {
                    b.HasOne("Talabat.Core.Entity.Order_Aggregrate.Order", null)
                        .WithMany("Items")
                        .HasForeignKey("OrderID");

                    b.OwnsOne("Talabat.Core.Entity.Order_Aggregrate.ProductOrderItem", "ProductOrderItem", b1 =>
                        {
                            b1.Property<int>("OrderItemID")
                                .HasColumnType("int");

                            b1.Property<string>("prodName")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("prodUrl")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<int>("productId")
                                .HasColumnType("int");

                            b1.HasKey("OrderItemID");

                            b1.ToTable("OrderItem");

                            b1.WithOwner()
                                .HasForeignKey("OrderItemID");
                        });

                    b.Navigation("ProductOrderItem")
                        .IsRequired();
                });

            modelBuilder.Entity("Talabat.Core.Entity.Product", b =>
                {
                    b.HasOne("Talabat.Core.Entity.ProductBrand", "productBrand")
                        .WithMany()
                        .HasForeignKey("BrandId");

                    b.HasOne("Talabat.Core.Entity.ProductCategory", "productCategory")
                        .WithMany()
                        .HasForeignKey("CategoryId");

                    b.Navigation("productBrand");

                    b.Navigation("productCategory");
                });

            modelBuilder.Entity("Talabat.Core.Entity.Order_Aggregrate.Order", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}

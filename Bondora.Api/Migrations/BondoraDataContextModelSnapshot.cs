﻿// <auto-generated />
using Bondora.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Bondora.Api.Migrations
{
    [DbContext(typeof(BondoraDataContext))]
    partial class BondoraDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Bondora.Api.Entities.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Firstname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lastname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "ava.mackenzie@bondora",
                            Firstname = "Ava",
                            Lastname = "Mackenzie",
                            Points = 0
                        },
                        new
                        {
                            Id = 2,
                            Email = "lucas.young@bondora",
                            Firstname = "Lucas",
                            Lastname = "Young",
                            Points = 0
                        },
                        new
                        {
                            Id = 3,
                            Email = "dorothy.wilkins@bondora",
                            Firstname = "Dorothy",
                            Lastname = "Wilkins",
                            Points = 0
                        },
                        new
                        {
                            Id = 4,
                            Email = "dorothy.wilkins@bondora",
                            Firstname = "Dominic",
                            Lastname = "Buckland",
                            Points = 0
                        },
                        new
                        {
                            Id = 5,
                            Email = "joe.harris@bondora",
                            Firstname = "Joe",
                            Lastname = "Harris",
                            Points = 0
                        });
                });

            modelBuilder.Entity("Bondora.Api.Entities.Equipment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Equipments");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Caterpillar Bulldozer",
                            Type = 1
                        },
                        new
                        {
                            Id = 2,
                            Name = "Kamaz truck",
                            Type = 2
                        },
                        new
                        {
                            Id = 3,
                            Name = "Komatsu Crane",
                            Type = 1
                        },
                        new
                        {
                            Id = 4,
                            Name = "Volvo Steamroller",
                            Type = 2
                        },
                        new
                        {
                            Id = 5,
                            Name = "Bosch Jackhammer",
                            Type = 3
                        });
                });

            modelBuilder.Entity("Bondora.Api.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("OrderTotalPoint")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("Bondora.Api.Entities.OrderDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Days")
                        .HasColumnType("int");

                    b.Property<int>("EquipmentId")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("EquipmentId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("Bondora.Api.Entities.Order", b =>
                {
                    b.HasOne("Bondora.Api.Entities.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Bondora.Api.Entities.OrderDetail", b =>
                {
                    b.HasOne("Bondora.Api.Entities.Equipment", "Equipment")
                        .WithMany()
                        .HasForeignKey("EquipmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bondora.Api.Entities.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Equipment");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Bondora.Api.Entities.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });
#pragma warning restore 612, 618
        }
    }
}

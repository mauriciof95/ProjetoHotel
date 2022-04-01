﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjetoHotel.Infrastructure.Context;

namespace ProjetoHotel.Infrastructure.Migrations
{
    [DbContext(typeof(SqlDbContext))]
    [Migration("20220401110249_migracao inicial")]
    partial class migracaoinicial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.23")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ProjetoHotel.Domain.Entities.Hotel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CNPJ")
                        .IsRequired()
                        .HasColumnName("cnpj")
                        .HasColumnType("nvarchar(14)")
                        .HasMaxLength(14);

                    b.Property<string>("Descricao")
                        .HasColumnName("descricao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Endereco")
                        .IsRequired()
                        .HasColumnName("endereco")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnName("nome")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("hotel");
                });

            modelBuilder.Entity("ProjetoHotel.Domain.Entities.HotelImagem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("Hotel_Id")
                        .HasColumnName("hotel_id")
                        .HasColumnType("bigint");

                    b.Property<string>("Image_Url")
                        .IsRequired()
                        .HasColumnName("image_url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Hotel_Id");

                    b.ToTable("hotel_imagem");
                });

            modelBuilder.Entity("ProjetoHotel.Domain.Entities.Quarto", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("Hotel_Id")
                        .HasColumnName("hotel_id")
                        .HasColumnType("bigint");

                    b.Property<string>("Nome")
                        .HasColumnName("nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Numero_Adultos")
                        .HasColumnName("numero_adultos")
                        .HasColumnType("int");

                    b.Property<int>("Numero_Criancas")
                        .HasColumnName("numero_criancas")
                        .HasColumnType("int");

                    b.Property<int>("Numero_Ocupantes")
                        .HasColumnName("numero_ocupantes")
                        .HasColumnType("int");

                    b.Property<decimal>("Preco")
                        .HasColumnName("preco")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("Hotel_Id");

                    b.ToTable("quarto");
                });

            modelBuilder.Entity("ProjetoHotel.Domain.Entities.QuartoImagem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Image_Url")
                        .IsRequired()
                        .HasColumnName("image_url")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Quarto_Id")
                        .HasColumnName("quarto_id")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("Quarto_Id");

                    b.ToTable("quarto_imagem");
                });

            modelBuilder.Entity("ProjetoHotel.Domain.Entities.HotelImagem", b =>
                {
                    b.HasOne("ProjetoHotel.Domain.Entities.Hotel", "Hotel")
                        .WithMany("Imagens")
                        .HasForeignKey("Hotel_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjetoHotel.Domain.Entities.Quarto", b =>
                {
                    b.HasOne("ProjetoHotel.Domain.Entities.Hotel", "Hotel")
                        .WithMany("Quartos")
                        .HasForeignKey("Hotel_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjetoHotel.Domain.Entities.QuartoImagem", b =>
                {
                    b.HasOne("ProjetoHotel.Domain.Entities.Quarto", "Quarto")
                        .WithMany("Imagens")
                        .HasForeignKey("Quarto_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
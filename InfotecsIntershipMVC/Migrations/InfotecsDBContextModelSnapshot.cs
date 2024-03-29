﻿// <auto-generated />
using System;
using InfotecsIntershipMVC.DAL.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InfotecsIntershipMVC.Migrations
{
    [DbContext(typeof(InfotecsDBContext))]
    partial class InfotecsDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("InfotecsIntershipMVC.DAL.Models.FileEntity", b =>
                {
                    b.Property<Guid>("FileID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("FileID");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("InfotecsIntershipMVC.DAL.Models.RecordEntity", b =>
                {
                    b.Property<Guid>("RecordID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("Duraion")
                        .HasColumnType("int");

                    b.Property<Guid>("FileID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("Value")
                        .HasColumnType("real");

                    b.HasKey("RecordID");

                    b.HasIndex("FileID");

                    b.ToTable("Values");
                });

            modelBuilder.Entity("InfotecsIntershipMVC.DAL.Models.ResultEntity", b =>
                {
                    b.Property<Guid>("ResultID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AllTime")
                        .HasColumnType("int");

                    b.Property<int>("AverageDuration")
                        .HasColumnType("int");

                    b.Property<float>("AverageValue")
                        .HasColumnType("real");

                    b.Property<Guid>("FileID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FirstOperation")
                        .HasColumnType("datetime2");

                    b.Property<float>("MaxValue")
                        .HasColumnType("real");

                    b.Property<float>("MedianByValue")
                        .HasColumnType("real");

                    b.Property<float>("MinValue")
                        .HasColumnType("real");

                    b.Property<int>("RowCount")
                        .HasColumnType("int");

                    b.HasKey("ResultID");

                    b.HasIndex("FileID")
                        .IsUnique();

                    b.ToTable("Results");
                });

            modelBuilder.Entity("InfotecsIntershipMVC.DAL.Models.RecordEntity", b =>
                {
                    b.HasOne("InfotecsIntershipMVC.DAL.Models.FileEntity", "File")
                        .WithMany("Records")
                        .HasForeignKey("FileID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("File");
                });

            modelBuilder.Entity("InfotecsIntershipMVC.DAL.Models.ResultEntity", b =>
                {
                    b.HasOne("InfotecsIntershipMVC.DAL.Models.FileEntity", "File")
                        .WithOne("Result")
                        .HasForeignKey("InfotecsIntershipMVC.DAL.Models.ResultEntity", "FileID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("File");
                });

            modelBuilder.Entity("InfotecsIntershipMVC.DAL.Models.FileEntity", b =>
                {
                    b.Navigation("Records");

                    b.Navigation("Result")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

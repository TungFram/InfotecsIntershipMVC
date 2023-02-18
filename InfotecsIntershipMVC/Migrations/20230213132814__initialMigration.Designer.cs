﻿// <auto-generated />
using System;
using InfotecsIntershipMVC.DAL.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InfotecsIntershipMVC.Migrations
{
    [DbContext(typeof(InfotecsDBContext))]
    [Migration("20230213132814__initialMigration")]
    partial class initialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<Guid>("FileId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("Value")
                        .HasColumnType("real");

                    b.HasKey("RecordID");

                    b.HasIndex("FileId");

                    b.ToTable("Values");
                });

            modelBuilder.Entity("InfotecsIntershipMVC.DAL.Models.ResultEntity", b =>
                {
                    b.Property<Guid>("ResultID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<TimeSpan>("AllTime")
                        .HasColumnType("time");

                    b.Property<int>("AverageDuration")
                        .HasColumnType("int");

                    b.Property<float>("AverageValue")
                        .HasColumnType("real");

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

                    b.ToTable("Results");
                });

            modelBuilder.Entity("InfotecsIntershipMVC.DAL.Models.RecordEntity", b =>
                {
                    b.HasOne("InfotecsIntershipMVC.DAL.Models.FileEntity", "File")
                        .WithMany("Records")
                        .HasForeignKey("FileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("File");
                });

            modelBuilder.Entity("InfotecsIntershipMVC.DAL.Models.FileEntity", b =>
                {
                    b.Navigation("Records");
                });
#pragma warning restore 612, 618
        }
    }
}

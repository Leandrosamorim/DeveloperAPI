﻿// <auto-generated />
using System;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Data.Migrations
{
    [DbContext(typeof(DeveloperDBContext))]
    [Migration("20230322203101_change-int-to-guid")]
    partial class changeinttoguid
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.ContactNS.Contact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Contact");
                });

            modelBuilder.Entity("Domain.DeveloperNS.Developer", b =>
                {
                    b.Property<Guid>("UId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ContactId")
                        .HasColumnType("int");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StackId")
                        .HasColumnType("int");

                    b.HasKey("UId");

                    b.HasIndex("ContactId");

                    b.HasIndex("StackId");

                    b.ToTable("Developer");
                });

            modelBuilder.Entity("Domain.ProgrammingStackNS.ProgrammingStack", b =>
                {
                    b.Property<int>("StackId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StackId"));

                    b.Property<string>("StackName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StackId");

                    b.ToTable("ProgrammingStack");

                    b.HasData(
                        new
                        {
                            StackId = 1,
                            StackName = ".NET"
                        },
                        new
                        {
                            StackId = 2,
                            StackName = "JAVA"
                        });
                });

            modelBuilder.Entity("Domain.DeveloperNS.Developer", b =>
                {
                    b.HasOne("Domain.ContactNS.Contact", "Contact")
                        .WithMany()
                        .HasForeignKey("ContactId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.ProgrammingStackNS.ProgrammingStack", "Stack")
                        .WithMany()
                        .HasForeignKey("StackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contact");

                    b.Navigation("Stack");
                });
#pragma warning restore 612, 618
        }
    }
}

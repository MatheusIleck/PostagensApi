﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PostagensApi.Data;

#nullable disable

namespace PostagensApi.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240904182122_AddLikes")]
    partial class AddLikes
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PostagensApi.Models.Like", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<long>("IdPost")
                        .HasColumnType("bigint");

                    b.Property<long?>("IdPostNavigationId")
                        .HasColumnType("bigint");

                    b.Property<long>("IdUsuario")
                        .HasColumnType("bigint");

                    b.Property<long?>("IdUsuarioNavigationId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("IdPostNavigationId");

                    b.HasIndex("IdUsuarioNavigationId");

                    b.ToTable("Likes");
                });

            modelBuilder.Entity("PostagensApi.Models.Post", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Post");
                });

            modelBuilder.Entity("PostagensApi.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("PostagensApi.Models.Like", b =>
                {
                    b.HasOne("PostagensApi.Models.Post", "IdPostNavigation")
                        .WithMany("Likes")
                        .HasForeignKey("IdPostNavigationId");

                    b.HasOne("PostagensApi.Models.User", "IdUsuarioNavigation")
                        .WithMany("Likes")
                        .HasForeignKey("IdUsuarioNavigationId");

                    b.Navigation("IdPostNavigation");

                    b.Navigation("IdUsuarioNavigation");
                });

            modelBuilder.Entity("PostagensApi.Models.Post", b =>
                {
                    b.Navigation("Likes");
                });

            modelBuilder.Entity("PostagensApi.Models.User", b =>
                {
                    b.Navigation("Likes");
                });
#pragma warning restore 612, 618
        }
    }
}

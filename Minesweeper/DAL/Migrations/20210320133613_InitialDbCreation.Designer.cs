﻿// <auto-generated />
using System;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DAL.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20210320133613_InitialDbCreation")]
    partial class InitialDbCreation
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.4");

            modelBuilder.Entity("Domain.SavedGame", b =>
                {
                    b.Property<int>("SavedGameId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("BoardData")
                        .HasColumnType("TEXT");

                    b.Property<int>("BoardHeight")
                        .HasColumnType("INTEGER");

                    b.Property<int>("BoardWidth")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("GameStatus")
                        .HasColumnType("INTEGER");

                    b.HasKey("SavedGameId");

                    b.ToTable("SavedGames");
                });
#pragma warning restore 612, 618
        }
    }
}

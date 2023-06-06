﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using chimneys.Data;

#nullable disable

namespace chimneys.Migrations
{
    [DbContext(typeof(ApplicationContextDb))]
    partial class ApplicationContextDbModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.15");

            modelBuilder.Entity("chimneys.Data.Name_variants", b =>
                {
                    b.Property<int>("Id_var")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name_var")
                        .HasColumnType("TEXT");

                    b.HasKey("Id_var");

                    b.ToTable("Name_var");
                });

            modelBuilder.Entity("chimneys.Data.Variants", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("C_co2")
                        .HasColumnType("REAL");

                    b.Property<double>("C_h2o")
                        .HasColumnType("REAL");

                    b.Property<double>("L")
                        .HasColumnType("REAL");

                    b.Property<string>("Name_var")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("T")
                        .HasColumnType("REAL");

                    b.Property<double>("T_okr")
                        .HasColumnType("REAL");

                    b.Property<double>("V")
                        .HasColumnType("REAL");

                    b.Property<double>("d_vn_f")
                        .HasColumnType("REAL");

                    b.Property<double>("d_vn_s")
                        .HasColumnType("REAL");

                    b.Property<double>("h")
                        .HasColumnType("REAL");

                    b.Property<double>("l1")
                        .HasColumnType("REAL");

                    b.Property<double>("l2")
                        .HasColumnType("REAL");

                    b.Property<double>("l3")
                        .HasColumnType("REAL");

                    b.Property<double>("l4")
                        .HasColumnType("REAL");

                    b.Property<double>("l5")
                        .HasColumnType("REAL");

                    b.Property<int>("num_wall")
                        .HasColumnType("INTEGER");

                    b.Property<double>("y1")
                        .HasColumnType("REAL");

                    b.Property<double>("y2")
                        .HasColumnType("REAL");

                    b.Property<double>("y3")
                        .HasColumnType("REAL");

                    b.Property<double>("y4")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("Variants");
                });
#pragma warning restore 612, 618
        }
    }
}
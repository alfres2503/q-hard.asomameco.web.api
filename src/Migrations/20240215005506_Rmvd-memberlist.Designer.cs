﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using src;

#nullable disable

namespace src.Migrations
{
    [DbContext(typeof(AppDBContext))]
    [Migration("20240215005506_Rmvd-memberlist")]
    partial class Rmvdmemberlist
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("src.Models.Associate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("email");

                    b.Property<string>("IdCard")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("id_card");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasColumnName("is_active");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("phone");

                    b.HasKey("Id")
                        .HasName("pk_associate");

                    b.ToTable("associate", (string)null);
                });

            modelBuilder.Entity("src.Models.Attendance", b =>
                {
                    b.Property<int>("IdAssociate")
                        .HasColumnType("int")
                        .HasColumnName("id_associate");

                    b.Property<int>("IdEvent")
                        .HasColumnType("int")
                        .HasColumnName("id_event");

                    b.Property<TimeOnly>("ArrivalTime")
                        .HasColumnType("time")
                        .HasColumnName("arrival_time");

                    b.Property<bool>("isConfirmed")
                        .HasColumnType("bit")
                        .HasColumnName("is_confirmed");

                    b.HasKey("IdAssociate", "IdEvent")
                        .HasName("pk_attendance");

                    b.HasIndex("IdEvent")
                        .HasDatabaseName("ix_attendance_id_event");

                    b.ToTable("attendance", (string)null);
                });

            modelBuilder.Entity("src.Models.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date")
                        .HasColumnName("date");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("description");

                    b.Property<int>("IdMember")
                        .HasColumnType("int")
                        .HasColumnName("id_member");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.Property<string>("Place")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("place");

                    b.Property<TimeOnly>("Time")
                        .HasColumnType("time")
                        .HasColumnName("time");

                    b.HasKey("Id")
                        .HasName("pk_event");

                    b.HasIndex("IdMember")
                        .HasDatabaseName("ix_event_id_member");

                    b.ToTable("event", (string)null);
                });

            modelBuilder.Entity("src.Models.Member", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("first_name");

                    b.Property<string>("IdCard")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("id_card");

                    b.Property<int>("IdRole")
                        .HasColumnType("int")
                        .HasColumnName("id_role");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasColumnName("is_active");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("last_name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("password");

                    b.HasKey("Id")
                        .HasName("pk_member");

                    b.HasIndex("IdRole")
                        .HasDatabaseName("ix_member_id_role");

                    b.ToTable("member", (string)null);
                });

            modelBuilder.Entity("src.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("description");

                    b.HasKey("Id")
                        .HasName("pk_role");

                    b.ToTable("role", (string)null);
                });

            modelBuilder.Entity("src.Models.Attendance", b =>
                {
                    b.HasOne("src.Models.Associate", "Associate")
                        .WithMany("Attendances")
                        .HasForeignKey("IdAssociate")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_attendance_associate_id_associate");

                    b.HasOne("src.Models.Event", "Event")
                        .WithMany("Attendances")
                        .HasForeignKey("IdEvent")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_attendance_event_id_event");

                    b.Navigation("Associate");

                    b.Navigation("Event");
                });

            modelBuilder.Entity("src.Models.Event", b =>
                {
                    b.HasOne("src.Models.Member", "Member")
                        .WithMany("Events")
                        .HasForeignKey("IdMember")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_event_member_id_member");

                    b.Navigation("Member");
                });

            modelBuilder.Entity("src.Models.Member", b =>
                {
                    b.HasOne("src.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("IdRole")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_member_role_id_role");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("src.Models.Associate", b =>
                {
                    b.Navigation("Attendances");
                });

            modelBuilder.Entity("src.Models.Event", b =>
                {
                    b.Navigation("Attendances");
                });

            modelBuilder.Entity("src.Models.Member", b =>
                {
                    b.Navigation("Events");
                });
#pragma warning restore 612, 618
        }
    }
}

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
    [Migration("20240222060003_nonavigationsfix")]
    partial class nonavigationsfix
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

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "luis@mail.com",
                            IdCard = "555555555",
                            IsActive = true,
                            Name = "Luis Gallego",
                            Phone = "88888888"
                        },
                        new
                        {
                            Id = 2,
                            Email = "mario@mail.com",
                            IdCard = "666666666",
                            IsActive = true,
                            Name = "Mario Gallego",
                            Phone = "77777777"
                        },
                        new
                        {
                            Id = 3,
                            Email = "pfer@mail.com",
                            IdCard = "777777777",
                            IsActive = true,
                            Name = "Pedro Fernández",
                            Phone = "66666666"
                        });
                });

            modelBuilder.Entity("src.Models.Attendance", b =>
                {
                    b.Property<int>("IdAssociate")
                        .HasColumnType("int")
                        .HasColumnName("id_associate");

                    b.Property<int>("IdEvent")
                        .HasColumnType("int")
                        .HasColumnName("id_event");

                    b.Property<TimeOnly?>("ArrivalTime")
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

                    b.HasData(
                        new
                        {
                            IdAssociate = 1,
                            IdEvent = 1,
                            ArrivalTime = new TimeOnly(7, 23, 11),
                            isConfirmed = true
                        },
                        new
                        {
                            IdAssociate = 2,
                            IdEvent = 1,
                            ArrivalTime = new TimeOnly(7, 23, 11),
                            isConfirmed = true
                        },
                        new
                        {
                            IdAssociate = 3,
                            IdEvent = 1,
                            ArrivalTime = new TimeOnly(7, 23, 11),
                            isConfirmed = true
                        },
                        new
                        {
                            IdAssociate = 1,
                            IdEvent = 2,
                            ArrivalTime = new TimeOnly(10, 30, 11),
                            isConfirmed = true
                        },
                        new
                        {
                            IdAssociate = 2,
                            IdEvent = 2,
                            ArrivalTime = new TimeOnly(10, 32, 11),
                            isConfirmed = true
                        },
                        new
                        {
                            IdAssociate = 3,
                            IdEvent = 2,
                            isConfirmed = false
                        },
                        new
                        {
                            IdAssociate = 1,
                            IdEvent = 3,
                            isConfirmed = false
                        },
                        new
                        {
                            IdAssociate = 2,
                            IdEvent = 3,
                            ArrivalTime = new TimeOnly(14, 0, 11),
                            isConfirmed = true
                        },
                        new
                        {
                            IdAssociate = 3,
                            IdEvent = 3,
                            ArrivalTime = new TimeOnly(14, 0, 11),
                            isConfirmed = true
                        });
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

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Date = new DateOnly(2022, 10, 21),
                            Description = "Reunión de la junta directiva para discutir temas importantes",
                            IdMember = 2,
                            Name = "Reunión de la junta directiva",
                            Place = "Sala de juntas",
                            Time = new TimeOnly(7, 23, 11)
                        },
                        new
                        {
                            Id = 2,
                            Date = new DateOnly(2022, 12, 23),
                            Description = "Fiestón para esuchar Luis Miguel",
                            IdMember = 3,
                            Name = "Fiesta Mariachi",
                            Place = "Sala de juntas",
                            Time = new TimeOnly(10, 30, 11)
                        },
                        new
                        {
                            Id = 3,
                            Date = new DateOnly(2023, 1, 15),
                            Description = "Reunión recapacitativa",
                            IdMember = 2,
                            Name = "Reunión porqué amo a mi esposita",
                            Place = "Sala de juntas",
                            Time = new TimeOnly(14, 0, 11)
                        });
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

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "lusuarezag@est.utn.ac.cr",
                            FirstName = "Fred",
                            IdCard = "111111111",
                            IdRole = 1,
                            IsActive = true,
                            LastName = "Suárez",
                            Password = "yQp4jaI+UcREW1GJCAfieA=="
                        },
                        new
                        {
                            Id = 2,
                            Email = "malopezsa@est.utn.ac.cr",
                            FirstName = "Pala",
                            IdCard = "222222222",
                            IdRole = 2,
                            IsActive = true,
                            LastName = "López",
                            Password = "yQp4jaI+UcREW1GJCAfieA=="
                        },
                        new
                        {
                            Id = 3,
                            Email = "gabulatem@est.utn.ac.cr",
                            FirstName = "Gabo",
                            IdCard = "333333333",
                            IdRole = 2,
                            IsActive = true,
                            LastName = "Ulate",
                            Password = "yQp4jaI+UcREW1GJCAfieA=="
                        },
                        new
                        {
                            Id = 4,
                            Email = "jgonzalez@mail.com",
                            FirstName = "Fio",
                            IdCard = "444444444",
                            IdRole = 2,
                            IsActive = false,
                            LastName = "Salas",
                            Password = "yQp4jaI+UcREW1GJCAfieA=="
                        });
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

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Member"
                        });
                });

            modelBuilder.Entity("src.Models.Attendance", b =>
                {
                    b.HasOne("src.Models.Associate", "Associate")
                        .WithMany()
                        .HasForeignKey("IdAssociate")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_attendance_associate_id_associate");

                    b.HasOne("src.Models.Event", "Event")
                        .WithMany()
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
                        .WithMany()
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
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using System;
using Exo_MVC1.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Exo_MVC1.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241014100411_json1")]
    partial class json1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Exo_MVC1.Models.Activite", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Id_compagnie")
                        .HasColumnType("int")
                        .HasColumnName("id_compagnie");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("imagePath");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("nom");

                    b.HasKey("Id");

                    b.HasIndex("Id_compagnie");

                    b.ToTable("Activites");
                });

            modelBuilder.Entity("Exo_MVC1.Models.Admin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Motdepasse")
                        .HasColumnType("longtext")
                        .HasColumnName("motdepasse");

                    b.Property<string>("Nom")
                        .HasColumnType("longtext")
                        .HasColumnName("nom");

                    b.Property<string>("Username")
                        .HasColumnType("longtext")
                        .HasColumnName("username");

                    b.HasKey("Id");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("Exo_MVC1.Models.Categorie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Categories")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("categorie");

                    b.Property<DateTime>("Datedebut")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("datedebut");

                    b.Property<DateTime>("Datefin")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("datefin");

                    b.Property<string>("Horaire")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("horaire");

                    b.Property<int>("Id_activite")
                        .HasColumnType("int")
                        .HasColumnName("id_activite");

                    b.Property<string>("Jour")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("jour");

                    b.Property<int>("Prix")
                        .HasColumnType("int")
                        .HasColumnName("prix");

                    b.HasKey("Id");

                    b.HasIndex("Id_activite");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Exo_MVC1.Models.Compagnie_Utilisateur", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Id_compagnie")
                        .HasColumnType("int")
                        .HasColumnName("id_compagnie");

                    b.Property<int>("Id_utilisateur")
                        .HasColumnType("int")
                        .HasColumnName("id_utilisateur");

                    b.Property<string>("Motdepasse")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("motdepasse");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("username");

                    b.HasKey("Id");

                    b.HasIndex("Id_compagnie");

                    b.HasIndex("Id_utilisateur");

                    b.ToTable("Compagnie_Utilisateurs");
                });

            modelBuilder.Entity("Exo_MVC1.Models.Compagny", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Compagnie")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("compagnie");

                    b.HasKey("Id");

                    b.ToTable("Compagnyes");
                });

            modelBuilder.Entity("Exo_MVC1.Models.Pratiquant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Adresse")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("adresse");

                    b.Property<string>("Carte_fede")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("carte_fede");

                    b.Property<string>("Carte_payement")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("carte_payement");

                    b.Property<string>("Consigne")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("consigne");

                    b.Property<string>("Courriel")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("courriel");

                    b.Property<string>("Etiquete")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("etiquete");

                    b.Property<string>("Evaluation")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("evaluation");

                    b.Property<string>("Groupe")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("groupe");

                    b.Property<int>("Id_activite")
                        .HasColumnType("int")
                        .HasColumnName("id_activite");

                    b.Property<int>("Id_categorie")
                        .HasColumnType("int")
                        .HasColumnName("id_categorie");

                    b.Property<int>("Id_session")
                        .HasColumnType("int")
                        .HasColumnName("id_session");

                    b.Property<string>("Mode_payement")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("mode_payement");

                    b.Property<DateTime>("Naissance")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("naissance");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("nom");

                    b.Property<int>("Payement")
                        .HasColumnType("int")
                        .HasColumnName("payement");

                    b.Property<string>("Sexe")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("sexe");

                    b.Property<string>("Tel_urgence")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("tel_urgence");

                    b.Property<string>("Telephone")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("telephone");

                    b.HasKey("Id");

                    b.HasIndex("Id_activite");

                    b.HasIndex("Id_categorie");

                    b.HasIndex("Id_session");

                    b.ToTable("Pratiquants");
                });

            modelBuilder.Entity("Exo_MVC1.Models.Presence", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Abscence")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("absence");

                    b.Property<int>("Id_activite")
                        .HasColumnType("int")
                        .HasColumnName("id_activite");

                    b.Property<int>("Id_categorie")
                        .HasColumnType("int")
                        .HasColumnName("id_categorie");

                    b.Property<int>("Id_pratiquant")
                        .HasColumnType("int")
                        .HasColumnName("id_pratiquant");

                    b.Property<int>("Id_session")
                        .HasColumnType("int")
                        .HasColumnName("id_session");

                    b.Property<string>("Jour")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("jour");

                    b.Property<bool>("Present")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("present");

                    b.HasKey("Id");

                    b.HasIndex("Id_activite");

                    b.HasIndex("Id_categorie");

                    b.HasIndex("Id_pratiquant");

                    b.HasIndex("Id_session");

                    b.ToTable("Presences");
                });

            modelBuilder.Entity("Exo_MVC1.Models.Session", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("nom");

                    b.HasKey("Id");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("Exo_MVC1.Models.UploadExcel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nom")
                        .HasColumnType("longtext")
                        .HasColumnName("nom");

                    b.Property<string>("Sexe")
                        .HasColumnType("longtext")
                        .HasColumnName("sexe");

                    b.HasKey("Id");

                    b.ToTable("UploadExcels");
                });

            modelBuilder.Entity("Exo_MVC1.Models.Utilisateur", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("nom");

                    b.HasKey("Id");

                    b.ToTable("Utilisateurs");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Exo_MVC1.Models.Activite", b =>
                {
                    b.HasOne("Exo_MVC1.Models.Compagny", "Compagny")
                        .WithMany()
                        .HasForeignKey("Id_compagnie")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Compagny");
                });

            modelBuilder.Entity("Exo_MVC1.Models.Categorie", b =>
                {
                    b.HasOne("Exo_MVC1.Models.Activite", "Ativite")
                        .WithMany()
                        .HasForeignKey("Id_activite")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ativite");
                });

            modelBuilder.Entity("Exo_MVC1.Models.Compagnie_Utilisateur", b =>
                {
                    b.HasOne("Exo_MVC1.Models.Compagny", "Compagny")
                        .WithMany()
                        .HasForeignKey("Id_compagnie")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Exo_MVC1.Models.Utilisateur", "Utilisateur")
                        .WithMany()
                        .HasForeignKey("Id_utilisateur")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Compagny");

                    b.Navigation("Utilisateur");
                });

            modelBuilder.Entity("Exo_MVC1.Models.Pratiquant", b =>
                {
                    b.HasOne("Exo_MVC1.Models.Activite", "Ativite")
                        .WithMany()
                        .HasForeignKey("Id_activite")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Exo_MVC1.Models.Categorie", "Categorie")
                        .WithMany()
                        .HasForeignKey("Id_categorie")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Exo_MVC1.Models.Session", "Session")
                        .WithMany()
                        .HasForeignKey("Id_session")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ativite");

                    b.Navigation("Categorie");

                    b.Navigation("Session");
                });

            modelBuilder.Entity("Exo_MVC1.Models.Presence", b =>
                {
                    b.HasOne("Exo_MVC1.Models.Activite", "Ativite")
                        .WithMany()
                        .HasForeignKey("Id_activite")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Exo_MVC1.Models.Categorie", "Categorie")
                        .WithMany()
                        .HasForeignKey("Id_categorie")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Exo_MVC1.Models.Pratiquant", "Pratiquant")
                        .WithMany()
                        .HasForeignKey("Id_pratiquant")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Exo_MVC1.Models.Session", "Session")
                        .WithMany()
                        .HasForeignKey("Id_session")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ativite");

                    b.Navigation("Categorie");

                    b.Navigation("Pratiquant");

                    b.Navigation("Session");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

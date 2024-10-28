
//using Exo_MVC1.Models;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore;

//namespace Exo_MVC1.Data
//{
//    public class ApplicationDbContext : IdentityDbContext
//    {
//        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
//            : base(options)
//        {
//        }
//        public DbSet<Activite> Activites { get; set; } = null!;
//        public DbSet<Categorie> Categories { get; set; } = null!;
//        public DbSet<Utilisateur> Utilisateurs { get; set; } = null!;
//        public DbSet<Admin> Admins { get; set; } = null!;
//        public DbSet<Pratiquant> Pratiquants { get; set; } = null!;
//        public DbSet<Presence> Presences { get; set; } = null!;

//        public DbSet<Session> Sessions { get; set; } = null!;

//        public DbSet<Compagny> Compagnyes { get; set; } = null!;

//        public DbSet<Compagnie_Utilisateur> Compagnie_Utilisateurs { get; set; } = null!;

//        public DbSet<UploadExcel> UploadExcels { get; set; } = null!;
//    }
//}

using Exo_MVC1.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Exo_MVC1.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Activite> Activites { get; set; } = null!;
        public DbSet<Categorie> Categories { get; set; } = null!;
        public DbSet<Utilisateur> Utilisateurs { get; set; } = null!;
        public DbSet<Admin> Admins { get; set; } = null!;
        public DbSet<Pratiquant> Pratiquants { get; set; } = null!;
        public DbSet<Presence> Presences { get; set; } = null!;
        public DbSet<Session> Sessions { get; set; } = null!;
        public DbSet<Compagny> Compagnyes { get; set; } = null!;
        public DbSet<Compagnie_Utilisateur> Compagnie_Utilisateurs { get; set; } = null!;
        public DbSet<UploadExcel> UploadExcels { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Set the table names in lowercase
            modelBuilder.Entity<Activite>().ToTable("activites");
            modelBuilder.Entity<Categorie>().ToTable("categories");
            modelBuilder.Entity<Utilisateur>().ToTable("utilisateurs");
            modelBuilder.Entity<Admin>().ToTable("admins");
            modelBuilder.Entity<Pratiquant>().ToTable("pratiquants");
            modelBuilder.Entity<Presence>().ToTable("presences");
            modelBuilder.Entity<Session>().ToTable("sessions");
            modelBuilder.Entity<Compagny>().ToTable("compagnies");
            modelBuilder.Entity<Compagnie_Utilisateur>().ToTable("compagnie_utilisateurs");
            modelBuilder.Entity<UploadExcel>().ToTable("uploadexcels");
        }
    }
}

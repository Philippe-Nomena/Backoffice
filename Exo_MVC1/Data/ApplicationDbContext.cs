
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
    }
}

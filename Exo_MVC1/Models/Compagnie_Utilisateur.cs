using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exo_MVC1.Models
{
    public class Compagnie_Utilisateur
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("username")]
        public string Username { get; set; }
        [Required]
        [Column("motdepasse")]
        public string Motdepasse { get; set; }

        [Column("id_utilisateur")]
        [DisplayName("utilisateur")]
        public int Id_utilisateur { get; set; }
        [ForeignKey("Id_utilisateur")]
        public virtual Utilisateur? Utilisateur { get; set; }

        [Column("id_compagnie")]
        [DisplayName("compagnie")]
        public int Id_compagnie { get; set; }
        [ForeignKey("Id_compagnie")]
        public virtual Compagny? Compagny { get; set; }
    }
}

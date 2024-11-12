using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exo_MVC1.Models
{
    public class Activite
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("nom")]
        public string Nom { get; set; }

        [Column("imagePath")]
        public string ImagePath { get; set; }

        [Column("id_compagnie")]
        [DisplayName("compagnie")]
        public int Id_compagnie { get; set; }
        [ForeignKey("Id_compagnie")]
        public virtual Compagny? Compagny { get; set; }

        public virtual ICollection<Pratiquant>? Pratiquants { get; set; }
    }
}

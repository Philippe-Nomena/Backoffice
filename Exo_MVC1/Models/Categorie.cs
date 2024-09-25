using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exo_MVC1.Models
{
    public class Categorie
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("categorie")]
        public string Categories { get; set; }
        [Column("horaire")]
        public string Horaire { get; set; }
        [Column("prix")]
        public int Prix { get; set; }
        [Column("jour")]
        public string Jour { get; set; }
        [Column("datedebut")]
        public DateTime Datedebut { get; set; }

        [Column("datefin")]
        public DateTime Datefin { get; set; }

        [Column("id_activite")]
        [DisplayName("activite")]
        public int Id_activite { get; set; }
        [ForeignKey("Id_activite")]
        public virtual Activite? Ativite { get; set; }
    }
}

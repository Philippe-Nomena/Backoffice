using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exo_MVC1.Models
{
    public class Presence
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

      
        [Column("id_session")]
        [DisplayName("session")]
        public int Id_session { get; set; }
        [ForeignKey("Id_session")]
        public virtual Session? Session { get; set; }


        [Column("id_activite")]
        [DisplayName("activite")]
        public int Id_activite { get; set; }
        [ForeignKey("Id_activite")]
        public virtual Activite? Ativite { get; set; }

        [Column("id_categorie")]
        [DisplayName("categorie")]
        public int Id_categorie { get; set; }
        [ForeignKey("Id_categorie")]
        public virtual Categorie? Categorie { get; set; }

        [Column("jour")]
        public string Jour { get; set; }

        [Column("present")]
        public bool Present { get; set; }

        [Column("absence")]
        public bool Abscence { get; set; }

        [Column("id_pratiquant")]

        [DisplayName("pratiquant")]
        public int Id_pratiquant { get; set; }

        [ForeignKey("Id_pratiquant")]
        public virtual Pratiquant? Pratiquant { get; set; }

    }
}

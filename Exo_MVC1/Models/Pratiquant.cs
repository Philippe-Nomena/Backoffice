using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
namespace Exo_MVC1.Models
{
    public class Pratiquant
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("id_session")]
        [DisplayName("session")]
        public int Id_session { get; set; }
        [ForeignKey("Id_session")]
        public virtual Session? Session { get; set; }

        [Column("nom")]
        public string Nom { get; set; }

        [Column("sexe")]
        public string Sexe { get; set; }

        [Column("naissance")]
        public DateOnly? Naissance { get; set; }

        [Column("payement")]
        //public string Payement { get; set; }
        public bool Payement { get; set; }

        [Column("consigne")]
        //public string Consigne { get; set; }
        public bool Consigne { get; set; }

        [Column("carte_fede")]
        //public string Carte_fede { get; set; }
        public bool Carte_fede { get; set; }

        [Column("etiquete")]
        //public string Etiquete { get; set; }
        public bool Etiquete { get; set; }

        [Column("courriel")]
        public string Courriel { get; set; }

        [Column("adresse")]
        public string Adresse { get; set; }

        [Column("telephone")]
        public string Telephone { get; set; }

        [Column("tel_urgence")]
        public string Tel_urgence { get; set; }

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

        [Column("evaluation")]
        public string Evaluation { get; set; }

        [Column("mode_payement")]
        public string Mode_payement { get; set; }

        [Column("carte_payement")]
        public string Carte_payement { get; set; }

        [Column("groupe")]
        public string Groupe { get; set; }
         [NotMapped]
        public string[]? GroupeArray
        {
            get
            {
                
                return !string.IsNullOrEmpty(Groupe) ? JsonConvert.DeserializeObject<string[]>(Groupe) : new string[0];
            }
            set
            {
     
                Groupe = JsonConvert.SerializeObject(value);
            }
        }

    }
}

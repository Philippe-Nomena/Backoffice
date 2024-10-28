using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

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


        [NotMapped]
        public string[]? JourArray
        {
            get
            {
                
                return !string.IsNullOrEmpty(Jour) ? JsonConvert.DeserializeObject<string[]>(Jour) : new string[0];
            }
            set
            {
     
                Jour = JsonConvert.SerializeObject(value);
            }
        }

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

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exo_MVC1.Models
{
    public class UploadExcel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("nom")]
        public string ?Nom { get; set; }
        [Column("sexe")]
        public string ?Sexe { get; set; }
    }
}

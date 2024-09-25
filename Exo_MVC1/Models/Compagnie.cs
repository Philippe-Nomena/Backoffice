using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exo_MVC1.Models
{
    public class Compagny
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("compagnie")]
        public string Compagnie { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exo_MVC1.Models
{
    public class Admin
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("nom")]
        public string ?Nom { get; set; }
        [Column("username")]
        public string ?Username { get; set; }
        [Column("motdepasse")]
        public string ?Motdepasse { get; set; }
    }
}

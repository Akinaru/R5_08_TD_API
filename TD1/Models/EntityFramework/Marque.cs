using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TD1.Models.EntityFramework
{
    [PrimaryKey("idMarque")]
    [Table("marque")]
    public class Marque
    {

        [Key]
        [Column("idmarque")]
        public int idMarque { get; set; }


        [Key]
        [Column("nommarque")]
        public string? nomMarque { get; set; }

        [InverseProperty(nameof(Produit.MarqueNavigation))]
        public virtual ICollection<Produit> Produits { get; set; } = new List<Produit>();
    }
}

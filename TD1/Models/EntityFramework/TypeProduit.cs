using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TD1.Models.EntityFramework
{
    [PrimaryKey("idTypeProduit")]
    [Table("typeproduit")]
    public class TypeProduit
    {

        [Key]
        [Column("idtypeproduit")]
        public int idTypeProduit { get; set; }


        [Key]
        [Column("nomtypeproduit")]
        public string? nomTypeProduit { get; set; }


        [InverseProperty(nameof(Produit.TypeProduitNavigation))]
        public virtual ICollection<Produit> Produits { get; set; } = new List<Produit>();

    }
}

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TD1.Models.EntityFramework
{
    [PrimaryKey("idProduit")]
    [Table("produit")]
    public class Produit
    {

        [Key]
        [Column("idproduit")]
        public int idProduit { get; set; }


        [Column("nomproduit")]
        public string? nomProduit { get; set; }

        [Column("description")]
        public string? description { get; set; }

        [Column("nomphoto")]
        public string? nomPhoto { get; set; }

        [Column("uriphoto")]
        public string? uriPhoto { get; set; }


        [Column("idtypeproduit")]
        public int idTypeProduit { get; set; }

        [Column("idmarque")]
        public int idMarque { get; set; }

        [Column("stockreel")]
        public int stockReel { get; set; }

        [Column("stockmin")]
        public int stockMin { get; set; }

        [Column("stockmax")]
        public int stockMax { get; set; }

        [ForeignKey("idMarque")]
        [InverseProperty(nameof(Marque.Produits))]
        public virtual Marque? MarqueNavigation { get; set; }

        [ForeignKey("idTypeProduit")]
        [InverseProperty(nameof(TypeProduit.Produits))]
        public virtual TypeProduit? TypeProduitNavigation { get; set; }
    }
}

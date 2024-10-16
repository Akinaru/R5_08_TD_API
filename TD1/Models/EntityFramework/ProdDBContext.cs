using Microsoft.EntityFrameworkCore;

namespace TD1.Models.EntityFramework
{
    public partial class ProdDBContext : DbContext
    {

        public ProdDBContext() { }

        public ProdDBContext(DbContextOptions<ProdDBContext> options) : base(options)
        {
        }

/*        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLoggerFactory(MyLoggerFactory)
                 .EnableSensitiveDataLogging()
                 .UseNpgsql("Server=localhost;port=5432;Database=TD1;uid = postgres; password = postgres; ");
                optionsBuilder.UseLazyLoadingProxies();
            }
        }*/

        public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

        public virtual DbSet<Produit> Produits { get; set; } = null!;
        public virtual DbSet<Marque> Marques { get; set; } = null!;
        public virtual DbSet<TypeProduit> TypeProduits { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuration de l'entité Produit
            modelBuilder.Entity<Produit>(entity =>
            {
                entity.HasKey(e => new { e.idProduit }).HasName("pk_produit");

                entity.HasOne(d => d.MarqueNavigation)
                    .WithMany(p => p.Produits)
                    .HasForeignKey(d => d.idMarque)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_produit_marque");

                entity.HasOne(d => d.TypeProduitNavigation)
                    .WithMany(p => p.Produits)
                    .HasForeignKey(d => d.idTypeProduit)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_produit_typeproduit");

                // Données de départ pour Produit
                entity.HasData(
                    new Produit
                    {
                        idProduit = 1,
                        nomProduit = "Iphone 16 Pro Max",
                        description = "Iphone dernière génération.",
                        nomPhoto = "photo1.jpg",
                        uriPhoto = "https://www.cdiscount.com/pdt2/a/t/u/1/700x700/ip16prom512natu/rw/apple-iphone-16-pro-max-512gb-natural-titanium.jpg",
                        idTypeProduit = 1,
                        idMarque = 1,
                        stockReel = 100,
                        stockMin = 10,
                        stockMax = 200
                    },
                    new Produit
                    {
                        idProduit = 2,
                        nomProduit = "Iphone 15 Pro Max",
                        description = "Iphone avant dernière génération.",
                        nomPhoto = "photo2.jpg",
                        uriPhoto = "https://static.fnac-static.com/multimedia/Images/FR/MDM/34/f1/52/22212916/1540-1/tsp20240918073442/Apple-iPhone-15-Pro-Max-6-7-5G-Double-SIM-256-Go-Bleu-Titanium.jpg",
                        idTypeProduit = 1,
                        idMarque = 1,
                        stockReel = 50,
                        stockMin = 5,
                        stockMax = 100
                    },
                    new Produit
                    {
                        idProduit = 3,
                        nomProduit = "Iphone 14 Pro Max",
                        description = "Iphone avant avant dernière génération.",
                        nomPhoto = "photo2.jpg",
                        uriPhoto = "https://m.media-amazon.com/images/I/71yzJoE7WlL.__AC_SX300_SY300_QL70_ML2_.jpg",
                        idTypeProduit = 1,
                        idMarque = 1,
                        stockReel = 50,
                        stockMin = 5,
                        stockMax = 100
                    },
                     new Produit
                     {
                         idProduit = 4,
                         nomProduit = "Apple Watch S10",
                         description = "Apple watch dernière génération.",
                         nomPhoto = "photo2.jpg",
                         uriPhoto = "https://www.cdiscount.com/pdt2/l/s/m/1/700x700/wat10cel46silsm/rw/apple-watch-series-10-gps-cellular-46mm-boit.jpg",
                         idTypeProduit = 2,
                         idMarque = 1,
                         stockReel = 50,
                         stockMin = 5,
                         stockMax = 100
                     }
                );
            });

            // Configuration de l'entité Marque
            modelBuilder.Entity<Marque>(entity =>
            {
                entity.HasKey(e => new { e.idMarque }).HasName("pk_marque");

                // Données de départ pour Marque
                entity.HasData(
                    new Marque
                    {
                        idMarque = 1,
                        nomMarque = "Apple"
                    }
                );
            });

            // Configuration de l'entité TypeProduit
            modelBuilder.Entity<TypeProduit>(entity =>
            {
                entity.HasKey(e => new { e.idTypeProduit }).HasName("pk_typeproduit");

                // Données de départ pour TypeProduit
                entity.HasData(
                    new TypeProduit
                    {
                        idTypeProduit = 1,
                        nomTypeProduit = "Téléphone"
                    },
                    new TypeProduit
                    {
                        idTypeProduit = 2,
                        nomTypeProduit = "Montre connecté"
                    }
                );
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TD1.Models.EntityFramework;
using TD1.Models.Repository;

namespace TD1.Models.DataManager
{
    public class ProduitManager : IDataRepository<Produit>
    {

        readonly ProdDBContext? prodDbContext;

        public ProduitManager() { }

        public ProduitManager(ProdDBContext context)
        {
            prodDbContext = context;
        }

        public async Task<ActionResult<IEnumerable<Produit>>> GetAllAsync()
        {
            return await prodDbContext.Produits
                .Include(p => p.MarqueNavigation) 
                .Include(p => p.TypeProduitNavigation) 
                .ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<Produit>>> GetAllByNameAsync(string name)
        {
            var produits = await prodDbContext.Produits
                .Where(u => u.nomProduit.ToLower().Contains(name.ToLower()))
                .ToListAsync();

            return produits;
        }


        public async Task<ActionResult<Produit>> GetByIdAsync(int id)
        {
            return await prodDbContext.Produits
                .Include(p => p.MarqueNavigation)
                .Include(p => p.TypeProduitNavigation)
                .FirstOrDefaultAsync(u => u.idProduit == id);
        }

        public async Task<ActionResult<Produit>> GetByNameAsync(string name)
        {
            return await prodDbContext.Produits
                .FirstOrDefaultAsync(u => u.nomProduit == name);
        }

        public async Task AddAsync(Produit entity)
        {
            await prodDbContext.Produits.AddAsync(entity);
            await prodDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Produit entity)
        {
            prodDbContext.Produits.Remove(entity);
            await prodDbContext.SaveChangesAsync();
        }


        public async Task UpdateAsync(Produit produit, Produit entity)
        {
            prodDbContext.Entry(produit).State = EntityState.Modified;
            produit.idProduit= entity.idProduit;
            produit.nomProduit = entity.nomProduit;
            produit.description = entity.description;
            produit.nomPhoto = entity.nomPhoto;
            produit.uriPhoto = entity.uriPhoto;
            produit.idTypeProduit = entity.idTypeProduit;
            produit.idMarque = entity.idMarque;
            produit.stockReel = entity.stockReel;
            produit.stockMin = entity.stockMin;
            produit.stockMax = entity.stockMax;
            await prodDbContext.SaveChangesAsync();
        }
    }
}

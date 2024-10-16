using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TD1.Models.EntityFramework;
using TD1.Models.Repository;

namespace TD1.Models.DataManager
{
    public class TypeProduitManager : IDataRepository<TypeProduit>
    {

        readonly ProdDBContext? prodDbContext;

        public TypeProduitManager() { }

        public TypeProduitManager(ProdDBContext context)
        {
            prodDbContext = context;
        }

        public async Task<ActionResult<IEnumerable<TypeProduit>>> GetAllAsync()
        {
            return await prodDbContext.TypeProduits.ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<TypeProduit>>> GetAllByNameAsync(string name)
        {
            var produits = await prodDbContext.TypeProduits
                .Where(u => u.nomTypeProduit.ToLower().Contains(name.ToLower()))
                .ToListAsync();

            return produits;
        }

        public async Task<ActionResult<TypeProduit>> GetByIdAsync(int id)
        {
            return await prodDbContext.TypeProduits.FirstOrDefaultAsync(u => u.idTypeProduit== id);
        }

        public async Task<ActionResult<TypeProduit>> GetByNameAsync(string name)
        {
            return await prodDbContext.TypeProduits.FirstOrDefaultAsync(u => u.nomTypeProduit== name);
        }

        public async Task AddAsync(TypeProduit entity)
        {
            await prodDbContext.TypeProduits.AddAsync(entity);
            await prodDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(TypeProduit entity)
        {
            prodDbContext.TypeProduits.Remove(entity);
            await prodDbContext.SaveChangesAsync();
        }


        public async Task UpdateAsync(TypeProduit typeProduit, TypeProduit entity)
        {
            prodDbContext.Entry(typeProduit).State = EntityState.Modified;
            typeProduit.idTypeProduit = entity.idTypeProduit;
            typeProduit.nomTypeProduit = entity.nomTypeProduit;
            await prodDbContext.SaveChangesAsync();
        }
    }
}

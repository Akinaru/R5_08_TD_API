using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TD1.Models.EntityFramework;
using TD1.Models.Repository;

namespace TD1.Models.DataManager
{
    public class MarqueManager : IDataRepository<Marque>
    {

        readonly ProdDBContext? prodDbContext;

        public MarqueManager () { }

        public MarqueManager (ProdDBContext context) {
            prodDbContext = context;
        }

        public async Task<ActionResult<IEnumerable<Marque>>> GetAllAsync()
        {
            return await prodDbContext.Marques.ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<Marque>>> GetAllByNameAsync(string name)
        {
            var produits = await prodDbContext.Marques
                .Where(u => u.nomMarque.ToLower().Contains(name.ToLower()))
                .ToListAsync();

            return produits;
        }

        public async Task<ActionResult<Marque>> GetByIdAsync(int id)
        {
            return await prodDbContext.Marques.FirstOrDefaultAsync(u => u.idMarque == id);
        }

        public async Task<ActionResult<Marque>> GetByNameAsync(string name)
        {
            return await prodDbContext.Marques.FirstOrDefaultAsync(u => u.nomMarque == name);
        }

        public async Task AddAsync(Marque entity)
        {
            await prodDbContext.Marques.AddAsync(entity);
            await prodDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Marque entity)
        {
            prodDbContext.Marques.Remove(entity);
            await prodDbContext.SaveChangesAsync();
        }


        public async Task UpdateAsync(Marque marque, Marque entity)
        {
            prodDbContext.Entry(marque).State = EntityState.Modified;
            marque.idMarque = entity.idMarque;
            marque.nomMarque = entity.nomMarque;
            await prodDbContext.SaveChangesAsync();
        }
    }
}

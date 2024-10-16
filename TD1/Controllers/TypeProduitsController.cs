using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TD1.Models.EntityFramework;
using TD1.Models.Repository;

namespace TD1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeProduitsController : ControllerBase
    {
        private readonly IDataRepository<TypeProduit> dataRepository;

        public TypeProduitsController(IDataRepository<TypeProduit> dataRepo)
        {
            dataRepository = dataRepo;
        }

        // GET: api/TypeProduits
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeProduit>>> GetAllTypeProduit()
        {
            return await dataRepository.GetAllAsync();
        }

        // GET: api/TypeProduits/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TypeProduit>> GetTypeProduitById(int id)
        {
            var typeProduit = await dataRepository.GetByIdAsync(id);

            if (typeProduit == null)
            {
                return NotFound();
            }

            return typeProduit;
        }

        // PUT: api/TypeProduits/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTypeProduit(int id, TypeProduit typeProduit)
        {
            if (id != typeProduit.idTypeProduit)
            {
                return BadRequest();
            }

            var typeproduitToUpdate = await dataRepository.GetByIdAsync(id);

            if (typeproduitToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.UpdateAsync(typeproduitToUpdate.Value, typeProduit);
                return NoContent();
            }
        }

        // POST: api/TypeProduits
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TypeProduit>> PostTypeProduit(TypeProduit typeProduit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(typeProduit);

            return CreatedAtAction("GetById", new { id = typeProduit.idTypeProduit}, typeProduit);
        }

        // DELETE: api/TypeProduits/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTypeProduit(int id)
        {
            var typeproduit = await dataRepository.GetByIdAsync(id);
            if (typeproduit == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(typeproduit.Value);
            return NoContent();
        }
    }
}

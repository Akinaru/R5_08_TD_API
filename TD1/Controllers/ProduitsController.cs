using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TD1.Models.DTO;
using TD1.Models.EntityFramework;
using TD1.Models.Repository;

namespace TD1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProduitsController : ControllerBase
    {
        private readonly IDataRepository<Produit> dataRepository;
        private readonly IMapper _mapper;

        public ProduitsController(IDataRepository<Produit> dataRepo, IMapper mapper = null)
        {
            dataRepository = dataRepo;
            _mapper = mapper;
        }


        [HttpGet("automapper")]
        public async Task<ActionResult<IEnumerable<ProduitDto>>> GetAllProduitWithAutoMapper()
        {
            // Récupération des produits
            var result = await dataRepository.GetAllAsync();
            var produits = result.Value;

            // Vérifie si la liste est vide
            if (produits == null || !produits.Any()) 
            {
                return NotFound();
            }

            // Mappage de la liste des produits vers ProduitDto
            var produitsDto = _mapper.Map<IEnumerable<ProduitDto>>(produits);

            // Retourne la liste des DTO
            return Ok(produitsDto);
        }




        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produit>>> GetAllProduit()
        {
            return await dataRepository.GetAllAsync();
        }

        [HttpGet("allbyname/{name}")]
        public async Task<ActionResult<IEnumerable<Produit>>> GetAllByNameProduit(string name)
        {
            return await dataRepository.GetAllByNameAsync(name);
        }




        // GET: api/Produits/5
        [HttpGet("automapper/{id}")]
        public async Task<ActionResult<ProduitDto>> GetProduitByIdWithAutoMapper(int id)
        {
            var produit = await dataRepository.GetByIdAsync(id);
            if (produit == null)
            {
                return NotFound();
            }
            var produitDto = _mapper.Map<ProduitDto>(produit);
            return Ok(produitDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Produit>> GetProduitById(int id)
        {
            var produit = await dataRepository.GetByIdAsync(id);
            if (produit == null)
            {
                return NotFound();
            }
            return produit;
        }

        [HttpGet("getbyname/{name}")]
        public async Task<ActionResult<Produit>> GetProduitByName(string name)
        {
            var produit = await dataRepository.GetByNameAsync(name);
            if (produit == null)
            {
                return NotFound();
            }
            return produit;
        }



        // PUT: api/Produits/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduit(int id, Produit produit)
        {
            if (id != produit.idProduit)
            {
                return BadRequest();
            }

            var produitToUpdate = await dataRepository.GetByIdAsync(id);

            if (produitToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.UpdateAsync(produitToUpdate.Value, produit);
                return NoContent();
            }
        }

        // POST: api/Produits
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Produit>> PostProduit(Produit produit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(produit);

            return CreatedAtAction("GetProduitById", new { id = produit.idProduit}, produit);
        }

        // DELETE: api/Produits/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduit(int id)
        {
            var produit = await dataRepository.GetByIdAsync(id);
            if (produit == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(produit.Value);
            return NoContent();
        }

    }
}

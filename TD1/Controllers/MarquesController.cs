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
    public class MarquesController : ControllerBase
    {
        // private readonly ProdDBContext _context;

        private readonly IDataRepository<Marque> dataRepository;

        public MarquesController(IDataRepository<Marque> dataRepo)
        {
            dataRepository = dataRepo;
        }

        // GET: api/Marques
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Marque>>> GetAllMarque()
        {
            return await dataRepository.GetAllAsync();
        }

        // GET: api/Marques/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Marque>> GetMarqueById(int id)
        {
            var marque = await dataRepository.GetByIdAsync(id);

            if (marque == null)
            {
                return NotFound();
            }

            return marque;
        }

        // PUT: api/Marques/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMarque(int id, Marque marque)
        {
            if (id != marque.idMarque)
            {
                return BadRequest();
            }

            var marqueToUpdate = await dataRepository.GetByIdAsync(id);

            if (marqueToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.UpdateAsync(marqueToUpdate.Value, marque);
                return NoContent();
            }
        }

        // POST: api/Marques
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Marque>> PostMarque(Marque marque)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(marque);

            return CreatedAtAction("GetById", new { id = marque.idMarque }, marque);
        }

        // DELETE: api/Marques/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMarque(int id)
        {
            var marque = await dataRepository.GetByIdAsync(id);
            if (marque == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(marque.Value);
            return NoContent();
        }

    }
}

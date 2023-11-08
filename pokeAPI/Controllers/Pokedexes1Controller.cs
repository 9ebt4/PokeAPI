using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pokeAPI.Models;

namespace pokeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Pokedexes1Controller : ControllerBase
    {
        private readonly PokemonDbContext _context;
        PokemonDbContext _db = new PokemonDbContext();
        public Pokedexes1Controller(PokemonDbContext context)
        {
            _context = context;
        }

        // GET: api/Pokedexes1
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pokedex>>> GetPokedices()
        {
          if (_context.Pokedices == null)
          {
              return NotFound();
          }
            return await _db.Pokedices.ToListAsync();
        }

        // GET: api/Pokedexes1/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pokedex>> GetPokedex(int id)
        {
          if (_context.Pokedices == null)
          {
              return NotFound();
          }
            var pokedex = await _context.Pokedices.FindAsync(id);

            if (pokedex == null)
            {
                return NotFound();
            }

            return pokedex;
        }

        // PUT: api/Pokedexes1/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPokedex(int id, Pokedex pokedex)
        {
            if (id != pokedex.Id)
            {
                return BadRequest();
            }

            _context.Entry(pokedex).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PokedexExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Pokedexes1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Pokedex>> PostPokedex(Pokedex pokedex)
        {
          if (_context.Pokedices == null)
          {
              return Problem("Entity set 'PokemonDbContext.Pokedices'  is null.");
          }
            _context.Pokedices.Add(pokedex);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPokedex", new { id = pokedex.Id }, pokedex);
        }
        PokeAPIController pokeAPIContext = new PokeAPIController();
        [HttpPost("Api")]
        public async Task<ActionResult<Pokedex>> createPokemonAPI()
        {
            Pokemon_Species pokemon = pokeAPIContext.getRandomPokemon();
            Pokedex pokedex = new Pokedex();
            pokedex.Name = pokemon.name;
            _context.Pokedices.Add(pokedex);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetPokedex", new { id = pokedex.Id }, pokedex);
        }

        // DELETE: api/Pokedexes1/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePokedex(int id)
        {
            if (_context.Pokedices == null)
            {
                return NotFound();
            }
            var pokedex = await _context.Pokedices.FindAsync(id);
            if (pokedex == null)
            {
                return NotFound();
            }

            _context.Pokedices.Remove(pokedex);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PokedexExists(int id)
        {
            return (_context.Pokedices?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

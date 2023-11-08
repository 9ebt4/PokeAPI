using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using pokeAPI.Models;

namespace pokeAPI.Controllers
{
    public class PokedexesController : Controller
    {
        private readonly PokemonDbContext _context;

        public PokedexesController(PokemonDbContext context)
        {
            _context = context;
        }

        // GET: Pokedexes

        public async Task<IActionResult> Index()
        {
              return _context.Pokedices != null ? 
                          View(await _context.Pokedices.ToListAsync()) :
                          Problem("Entity set 'PokemonDbContext.Pokedices'  is null.");
        }

        // GET: Pokedexes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pokedices == null)
            {
                return NotFound();
            }

            var pokedex = await _context.Pokedices
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pokedex == null)
            {
                return NotFound();
            }

            return View(pokedex);
        }

        // GET: Pokedexes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pokedexes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Pokedex pokedex)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pokedex);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pokedex);
        }

        // GET: Pokedexes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pokedices == null)
            {
                return NotFound();
            }

            var pokedex = await _context.Pokedices.FindAsync(id);
            if (pokedex == null)
            {
                return NotFound();
            }
            return View(pokedex);
        }

        // POST: Pokedexes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Pokedex pokedex)
        {
            if (id != pokedex.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pokedex);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PokedexExists(pokedex.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pokedex);
        }

        // GET: Pokedexes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pokedices == null)
            {
                return NotFound();
            }

            var pokedex = await _context.Pokedices
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pokedex == null)
            {
                return NotFound();
            }

            return View(pokedex);
        }

        // POST: Pokedexes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pokedices == null)
            {
                return Problem("Entity set 'PokemonDbContext.Pokedices'  is null.");
            }
            var pokedex = await _context.Pokedices.FindAsync(id);
            if (pokedex != null)
            {
                _context.Pokedices.Remove(pokedex);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PokedexExists(int id)
        {
          return (_context.Pokedices?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

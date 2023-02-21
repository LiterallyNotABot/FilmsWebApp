using FilmsWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilmsWebApp.Controllers
{
    public class DirectorsController : Controller
    {
        private readonly MoviesContext _context;

        public DirectorsController(MoviesContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            IQueryable<Director> directors = _context.Directors.Select(d => d);
            return View(directors);
        }

        public IActionResult Create() 
        {
            return View();       
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DirectorName,DirectorTableId")] Director director)
        {
            if (ModelState.IsValid)
            {
                _context.Add(director);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(director);
        }

        public IActionResult Details(int? id) 
        {
            if (id == null || _context.Directors == null)
            {
                return NotFound();
            }

            var director = _context.Directors.Where(d => d.DirectorTableId == id).Include(f => f.Films)
                           .FirstOrDefault();

            if (director != null) 
            {
                ViewData["Count"] = director.Films.Count();
                return View(director);
            }

            return NotFound();
        }

        public IActionResult UpdateIndex()
        {
            var directorsQuery = _context.Directors.Select(f => f);

            if (directorsQuery.Any())
            {
                return View(directorsQuery);
            }

            return NotFound();
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Directors == null)
            {
                return NotFound();
            }

            var director = await _context.Directors.FindAsync(id);

            if (director == null)
            {
                return NotFound();
            }
            return View(director);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DirectorName,DirectorTableId")] Director director)
        {
            if (id != director.DirectorTableId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(director);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DirectorExists(director.DirectorTableId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(director);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Directors == null)
            {
                return NotFound();
            }

            var director = await _context.Directors
                .FirstOrDefaultAsync(d => d.DirectorTableId == id);
            if (director == null)
            {
                return NotFound();
            }

            return View(director);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Directors == null)
            {
                return Problem("Entity set 'MoviesDBContext.Directors'  is null.");
            }

            var director = await _context.Directors.FindAsync(id);
            
            if (director != null)
            {
                _context.Directors.Remove(director);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool DirectorExists(int id)
        {
            return _context.Directors.Any(e => e.DirectorTableId == id);
        }
    }
}

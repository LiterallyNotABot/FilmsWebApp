using FilmsWebApp.Models;
using FilmsWebApp.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;

namespace FilmsWebApp.Controllers
{
    public class ActorsController : Controller
    {
        private readonly MoviesContext _context;

        public ActorsController(MoviesContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            IQueryable<Actor> actors = _context.Actors.Select(a => a);
            return View(actors);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ActorName,ActorId")] Actor actor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(actor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(actor);
        }

        public IActionResult Details(int? id) 
        {
            if (id == null || _context.Actors == null)
            {
                return NotFound();
            }

            var actor = _context.Actors.Where(a => a.ActorId == id).FirstOrDefault();

            if (actor != null) 
            {
                var filmsQuery = _context.FilmsAndActors.Where(fa => fa.ActorIdFk == actor.ActorId)
                                .Join(_context.Films, fa => fa.FilmIdFk, f => f.FilmId, (fa, f) => f)
                                .Include(f=>f.Director);

                ActorsDetailsViewModel actorVM = new ActorsDetailsViewModel()
                {
                    actor = actor,
                    filmography = filmsQuery
                };

                ViewData["Count"] = actorVM.filmography.Count();
                return View(actorVM);
            }

            return NotFound();
        }

        public IActionResult UpdateIndex()
        {
            var actorsQuery = _context.Actors.Select(f => f);

            if (actorsQuery.Any())
            {
                return View(actorsQuery);
            }

            return NotFound();
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Actors == null)
            {
                return NotFound();
            }

            var actor = await _context.Actors.FindAsync(id);

            if (actor == null)
            {
                return NotFound();
            }
            return View(actor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ActorName,ActorId")] Actor actor)
        {
            if (id != actor.ActorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(actor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActorExists(actor.ActorId))
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
            return View(actor);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Actors == null)
            {
                return NotFound();
            }

            var actor = await _context.Actors.FindAsync(id);

            if (actor == null)
            {
                return NotFound();
            }

            return View(actor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Actors == null)
            {
                return Problem("Entity set 'MoviesDBContext.Actors'  is null.");
            }
            
            var actor = await _context.Actors.FindAsync(id);
            
            if (actor != null)
            {
                _context.Actors.Remove(actor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> UpdateFilms(int? id) 
        {
            if (id == null || _context.Actors == null)
            {
                return NotFound();
            }

            var actor = await _context.Actors.FindAsync(id);

            if (actor == null)
            {
                return NotFound();
            }

            var filmsQuery = _context.FilmsAndActors.Where(fa => fa.ActorIdFk == actor.ActorId)
                            .Join(_context.Films, fa => fa.FilmIdFk, f => f.FilmId, (fa, f) => f)
                            .Include(d=>d.Director);

            var restOfFilmsQuery = _context.Films.Where(f => !filmsQuery.Contains(f));

            IEnumerable<Film> mylist = filmsQuery.ToList();
            ActorsUpdateFilmsViewModel actorVM = new ActorsUpdateFilmsViewModel()
            {
                actor = actor,
                bindedFilms = mylist,
                ActorId = actor.ActorId
            };

            ViewData["Unbinded"] = new SelectList(restOfFilmsQuery, "FilmId", "Title");
            return View(actorVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Bind(int id, [Bind("actor,FilmId,bindedFilms,ActorId")]ActorsUpdateFilmsViewModel model) 
        {       
            if (ModelState.IsValid)
            {
                if (model.FilmId != null)
                {
                    FilmsAndActor newBinding = new FilmsAndActor()
                    {
                        FilmIdFk = model.FilmId,
                        ActorIdFk = model.actor.ActorId
                    };

                    var verif = _context.FilmsAndActors.Any(r=>r.FilmIdFk == newBinding.FilmIdFk &&
                                r.ActorIdFk == newBinding.ActorIdFk);

            //var verif2 = _context.FilmsAndActors.Any(r => r.FilmIdFk == newBinding.FilmIdFk);


            //    if (verif2) { return View(newBinding); }

                    if (!verif) 
                    {
                        _context.FilmsAndActors.Add(newBinding);
                        await _context.SaveChangesAsync();

                        return RedirectToAction(nameof(Index));
                    }
                }

            }
            return RedirectToAction(nameof(UpdateIndex));     
        }

        public IActionResult TEST(FilmsAndActor a) { return View(a); }

        private bool ActorExists(int id)
        {
            return _context.Actors.Any(e => e.ActorId == id);
        }
    }
}

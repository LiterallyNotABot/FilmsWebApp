using FilmsWebApp.Models;
using FilmsWebApp.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FilmsWebApp.Controllers
{
    public class FilmsController : Controller
    {
        private readonly MoviesContext _context;

        public FilmsController(MoviesContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var SetQuery = from film in _context.Films
                           join director in _context.Directors on film.DirectorId equals director.DirectorTableId
                           select new FilmsIndexViewModel
                           {
                               Film = film,
                               Director = director,
                               Actors = (from actor in _context.Actors
                                         join fiac in _context.FilmsAndActors on actor.ActorId equals fiac.ActorIdFk
                                         where fiac.FilmIdFk == film.FilmId
                                         select actor).ToList()
                           };

            return View(SetQuery.ToList());
        }

        public IActionResult Create()
        {
            ViewData["Directors"] = new SelectList(_context.Directors, "DirectorTableId", "DirectorName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Genre,ReleaseYear,DirectorTableId")] FilmsCreateViewModel filmModel)
        {   

            if (ModelState.IsValid)
            {
                //verify there's not a film with same title and same director on the DB already

                var verif = _context.Films.Any(f => f.Title.ToUpper() == filmModel.Title.ToUpper() &&
                                                    f.DirectorId == filmModel.DirectorTableId);

                if (!verif) 
                {
                    var film = new Film()
                    {
                        Title = filmModel.Title,
                        Genre = filmModel.Genre,
                        ReleaseYear = filmModel.ReleaseYear,
                        DirectorId = filmModel.DirectorTableId
                    };

                    _context.Add(film);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else 
                {
                    ViewBag.Coincidence = "There is already a film with the same title and director";
                    return View(filmModel);
                }               
            }

            return View(filmModel);
        }

        public IActionResult Details(int? id) 
        {
            if (id == null || _context.Films == null)
            {
                return NotFound();
            }

            var filmQuery = _context.Films.Where(f => f.FilmId == id).Include(f => f.Director).FirstOrDefault();
            
            if (filmQuery != null) 
            {
                var castQuery = _context.FilmsAndActors.Where(fa => fa.FilmIdFk == filmQuery.FilmId)
                   .Join(_context.Actors, fa => fa.ActorIdFk, a => a.ActorId, (fa, a) => a);

                FilmsDetailsViewModel filmVM = new FilmsDetailsViewModel()
                {
                    film = filmQuery,
                    Cast = castQuery
                };

                return View(filmVM);

            }

            return NotFound();
        }

        public IActionResult UpdateIndex() 
        {
            var filmsQuery = _context.Films.Select(f=>f);

            if (filmsQuery.Any()) 
            {
                return View(filmsQuery);
            }

            return NotFound();
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Films == null)
            {
                return NotFound();
            }

            var film = await _context.Films.FindAsync(id);

            if (film == null)
            {
                return NotFound();
            }

            var director = await _context.Directors.Where(d => d.DirectorTableId == film.DirectorId)
                          .FirstOrDefaultAsync();

            if (director == null)
            {
                return NotFound();
            }

            FilmsEditViewModel filmVM = new FilmsEditViewModel()
            {
                Title = film.Title,
                Genre = film.Genre,
                FilmId = film.FilmId,
                ReleaseYear = film.ReleaseYear,
                directorTableId = film.DirectorId
            };
            
            ViewData["Directors"] = new SelectList(_context.Directors, "DirectorTableId", "DirectorName");
            return View(filmVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Title,Genre,ReleaseYear,directorTableId")] FilmsEditViewModel filmVM)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var existingFilm = _context.Films.Find(id);

                    existingFilm.Title = filmVM.Title;
                    existingFilm.Genre = filmVM.Genre;
                    existingFilm.ReleaseYear = filmVM.ReleaseYear;
                    existingFilm.DirectorId = filmVM.directorTableId;

                    var coincidenceQuery = _context.Films.Any(f => f.DirectorId == existingFilm.DirectorId
                                           && f.Title.ToUpper() == existingFilm.Title.ToUpper());
                    
                    if (!coincidenceQuery) 
                    {
                        _context.Update(existingFilm);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        ViewBag.Coincidence = "There is already a film with the same title and director";
                        ViewData["Directors"] = new SelectList(_context.Directors, "DirectorTableId", "DirectorName");
                        return View(filmVM);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilmExists(filmVM.FilmId))
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
            
            ViewData["Directors"] = new SelectList(_context.Directors, "DirectorTableId", "DirectorName");
            return View(filmVM);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Actors == null)
            {
                return NotFound();
            }

            var film = await _context.Films
                .FirstOrDefaultAsync(f => f.FilmId == id);

            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Films == null)
            {
                return Problem("Entity set 'MoviesDBContext.Directors'  is null.");
            }

            var film = await _context.Films.FindAsync(id);

            if (film != null)
            {
                _context.Films.Remove(film);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

         public async Task<IActionResult> UpdateCast(int? id)
        {
            if (id == null || _context.Actors == null)
            {
                return NotFound();
            }

            var film = await _context.Films.FindAsync(id);

            if (film == null)
            {
                return NotFound();
            }

            film.Director = await _context.Directors.FindAsync(film.DirectorId);

            var castQuery = _context.FilmsAndActors.Where(fa => fa.FilmIdFk == film.FilmId)
                            .Join(_context.Actors, fa => fa.ActorIdFk, a => a.ActorId, (fa, a) => a);

            var restOfActorsQuery = _context.Actors.Where(a => !castQuery.Contains(a));

            IEnumerable<Actor> mylist = castQuery.ToList();

            FilmsUpdateCastViewModel filmVM = new FilmsUpdateCastViewModel()
            {
                film = film,
                cast = mylist,
                Director = film.Director
            };

            ViewData["Unbinded"] = new SelectList(restOfActorsQuery, "ActorId", "ActorName");
            return View(filmVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Bind(int id, [Bind("film,ActorId,Director,cast,DirectorName")] FilmsUpdateCastViewModel model)
        {

            if (ModelState.IsValid)
            {   
                if (model.ActorId != null)
                {
                    


                        FilmsAndActor newBinding = new FilmsAndActor()
                        {
                             FilmIdFk = model.film.FilmId,
                             ActorIdFk = (int)model.ActorId
                        }; 

                    var verif = _context.FilmsAndActors.Any(r => r.FilmIdFk == newBinding.FilmIdFk &&
                                r.ActorIdFk == newBinding.ActorIdFk);

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Unbind(int id, [Bind("film,ActorId,cast,Director")] FilmsUpdateCastViewModel model)
        {
            if (id != null)
            {
                model.ActorId = id;
            }

            if (ModelState.IsValid)
            {

                FilmsAndActor newUnbinding = new FilmsAndActor()
                {
                    FilmIdFk = model.film.FilmId,
                    ActorIdFk = model.ActorId
                };

                var pkRecord = _context.FilmsAndActors.Where(r => r.FilmIdFk == newUnbinding.FilmIdFk
                            && r.ActorIdFk == newUnbinding.ActorIdFk).Select(r => r.TableId)
                            .FirstOrDefault();

                if (pkRecord != null)
                {
                    newUnbinding.TableId = pkRecord;
                    _context.FilmsAndActors.Remove(newUnbinding);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }

            }
            return RedirectToAction(nameof(UpdateIndex));
        }

        private bool FilmExists(int id)
        {
            return _context.Films.Any(f => f.FilmId == id);
        }

    }
}

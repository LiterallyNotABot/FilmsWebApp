using FilmsWebApp.Models;
using FilmsWebApp.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace FilmsWebApp.Controllers
{
    public class SearchController : Controller
    {
        private readonly MoviesContext _context;

        public SearchController(MoviesContext context)
        {
            _context = context;
        }

        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Search(string searchString)
        {
            var filmsQuery = _context.Films.Where(f => f.Title.ToUpper().Contains(searchString.ToUpper()) ||
                                                 f.Genre.ToUpper().Contains(searchString.ToUpper()) ||
                                                 f.ReleaseYear.ToString().Contains(searchString))
                                                 .ToList();

            var actorsQuery = _context.Actors.Where(a => a.ActorName.ToUpper().Contains(searchString.ToUpper()))
                                                   .ToList();

            var directorsQuery = _context.Directors.Where(d => d.DirectorName.ToUpper().Contains(searchString.ToUpper()))
                                                         .ToList();

            /*it is more convenient to work with IEnumerable, since the search engine won't perform
             any update task, nor iterate over an item several times*/

            IEnumerable<Film> films = filmsQuery;
            IEnumerable<Director> directors = directorsQuery;
            IEnumerable<Actor> actors = actorsQuery;

            if (films.Any() || directors.Any() || actors.Any())
            {
                var resultSet = new SearchViewModel()
                {
                    filmSet = films,
                    actorSet = actors,
                    directorSet = directors
                };

                return View(resultSet);
            }
            
            ViewBag.Message = "No content found";
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel;

namespace FilmsWebApp.Models.ViewModel
{
    public class ActorsUpdateFilmsViewModel
    {
        public Actor actor { get; set; }

        public IEnumerable<Film> bindedFilms { get; set; }

        [DisplayName("Title")]
        public int FilmId { get; set; }

        public int ActorId { get; set; }
    }
}

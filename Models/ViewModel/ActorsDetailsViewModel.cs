namespace FilmsWebApp.Models.ViewModel
{
    public class ActorsDetailsViewModel
    {
        public Actor actor { get; set; } = null!;

        public IQueryable<Film> filmography { get; set; }
    }
}

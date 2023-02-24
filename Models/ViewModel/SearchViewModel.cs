namespace FilmsWebApp.Models.ViewModel
{
    public class SearchViewModel
    {
        public IEnumerable<Film> filmSet { get; set; }
        public IEnumerable<Director> directorSet { get; set; }
        public IEnumerable<Actor> actorSet { get; set; }
    }
}

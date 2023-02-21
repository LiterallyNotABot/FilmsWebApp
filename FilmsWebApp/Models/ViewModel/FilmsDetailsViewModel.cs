namespace FilmsWebApp.Models.ViewModel
{
    public class FilmsDetailsViewModel
    {
        public Film film { get; set; } = null!;

        public IQueryable<Actor> Cast { get; set; }
    }
}

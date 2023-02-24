namespace FilmsWebApp.Models.ViewModel
{
    public class FilmsUpdateCastViewModel
    {
        public Film film { get; set; }

        public IEnumerable<Actor> cast { get; set; }

        public int ActorId { get; set; }

        public virtual Director Director { get; set; }
    }
}

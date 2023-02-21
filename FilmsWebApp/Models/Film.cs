namespace FilmsWebApp.Models;

public partial class Film
{
    public string Title { get; set; } = null!;

    public string? Genre { get; set; }

    public int DirectorId { get; set; }

    public int FilmId { get; set; }

    public int? ReleaseYear { get; set; }

    public virtual Director Director { get; set; } = null!;

    public virtual ICollection<FilmsAndActor> FilmsAndActors { get; } = new List<FilmsAndActor>();
}

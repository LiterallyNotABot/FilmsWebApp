namespace FilmsWebApp.Models;

public partial class FilmsAndActor
{
    public int TableId { get; set; }

    public int FilmIdFk { get; set; }

    public int ActorIdFk { get; set; }

    public virtual Actor ActorIdFkNavigation { get; set; } = null!;

    public virtual Film FilmIdFkNavigation { get; set; } = null!;
}

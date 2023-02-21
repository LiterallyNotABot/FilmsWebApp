using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FilmsWebApp.Models;

public partial class Actor
{
    [DisplayName("Actor name")]
    [Required]
    public string ActorName { get; set; } = null!;

    public int ActorId { get; set; }

    public virtual ICollection<FilmsAndActor> FilmsAndActors { get; } = new List<FilmsAndActor>();
}

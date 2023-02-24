using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FilmsWebApp.Models;

public partial class Director
{
    [DisplayName("Director")]
    [Required]
    public string DirectorName { get; set; } = null!;

    public int DirectorTableId { get; set; }
    
    [DisplayName("Filmography")]
    public virtual ICollection<Film> Films { get; } = new List<Film>();
}

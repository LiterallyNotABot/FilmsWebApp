using Microsoft.Build.Framework;
using System.ComponentModel;

namespace FilmsWebApp.Models.ViewModel
{
    public class FilmsCreateViewModel
    {
        [Required]
        public string Title { get; set; } = null!;

        public string? Genre { get; set; }

        public int? ReleaseYear { get; set; }
        public int DirectorTableId { get; set; }
    }
}

using System.ComponentModel;

namespace FilmsWebApp.Models.ViewModel
{
    public class FilmsEditViewModel
    {
        public string Title { get; set; } = null!;

        public string? Genre { get; set; }

        public int directorTableId { get; set; }
        public int FilmId { get; set; }

        public int? ReleaseYear { get; set; }
    }
}

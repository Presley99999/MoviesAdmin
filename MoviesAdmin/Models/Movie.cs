using System.ComponentModel.DataAnnotations;

namespace MoviesAdmin.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Synopsis { get; set; } = string.Empty;

        [Required]
        public string Genre {  get; set; } = string.Empty;

        [Required]
        public string Rating {  get; set; } = string.Empty; //Age rating

        [Display(Name = "Runtime in minutes")]
        [Required]
        public int Runtime { get; set; } //Probably in minutes 

        [Display(Name = "Release Date")]
        [Required]
        public int ReleaseDate {  get; set; }
        
    }
}

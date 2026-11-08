using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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

        [Display(Name = "Genre")]
        public int GenreId { get; set; }

        
        [ValidateNever]
        [ScaffoldColumn(false)]
        public Genre Genre { get; set; }

        [Display(Name = "Audience Rating")]
        public int AudienceRatingId { get; set; }

        
        [ValidateNever]
        [ScaffoldColumn(false)]
        public AudienceRating AudienceRating { get; set; }


        [Display(Name = "Runtime in minutes")]
        [Required]
        public int Runtime { get; set; } //Probably in minutes 

      
        public string ImageUrl { get; set; } = string.Empty;

        [DisplayFormat(DataFormatString = "{0:MMMM dd, yyyy}")]
        [Display(Name = "Release Date")]
        [Required]
        public DateTime ReleaseDate {  get; set; }

        public List<CriticReview> CriticReviews { get; set; } = new();


    }
}

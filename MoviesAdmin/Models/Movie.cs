namespace MoviesAdmin.Models
{
    public class Movie
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Synopsis { get; set; } = string.Empty;

        public string Genre {  get; set; } = string.Empty;

        public string Rating {  get; set; } = string.Empty; //Age rating

        public int Runtime { get; set; } //Probably in minutes 

        public int ReleaseDate {  get; set; } 
    }
}

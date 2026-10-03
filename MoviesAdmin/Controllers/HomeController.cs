using Microsoft.AspNetCore.Mvc;
using MoviesAdmin.Models;
using System.Diagnostics;

namespace MoviesAdmin.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Movies()
        {
            //sample movie
            Movie movie = new Movie();
            movie.Id = 1;
            movie.Title = "Wings";
            movie.Synopsis = "Two young men, one rich, one middle class, both in love with the same woman, become US Air Corps fighter pilots and, eventually, heroic flying aces during World War I.";
            movie.Genre = "War";
            movie.Rating = "PG-13";
            movie.Runtime = 144;
            movie.ReleaseDate = 1927;
            return View(movie);
        }

        public IActionResult AllMovies(List<Movie> movies1)
        {
            List<Movie> movies = new List<Movie>();
            Movie movie = new Movie();
            movie.Id = 1;
            movie.Title = "Wings";
            movie.Synopsis = "Two young men, one rich, one middle class, both in love with the same woman, become US Air Corps fighter pilots and, eventually, heroic flying aces during World War I.";
            movie.Genre = "War";
            movie.Rating = "PG-13";
            movie.Runtime = 144;
            movie.ReleaseDate = 1927;

            Movie movie2 = new Movie();
            movie2.Id = 2;
            movie2.Title = "Sanjuro";
            movie2.Synopsis = "Jaded samurai Sanjuro helps an idealistic group of young warriors weed out their clan’s evil influences.";
            movie2.Genre = "Action";
            movie2.Rating = "R";
            movie2.Runtime = 96;
            movie2.ReleaseDate = 1962;
            

            Movie movie3 = new Movie();
            movie3.Id = 1;
            movie3.Title = "The Great Muppet Caper";
            movie3.Synopsis = "Kermit and Fozzie are newspaper reporters sent to London to interview Lady Holiday, a wealthy fashion designer whose priceless diamond necklace is stolen.";
            movie3.Genre = "Mystery";
            movie3.Rating = "G";
            movie3.Runtime = 97;
            movie3.ReleaseDate = 1981;
            
            movies.Add(movie);
            movies.Add(movie2);
            movies.Add(movie3);

            return View(movies);

        }

            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoviesAdmin.Models;
using MoviesAdmin.Services;

public class MoviesController : Controller
{
    private readonly MoviesAdminContext _context;
    private readonly IAzureStorageService _storage;

    public MoviesController(
        MoviesAdminContext context,
        IAzureStorageService storage)
    {
        _context = context;
        _storage = storage;
    }

    // GET: Movies
    public async Task<IActionResult> Index()
    {
        var movies = await _context.Movie
            .Include(m => m.Genre)
            .Include(m => m.AudienceRating)
            .OrderBy(m => m.ReleaseDate)
            .ToListAsync();

        return View(movies);
    }

    // GET: Movies/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
            return NotFound();

        var movie = await _context.Movie
            .Include(m => m.Genre)
            .Include(m => m.AudienceRating)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (movie == null)
            return NotFound();

        return View(movie);
    }

    // GET: Movies/Create
    public IActionResult Create()
    {
        var genres = _context.Genre.ToList();
        ViewBag.GenreId = new SelectList(genres, "Id", "Title");

        var audienceRatings = _context.AudienceRating.ToList();
        ViewBag.AudienceRatingId = new SelectList(audienceRatings, "Id", "Title");

        return View();
    }

    // POST: Movies/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("Id,Title,Synopsis,GenreId,AudienceRatingId,Runtime,ReleaseDate")]
    Movie movie,
        IFormFile? imageFile)
    {
        if (imageFile == null)
        {
            ModelState.AddModelError(
                "ImageFile",
                "The image file upload is required.");
        }

        if (ModelState.IsValid)
        {
            var extension = Path.GetExtension(imageFile!.FileName);
            var uniqueFileName = $"{Guid.NewGuid()}{extension}";

            movie.ImageUrl = await _storage.UploadFileAsync(imageFile,uniqueFileName);

            _context.Add(movie);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        var genres = _context.Genre.ToList();
        ViewBag.GenreId = new SelectList(genres, "Id", "Title");

        var audienceRatings = _context.AudienceRating.ToList();
        ViewBag.AudienceRatingId = new SelectList(
            audienceRatings,
            "Id",
            "Title");

        return View(movie);
    }

    // GET: Movies/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
            return NotFound();

        var movie = await _context.Movie.FindAsync(id);

        if (movie == null)
            return NotFound();

        ViewBag.GenreId = new SelectList(
            _context.Genre.ToList(),
            "Id",
            "Title",
            movie.GenreId);

        ViewBag.AudienceRatingId = new SelectList(
            _context.AudienceRating.ToList(),
            "Id",
            "Title",
            movie.AudienceRatingId);

        return View(movie);
    }

    // POST: Movies/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        Movie movie,
        IFormFile ImageFile)
    {
        if (id != movie.Id)
            return NotFound();

        if (ModelState.IsValid)
        {
            if (ImageFile != null && ImageFile.Length > 0)
            {
                var safeName =
                    Guid.NewGuid().ToString() +
                    Path.GetExtension(ImageFile.FileName);

                movie.ImageUrl =
                    await _storage.UploadFileAsync(
                        ImageFile,
                        safeName);
            }

            _context.Update(movie);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        ViewBag.GenreId = new SelectList(
            _context.Genre.ToList(),
            "Id",
            "Title",
            movie.GenreId);

        ViewBag.AudienceRatingId = new SelectList(
            _context.AudienceRating.ToList(),
            "Id",
            "Title",
            movie.AudienceRatingId);

        return View(movie);
    }

    // GET: Movies/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
            return NotFound();

        var movie = await _context.Movie
            .Include(m => m.Genre)
            .Include(m => m.AudienceRating)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (movie == null)
            return NotFound();

        return View(movie);
    }

    // POST: Movies/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var movie = await _context.Movie.FindAsync(id);

        if (movie != null)
        {
            _context.Movie.Remove(movie);
        }

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    private bool MovieExists(int? id)
    {
        return _context.Movie.Any(e => e.Id == id);
    }
}
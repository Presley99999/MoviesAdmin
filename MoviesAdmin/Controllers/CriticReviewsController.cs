
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoviesAdmin.Models;

public class CriticReviewsController : Controller
{
    private readonly MoviesAdminContext _context;

    public CriticReviewsController(MoviesAdminContext context)
    {
        _context = context;
    }

    // GET: CRITICREVIEWS
    public async Task<IActionResult> Index()    
    {
        var reviews = await _context.CriticReview
        .Include(r => r.Movie)
        .ToListAsync();

        return View(reviews);
    }

    // GET: CRITICREVIEWS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var criticreview = await _context.CriticReview
            .FirstOrDefaultAsync(m => m.Id == id);
        if (criticreview == null)
        {
            return NotFound();
        }

        return View(criticreview);
    }

    // GET: CRITICREVIEWS/Create
    public IActionResult Create()
    {
        ViewBag.MovieId = new SelectList(_context.Movie.ToList(),"Id","Title");

        return View();
    }

    // POST: CRITICREVIEWS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Description,Rating,IsPublished,CreatedBy,CreatedDate,MovieId")] CriticReview criticreview)
    {
        if (ModelState.IsValid)
        {
            criticreview.CreatedBy = User.Identity?.Name ?? "Unknown";
            criticreview.CreatedDate = DateTime.Now;
            _context.Add(criticreview);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        ViewBag.MovieId = new SelectList(_context.Movie.ToList(), "Id", "Title");
        return View(criticreview);
    }

    // GET: CRITICREVIEWS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var criticreview = await _context.CriticReview.FindAsync(id);
        if (criticreview == null)
        {
            return NotFound();
        }
        ViewBag.MovieId = new SelectList(_context.Movie.ToList(), "Id", "Title");
        return View(criticreview);
    }

    // POST: CRITICREVIEWS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Description,Rating,IsPublished,CreatedBy,CreatedDate,MovieId")] CriticReview criticreview)
    {
        if (id != criticreview.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(criticreview);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CriticReviewExists(criticreview.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        ViewBag.MovieId = new SelectList(_context.Movie.ToList(), "Id", "Title");
        return View(criticreview);
    }

    // GET: CRITICREVIEWS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var criticreview = await _context.CriticReview
            .FirstOrDefaultAsync(m => m.Id == id);
        if (criticreview == null)
        {
            return NotFound();
        }

        return View(criticreview);
    }

    // POST: CRITICREVIEWS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var criticreview = await _context.CriticReview.FindAsync(id);
        if (criticreview != null)
        {
            _context.CriticReview.Remove(criticreview);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool CriticReviewExists(int? id)
    {
        return _context.CriticReview.Any(e => e.Id == id);
    }
}

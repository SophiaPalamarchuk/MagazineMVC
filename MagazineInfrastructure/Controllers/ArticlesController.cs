using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MagazineDomain.Model;

namespace MagazineInfrastructure.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly IstpContext _context;

        public ArticlesController(IstpContext context)
        {
            _context = context;
        }

        // GET: Articles

        public async Task<IActionResult> Index(int? id, string? name)
        {
            if (id == null)
            {
                // Redirect to the Authors Index if id is null
                return RedirectToAction("Authors", "Index");
            }

            ViewBag.AuthorId = id;
            ViewBag.AuthorName = name; // Assign "Unknown" if name is null

            // Query to fetch articles by author asynchronously
            var articlesByAuthor = await _context.Article
               .Where(b => b.AuthorId == id)
                .Include(b => b.Author)
                .ToListAsync();

            return View(articlesByAuthor);
        }



        // GET: Articles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Article
                .Include(a => a.Author)
                .Include(a => a.Editor)
                
                .Include(a => a.Magazine)
                .FirstOrDefaultAsync(
             m => m.ArticleId == id
                )
                ;
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // GET: Articles/Create
        public IActionResult Create()
        {
         ViewData["AuthorId"] = new SelectList(_context.Author, "AuthorId", "AuthorName");
           ViewData["EditorId"] = new SelectList(_context.Editor, "EditorId", "EditorName");
            ViewData["MagazineId"] = new SelectList(_context.Magazine, "MagazineId", "MagazineName");
            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,TextContent,AuthorId,EditorId,MagazineId")] Article article)
        {
            if (ModelState.IsValid)
            {
                _context.Add(article);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Authors");
            } return View(article);
            // _context.Add(article);
            //await _context.SaveChangesAsync();
            // return RedirectToAction(nameof(Index));
            //  }
            //  ViewData["AuthorId"] = new SelectList(_context.Author, "AuthorId", "AuthorName", article.AuthorId);
             ViewData["EditorId"] = new SelectList(_context.Editor, "EditorId", "EditorName", article.EditorId);
            // ViewData["MagazineId"] = new SelectList(_context.Magazines, "MagazineId", "MagazineName", article.MagazineId);
           
        }

        // GET: Articles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Article.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }
          ViewData["AuthorId"] = new SelectList(_context.Author, "AuthorId", "AuthorName", article.AuthorId);
           ViewData["EditorId"] = new SelectList(_context.Editor, "EditorId", "EditorName", article.EditorId);
         ViewData["MagazineId"] = new SelectList(_context.Magazine, "MagazineId", "MagazineName", article.MagazineId);
            return View(article);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ArticleId,Title,TextContent,AuthorId,EditorId,MagazineId")] Article article)
        {
            if (id != article.ArticleId)
            {
                return NotFound();
            }
           
          
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(article);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticleExists(article.ArticleId))
                    {
                        return RedirectToAction("Index", "Authors");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Authors");
            }
                
               
        ViewData["AuthorId"] = new SelectList(_context.Author, "AuthorId", "AuthorName", article.AuthorId);
           ViewData["EditorId"] = new SelectList(_context.Editor, "EditorId", "EditorName", article.EditorId);
          return View(article);
        }

        // GET: Articles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Article
                .Include(a => a.Author)
                .Include(a => a.Editor)
                .Include(a => a.Magazine)
                .FirstOrDefaultAsync(m => m.ArticleId == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var article = await _context.Article.FindAsync(id);
            if (article != null)
            {
                _context.Article.Remove(article);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Authors");
       
        }

        private bool ArticleExists(int id)
        {
            return _context.Article.Any(e => e.ArticleId == id);
        }
    }
}

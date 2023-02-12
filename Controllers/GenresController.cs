using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhotoStore.Data;
using PhotoStore.Data.Models;
using System;
using System.Linq;

namespace PhotoStore.Controllers
{
    public class GenresController : Controller
    {
        private readonly ApplicationDbContext db;

        public GenresController(ApplicationDbContext context)
        {
            this.db = context;
        }
        // GET: Genres
        public IActionResult Index()
        {
            return View(db.Genres.ToList());
        }

        // GET: Genres/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genre = db.Genres
                .Include(g => g.Photo)
                .FirstOrDefault(m => m.Id == id);
            if (genre == null)
            {
                return NotFound();
            }

            return View(genre);
        }
        // GET: Genres/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Genres/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind("Title,Id")] Genre genre)
        {
            if (ModelState.IsValid)
            {
                genre.Id = Guid.NewGuid();
                db.Add(genre);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(genre);
        }
        // GET: Genres/Edit/5
        [Authorize]
        public IActionResult Edit(Guid? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var genre = db.Genres.Find(id);
            if (genre == null)
            {
                return NotFound();
            }
            return View(genre);
        }

        // POST: Genres/Edit/5
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Title,Id")] Genre genre)
        {

            if (id != genre.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(genre);
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GenreExists(genre.Id))
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
            return View(genre);
        }

        // GET: Genres/Delete/5
        [Authorize]
        public IActionResult Delete(Guid? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var genre = db.Genres
                .FirstOrDefault(m => m.Id == id);
            if (genre == null)
            {
                return NotFound();
            }

            return View(genre);
        }

        // POST: Genres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var genre = db.Genres.Find(id);
            db.Genres.Remove(genre);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool GenreExists(Guid id)
        {
            return db.Genres.Any(e => e.Id == id);
        }
    }
}

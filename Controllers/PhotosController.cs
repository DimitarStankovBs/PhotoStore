using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PhotoStore.Data;
using PhotoStore.Data.Models;
using System;
using System.Linq;

namespace PhotoStore.Controllers
{
    public class PhotosController : Controller
    {
        private readonly ApplicationDbContext db;

        public PhotosController(ApplicationDbContext context)
        {
            this.db = context;
        }
        // GET: Photos not sold
        public IActionResult Index()
        {
            var applicationDbContext = db.Photos
                .Include(b => b.Genre)
                .Where(b => b.BuyerName == null);
            return View(applicationDbContext.ToList());

        }
        // GET: Photos/Create - when send data to view
        [Authorize]

        public IActionResult Create()
        {
            ViewData["GenreId"] = new SelectList(db.Genres, "Id", "Title");
            return View();
        }

        // POST: Photos/Create - when receive data fron view
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Create([Bind("Title,ImageUrl,GenreId,Id,Price,Description")] Photo Photo)
        {
            if (ModelState.IsValid)
            {
                Photo.Id = Guid.NewGuid();
                db.Add(Photo);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenreId"] = new SelectList(db.Genres, "Id", "Title", Photo.GenreId);
            return View(Photo);
        }
        // GET: Photos/Edit/5
        [Authorize]
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Photo = db.Photos.Find(id);
            if (Photo == null)
            {
                return NotFound();
            }

            ViewData["GenreId"] = new SelectList(db.Genres, "Id", "Title", Photo.GenreId);
            return View(Photo);
        }

        // POST: Photos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Title,ImageUrl,GenreId,Id, Price, Description")] Photo Photo)
        {
            if (id != Photo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(Photo);
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhotoExists(Photo.Id))
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
            ViewData["GenreId"] = new SelectList(db.Genres, "Id", "Title", Photo.GenreId);
            return View(Photo);
        }
        // GET: Photos/Delete/5
        [Authorize]
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Photo = db.Photos
                .Include(b => b.Genre)
                .FirstOrDefault(m => m.Id == id);
            if (Photo == null)
            {
                return NotFound();
            }

            return View(Photo);
        }

        // POST: Photos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var Photo = db.Photos.Find(id);
            db.Photos.Remove(Photo);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool PhotoExists(Guid id)
        {
            return db.Photos.Any(e => e.Id == id);
        }
        // GET: Photos/Buy
        [Authorize]
        public IActionResult Buy(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Photo = db.Photos.Find(id);
            if (Photo == null)
            {
                return NotFound();
            }
            ViewData["GenreId"] = new SelectList(db.Genres, "Id", "Title", Photo.GenreId);
            return View(Photo);
        }

        // POST: Photos/Buy
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Buy(Guid id, [Bind("Title,ImageUrl,GenreId,Id, Price, Description, BuyerName, BuyerAddress, BuyerPhone, PurchseDate")] Photo Photo)
        {
            if (id != Photo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(Photo);
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhotoExists(Photo.Id))
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
            ViewData["GenreId"] = new SelectList(db.Genres, "Id", "Title", Photo.GenreId);
            return View(Photo);
        }
        public IActionResult Sold()
        {
            var applicationDbContext = db.Photos
                .Include(b => b.Genre)
                .Where(b => b.BuyerName != null);
            return View(applicationDbContext.ToList());

        }

    }
}

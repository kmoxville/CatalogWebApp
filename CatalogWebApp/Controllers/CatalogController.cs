using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CatalogWebApp.DAL;
using CatalogWebApp.DAL.Models;

namespace CatalogWebApp.Controllers
{
    public class CatalogController : Controller
    {
        private readonly CatalogContext _context;

        public CatalogController(CatalogContext context)
        {
            _context = context;
        }

        // GET: Catalog
        [Route("Products")]
        public async Task<IActionResult> Index()
        {
              return _context.Catalogs != null ? 
                          View(await _context.Catalogs.ToListAsync()) :
                          Problem("Entity set 'CatalogContext.Catalogs'  is null.");
        }

        public IActionResult GetImage(int id)
        {
            var image = _context.CatalogImages.FirstOrDefault(x => x.Id == id);
            if (image == null)
                return NotFound();

            return File(image.Image, "image/jpeg");
        }

        // GET: Catalog/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Catalogs == null)
            {
                return NotFound();
            }

            var catalogModel = await _context.Catalogs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (catalogModel == null)
            {
                return NotFound();
            }

            return View(catalogModel);
        }

        // GET: Catalog/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Catalog/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CatalogModel catalogModel)
        {
            using (var memoryStream = new MemoryStream())
            {
                catalogModel.ImageFile.CopyTo(memoryStream);
                catalogModel.Image = new CatalogImageModel()
                {
                    Image = memoryStream.ToArray()
                };
            }

            _context.Add(catalogModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Catalog/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Catalogs == null)
            {
                return NotFound();
            }

            var catalogModel = await _context.Catalogs.FindAsync(id);
            if (catalogModel == null)
            {
                return NotFound();
            }

            return View(catalogModel);
        }

        // POST: Catalog/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CatalogModel catalogModel)
        {
            if (id != catalogModel.Id)
            {
                return NotFound();
            }

            using (var memoryStream = new MemoryStream())
            {
                catalogModel.ImageFile.CopyTo(memoryStream);
                catalogModel.Image = new CatalogImageModel()
                {
                    Image = memoryStream.ToArray()
                };
            }

            try
            {
                _context.Update(catalogModel);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CatalogModelExists(catalogModel.Id))
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

        // GET: Catalog/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Catalogs == null)
            {
                return NotFound();
            }

            var catalogModel = await _context.Catalogs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (catalogModel == null)
            {
                return NotFound();
            }

            return View(catalogModel);
        }

        // POST: Catalog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Catalogs == null)
            {
                return Problem("Entity set 'CatalogContext.Catalogs'  is null.");
            }
            var catalogModel = await _context.Catalogs.FindAsync(id);
            if (catalogModel != null)
            {
                _context.Catalogs.Remove(catalogModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CatalogModelExists(int id)
        {
          return (_context.Catalogs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

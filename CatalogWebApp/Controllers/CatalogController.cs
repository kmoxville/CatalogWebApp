using CatalogWebApp.Services.CatalogService;
using CatalogWebApp.ViewModels.Catalog;
using Microsoft.AspNetCore.Mvc;

namespace CatalogWebApp.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ICatalogService _catalogService;

        public CatalogController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        // GET: Catalog
        [Route("Products")]
        public async Task<IActionResult> Index()
        {
            return View(await _catalogService.IndexAsync());
        }

        public async Task<IActionResult> GetImage(int id)
        {
            var image = await _catalogService.GetImageAsync(id);
            if (image == null)
                return NotFound();

            return File(image, "image/jpeg");
        }

        // GET: Catalog/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var details = await _catalogService.Details(id);

            if (details == null)
            {
                return RedirectToPage(nameof(Index));
            }

            return View(details);
        }

        // GET: Catalog/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Catalog/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CatalogViewModel catalogModel)
        {
            await _catalogService.Create(catalogModel);
            return RedirectToAction(nameof(Index));
        }

        // GET: Catalog/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var details = await _catalogService.Details(id);

            if (details == null)
            {
                return Redirect("/Home/Error");
            }

            return View(details);
        }

        // POST: Catalog/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CatalogViewModel catalogModel)
        {
            if (id != catalogModel.Id)
            {
                return Redirect("/Home/Error");
            }

            if (await _catalogService.Save(catalogModel))
            {
                return RedirectToAction(nameof(Index));
            }

            return Redirect("/Home/Error");
        }

        // GET: Catalog/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var catalogModel = await _catalogService.Details(id);
            return View(catalogModel);
        }

        // POST: Catalog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _catalogService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

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
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            return View(await _catalogService.IndexAsync(cancellationToken));
        }

        public async Task<IActionResult> GetImage(int id, CancellationToken cancellationToken)
        {
            var image = await _catalogService.GetImageAsync(id, cancellationToken);
            if (image == null)
                return NotFound();

            return File(image, "image/jpeg");
        }

        // GET: Catalog/Details/5
        public async Task<IActionResult> Details(int? id, CancellationToken cancellationToken)
        {
            var details = await _catalogService.Details(id, cancellationToken);

            if (details == null)
            {
                return RedirectToPage(nameof(Index));
            }

            return View(details);
        }

        // GET: Catalog/Create
        public IActionResult Create(CancellationToken cancellationToken)
        {
            return View();
        }

        // POST: Catalog/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CatalogViewModel catalogModel, CancellationToken cancellationToken)
        {
            await _catalogService.Create(catalogModel, cancellationToken);
            return RedirectToAction(nameof(Index));
        }

        // GET: Catalog/Edit/5
        public async Task<IActionResult> Edit(int? id, CancellationToken cancellationToken)
        {
            var details = await _catalogService.Details(id, cancellationToken);

            if (details == null)
            {
                return Redirect("/Home/Error");
            }

            return View(details);
        }

        // POST: Catalog/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CatalogViewModel catalogModel, CancellationToken cancellationToken)
        {
            if (id != catalogModel.Id)
            {
                return Redirect("/Home/Error");
            }

            if (await _catalogService.Save(catalogModel, cancellationToken))
            {
                return RedirectToAction(nameof(Index));
            }

            return Redirect("/Home/Error");
        }

        // GET: Catalog/Delete/5
        public async Task<IActionResult> Delete(int? id, CancellationToken cancellationToken)
        {
            var catalogModel = await _catalogService.Details(id, cancellationToken);
            return View(catalogModel);
        }

        // POST: Catalog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken cancellationToken)
        {
            await _catalogService.Delete(id, cancellationToken);
            return RedirectToAction(nameof(Index));
        }
    }
}

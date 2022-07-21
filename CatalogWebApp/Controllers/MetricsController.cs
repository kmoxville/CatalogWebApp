using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CatalogWebApp.DAL;
using CatalogWebApp.DAL.Models;
using CatalogWebApp.Services.MetricsService;

namespace CatalogWebApp.Controllers
{
    public class MetricsController : Controller
    {
        private readonly IMetricsService _metricsService;

        public MetricsController(IMetricsService metricsService)
        {
            _metricsService = metricsService;
        }

        // GET: Metrics
        public async Task<IActionResult> Index()
        {
              return View(await _metricsService.GetAll());
        }
    }
}

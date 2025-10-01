using System.Diagnostics;
using EComApp.Models;
using EComApp.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace EComApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeRepository _homeRepository;

        public HomeController(ILogger<HomeController> logger, IHomeRepository homeRepository)
        {
            _homeRepository = homeRepository;
            _logger = logger;
        }

        public async Task<IActionResult> Index(string sterm="", int categoryId= 0)
        {
            var ArcticleModel = new ArticleDisplayModel()
            {
                Articles = await _homeRepository.GetArticles(sterm, categoryId),
                Categories = await _homeRepository.Category(),
                STerm = sterm,
                CategoryId = categoryId
            };
            return View(ArcticleModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

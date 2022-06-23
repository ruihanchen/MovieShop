using Microsoft.AspNetCore.Mvc;
using MovieShopMVC.Models;
using System.Diagnostics;
using ApplicationCore.Contract.Service;
using Infrastructure.Service;

namespace MovieShopMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMovieService _movieService;
        private readonly ICastService _castService;

        public HomeController(ILogger<HomeController> logger, IMovieService movieService, ICastService castService)
        {
            _logger = logger;
            _movieService = movieService;
            _castService = castService;
        }

        public async Task<IActionResult> Index()
        {
            var movies = await _movieService.GetTopGrossingMovies();
            return View(movies);
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
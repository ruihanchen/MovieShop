using ApplicationCore.Contract.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MovieShopMVC.Views.Shared.Components.Genres
{
    public class GenresViewComponent : ViewComponent
    {
        private readonly IGenreService _genreService;

        public GenresViewComponent(IGenreService genreService)
        {
            _genreService = genreService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var genres = await _genreService.GetAllGenres();

            return View(genres);
        }
    }
}

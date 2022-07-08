using ApplicationCore.Contract.Repository;
using ApplicationCore.Contract.Service;
using ApplicationCore.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly IMovieRepository _movieRepository;

        public MovieController(IMovieService movieService, IMovieRepository movieRepository)
        {
            _movieService = movieService;
            _movieRepository = movieRepository;
        }

        [HttpGet]
        public async Task<IActionResult> MovieByPage(int Id)
        {
            var pagedMovies = await _movieService.GetMovies(Id);

            return Ok(pagedMovies);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetMovie(int id)
        {
            var movie = await _movieService.GetMovieDetails(id);
            if (movie == null)
            {
                return NotFound(new { errorMessage = $"No Movie Found for id: {id}" });
            }

            return Ok(movie);
        }

        [HttpGet]
        [Route("top-rated")]
        public async Task<IActionResult> TopRatedMovie()
        {
            var topRating = await _movieRepository.Get30HighestRatedMovies();

            if (topRating == null || !topRating.Any())
            {
                // 404
                return NotFound(new { errorMessage = "No Movies Found" });
            }
            return Ok(topRating);
        }

        [HttpGet]
        [Route("top-grossing")]
        public async Task<IActionResult> GetTopGrossingMovies()
        {
            var movies = await _movieService.GetTopGrossingMovies();

            if (movies == null || !movies.Any())
            {
                // 404
                return NotFound(new { errorMessage = "No Movies Found" });
            }
            return Ok(movies);
        }

        [HttpGet]
        [Route("genre/{genreId:int}")]
        public async Task<IActionResult> Genre(int Id)
        {
            var pagedMovies = await _movieService.GetMoviesByGenre(Id);

            return Ok(pagedMovies);
        }

        [HttpGet]
        [Route("{id}/reviews")]
        public async Task<IActionResult> MovieReviews(int id)
        {
            var reviewMovies = await _movieRepository.GetReviewByMovie(id);

            return Ok(reviewMovies);
        }

    }
}

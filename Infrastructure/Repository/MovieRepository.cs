using ApplicationCore.Contract.Repository;
using ApplicationCore.Entity;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Model;

namespace Infrastructure.Repository
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        public MovieRepository(MovieShopDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<IEnumerable<Movie>> Get30HighestGrossingMovies()
        {
            // linQ code to get to 30
            var movies = await _dbContext.Movies.OrderByDescending(m => m.Revenue).Take(30).ToListAsync();
            return movies;
        }

        public async Task<IEnumerable<Review>> Get30HighestRatedMovies()
        {
            var movies = await _dbContext.Reviews
                .OrderByDescending(m => m.Rating)
                .Take(30)
                .ToListAsync();

            return movies;
        }

            public async Task<decimal> GetAverageRatingForMovie(int movieId)
        {
            var rating = await _dbContext.Reviews
                .Where(r => r.MovieId == movieId)
                .AverageAsync(x => x.Rating);

            return rating;
        }

        public async override Task<Movie> GetById(int id)
        {
            var movieDetails = await _dbContext.Movies
                .Include(x => x.GenreMovie).ThenInclude(x => x.Genre)
                .Include(x => x.Trailers)
                .Include(x => x.movieOfCasts).ThenInclude(x => x.Cast)
                .Include(x => x.Purchase)
                .Include(x => x.Reviews)
                .FirstOrDefaultAsync(x => x.Id == id);
            return movieDetails;
        }

        public async Task<PagedResultSetModel<Movie>> GetMoviesByGenre(int genreId, int pageSize = 30, int pageNumber = 1)
        {
            // get total count movies for the genre
            var totalMoviesForGenre = await _dbContext.MoviesGenres.Where(g => g.GenreId == genreId).CountAsync();

            var movies = await _dbContext.MoviesGenres
                .Where(g => g.GenreId == genreId)
                .Include(g => g.Movie)
                .OrderByDescending(m => m.Movie.Revenue)
                .Select(m => new Movie { Id = m.MovieId, PosterUrl = m.Movie.PosterUrl, Title = m.Movie.Title })
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToListAsync();

            var pagedMovies = new PagedResultSetModel<Movie>(pageNumber, totalMoviesForGenre, pageSize, movies);
            return pagedMovies;
        }
        public async Task<PagedResultSetModel<Review>> GetReviewByMovie(int movieId, int pageSize = 30, int pageNumber = 1)
        {
            // get total count movies for the genre
            var ReviewsForMovie = await _dbContext.Reviews.Where(g => g.MovieId == movieId).CountAsync();

            var reviews = await _dbContext.Reviews
                .Where(g => g.MovieId == movieId)
                .Include(g => g.movie)
                .Select(m => new Review { MovieId = m.MovieId, Rating = m.Rating, ReviewText = m.ReviewText, UserId = m.UserId })
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToListAsync();

            var pagedMovies = new PagedResultSetModel<Review>(pageNumber, ReviewsForMovie, pageSize, reviews);
            return pagedMovies;
        }

        public async Task<PagedResultSetModel<Movie>> GetMovies(int movieId, int pageSize = 30, int pageNumber = 1)
        {
            
            var totalMovies = await _dbContext.Movies.Where(g => g.Id == movieId).CountAsync();

            var movies = await _dbContext.Movies
                .Select(m => new Movie { Id = m.Id, PosterUrl = m.PosterUrl, Title = m.Title })
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToListAsync();


            var pagedMovies = new PagedResultSetModel<Movie>(pageNumber, totalMovies, pageSize, movies);
            return pagedMovies;
        }
        


    }
}

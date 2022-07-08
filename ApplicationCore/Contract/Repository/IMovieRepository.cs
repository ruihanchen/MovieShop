using ApplicationCore.Entity;
using ApplicationCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contract.Repository
{
    public interface IMovieRepository: IRepository<Movie>
    {
        Task<IEnumerable<Movie>> Get30HighestGrossingMovies();
        Task<IEnumerable<Review>> Get30HighestRatedMovies();
        Task<decimal> GetAverageRatingForMovie(int movieId);
        Task<PagedResultSetModel<Movie>> GetMoviesByGenre(int genreId, int pageSize = 30, int pageNumber = 1);
        Task<PagedResultSetModel<Movie>> GetMovies(int movieId, int pageSize = 30, int pageNumber = 1);
        Task<PagedResultSetModel<Review>> GetReviewByMovie(int movieId, int pageSize = 30, int pageNumber = 1);
    }
}

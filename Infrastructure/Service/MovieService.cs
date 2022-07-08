using ApplicationCore.Contract.Repository;
using ApplicationCore.Contract.Service;
using ApplicationCore.Model;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Service
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<MovieDetailsModel> GetMovieDetails(int id) {

            var movieDetails = await _movieRepository.GetById(id);

            var movie = new MovieDetailsModel
            {
                Id = movieDetails.Id,
                Tagline = movieDetails.Tagline,
                Title = movieDetails.Title,
                Overview = movieDetails.Overview,
                PosterUrl = movieDetails.PosterUrl,
                BackdropUrl = movieDetails.BackdropUrl,
                ImdbUrl = movieDetails.ImdbUrl,
                RunTime = movieDetails.RunTime,
                TmdbUrl = movieDetails.TmdbUrl,
                Revenue = movieDetails.Revenue,
                Budget = movieDetails.Budget,
                ReleaseDate = movieDetails.ReleaseDate,
                Price = movieDetails.Price
            };

            foreach (var genre in movieDetails.GenreMovie)
            {
                movie.Genres.Add(new GenreModel { Id = genre.GenreId, Name = genre.Genre.Name });
            }

            foreach (var trailer in movieDetails.Trailers)
            {
                movie.Trailers.Add(new TrailerModel { Id = trailer.Id, Name = trailer.Name, TrailerUrl = trailer.TrailerUrl });
            }
            foreach (var cast in movieDetails.movieOfCasts)
            {
                movie.Casts.Add(new CastModel { Id = cast.CastId, Name = cast.Cast.Name, ProfilePath = cast.Cast.Profilepath, Character = cast.Character });
            }
            foreach (var review in movieDetails.Reviews)
            {
                movie.Reviews.Add(new ReviewModel { MovieId = review.MovieId, Rating = review.Rating });
            }

            return movie;
        }

        public async Task<PagedResultSetModel<MovieCardModel>> GetMoviesByGenre(int genreId, int pageSize = 30, int pageNumber = 1)
        {
            var movies = await _movieRepository.GetMoviesByGenre(genreId, pageSize, pageNumber);

            var movieCard = new List<MovieCardModel>();

            foreach (var movie in movies.PagedData)
            {
                movieCard.Add(new MovieCardModel { Id = movie.Id, PosterUrl = movie.PosterUrl, Title = movie.Title });
            }
            return new PagedResultSetModel<MovieCardModel>(pageNumber, movies.TotalRecords, pageSize, movieCard);
        }
        public async Task<PagedResultSetModel<MovieCardModel>> GetMovies(int movieId, int pageSize = 30, int pageNumber = 1)
        {
            var movies = await _movieRepository.GetMovies(movieId, pageSize, pageNumber);

            var movieCard = new List<MovieCardModel>();

            foreach (var movie in movies.PagedData)
            {
                movieCard.Add(new MovieCardModel { Id = movie.Id, PosterUrl = movie.PosterUrl, Title = movie.Title });
            }
            return new PagedResultSetModel<MovieCardModel>(pageNumber, movies.TotalRecords, pageSize, movieCard);
        }

        public async Task<List<MovieCardModel>> GetTopGrossingMovies()
        {
            var movies = await _movieRepository.Get30HighestGrossingMovies();

            var movieCards = new List<MovieCardModel>();

            foreach (var movie in movies)
            {
                movieCards.Add( new MovieCardModel { Id = movie.Id,  PosterUrl = movie.PosterUrl, Title = movie.Title  });
            }
            return movieCards;
        }

    }
}

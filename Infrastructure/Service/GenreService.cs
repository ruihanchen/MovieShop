using ApplicationCore.Contract.Repository;
using ApplicationCore.Contract.Service;
using ApplicationCore.Entity;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Infrastructure.Service
{
    public class GenreService : IGenreService
    {
        private readonly IRepository<Genre> _genreRepository;

        public GenreService(IRepository<Genre> genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<IEnumerable<GenreModel>> GetAllGenres()
        {
            var genres = await _genreRepository.GetAll();

            var genresModel = genres.Select(g => new GenreModel { Id = g.Id, Name = g.Name, });
            return genresModel;
        }

    }
}

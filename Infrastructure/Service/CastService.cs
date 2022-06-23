using ApplicationCore.Contract.Respository;
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
    public class CastService : ICastService
    {
        private readonly ICastRepository _castRepository;

        public CastService(ICastRepository castRepository)
        {
            _castRepository = castRepository;
        }
        public async Task<CastDetailsModel> GetCastDetails(int id)
        {

            var castDetails = await _castRepository.GetById(id);

            var cast = new CastDetailsModel
            {
                Id = castDetails.Id,
                Name = castDetails.Name,
                ProfilePath = castDetails.Profilepath,
                Gender = castDetails.Gender

            };
            foreach (var moviecast in castDetails.MovieOfCast)
            {
                cast.movies.Add(new MovieCastModel{ Id = moviecast.MovieId, Character = moviecast.Character,});
            }
           

            return cast;
        }
    }
}

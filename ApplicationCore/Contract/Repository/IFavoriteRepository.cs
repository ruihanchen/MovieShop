using ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contract.Repository
{
    public interface IFavoriteRepository : IRepository<Favorite>
    {
        Task<Favorite> GetFavoriteById(int userId, int movieId);
        Task<IEnumerable<Favorite>> GetAllFavoriteMoviesByUserId(int userId);
    }
}

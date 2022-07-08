using ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contract.Repository
{
    public interface IReviewRepository : IRepository<Review>
    {
        Task<int> GetAverageMovieRating(int movieId);
        Task<Review> GetReview(int userId, int movieId);
    }
}

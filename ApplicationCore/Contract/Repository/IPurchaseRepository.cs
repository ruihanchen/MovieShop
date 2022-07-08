using ApplicationCore.Entity;
using ApplicationCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contract.Repository
{
    public interface IPurchaseRepository : IRepository<Purchase>
    {
        Task<IEnumerable<Purchase>> GetPurchasesByUserId(int id);
        Task<bool> CheckIfPurchaseExists(int userId, int movieId);
    }
}

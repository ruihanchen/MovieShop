using ApplicationCore.Entity;
using ApplicationCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contract.Repository
{
    public interface IReportRepository
    {
        Task<IEnumerable<MoviesReportModel>> GetPurchasedMovies (DateTime? fromDate = null, DateTime? toDate = null, int pageSize = 30, int pageIndex = 1);
    }
}

using ApplicationCore.Contract.Repository;
using ApplicationCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class ReportRepository : IReportRepository
    {
        public Task<IEnumerable<MoviesReportModel>> GetPurchasedMovies(DateTime? fromDate = null, DateTime? toDate = null, int pageSize = 30, int pageIndex = 1)
        {
            throw new NotImplementedException();
        }
    }
}

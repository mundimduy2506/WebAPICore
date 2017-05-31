using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPICore.Models
{
    public interface IPortfolioRepository
    {
        Task<PortfolioTransaction> GetTransactionDetailsAsync(string seiAccountId, string dateType, string reportBasis, string startDate, string endDate, int pageSize, int pageNumber);
        bool IsValidDateFormmat(string date);
    }
}

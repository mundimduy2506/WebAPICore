using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPICore.Models
{
    public class PortfolioRepository: IPortfolioRepository
    {
        private readonly WebAPIContext _context;

        public PortfolioRepository(WebAPIContext context)
        {
            _context = context;
        }
        public Task<PortfolioTransaction> GetTransactionDetailsAsync(string seiAccountId, string dateType, string reportBasis,string startDate, string endDate, int pageSize, int pageNumber)
        {
            PortfolioTransaction po = new PortfolioTransaction();
            var sql = string.Format("SELECT * FROM [api].[v_Portfolio_Transactions_Details] WHERE SEIAccountId = '{0}' ",seiAccountId);
                sql += string.Format("AND {0} BETWEEN '{1}' AND '{2}' ", dateType, startDate, endDate);
                sql += string.Format("AND ReportDataTypeName = '{0}'", reportBasis);

            var query = _context.Set<TransactionDetails>().FromSql(sql);
            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            if (query.Count() > 0)
            {
                var firstItem = query.FirstOrDefault();
                po.AccountName = firstItem.AccountName;
                po.CustodianAccountID = firstItem.CustodianAccountID;
                po.CustodianAccountDescription = firstItem.CustodianAccountDescription;
                po.CustodianName = firstItem.CustodianName;
                po.ExternalAccountID = firstItem.ExternalAccountID;
                po.TransactionsDetails = new List<TransactionDetailsVM>();
                var temp = query.Count();
                query.ToList().ForEach(t =>
                {
                    po.TransactionsDetails.Add(new MapperConfiguration(cfg => { cfg.CreateMap<TransactionDetails, TransactionDetailsVM>(); }).CreateMapper().Map<TransactionDetailsVM>(t));
                });
            }
            
            return Task.Run(() => po);
        }
        public bool IsValidDateFormmat(string date)
        {
            try
            {
                DateTime.Parse(date);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}

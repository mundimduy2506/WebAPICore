using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPICore.Models
{
    public class PortfolioTransaction
    {
        public string AccountID { get; set; }
        public string AccountName { get; set; } 
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int InquiryBasis { get; set; }
        public string DateType { get; set; }
        public string BookID { get; set; }
        public string OrganizationID { get; set; }
        public string CustodianAccountDescription { get; set; }
        public int? CustodianAccountID { get; set; }
        public string CustodianName { get; set; }
        public string ExternalAccountID { get; set; }
        public List<TransactionDetailsVM> TransactionsDetails { get; set; }
    }
}

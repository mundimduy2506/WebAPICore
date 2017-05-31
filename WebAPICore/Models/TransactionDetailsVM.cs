using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPICore.Models
{
    public class TransactionDetailsVM
    {
        [Key]
        public int TransactionsID { get; set; }
        public DateTime? ActualSettlementDate { get; set; }
        public DateTime EventCreatedDate { get; set; }
        public double? BrokerCommissionAmountBase { get; set; }
        public double? BrokerCommissionAmountLocal { get; set; }
        public string BrokerName { get; set; }
        public string Comment { get; set; }
        public DateTime? ContractualSettlementDate { get; set; }
        public double? CostAmountBase { get; set; }
        public double? CostAmountLocal { get; set; }
        public string CUSIP { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? EntryDate { get; set; }
        public double? GrossAmountBase { get; set; }
        public double? GrossAmountLocal { get; set; }
        public string IncomeCurrencyCode { get; set; }
        public string IncomeCurrencyName { get; set; }
        public double? IncomePurchaseSoldAmountBase { get; set; }
        public double? IncomePurchaseSoldAmountLocal { get; set; }
        public string InvestmentDescription { get; set; }
        public int InvestmentID { get; set; }
        public string InvestmentName { get; set; }
        public string ISIN { get; set; }
        public DateTime? KnowledgeDate { get; set; }
        public string LIN { get; set; }
        public double? NetAmountBase { get; set; }
        public double? NetAmountLocal { get; set; }
        public double? NotionalAmountBase { get; set; }
        public double? NotionalAmountLocal { get; set; }
        public double? OtherExpenseBase { get; set; }
        public double? OtherExpenseLocal { get; set; }
        public double? PriceAmountBase { get; set; }
        public double? PriceAmountLocal { get; set; }
        public double? PrincipalAmountBase { get; set; }
        public double? PrincipalAmountLocal { get; set; }
        public double? Quantity { get; set; }
        public double? RealizedGainLossBase { get; set; }
        public double? RealizedGainLossLocal { get; set; }
        public string ReportDataTypeName { get; set; }
        public double? SECFeesBase { get; set; }
        public double? SECFeesLocal { get; set; }
        public string SEDOL { get; set; }
        public string SEIAccountID { get; set; }
        public Int64 SourceSystemTransactionID { get; set; }
        public string Ticker { get; set; }
        public double? TotalGainLossBase { get; set; }
        public DateTime? TradeDate { get; set; }
        public string TransactionDescription { get; set; }
        public string TransactionTypeName { get; set; }
    }
}

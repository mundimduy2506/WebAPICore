using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPICore.Models
{
    public class TransDetails
    {
        public string IssueName { get; set; }
        public string TransactionDescription { get; set; }
        public string ReversingAccountingEventID { get; set; }
        public string CurrencyCodeLocal  { get; set; }
        public string AbsoluteQuantity { get; set; }
        public string UnitPrice { get; set; }
        public string NetAmountLocal { get; set; }
        public DateTime? TradeDate { get; set; }
        public DateTime? SettleDate { get; set; }
        public string AssetClassMinor { get; set; }
    }
}

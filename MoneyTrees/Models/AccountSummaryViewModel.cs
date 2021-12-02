using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoneyTrees.Models
{
    public class AccountSummaryViewModel
    {
        public List<TransactionLocation> TransactionLocation { get; set; }

        public double balance;
        public double Balance
        {
            get { return balance; }
            set { balance = value / 100; }
        }


        public double spendToday;
        public double SpendToday
        {
            get { return spendToday; }
            set { spendToday = value / 100; }
        }

        public string AccountHolderName { get; set; }
    }
}
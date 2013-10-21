using System;
using System.Collections.Generic;

namespace Agile.Gateways.Redsys.Domain.Model
{
    public class RedsysRecurringTransaction
    {
        public RedsysRecurringTransaction()
        {
            SucessiveTransactions = new List<RedsysSuccessiveTransaction>();
        }
        public IList<RedsysSuccessiveTransaction> SucessiveTransactions { get; set; } 
        public string Order { get; set; }
        //public string AuthorizationCode { get; set; }
        public decimal Amount { get; set; }
        public int Frequency { get; set; }
        public DateTime StartDate { get; set; }
        public int Recurrences { get; set; }

        public DateTime EndDate
        {
            get
            {
                return StartDate + TimeSpan.FromDays(Recurrences*Frequency);
            }
        }




 

    }
}

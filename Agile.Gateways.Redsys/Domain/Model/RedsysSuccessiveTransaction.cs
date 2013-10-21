using System;

namespace Agile.Gateways.Redsys.Domain.Model
{
    public class RedsysSuccessiveTransaction
    {
        public RedsysRecurringTransaction RecurrentTransaction { get; set; } 
        public DateTime Date { get; set; }
    }
}

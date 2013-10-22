using System;
using System.Collections.Generic;

namespace Agile.Gateways.Redsys.Domain.Model
{
    /// <summary>
    /// Redsys Recurring Transaction
    /// </summary>
    public class RedsysRecurringTransaction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RedsysRecurringTransaction"/> class.
        /// </summary>
        public RedsysRecurringTransaction()
        {
            SucessiveTransactions = new List<RedsysSuccessiveTransaction>();
        }
        /// <summary>
        /// Sucessive transactions associated to this recurring transaction.
        /// </summary>
        /// <value>The sucessive transactions.</value>
        public IList<RedsysSuccessiveTransaction> SucessiveTransactions { get; set; }
        /// <summary>
        /// Gets or sets the order.
        /// </summary>
        /// <value>The order identifier.</value>
        public string Order { get; set; }
        //public string AuthorizationCode { get; set; }
        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        /// <value>The amount.</value>
        public decimal Amount { get; set; }
        /// <summary>
        /// Gets or sets the frequency.
        /// </summary>
        /// <value>The frequency.</value>
        public int Frequency { get; set; }
        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>The start date.</value>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// Gets or sets the recurrences.
        /// </summary>
        /// <value>The recurrences.</value>
        public int Recurrences { get; set; }

        /// <summary>
        /// Gets the end date.
        /// </summary>
        /// <value>The end date.</value>
        public DateTime EndDate
        {
            get
            {
                return StartDate + TimeSpan.FromDays(Recurrences*Frequency);
            }
        }




 

    }
}

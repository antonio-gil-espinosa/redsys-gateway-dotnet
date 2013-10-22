using System;

namespace Agile.Gateways.Redsys.Domain.Model
{
    /// <summary>
    /// Class RedsysSuccessiveTransaction
    /// </summary>
    public class RedsysSuccessiveTransaction
    {
        /// <summary>
        /// Gets or sets the associated recurrent transaction.
        /// </summary>
        /// <value>The recurrent transaction.</value>
        public RedsysRecurringTransaction RecurrentTransaction { get; set; }
        /// <summary>
        /// Gets or sets the generation date.
        /// </summary>
        /// <value>The generation date.</value>
        public DateTime Date { get; set; }
    }
}

namespace Agile.Gateways.Redsys.Domain.Model
{


    /// <summary>
    /// Redsys Transaction Types
    /// </summary>
    public enum RedsysTransactionType
    {
        /// <summary>
        /// Authorization
        /// </summary>
        Authorization = '0',
        /// <summary>
        /// Preauthorization
        /// </summary>
        Preauthorization = '1',
        /// <summary>
        /// Confirmation
        /// </summary>
        Confirmation = '2',
        /// <summary>
        /// Automatic refund
        /// </summary>
        AutomaticRefund = '3',
        /// <summary>
        /// Payment reference
        /// </summary>
        PaymentReference = '4',
        /// <summary>
        /// Recurring transaction
        /// </summary>
        RecurringTransaction = '5',
        /// <summary>
        /// Successive transaction
        /// </summary>
        SuccessiveTransaction = '6',
        /// <summary>
        /// Authentication
        /// </summary>
        Authentication = '7',
        /// <summary>
        /// Authentication confirmation
        /// </summary>
        AuthenticationConfirmation = '8',
        /// <summary>
        /// Preauthorization cancellation
        /// </summary>
        PreauthorizationCancellation = '9',
        /// <summary>
        /// Deferred authorization
        /// </summary>
        DeferredAuthorization = 'O',
        /// <summary>
        /// Deferred authorization confirmation
        /// </summary>
        DeferredAuthorizationConfirmation = 'P',
        /// <summary>
        /// Deferred authorization cancelled
        /// </summary>
        DeferredAuthorizationCancelled = 'Q',
        /// <summary>
        /// Initial deferred recurring authorization
        /// </summary>
        InitialDeferredRecurringAuthorization = 'R',
        /// <summary>
        /// Successive recurring authorization
        /// </summary>
        SuccessiveRecurringAuthorization = 'S',
    }
}

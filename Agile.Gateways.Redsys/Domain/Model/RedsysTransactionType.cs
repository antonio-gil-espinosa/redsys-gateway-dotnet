namespace Agile.Gateways.Redsys.Domain.Model
{


    public enum RedsysTransactionType
    {
        Authorization = '0',
        Preauthorization = '1',
        Confirmation = '2',
        AutomaticRefund = '3',
        PaymentReference = '4',
        RecurringTransaction = '5',
        SuccessiveTransaction = '6',
        Authentication = '7',
        AuthenticationConfirmation = '8',
        PreauthorizationCancellation = '9',
        DeferredAuthorization = 'O',
        DeferredAuthorizationConfirmation = 'P',
        DeferredAuthorizationCancelled = 'Q',
        InitialDeferredRecurringAuthorization = 'R',
        SuccessiveRecurringAuthorization = 'S',
    }
}

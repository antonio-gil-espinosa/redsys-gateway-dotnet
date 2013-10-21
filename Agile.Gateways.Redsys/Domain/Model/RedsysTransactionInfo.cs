namespace Agile.Gateways.Redsys.Domain.Model
{
    public class RedsysTransactionInfo
    {
        public RedsysTransactionType TransactionType
        {
            get; set;
        }
        public int Currency { get; set; }
        public string Callback { get; set; }
        public string OnSuccess { get; set; }
        public string OnError { get; set; }
        public int Frequency { get; set; }
        public bool TestEnviroment { get; set; }
        public int MerchantCode { get; set; }
        public int Terminal { get; set; }
        public int Recurrences { get; set; }
        public string Secret { get; set; }

        public string Signature
        {
            get
            {

                if (TransactionType == RedsysTransactionType.RecurringTransaction)
                    return SignatureHelper.GetSignature(Amount,
                                                        OrderId,
                                                        MerchantCode,
                                                        Currency,
                                                        Recurrences*Amount,
                                                        TransactionType,
                                                        Callback,
                                                        Secret);


                return SignatureHelper.GetSignature(Amount, OrderId, MerchantCode, Currency, TransactionType, Callback, Secret);
            }
        }
        public string MerchantName { get; set; }
        public string Product { get; set; }
        public string Client { get; set; }
        public string MerchantData { get; set; }

        public string OrderId { get; set; }

        public decimal Amount { get; set; }
    }
}
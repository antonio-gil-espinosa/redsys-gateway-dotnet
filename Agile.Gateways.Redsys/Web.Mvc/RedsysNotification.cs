using System;
using System.Web.Mvc;
using Agile.Gateways.Redsys.Domain.Model;

namespace Agile.Gateways.Redsys.Web.Mvc
{
    [ModelBinder(typeof(RedsysNotificationModelBinder))]
    public class RedsysNotification {
        public RedsysNotification(string merchantData, decimal amount, string order, int response, string authorizationCode, string consumerLanguage, string cardType, DateTime date,  string merchantCode, int terminal, string signature, bool securePayment, RedsysTransactionType transactionType, string cardCountry, string currency)
        {
            MerchantData = merchantData;
            Amount = amount;
            Order = order;
            Response = response;
            AuthorizationCode = authorizationCode;
            ConsumerLanguage = consumerLanguage;
            CardType = cardType;
            DateTime = date;
            MerchantCode = merchantCode;
            Terminal = terminal;
            Signature = signature;
            SecurePayment = securePayment;
            TransactionType = transactionType;
            CardCountry = cardCountry;
            Currency = currency;
        }

        public string MerchantData { get; private set; }
        public decimal Amount { get; private set; }
        public string Order { get; private set; }
        public int Response { get; private set; }
        public string AuthorizationCode { get; private set; }
        public string ConsumerLanguage { get; private set; }
        public string CardType { get; private set; }
        public DateTime DateTime { get; private set; }
        public string MerchantCode { get; private set; }
        public int Terminal { get; private set; }
        public string Signature { get; private set; }
        public bool SecurePayment { get; private set; }
        public RedsysTransactionType TransactionType { get; private set; }
        public string CardCountry { get; private set; }
        public string Currency { get; private set; }

        public bool IsValid(string secret)
        {

            return SignatureHelper.IsNotificationValid(Amount, Order, MerchantCode, Convert.ToInt32(Currency), Response, secret, Signature);

        }

   
    }
}
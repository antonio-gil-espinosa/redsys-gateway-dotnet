using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Agile.Gateways.Redsys.Domain.Model;
using Agile.Gateways.Redsys.Web.Mvc;

namespace Agile.Gateways.Redsys.Domain.Services
{
    public abstract class RedsysServiceBase : IRedsysService
    {
        public abstract int MerchantCode { get; }
        public abstract int Terminal { get; }
        public abstract string Secret { get; }
        public abstract string MerchantName { get; }
        public abstract string NotificationUrl { get; }
        public abstract bool TestEnviroment { get; }

        public abstract IEnumerable<RedsysRecurringTransaction> GetRecurrentTransactions();
        public abstract void OnSucesiveTransactionReceived(RedsysSuccessiveTransaction sucesiveTransaction);
        public abstract void OnNotificationReceived(RedsysNotification RedsysNotification);

        public RedsysTransactionInfo CreateTransactionInfo()
        {
            HttpRequest httpRequest = HttpContext.Current.Request;
            string callback = new UrlHelper(httpRequest.RequestContext).Action("Callback", "Redsys", null, httpRequest.Url.Scheme);
            return new RedsysTransactionInfo
            {
                MerchantCode = MerchantCode,
                MerchantName = MerchantName,
                TestEnviroment = TestEnviroment,
                Secret = Secret,
                Terminal = Terminal,
                Callback = callback
            };
        }
    }
}

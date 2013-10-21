using System.Collections.Generic;
using System.Dynamic;
using Agile.Gateways.Redsys.Domain.Model;
using Agile.Gateways.Redsys.Web.Mvc;

namespace Agile.Gateways.Redsys.Domain.Services
{
    public interface IRedsysService
    {
        int MerchantCode { get;  }
        int Terminal { get;  }
        string Secret { get;  }
        string MerchantName { get;  }
        string NotificationUrl { get; }
        bool TestEnviroment { get; }

        IEnumerable<RedsysRecurringTransaction> GetRecurrentTransactions();
        void OnSucesiveTransactionReceived(RedsysSuccessiveTransaction sucesiveTransaction);
        void OnNotificationReceived(RedsysNotification RedsysNotification);
        RedsysTransactionInfo CreateTransactionInfo();


    }
}

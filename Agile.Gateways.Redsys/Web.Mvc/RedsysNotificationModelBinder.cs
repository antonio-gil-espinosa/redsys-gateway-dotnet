using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Web;
using System.Web.Mvc;
using Agile.Gateways.Redsys.Domain.Model;

namespace Agile.Gateways.Redsys.Web.Mvc
{
    public class RedsysNotificationModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            HttpRequestBase request = controllerContext.HttpContext.Request;
            NameValueCollection values = request.HttpMethod.ToUpper() == "POST" ? request.Form : request.QueryString;

            string temp = string.Format("{0} {1}", values["Ds_Date"], values["Ds_Hour"]);
            DateTime datetime = DateTime.ParseExact(temp, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
            bool securePayment = values["Ds_SecurePayment"] != null && values["Ds_SecurePayment"] == "1";
            decimal amount = Decimal.Parse(values["Ds_Amount"])/100;
            int terminal = Int32.Parse(values["Ds_Terminal"]);
            int response = Int32.Parse(values["Ds_Response"]);
            string merchantData = values["Ds_MerchantData"];
            RedsysTransactionType transactionType = (RedsysTransactionType)char.Parse(values["Ds_TransactionType"]);

            return new RedsysNotification(merchantData,
                                           amount,
                                           values["Ds_Order"],
                                           response,
                                           values["Ds_AuthorisationCode"],
                                           values["Ds_ConsumerLanguage"],
                                           values["Ds_Card_Type"],
                                           datetime,
                                           values["Ds_MerchantCode"],
                                           terminal,
                                           values["Ds_Signature"],
                                           securePayment,
                                           transactionType,
                                           values["Ds_Card_Country"],
                                           values["Ds_Currency"]);
        }
    }
}
using System;
using System.IO;
using System.Web.Mvc;
using Agile.Gateways.Redsys.Domain.Model;

namespace Agile.Gateways.Redsys.Web.Mvc.Views
{
    internal class RedsysRedirectToGatewayView : IView
    {
        private readonly RedsysTransactionInfo _lcti;


        public RedsysRedirectToGatewayView(RedsysTransactionInfo lcti)
        {
            _lcti = lcti;
        }

        public void Render(ViewContext viewContext, TextWriter writer)
        {

            string url = _lcti.TestEnviroment ? "https://sis-t.redsys.es:25443/sis/realizarPago" : "https://sis.redsys.es/sis/realizarPago";


            writer.WriteLine("<html>");
            writer.WriteLine("<head>");
            writer.WriteLine("</head>");
            writer.WriteLine(viewContext.RequestContext.HttpContext.IsDebuggingEnabled
                                 ? "<body>" : "<body onload=\"document.getElementById('theForm').submit();\">");

            writer.WriteLine("<form id=\"theForm\" action=\"{0}\" method=\"POST\" >", url);
            writer.WriteLine("<input type=\"hidden\" name=\"{0}\" value=\"{1}\" />", "Ds_Merchant_Amount", (int)Math.Round(_lcti.Amount * 100,2));
            writer.WriteLine("<input type=\"hidden\" name=\"{0}\" value=\"{1}\" />", "Ds_Merchant_Currency", _lcti.Currency);

            if (_lcti.TransactionType == RedsysTransactionType.RecurringTransaction)
            {
                writer.WriteLine("<input type=\"hidden\" name=\"{0}\" value=\"{1}\" />", "Ds_Merchant_DateFrecuency", _lcti.Frequency);
                writer.WriteLine("<input type=\"hidden\" name=\"{0}\" value=\"{1}\" />", "Ds_Merchant_SumTotal", (int)Math.Round(100 * _lcti.Recurrences * _lcti.Amount,2));
                writer.WriteLine("<input type=\"hidden\" name=\"{0}\" value=\"{1}\" />", "Ds_Merchant_ChargeExpiryDate", (DateTime.Now + TimeSpan.FromDays(_lcti.Recurrences * _lcti.Frequency)).ToString("yyyy-MM-dd"));
            }

            writer.WriteLine("<input type=\"hidden\" name=\"{0}\" value=\"{1}\" />", "Ds_Merchant_MerchantCode", _lcti.MerchantCode);
            writer.WriteLine("<input type=\"hidden\" name=\"{0}\" value=\"{1}\" />", "Ds_Merchant_MerchantData", _lcti.MerchantData);
            writer.WriteLine("<input type=\"hidden\" name=\"{0}\" value=\"{1}\" />", "Ds_Merchant_MerchantSignature", _lcti.Signature);
            writer.WriteLine("<input type=\"hidden\" name=\"{0}\" value=\"{1}\" />", "Ds_Merchant_ProductDescription", _lcti.Product);
            writer.WriteLine("<input type=\"hidden\" name=\"{0}\" value=\"{1}\" />", "Ds_Merchant_MerchantURL", _lcti.Callback);
            writer.WriteLine("<input type=\"hidden\" name=\"{0}\" value=\"{1}\" />", "Ds_Merchant_Order", _lcti.OrderId);
            writer.WriteLine("<input type=\"hidden\" name=\"{0}\" value=\"{1}\" />", "Ds_Merchant_Terminal", _lcti.Terminal);
            writer.WriteLine("<input type=\"hidden\" name=\"{0}\" value=\"{1}\" />", "Ds_Merchant_Titular", _lcti.Client);

            writer.WriteLine("<input type=\"hidden\" name=\"{0}\" value=\"{1}\" />", "Ds_Merchant_TransactionType", (char)_lcti.TransactionType);
            writer.WriteLine("<input type=\"hidden\" name=\"{0}\" value=\"{1}\" />", "Ds_Merchant_UrlOK", _lcti.OnSuccess);
            writer.WriteLine("Si no es redireccionado en breves momentos, haga click ");
            writer.WriteLine("<input type=\"submit\" value=\"Aqui\">");
            writer.WriteLine("</form>");
            writer.WriteLine("</body>");
            writer.WriteLine("</html>");
        }
    }
}

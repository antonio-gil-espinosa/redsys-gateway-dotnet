using System.Web.Mvc;
using Agile.Gateways.Redsys.Domain.Model;
using Agile.Gateways.Redsys.Web.Mvc.Views;

namespace Agile.Gateways.Redsys.Web.Mvc
{
    public class RedsysRedirectToGatewayActionResult : ViewResult
    {
        public RedsysRedirectToGatewayActionResult(RedsysTransactionInfo tx)
        {
            View = new RedsysRedirectToGatewayView(tx);
        }
    }
}

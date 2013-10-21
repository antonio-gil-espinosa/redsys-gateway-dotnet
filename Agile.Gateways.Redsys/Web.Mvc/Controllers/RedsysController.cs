using System.Web.Mvc;
using Agile.Gateways.Redsys.Domain.Services;

namespace Agile.Gateways.Redsys.Web.Mvc.Controllers
{
    public class RedsysController : Controller
    {
        private readonly IRedsysService _RedsysGateway;

        public RedsysController(IRedsysService RedsysGateway) {
            _RedsysGateway = RedsysGateway;
            
        }

     
        public ActionResult Callback(RedsysNotification lcn)
        {
            _RedsysGateway.OnNotificationReceived(lcn);
            return new HttpStatusCodeResult(200);
        }

    



    }
}

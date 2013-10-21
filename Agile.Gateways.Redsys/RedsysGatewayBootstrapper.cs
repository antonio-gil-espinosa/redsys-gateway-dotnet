using System;
using System.Web.Mvc;
using System.Web.Routing;
using Agile.Gateways.Redsys.Domain.Services;
using Agile.Gateways.Redsys.Jobs;
using Agile.Gateways.Redsys.Web.Mvc.Controllers;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Triggers;

namespace Agile.Gateways.Redsys
{
    public static class RedsysGatewayBootstrapper
    {
        internal static Route Route { get; private set; }
        public static void Register(RouteCollection routes,string url,Func<IRedsysService> serviceResolver)
        {

            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();

            CronTriggerImpl RedsysGatewayTrigger = new CronTriggerImpl("RedsysGatewayTrigger") {CronExpressionString = "0 * * * * ?"};

            JobDetailImpl RedsysGatewayJobDetail = new JobDetailImpl("RedsysGatewayJobDetail", typeof(ProcessRecurringTransactionsJob));
            RedsysGatewayJobDetail.JobDataMap["serviceResolver"] = serviceResolver;
            scheduler.ScheduleJob(RedsysGatewayJobDetail, RedsysGatewayTrigger);

            if (!scheduler.IsStarted)
                scheduler.Start();

            string[] namespaces = {typeof (RedsysController).Namespace};

            string name = Guid.NewGuid()
                              .ToString();
            var route = new {action = "Callback", controller = "Redsys"};

            if (routes != null)
                Route = routes.MapRoute(name, url, route, namespaces);


        }

    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using Agile.Gateways.Redsys.Domain.Model;
using Agile.Gateways.Redsys.Domain.Services;
using Quartz;
using RestSharp;

namespace Agile.Gateways.Redsys.Jobs
{
    public class ProcessRecurringTransactionsJob : IJob
    {


        public void Execute(IJobExecutionContext context)
        {

            IRedsysService lcs = ((Func<IRedsysService>)context.JobDetail.JobDataMap["serviceResolver"]).Invoke();

            try
            {
                ProcessRecurringTransactions(lcs,DateTime.Now);
            }
            catch (Exception ex)
            {
                Debug.Fail(ex.Message);
            }
           
        }

        public static void ProcessRecurringTransactions(IRedsysService lcs,DateTime now)
        {

            Expression<Func<RedsysRecurringTransaction, bool>> rTxShouldBeProcessed =
                rTx =>
                rTx.Amount > 0 && rTx.Order != null && now >= rTx.StartDate && now < rTx.EndDate &&
                !rTx.SucessiveTransactions.Any(sTx => sTx.Date > now.AddDays(-rTx.Frequency) && sTx.Date < now);


            IEnumerable<RedsysRecurringTransaction> recurrentTransactions = lcs.GetRecurrentTransactions()
                                                                               .AsQueryable()
                                                                               .Where(rTxShouldBeProcessed)
                                                                               .ToArray();

            foreach (RedsysRecurringTransaction rtx in recurrentTransactions)
            {
                string signature = SignatureHelper.GetSignature(rtx.Amount,
                                                                rtx.Order,
                                                                lcs.MerchantCode,
                                                                978,
                                                                RedsysTransactionType.SuccessiveTransaction,
                                                                lcs.NotificationUrl,
                                                                lcs.Secret);

                /*RestClient restClient = new RestClient(lcs.TestEnviroment ? "https://sis-t.redsys.es:25443/sis" : "https://sis.redsys.es/sis");
                RestRequest restRequest = new RestRequest("/realizarPago");

                restRequest.AddParameter("Ds_Merchant_Amount", (int) Math.Round(rtx.Amount*100, 2));
                restRequest.AddParameter("Ds_Merchant_TransactionType", 6);
                restRequest.AddParameter("Ds_Merchant_MerchantCode", lcs.MerchantCode);
                restRequest.AddParameter("Ds_Merchant_Terminal", lcs.Terminal);
                restRequest.AddParameter("Ds_Merchant_Order", rtx.Order);
                restRequest.AddParameter("Ds_Merchant_Currency", 978);

                restRequest.AddParameter("Ds_Merchant_MerchantSignature", signature);

                IRestResponse response = restClient.Post(restRequest);
                if (response.Content == "Whyyt")*/
                {
                    RedsysSuccessiveTransaction stx = new RedsysSuccessiveTransaction();
                    rtx.SucessiveTransactions.Add(stx);
                    stx.RecurrentTransaction = rtx;
                    stx.Date = now;
                    lcs.OnSucesiveTransactionReceived(stx);
                }
            }
        }
    }
}

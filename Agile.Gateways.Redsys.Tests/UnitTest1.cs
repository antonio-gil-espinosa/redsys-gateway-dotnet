using System;
using System.Diagnostics;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Agile.Gateways.Redsys.Domain.Model;
using Agile.Gateways.Redsys.Domain.Services;
using Agile.Gateways.Redsys.Jobs;
using Agile.Gateways.Redsys.Web.Mvc.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Agile.Gateways.Redsys.Tests
{
    /// <summary>
    /// Descripción resumida de UnitTest1
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        public UnitTest1()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Obtiene o establece el contexto de las pruebas que proporciona
        ///información y funcionalidad para la ejecución de pruebas actual.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Atributos de prueba adicionales
        //
        // Puede usar los siguientes atributos adicionales conforme escribe las pruebas:
        //
        // Use ClassInitialize para ejecutar el código antes de ejecutar la primera prueba en la clase
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup para ejecutar el código una vez ejecutadas todas las pruebas en una clase
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Usar TestInitialize para ejecutar el código antes de ejecutar cada prueba 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup para ejecutar el código una vez ejecutadas todas las pruebas
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestMethod1()
        {
            DateTime now = DateTime.Now;
            List<Redsys.Domain.Model.RedsysRecurringTransaction> rTxs = new List<RedsysRecurringTransaction>();
            List<Redsys.Domain.Model.RedsysSuccessiveTransaction> sTxs = new List<RedsysSuccessiveTransaction>();
            rTxs.Add(new RedsysRecurringTransaction() {Amount = 30, Frequency = 30, Order = new Guid().ToString(), StartDate = now , Recurrences = 12});

            IRedsysService redsisService = Rhino.Mocks.MockRepository.GenerateMock<RedsysServiceBase>();
            redsisService.Stub(x => x.GetRecurrentTransactions())
                         .Return(rTxs);

            redsisService.Stub(x => x.OnSucesiveTransactionReceived(null))
                         .IgnoreArguments()
                         .Do(new Action<RedsysSuccessiveTransaction>(x =>
                                                                     {
                                                                         Debug.WriteLine("Generating sTx for " + x.Date);
                                                                         sTxs.Add(x);
                                                                     }))
            ;

            for (int i = 0; i < 800; i++)
            {
                ProcessRecurringTransactionsJob.ProcessRecurringTransactions(redsisService,now.AddDays(i-300));
            }
           /* RedsysGatewayBootstrapper.Register(null, "", () => redsisService);*/


          


        }
    }
}

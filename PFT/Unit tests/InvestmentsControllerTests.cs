using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PFT.Controllers;
using TwelveDataSharp.Library.ResponseModels;

namespace PFT.Unit_tests
{
    [TestClass]
    public class InvestmentsControllerTests
    {
        [TestMethod]
        public void Test_InvestmentsView()
        {
            InvestmentsController controller = new();
            var result = controller.Index() as ViewResult;
            Assert.AreEqual("Investments", result.ViewName);
        }

        [TestMethod]
        public async Task Test_StockAPI()
        {
            InvestmentsController controller = new();
            TwelveDataQuote data = await controller.RequestStockData("NVDA");
            Assert.IsNotNull(data);
            Assert.AreEqual("NVDA", data.Symbol);
        }
    }
}

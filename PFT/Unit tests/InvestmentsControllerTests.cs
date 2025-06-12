using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PFT.Controllers;
using PFT.Utilities;
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
        public void Test_CanReadExternalFile()
        {
            string testKey = Utilities.Utilities.ReadFromFile("keyTest.txt");
            Assert.IsNotEmpty(testKey);
            Assert.AreEqual("testing the reading functionality", testKey);
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

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using PFT.Controllers;
using PFT.Data;
using PFT.Models.Budgets;
using PFT.Models.Investments;
using PFT.Repositories.Investments;
using PFT.Services;
using PFT.Services.Investments;
using PFT.Utilities;
using TwelveDataSharp.Library.ResponseModels;
using Xunit;

namespace PFT.Integration_tests
{
    public class InvestmentsTests
    {
        private DbContextOptions<PFTContext> _options;

        public InvestmentsTests()
        {
            _options = new DbContextOptionsBuilder<PFTContext>().UseInMemoryDatabase("TestDatabase").Options;
        }

        [Fact]
        public async Task CanAddInvestment()
        {
            Mock<IInvestmentRepository> _mockRepo = new Mock<IInvestmentRepository>();
            InvestmentService _service = new InvestmentService(_mockRepo.Object);

            InvestmentRequest request = new InvestmentRequest {  Symbol = "NVDA", Quantity = 40, Type = (int)InvestmentType.Stock };
            await _service.AddInvestmentAsync(request);

            _mockRepo.Verify(repo => repo.AddInvestmentAsync(It.IsAny<Investment>()), Times.Once);
        }

        [Fact]
        public async Task AddInvestment_RefuseNegativeAmount()
        {
            Mock<IInvestmentRepository> _mockRepo = new Mock<IInvestmentRepository>();
            InvestmentService _service = new InvestmentService(_mockRepo.Object);

            InvestmentRequest request = new InvestmentRequest {  Symbol = "NVDA", Quantity = -5, Type =  (int)InvestmentType.Stock };

            await _service.AddInvestmentAsync(request);
        }

        [Fact]
        public async Task AddInvestment_RefuseInvalidSymbol()
        {
            Mock<IInvestmentRepository> _mockRepo = new Mock<IInvestmentRepository>();
            InvestmentService _service = new InvestmentService(_mockRepo.Object);

            InvestmentRequest request = new InvestmentRequest { Symbol = "INVALID", Quantity = 10, Type = (int)InvestmentType.Stock };

            await _service.AddInvestmentAsync(request);
        }
    }
}

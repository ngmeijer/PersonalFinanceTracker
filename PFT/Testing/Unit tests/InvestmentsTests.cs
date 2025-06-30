using Microsoft.EntityFrameworkCore;
using PFT.Data;
using PFT.Models.Investments;
using PFT.Repositories.Investments;
using PFT.Services.Investments;
using Xunit;

namespace PFT.Testing.Unit_tests
{
    public class InvestmentsTests
    {
        private DbContextOptions<PFTContext> _options;

        public InvestmentsTests()
        {
            _options = new DbContextOptionsBuilder<PFTContext>().UseInMemoryDatabase("TestDatabase").Options;
        }

        [Fact]
        public async Task AddInvestment_ShouldSaveToDatabase()
        {
            using var context  = new PFTContext(_options);
            var repo = new InvestmentRepository(context);
            var service = new InvestmentService(repo);

            var investment = new InvestmentRequest
            {
                Symbol = "GOOGL",
                Quantity = 5,
                Type = (int)InvestmentType.Stock
            };

            await service.AddInvestmentAsync(investment);

            var savedInvestment = await context.Investments.FirstOrDefaultAsync(investment => investment.Symbol == "GOOGL");

            Assert.NotNull(savedInvestment);
            Assert.Equal("GOOGL", savedInvestment.Symbol);
            Assert.Equal(5, savedInvestment.Quantity);
            Assert.Equal(InvestmentType.Stock, savedInvestment.Type);
        }
    }
}

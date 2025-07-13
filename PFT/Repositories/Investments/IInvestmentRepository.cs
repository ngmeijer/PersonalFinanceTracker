using PFT.Models.Investments;

namespace PFT.Repositories.Investments
{
    public interface IInvestmentRepository
    {
        void AddInvestmentAsync(Investment investment);
        void RemoveInvestmentAsync(int id);
        void ChangeInvestmentAsync(Investment investment);
        Dictionary<string, Investment> GetAllInvestmentsAsync();
        Investment GetInvestment(int id);
    }
}

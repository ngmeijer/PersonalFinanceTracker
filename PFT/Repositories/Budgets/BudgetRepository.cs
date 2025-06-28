using Microsoft.Data.SqlClient;
using PFT.Models.Budgets;
using PFT.Models.Investments;
using PFT.Utilities;
using System.Data;

namespace PFT.Repositories.Budgets
{
    public class BudgetRepository : IBudgetRepository
    {
        private string _connectionString;

        public BudgetRepository(IConfiguration config)
        {
            if(config == null) throw new ArgumentNullException("config in BudgetRepository is null");

            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public async Task<ServiceResult> AddBudgetAsync(Budget budget)
        {
            if (_connectionString == null)
            {
                return new ServiceResult
                {
                    Success = false,
                    Message = "Couldn't connect to database."
                };
            }

            using var connection = new SqlConnection(_connectionString);

            await connection.OpenAsync();

            if (connection.State != System.Data.ConnectionState.Open)
            {
                return new ServiceResult
                {
                    Success = false,
                    Message = $"Invalid connection to database. Current connection type: {connection.State}"
                };
            }

            SqlCommand command = new SqlCommand(@"INSERT INTO Budgets (Name, Interval, MaxAmount, CurrentlySpent) VALUES (@name, @interval, @maxAmount, @currentlySpent)", connection);

            //TODO check double names from being entered.

            command.Parameters.AddWithValue("@name", budget.Name);
            command.Parameters.AddWithValue("@interval", budget.Interval);
            command.Parameters.AddWithValue("@maxAmount", budget.MaxAmount);
            command.Parameters.AddWithValue("@currentlySpent", budget.CurrentlySpent);

            var result = await command.ExecuteNonQueryAsync();

            if (result == 0)
            {
                return new ServiceResult
                {
                    Success = false,
                    Message = "Failed to execute AddBudget command."
                };
            }

            return new ServiceResult
            {
                Success = true,
                Message = "Succesfully added new budget."
            };
        }

        public Task ChangeBudgetAmountAsync(Budget budget)
        {
            throw new NotImplementedException();
        }

        public async Task<Dictionary<string, Budget>> GetAllBudgetsAsync()
        {
            Dictionary<string, Budget> data = new();

            using var connection = new SqlConnection(_connectionString);

            await connection.OpenAsync();

            using var command = new SqlCommand("SELECT Name, Interval, MaxAmount, CurrentlySpent FROM Budgets", connection);
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                string name = reader.GetString("Name");
                if (data.ContainsKey(name))
                    continue;

                data.Add(name, new Budget
                {
                    Name = reader.GetString("Name"),
                    MaxAmount = reader.GetInt32("MaxAmount"),
                    CurrentlySpent = reader.GetInt32("CurrentlySpent"),
                    Interval = (Timeframe)reader.GetInt32("Interval"),
                });;
            }

            return data;
        }

        public Task RemoveBudgetAsync(string symbol)
        {
            throw new NotImplementedException();
        }
    }
}

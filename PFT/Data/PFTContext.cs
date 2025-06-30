using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PFT.Identity;
using PFT.Models.Budgets;
using PFT.Models.Investments;

namespace PFT.Data
{
    public class PFTContext : IdentityDbContext<PFTUser>
    {
        public DbSet<Investment> Investments { get; set; }
        public DbSet<Budget> Budgets { get; set; }

        public PFTContext(DbContextOptions<PFTContext> options) : base(options)
        {
        }
    }
}

using Microsoft.EntityFrameworkCore;
using PFT.Models;

namespace PFT.Data
{
    public class PFTContext : DbContext
    {
        public DbSet<Investment> Investments { get; set; }

        public PFTContext()
        {

        }

        public PFTContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}

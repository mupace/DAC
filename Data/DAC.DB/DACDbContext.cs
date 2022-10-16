using Microsoft.EntityFrameworkCore;

namespace DAC.DB
{
    public class DACDbContext : DbContext
    {
        public DACDbContext(DbContextOptions<DACDbContext> options) : base(options) {}
    }
}

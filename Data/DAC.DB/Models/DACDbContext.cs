using Microsoft.EntityFrameworkCore;

namespace DAC.DB.Models
{
    public class DAC_DbContext : DbContext
    {
        public DAC_DbContext(DbContextOptions<DAC_DbContext> options) : base(options) { }
    }
}

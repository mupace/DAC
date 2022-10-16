using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAC.DB;

public class DACIdentityDbContext : IdentityDbContext
{
    //public DACDbContext(){}

    public DACIdentityDbContext(DbContextOptions<DACIdentityDbContext> options)
        : base(options)
    {
    }
}
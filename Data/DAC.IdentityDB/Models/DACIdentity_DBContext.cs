using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DAC.IdentityDB.Models
{
    public class DACIdentity_DBContext : IdentityDbContext
    {
        public DACIdentity_DBContext(DbContextOptions<DACIdentity_DBContext> options)
            : base(options)
        {
        }
    }
}

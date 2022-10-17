using DAC.DB.Models;
using Microsoft.Extensions.Logging;

namespace DAC.Business;

public abstract class BusinessBase
{
    protected readonly DACDBContext _dacDbContext;

    protected ILogger _logger;

    protected BusinessBase(DACDBContext dacDbContext, ILogger logger)
    {
        _dacDbContext = dacDbContext;
        _logger = logger;
    }
}
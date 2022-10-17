using DAC.DB.Models;
using Microsoft.Extensions.Logging;

namespace DAC.Business;

internal class BusinessBase
{
    private readonly DACDBContext _dacDbContext;

    private ILogger _logger;
    public BusinessBase(DACDBContext dacDbContext, ILogger logger)
    {
        _dacDbContext = dacDbContext;
        _logger = logger;
    }
}
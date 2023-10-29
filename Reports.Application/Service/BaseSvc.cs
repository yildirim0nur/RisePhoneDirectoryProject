

using Reports.Application;

public class BaseSvc
{
    protected readonly IDbReportContext _dbContext;

    public BaseSvc(IDbReportContext context)
    {
        _dbContext = context;
    }
}
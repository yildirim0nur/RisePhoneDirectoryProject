namespace Contacts.Application.Service;

public class BaseSvc
{
    protected readonly IDbContext _dbContext;

    public BaseSvc(IDbContext context)
    {
        _dbContext = context;
    }
}
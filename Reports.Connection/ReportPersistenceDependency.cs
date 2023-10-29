
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reports.Persistence.Context;

namespace Reports.Persistence
{
    public static class ReportPersistenceDependency
    {
        public static void AddPersistenceService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DbReportContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("pgConnectionString"),
                    builder => builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
            });

        }
    }
}

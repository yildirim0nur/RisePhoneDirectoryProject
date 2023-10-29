
using Microsoft.EntityFrameworkCore;
using Reports.Application;
using Reports.Domain.Entity;

namespace Reports.Persistence.Context;

public class DbReportContext : DbContext, IDbReportContext

{
    public DbReportContext(DbContextOptions<DbReportContext> options) : base(options)
    {

    }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<ContactDetail> ContactDetails { get; set; }
    public DbSet<Report> Reports { get; set; }

}
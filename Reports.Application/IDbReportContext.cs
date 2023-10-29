using Microsoft.EntityFrameworkCore;
using Reports.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Application
{
    public interface IDbReportContext
    {
        DbSet<Contact> Contacts { get; set; }
        DbSet<ContactDetail> ContactDetails { get; set; }
        DbSet<Report> Reports { get; set; }

        Task<int> SaveChangesAsync(CancellationToken token);
    }
}

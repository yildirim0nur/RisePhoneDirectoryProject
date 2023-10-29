using Contacts.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Contacts.Application.Service;

public interface IDbContext
{
    DbSet<Domain.Entity.Contact> Contacts { get; set; }
    DbSet<ContactDetail> ContactDetails { get; set; }
    Task<int> SaveChangesAsync(CancellationToken token);
}
using Contacts.Application.Service;
using Contacts.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Contacts.Persistence.Context;

public class ApplicationDbContext:DbContext,IDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    public virtual DbSet<Contact> Contacts { get; set; }
    public virtual DbSet<ContactDetail> ContactDetails { get; set; }
}
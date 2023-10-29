using Contacts.Domain.Entity.Base;

namespace Contacts.Domain.Entity;

public class Contact:BaseEntity
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string CompanyName { get; set; }
    public virtual List<ContactDetail> ContactDetails { get; set; }
}
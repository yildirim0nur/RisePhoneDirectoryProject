using Contacts.Domain.Entity.Base;
using Shared.Dto;

namespace Contacts.Domain.Entity;

public class ContactDetail:BaseEntity
{
    public ContactTypeEnum ContactType { get; set; }
    public string Content { get; set; }
    public Guid ContactId { get; set; }
    public virtual Contact Contact { get; set; }
}
using Contacts.Domain.Entity.Base;
using Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Domain.Entity
{
    public class ContactDetail : BaseEntity
    {
        public ContactTypeEnum ContactType { get; set; }
        public string Content { get; set; }
        public Guid ContactId { get; set; }
        public virtual Contact Contact { get; set; }
    }
}

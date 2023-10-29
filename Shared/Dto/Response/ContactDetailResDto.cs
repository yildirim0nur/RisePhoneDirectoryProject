using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dto.Response
{
    public class ContactDetailResDto
    {
        public ContactTypeEnum ContactType { get; set; }
        public string Content { get; set; }
        public Guid ContactId { get; set; }
    }
}

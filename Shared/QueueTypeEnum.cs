using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public enum QueueTypeEnum
    {
        [Description("ContactCreated")]
        ContactCreated,
        [Description("ContactDeleted")]
        ContactDeleted,
        [Description("DetailCreated")]
        DetailCreated,
        [Description("DetailDeleted")]
        DetailDeleted,
        [Description("ReportRequested")]
        ReportRequested
    }
}

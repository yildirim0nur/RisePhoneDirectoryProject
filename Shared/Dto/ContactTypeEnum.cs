using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dto
{
    public enum ContactTypeEnum
    {
        [Description("GSM")]
        Gsm,
        [Description("Email")]
        Email,
        [Description("Location")]
        Location
    }
}

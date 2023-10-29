using Contacts.Domain.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Domain.Entity
{
    public class Report:BaseEntity
    {
        public DateTime RequestDate { get; set; }
        public ReportStatus ReportStatus { get; set; }
        public byte[] ReportData { get; set; }
    }
}
public enum ReportStatus
{
    [Description("Hazırlanıyor")]
    Preparing,
    [Description("Tamamlandi")]
    Completed
}
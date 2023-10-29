using Contacts.Application.Service.Contact.Interface;
using Contacts.Constants;
using Contacts.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Shared;
using Shared.Wrapper;

namespace Reports.Api.Controllers
{
    public class ReportController : BaseController
    {


        private readonly IContactReportSvc _reportSvc;
        private readonly ContactSettings _contactSettings;

        public ReportController(IContactReportSvc reportSvc, IOptions<ContactSettings> options)
        {
            _reportSvc = reportSvc;
            _contactSettings = options.Value;
        }
        [HttpPost]
        public async Task<ActionResult<SingleDataResponse<Guid>>> GenerateReport()
        {
            var report = await _reportSvc.GenerateReport();
            QueueSender.Send(report.Data, QueueTypeEnum.ReportRequested, _contactSettings.RabbitMqConnectionString);
            return await Execute<Guid>(report);
        }

        [HttpGet]
        public async Task<ActionResult> GetReport(Guid id)
        {
            var report = await _reportSvc.GetReport(id);
            return File(new MemoryStream(report, 0, report.Length), "application/octet-stream", "report.xlsx");

        }
    }
}

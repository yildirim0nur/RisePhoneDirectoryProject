

using Contacts.Application.Service.Contact.Interface;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using Reports.Application;
using Reports.Domain.Entity;
using Shared.Dto.Request;
using Shared.Dto.Response;
using Shared.Wrapper;

public class ContactReportSvc : BaseSvc, IContactReportSvc
{
    public ContactReportSvc(IDbReportContext context) : base(context)
    {
    }
    public async Task CreateContact(ContactResDto dto)
    {
        var contact = new Contact
        {
            Id = dto.Id,
            Name = dto.Name,
            Surname = dto.Surname,
            CompanyName = dto.CompanyName
        };
        await _dbContext.Contacts.AddAsync(contact);
        await _dbContext.SaveChangesAsync(CancellationToken.None);

    }
    public async Task DeleteContact(Guid id)
    {
        var contact = _dbContext.Contacts.FirstOrDefault(i => i.Id == id);

        var details = _dbContext.ContactDetails.Where(i => i.ContactId == id).ToList();
        _dbContext.ContactDetails.RemoveRange(details);
        _dbContext.Contacts.Remove(contact);
        await _dbContext.SaveChangesAsync(CancellationToken.None);
    }
    public async Task AddContactDetail(ContactDetailResDto dto)
    {
        var detail = new ContactDetail
        {
            ContactId = dto.ContactId,
            Content = dto.Content,
            ContactType = dto.ContactType
        };
        await _dbContext.ContactDetails.AddAsync(detail);
        await _dbContext.SaveChangesAsync(CancellationToken.None);
    }

    public async Task RemoveContactDetail(Guid id)
    {
        var detail = _dbContext.ContactDetails.FirstOrDefault(i => i.Id == id);
        _dbContext.ContactDetails.Remove(detail);
        await _dbContext.SaveChangesAsync(CancellationToken.None);
    }

    public async Task<byte[]> GetReport(Guid id)
    {
        var report = await _dbContext.Reports.FirstOrDefaultAsync(i => i.Id == id);
        if (report == null)
            throw new ArgumentNullException();

        return report.ReportData;
    }

    public async Task<SingleDataResponse<Guid>> GenerateReport()
    {
        var entity = new Report
        {
            ReportStatus = ReportStatus.Preparing,
            ReportData = new byte[0]
        };
        await _dbContext.Reports.AddAsync(entity);
        await _dbContext.SaveChangesAsync(CancellationToken.None);
        return new SingleDataResponse<Guid>(entity.Id);
    }

    public async Task GenerateExcel(Guid id)
    {
        var statisticsReport = _dbContext.ContactDetails.Where(x => x.ContactType == Shared.Dto.ContactTypeEnum.Location).Select(x => x.Content).Distinct().Select(x => new 
        {
            ReportId = id,
            Location = x,
            PersonCount = _dbContext.ContactDetails.Where(y => y.ContactType == Shared.Dto.ContactTypeEnum.Location && y.Content == x).Count(),
            PhoneNumberCount = _dbContext.ContactDetails.Where(y => y.ContactType == Shared.Dto.ContactTypeEnum.Gsm && _dbContext.ContactDetails.Where(y => y.ContactType == Shared.Dto.ContactTypeEnum.Location && y.Content == x).Select(x => x.ContactId).Contains(y.ContactId)).Count()
        }).ToList();

        using var p = new ExcelPackage();
        var ws = p.Workbook.Worksheets.Add("Report");
        ws.Cells.LoadFromCollection(statisticsReport, true);

        var report = _dbContext.Reports.FirstOrDefault(i => i.Id == id);
        report.ReportData = await p.GetAsByteArrayAsync();
        report.ReportStatus = ReportStatus.Completed;
        await _dbContext.SaveChangesAsync(CancellationToken.None);
    }
}
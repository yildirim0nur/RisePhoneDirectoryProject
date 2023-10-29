
using Shared.Dto.Request;
using Shared.Dto.Response;
using Shared.Wrapper;

namespace Contacts.Application.Service.Contact.Interface;

public interface IContactReportSvc
{
    Task CreateContact(ContactResDto dto);
    Task DeleteContact(Guid id);
    Task AddContactDetail(ContactDetailResDto dto);
    Task RemoveContactDetail(Guid id);

    Task<SingleDataResponse<Guid>> GenerateReport();
    Task<byte[]> GetReport(Guid id);
    Task GenerateExcel(Guid id);
}
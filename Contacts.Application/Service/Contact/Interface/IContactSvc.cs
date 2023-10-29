
using Shared.Dto.Request;
using Shared.Dto.Response;
using Shared.Wrapper;

namespace Contacts.Application.Service.Contact.Interface;

public interface IContactSvc
{
    Task<SingleDataResponse<ContactResDto>> CreateContact(ContactReqDto dto);
    Task<SingleDataResponse<Guid>> DeleteContact(Guid id);
    Task<ListDataResponse<ContactResDto>> GetAllContacts();
    Task<SingleDataResponse<ContactResDto>> GetContactById(Guid id);
    Task<SingleDataResponse<ContactDetailResDto>> AddContactDetail(ContactDetailReqDto dto);
    Task<SingleDataResponse<Guid>> RemoveContactDetail(Guid id);
    Task<SingleDataResponse<ContactDetailResDto>> GetContactDetailById(Guid id);
}
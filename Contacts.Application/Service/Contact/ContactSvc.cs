
using Contacts.Application.Service.Contact.Interface;
using Contacts.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Shared.Dto.Request;
using Shared.Dto.Response;
using Shared.Wrapper;

namespace Contacts.Application.Service.Contact;

public class ContactSvc : BaseSvc, IContactSvc
{
    public ContactSvc(IDbContext context) : base(context)
    {
    }

    public async Task<SingleDataResponse<ContactResDto>> CreateContact(ContactReqDto dto)
    {
        var contact = new Domain.Entity.Contact
        {
            Name = dto.Name,
            Surname = dto.Surname,
            CompanyName = dto.CompanyName
        };
        await _dbContext.Contacts.AddAsync(contact);
        await _dbContext.SaveChangesAsync(CancellationToken.None);
        var result = new ContactResDto
        {
            Id = contact.Id,
            Name = contact.Name,
            CompanyName = contact.CompanyName,
            Surname = contact.Surname

        };
        return new SingleDataResponse<ContactResDto>(result, message: "Başarıyla Eklendi!");
    }

    public async Task<SingleDataResponse<Guid>> DeleteContact(Guid id)
    {
        var contact = _dbContext.Contacts.FirstOrDefault(i => i.Id == id);
        if (contact == null)
            return new SingleDataResponse<Guid>(id, message: "Silinecek Kişi Bulunamadı!");
        var details = _dbContext.ContactDetails.Where(i => i.ContactId == id).ToList();
        _dbContext.ContactDetails.RemoveRange(details);
        _dbContext.Contacts.Remove(contact);
        await _dbContext.SaveChangesAsync(CancellationToken.None);
        return new SingleDataResponse<Guid>(id, message: "Kişi Ve Detayları Başarıyla Silindi");
    }

    public async Task<ListDataResponse<ContactResDto>> GetAllContacts()
    {
        var contactList = await _dbContext.Contacts.Select(i => new ContactResDto()
        {
            Id = i.Id,
            Name = i.Name,
            Surname = i.Surname,
            CompanyName = i.CompanyName
        }).ToListAsync();
        return new ListDataResponse<ContactResDto>(contactList);
    }

    public async Task<SingleDataResponse<ContactResDto>> GetContactById(Guid id)
    {
        var contact = await _dbContext.Contacts.FirstOrDefaultAsync(i => i.Id == id);
        if (contact == null)
            return new SingleDataResponse<ContactResDto>(new ContactResDto());

        return new SingleDataResponse<ContactResDto>(new ContactResDto()
        {
            Id = contact.Id,
            Name = contact.Name,
            Surname = contact.Surname,
            CompanyName = contact.CompanyName
        });
    }

    public async Task<SingleDataResponse<ContactDetailResDto>> AddContactDetail(ContactDetailReqDto dto)
    {
        var detail = new ContactDetail
        {
            ContactId = dto.ContactId,
            Content = dto.Content,
            ContactType = dto.ContactType
        };
        await _dbContext.ContactDetails.AddAsync(detail);
        await _dbContext.SaveChangesAsync(CancellationToken.None);
        var result = new ContactDetailResDto
        {
            ContactId = dto.ContactId,
            ContactType = dto.ContactType,
            Content = dto.Content
        };
        return new SingleDataResponse<ContactDetailResDto>(result, "Detay Başarıyla Eklendi");
    }

    public async Task<SingleDataResponse<Guid>> RemoveContactDetail(Guid id)
    {
        var detail = _dbContext.ContactDetails.FirstOrDefault(i => i.Id == id);
        if (detail == null)
            return new SingleDataResponse<Guid>(id, "Silinecek Kayıt Bulunamadı!");
        
        _dbContext.ContactDetails.Remove(detail);
        await _dbContext.SaveChangesAsync(CancellationToken.None);
        return new SingleDataResponse<Guid>(id, "Detay Başarıyla Silindi");
    }

    public async Task<SingleDataResponse<ContactDetailResDto>> GetContactDetailById(Guid id)
    {
        var detail = await _dbContext.ContactDetails.Where(i => i.Id == id).FirstOrDefaultAsync();
        if(detail==null)
            throw new ArgumentNullException("No Detail Found!");

        return new SingleDataResponse<ContactDetailResDto>(new ContactDetailResDto
        {
            Content = detail.Content,
            ContactId = detail.ContactId,
            ContactType = detail.ContactType
        });
    }
}
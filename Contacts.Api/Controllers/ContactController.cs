using Contacts.Application.Service.Contact.Interface;
using Contacts.Constants;
using Contacts.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Shared;
using Shared.Dto.Common;
using Shared.Dto.Request;
using Shared.Dto.Response;

namespace Contacts.Api.Controllers
{
    public class ContactController : BaseController
    {
        private readonly IContactSvc _contactSvc;
        private readonly ContactSettings _contactSettings;
        public ContactController(IContactSvc contactSvc, IOptions<ContactSettings> options)
        {
            _contactSvc = contactSvc;
            _contactSettings = options.Value;
        }

        [HttpPost]
        public async Task<ActionResult<ContactResDto>> Create([FromBody] ContactReqDto dto)
        {
            if (dto == null) return BadRequest();
            var result = await _contactSvc.CreateContact(dto);
            QueueSender.Send(result.Data, QueueTypeEnum.ContactCreated, _contactSettings.RabbitMqConnectionString);
            return await Execute(result);
        }

        [HttpDelete]
        public async Task<ActionResult<Guid>> Delete([FromBody] GuidReqDto guid)
        {
            if (guid == null) return BadRequest();
            var result = await _contactSvc.DeleteContact(guid.Id);
            QueueSender.Send(result.Data, QueueTypeEnum.ContactDeleted, _contactSettings.RabbitMqConnectionString);
            return await Execute(result);
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<ContactResDto>>> GetAll()
        {
            return await Execute(await _contactSvc.GetAllContacts());
        }
        [HttpGet("{Id:guid}")]
        public async Task<ActionResult<IEnumerable<ContactResDto>>> GetContactById(Guid id)
        {
            return await Execute(await _contactSvc.GetContactById(id));
        }

        [HttpPost("ContactDetail")]
        public async Task<ActionResult<bool>> AddContactDetail([FromBody] ContactDetailReqDto dto)
        {
            if (dto == null) return BadRequest();
            var result = await _contactSvc.AddContactDetail(dto);
            QueueSender.Send(result.Data, QueueTypeEnum.DetailCreated, _contactSettings.RabbitMqConnectionString);
            return await Execute(result);
        }
        [HttpDelete("ContactDetail/{Id:guid}")]
        public async Task<ActionResult<bool>> RemoveContactDetail(Guid id)
        {
            var result = await _contactSvc.RemoveContactDetail(id);
            QueueSender.Send(result.Data, QueueTypeEnum.DetailDeleted, _contactSettings.RabbitMqConnectionString);
            return await Execute(result);
        }
        [HttpGet("ContactDetail")]
        public async Task<ActionResult<ContactDetailResDto>> ContactDetailById(Guid id)
        {
            return await Execute(await _contactSvc.GetContactDetailById(id));
        }
    }
}

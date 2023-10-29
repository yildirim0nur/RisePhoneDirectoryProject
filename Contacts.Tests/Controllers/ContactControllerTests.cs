using Contacts.Application.Service.Contact.Interface;
using Contacts.Constants;
using Contacts.Api.Controllers;
using Contacts.Tests.Helpers;
using Microsoft.Extensions.Options;
using Moq;
using Shared.Dto.Request;
using Shared.Dto.Response;
using Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;


namespace Contacts.Tests.Controllers
{
    public class ContactControllerTests
    {
        [Fact]
        public async Task AddContact_With_Valid_Params_Should_Return_200()
        {
            var mockPersonService = new Mock<IContactSvc>();
            mockPersonService
                .Setup(x => x.CreateContact(It.IsAny<ContactReqDto>()))
                .ReturnsAsync(() => new SingleDataResponse<ContactResDto>(new ContactResDto
                {
                    Name = "test",
                    Surname = "test",
                    CompanyName = "test"
                }));


            var controller = new ContactController(mockPersonService.Object, Options.Create(new ContactSettings()));

            var result = await controller.Create(new ContactReqDto()
            {
                Name = "test",
                Surname = "test",
                CompanyName = "test"
            });
            Assert.Equal(200, TestHelper.GetStatusCodeFromActionResult(result));
        }
        [Fact]
        public async Task AddContact_With_InValid_Params_Should__Return_400()
        {
            var mockPersonService = new Mock<IContactSvc>();
            mockPersonService
                .Setup(x => x.CreateContact(It.IsAny<ContactReqDto>()))
                .ReturnsAsync(() => new SingleDataResponse<ContactResDto>(new ContactResDto
                {

                }));
            var controller = new ContactController(mockPersonService.Object, Options.Create(new ContactSettings()));
            var result = await controller.Create(null);
            Assert.Equal(400, TestHelper.GetStatusCodeFromActionResult(result));
        }
        [Fact]
        public async Task AddContact_Details_With_Valid_Params_Should_Return_200()
        {
            var mockPersonService = new Mock<IContactSvc>();
            mockPersonService
                .Setup(x => x.AddContactDetail(It.IsAny<ContactDetailReqDto>()))
                .ReturnsAsync(() => new SingleDataResponse<ContactDetailResDto>(new ContactDetailResDto
                {
                    Content = "05875557799",
                    ContactType = Shared.Dto.ContactTypeEnum.Gsm
                }));


            var controller = new ContactController(mockPersonService.Object, Options.Create(new ContactSettings()));

            var result = await controller.AddContactDetail(new ContactDetailReqDto()
            {
                Content = "05875557799",
                ContactType = Shared.Dto.ContactTypeEnum.Gsm
            });
            Assert.Equal(200, TestHelper.GetStatusCodeFromActionResult(result));
        }
        [Fact]
        public async Task AddContact_Details_With_InValid_Params_Should_Return_400()
        {
            var mockPersonService = new Mock<IContactSvc>();
            mockPersonService
                .Setup(x => x.AddContactDetail(It.IsAny<ContactDetailReqDto>()))
                .ReturnsAsync(() => new SingleDataResponse<ContactDetailResDto>(new ContactDetailResDto
                {
                    Content = "05875557799",
                    ContactType = Shared.Dto.ContactTypeEnum.Gsm
                }));


            var controller = new ContactController(mockPersonService.Object, Options.Create(new ContactSettings()));

            var result = await controller.AddContactDetail(null);
            Assert.Equal(400, TestHelper.GetStatusCodeFromActionResult(result));
        }
        [Fact]
        public async Task Delete_Contact_With_Valid_Params_Should_Return_200()
        {
            var id = Guid.NewGuid();
            var mockPersonService = new Mock<IContactSvc>();
            mockPersonService
                .Setup(x => x.DeleteContact(id))
               .ReturnsAsync(() => new SingleDataResponse<Guid>(id));


            var controller = new ContactController(mockPersonService.Object, Options.Create(new ContactSettings()));

            var result = await controller.Delete(new Shared.Dto.Common.GuidReqDto { Id = id });
            Assert.Equal(200, TestHelper.GetStatusCodeFromActionResult(result));
        }
        [Fact]
        public async Task Delete_Contact_With_InValid_Params_Should_Return_400()
        {
            var mockPersonService = new Mock<IContactSvc>();
            mockPersonService
                .Setup(x => x.DeleteContact(Guid.Empty))
               .ReturnsAsync(() => new SingleDataResponse<Guid>(Guid.Empty));


            var controller = new ContactController(mockPersonService.Object, Options.Create(new ContactSettings()));

            var result = await controller.Delete(null);
            Assert.Equal(400, TestHelper.GetStatusCodeFromActionResult(result));
        }
        [Fact]
        public async Task Get_All_Contact_With_Valid_Params_Should_Return_404()
        {
            var mockPersonService = new Mock<IContactSvc>();
            mockPersonService
                  .Setup(x => x.GetAllContacts())
                  .ReturnsAsync(new ListDataResponse<ContactResDto>(new List<ContactResDto>()));
            var controller = new ContactController(mockPersonService.Object, Options.Create(new ContactSettings()));

            var result = await controller.GetAll();
            Assert.Equal(404, TestHelper.GetStatusCodeFromActionResult(result));
        }
        [Fact]
        public async Task Get_Contact_By_Id_With_Valid_Params_Should_Return_200()
        {
            var id = Guid.NewGuid();
            var mockPersonService = new Mock<IContactSvc>();
            mockPersonService
                .Setup(x => x.GetContactById(id))
                .ReturnsAsync(() => new SingleDataResponse<ContactResDto>(new ContactResDto
                {
                    Name = "test",
                    Surname = "test",
                    CompanyName = "test"
                }));


            var controller = new ContactController(mockPersonService.Object, Options.Create(new ContactSettings()));

            var result = await controller.GetContactById(id);
            Assert.Equal(200, TestHelper.GetStatusCodeFromActionResult(result));
        }
    }
}

using Contacts.Constants;
using Reports.Api.Controllers;
using Reports.Tests.Helpers;
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
using Microsoft.AspNetCore.Mvc;
using Contacts.Application.Service.Contact.Interface;

namespace Reports.Tests.Controllers
{
    public class ReportControllerTests
    {
        [Fact]
        public async Task Get_Generate_Report_By_Id_With_Valid_Params_Should_Return_200()
        {
            var id = Guid.NewGuid();
            var mockPersonService = new Mock<IContactReportSvc>();
            mockPersonService
                  .Setup(x => x.GenerateReport())
                  .ReturnsAsync(new SingleDataResponse<Guid>(id));
            var controller = new ReportController(mockPersonService.Object, Options.Create(new ContactSettings()));

            var result = await controller.GenerateReport();
            Assert.Equal(200, TestHelper.GetStatusCodeFromActionResult(result));
        }
        [Fact]
        public async Task Get_Generate_Report_By_Id_With_InValid_Params_Should_Return_400()
        {
            var id = Guid.NewGuid();
            var mockPersonService = new Mock<IContactReportSvc>();
            mockPersonService
                  .Setup(x => x.GenerateReport())
                  .ReturnsAsync(new SingleDataResponse<Guid>(Guid.Empty));
            var controller = new ReportController(mockPersonService.Object, Options.Create(new ContactSettings()));

            var result = await controller.GenerateReport();
            Assert.Equal(400, TestHelper.GetStatusCodeFromActionResult(result));
        }
    }
}

using Contacts.Persistence.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reports.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Tests.Helpers
{
    public static class TestHelper
    {
        public static DbContextOptions<ApplicationDbContext> GetPhoneBookContextForInMemoryDb()
        {
            return new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ContactTestDb")
                .Options;
        }

        public static DbContextOptions<DbReportContext> GetReportContextForInMemoryDb()
        {
            return new DbContextOptionsBuilder<DbReportContext>()
                .UseInMemoryDatabase(databaseName: "ReportTestDb")
                .Options;
        }
        public static int? GetStatusCodeFromActionResult<T>(ActionResult<T> actionResult)
        {
            try
            {
                return ((ObjectResult)actionResult.Result).StatusCode;
            }
            catch (Exception e)
            {
                return ((BadRequestResult)actionResult.Result).StatusCode;
            }

        }
    }
}

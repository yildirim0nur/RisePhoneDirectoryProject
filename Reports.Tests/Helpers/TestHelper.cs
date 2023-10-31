using Reports.Persistence.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using Reports.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Tests.Helpers
{
    public class TestHelper
    {
        public static DbContextOptions<DbReportContext> GetPhoneBookContextForInMemoryDb()
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

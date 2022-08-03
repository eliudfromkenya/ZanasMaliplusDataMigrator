using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZanasMigratorTest
{
    [TestFixture]
    public class MsExcelReportTests
    {
        [Test]
        public void Check_If_report_Generation_is_Completed()
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            MsExcelReportService service = new();

            var path = service.GenerateReport(TestData.ZanasColumns().ToArray(), TestData.MaliplusColumns().ToArray(), Array.Empty<DBRelationship>());

            Assert.IsNotEmpty(path);
        }
    }
}

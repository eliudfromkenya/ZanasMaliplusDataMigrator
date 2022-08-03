using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZanasMigratorTest
{
    [TestFixture]
    public class MySQLDBServiceTests
    {
        [Test]
        public void Check_If_report_Generation_is_Completed()
        {
            CreateMySQLService service = new();

            var path = service.CreateMySQLDatabase(TestData.ZanasColumns().ToArray(), TestData.MaliplusColumns().ToArray(), Array.Empty<DBRelationship>());

            Assert.IsNotEmpty(path);
        }
    }
}

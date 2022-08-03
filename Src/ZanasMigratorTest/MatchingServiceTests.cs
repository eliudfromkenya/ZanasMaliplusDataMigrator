using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ZanasMaliplusDataMigrator.Services.MatchingService;

namespace ZanasMigratorTest;

[TestFixture]
public class MatchingServiceTests
{
    private List<DbColumnModel>? _zanasColumns, _maliplusColumns;
    [SetUp]
    public void SetupReportServiceTests()
    {
        _zanasColumns = TestData.ZanasColumns().ToList();
        _maliplusColumns = TestData.MaliplusColumns().ToList();
    }

    [Test]
    public void Check_Same_Column_Names_And_Table_Names()
    {
        MatchingService service = new();
        var columnPairs = _maliplusColumns?.Select(c => new ColumnPair { MaliplusColumn = c }).ToList();

        service.ComparerByTableNameAndSameColumns(ref columnPairs, _zanasColumns?.ToArray());
        var matchingColumns = columnPairs.Where(c => c.ZanasColumn != null).ToList();
        var matchingTables = columnPairs
            .Where(c => c.ZanasColumn != null)
            .Select(v => v.MaliplusColumn?.TableName)
            .Distinct().ToList();

        Assert.AreEqual(6, matchingColumns.Count);
        Assert.AreEqual(new[] {"Table 5"}, matchingTables);
    }

    [Test]
    public void Check_If_Same_Column_Names_And_Different_Table_Name()
    {
        MatchingService service = new();
        var columnPairs = _maliplusColumns?.Select(c => new ColumnPair { MaliplusColumn = c }).ToList();

        service.ComparerTableNameDifferentWithSameColumns(ref columnPairs, _zanasColumns?.ToArray());
        var matchingColumns = columnPairs.Where(c => c.ZanasColumn != null).ToList();
        var matchingTables = columnPairs
            .Where(c => c.ZanasColumn != null)
            .Select(v => v.MaliplusColumn?.TableName)
            .Distinct().ToList();

        Assert.AreEqual(4, matchingColumns.Count);
        Assert.AreEqual(new[] { "Table 3" }, matchingTables);
    }


    [Test]
    public void Check_Same_Table_Name_But_different_column_Numbers()
    {
        MatchingService service = new();
        var columnPairs = _maliplusColumns?.Select(c => new ColumnPair { MaliplusColumn = c }).ToList();

        service.ComparerTableNameDifferentWithSomeSameColumns(ref columnPairs, _zanasColumns?.ToArray());
        var matchingColumns = columnPairs.Where(c => c.ZanasColumn != null).ToList();
        var matchingTables = columnPairs
            .Where(c => c.ZanasColumn != null)
            .Select(v => v.MaliplusColumn?.TableName)
            .Distinct().ToList();

        Assert.AreEqual(7, matchingColumns.Count);
        Assert.AreEqual(new[] { "Table 6","Table 7" }, matchingTables);
    }


    [Test]
    public void Check_Generated_Match_Columns()
    {
        MatchingService service = new();
     
        var (pairs, unMatchedZanasColumns) = 
            service.GenerateMatchColumns(_zanasColumns.ToArray(), _maliplusColumns.ToArray());
        var matched = pairs.Where(c => c?.ZanasColumn != null).ToArray();
        var unMatchedMaliplusColumns = pairs
            .Where(c => c?.ZanasColumn == null)
            .Select(v => v?.MaliplusColumn)
            .ToArray();

        Assert.AreEqual(27, pairs.Length);
        Assert.AreEqual(6, unMatchedZanasColumns.Length);
        Assert.AreEqual(7, unMatchedMaliplusColumns.Length);
    }


    [Test]
    public void Check_If_Some_Same_Column_Names_And_Different_Table_Names()
    {
        MatchingService service = new();
        var columnPairs = _maliplusColumns?.Select(c => new ColumnPair { MaliplusColumn = c }).ToList();

        service.ComparerTableNameDifferentWithSomeSameColumns(ref columnPairs, _zanasColumns?.ToArray());
        var matchingColumns = columnPairs.Where(c => c.ZanasColumn != null).ToList();
        var matchingTables = columnPairs
            .Where(c => c.ZanasColumn != null)
            .Select(v => v.MaliplusColumn?.TableName)
            .Distinct().ToList();

        Assert.AreEqual(7, matchingColumns.Count);
        Assert.AreEqual(new[] { "Table 6","Table 7" }, matchingTables);
    }
}

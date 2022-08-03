namespace ZanasMaliplusDataMigrator.Services;
public class MatchingService
{
    public record ColumnPair 
    { 
        public DbColumnModel? ZanasColumn { get; set; }
        public DbColumnModel? MaliplusColumn { get; set; }
    }

    public (ColumnPair?[] pairs, DbColumnModel?[] unMatchedZanasColumns) GenerateMatchColumns(DbColumnModel[] zanasColumnModels, DbColumnModel[] maliplusColumnModels)
    {
        var columnPairs = maliplusColumnModels.Select(c => new ColumnPair { MaliplusColumn = c }).ToList();
        ComparerByTableNameAndSameColumns(ref columnPairs, zanasColumnModels);
        ComparerTableNameDifferentWithSameColumns(ref columnPairs, zanasColumnModels);
        ComparerByTableNameSameWithSomeSameColumnNames(ref columnPairs, zanasColumnModels);
        ComparerTableNameDifferentWithSomeSameColumns(ref columnPairs, zanasColumnModels);

        return (columnPairs.ToArray(), zanasColumnModels
            .Except(columnPairs.Where(v => v.ZanasColumn != null)
            .Select(v => v.ZanasColumn))
            .ToArray());
    }


    public void ComparerTableNameDifferentWithSomeSameColumns(ref List<ColumnPair> columnPairs, DbColumnModel[] zanasModels)
    {
        //Try to match some columns (same names) and table names different - Renamed zanas table with added or removed columns to Maliplus

        var zanasGroupedColumns = zanasModels
            .GroupBy(c => c.TableName)
            .Select(c =>
             new {
                     Table = c.Key,
                     Models = c.ToArray(),
                     Columns = c.Select(v => v.ColumnName?.Replace(" ", "")).ToArray()
                 });

        var intersects = columnPairs
               .Where(c => c.ZanasColumn == null)
               .GroupBy(c => c.MaliplusColumn?.TableName)
               .Select(m =>
               {
                   var columnNames = m.Select(c => c.MaliplusColumn?.ColumnName?.Replace(" ", "")).ToArray();
                   var intersects = zanasGroupedColumns.Select(x =>
                   {
                       return new
                       {
                           Group = x,
                           Intersects = columnNames.Intersect(x.Columns, StringComparer.InvariantCultureIgnoreCase)
                                   .Select(v => v?.Replace(" ", ""))
                       };
                   }).Where(u => u.Group.Table != m.Key)
                   .Where(u => !u.Group.Columns.SequenceEqual(columnNames)).ToList();

                   return new
                   {
                       Pairs = m.ToList(),
                       Intersects = intersects.Where(c => c.Intersects.Count() > 2).ToArray()
                   };
               }).Where(c => c.Intersects.Any()).ToList();

        intersects.ForEach(c =>
        {
            foreach (var maliplusColumn in c.Pairs)
            {
                foreach (var grp in c.Intersects)
                {
                    var zanasColumn = grp.Group.Models
                    .FirstOrDefault(c => c.ColumnName?.Replace(" ", "")?
                    .Equals(maliplusColumn.MaliplusColumn?.ColumnName?.Replace(" ", ""),
                        StringComparison.InvariantCultureIgnoreCase) ?? false);

                    if (zanasColumn != null && maliplusColumn.ZanasColumn == null)
                    {
                        maliplusColumn.ZanasColumn = zanasColumn;
                    }
                }
            }
        });
    }


    public void ComparerByTableNameSameWithSomeSameColumnNames(ref List<ColumnPair> columnPairs, DbColumnModel[] zanasModels)
    {
        //Try to match columns where table names and columns are same
        columnPairs
                .Where(c => c.ZanasColumn == null)
                .GroupBy(c => c.MaliplusColumn?.TableName)
                .ToList()
                .ForEach(cc =>
                {
                    try
                    {
                        var zanasColumnTables =
                            zanasModels.Where(c => c.TableName?.Replace(" ", "")?.Equals(cc.Key?.Replace(" ", ""), StringComparison.InvariantCultureIgnoreCase) ?? false)
                            .ToList();

                        if (zanasColumnTables.Any() && zanasColumnTables.Count != cc.Count())
                        {
                            foreach (var maliplusColumn in cc)
                            {
                                var zanasColumn = zanasColumnTables
                                   .FirstOrDefault(c => c.ColumnName?.Replace(" ", "")?
                                   .Equals(maliplusColumn.MaliplusColumn?.ColumnName?.Replace(" ", ""),
                                       StringComparison.InvariantCultureIgnoreCase) ?? false);

                                if (zanasColumn != null && maliplusColumn.ZanasColumn == null)
                                {
                                    maliplusColumn.ZanasColumn = zanasColumn;
                                }
                            }
                        }
                    }
                    catch { }
                });
    }


    public void ComparerTableNameDifferentWithSameColumns(ref List<ColumnPair> columnPairs, DbColumnModel[] zanasModels)
    {
        //Try to match all columns match (same) and table names different- Renamed zanus to Maliplus table name

        var zanasGroupedColumns = zanasModels
            .GroupBy(c => c.TableName)
            .Select(c =>
             new {
                 Table = c.Key,
                 Models = c.ToArray(),
                 Columns = c.Select(v => v.ColumnName?.Replace(" ", "")).ToArray()
             });

        columnPairs
                .Where(c => c.ZanasColumn == null)
                .GroupBy(c => c.MaliplusColumn?.TableName)
                .ToList()
                .ForEach(cc =>
                {
                    try
                    {
                        var columnNames = cc.Select(c => c.MaliplusColumn?.ColumnName?.Replace(" ", "")).ToArray();
                        var groups = zanasGroupedColumns.Where(u => columnNames.SequenceEqual(u.Columns, StringComparer.InvariantCultureIgnoreCase) && !(u.Table?.Equals(cc.Key, StringComparison.CurrentCultureIgnoreCase) ?? false));

                        if (groups.Any())
                        {
                            foreach (var maliplusColumn in cc)
                            {
                                var zanasColumn = groups.First().Models
                                    .FirstOrDefault(c => c.ColumnName?.Replace(" ", "")?
                                    .Equals(maliplusColumn.MaliplusColumn?.ColumnName?.Replace(" ", ""),
                                        StringComparison.InvariantCultureIgnoreCase) ?? false);

                                if (zanasColumn != null && maliplusColumn.ZanasColumn == null)
                                {
                                    maliplusColumn.ZanasColumn = zanasColumn;
                                }
                            }
                        }
                    }
                    catch { }
                });
    }


    public void ComparerByTableNameAndSameColumns(ref List<ColumnPair> columnPairs, DbColumnModel[] zanasModels)
    {
        //Try to match columns where table names and columns are same
        columnPairs
                .Where(c => c.ZanasColumn == null)
                .GroupBy(c => c.MaliplusColumn?.TableName)
                .ToList()
                .ForEach(cc =>
                {
                    try
                    {
                        var zanasColumnTables =
                            zanasModels.Where(c => c.TableName?.Replace(" ", "")?.Equals(cc.Key?.Replace(" ", ""), StringComparison.InvariantCultureIgnoreCase) ?? false)
                            .ToList();

                        if (zanasColumnTables.Any() && zanasColumnTables.Count == cc.Count())
                        {
                            foreach (var maliplusColumn in cc)
                            {
                                var zanasColumn = zanasColumnTables
                                   .FirstOrDefault(c => c.ColumnName?.Replace(" ", "")?
                                   .Equals(maliplusColumn.MaliplusColumn?.ColumnName?.Replace(" ", ""),
                                       StringComparison.InvariantCultureIgnoreCase) ?? false);

                                if (zanasColumn != null && maliplusColumn.ZanasColumn == null)
                                {
                                    maliplusColumn.ZanasColumn = zanasColumn;
                                }
                            }
                        }
                    }
                    catch { }
                });
    }
}

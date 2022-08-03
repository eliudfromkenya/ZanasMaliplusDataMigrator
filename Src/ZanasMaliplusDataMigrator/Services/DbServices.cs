using ReactiveUI;
using System.Data;

namespace ZanasMaliplusDataMigrator.Services;

public class DbServices : ReactiveObject
{
    public async Task<List<DbColumnModel>> GetColumns(Func<IBM.Data.DB2.Core.DB2Connection> getCon, string schema)
    {
        try
        {
            using var con = getCon();
            using var cmd = con.CreateCommand();
            await con.OpenAsync();
            cmd.CommandText = $@"select colno as position,
       colname as column_name,
       TABNAME as table_name,
       typename as data_type,
       length,
       tabschema,
       scale,
       default,
       remarks as description,
       case when nulls='Y' then 1 else 0 end as nullable,
       case when identity ='Y' then 1 else 0 end as is_identity,
       case when generated ='' then 0 else 1 end as  is_computed,
       text as computed_formula
from syscat.columns
 where -- tabname = 'PROJECT' -- enter table name here
      tabschema = '{schema}'
order by tabname,colno;
";
            using var reader = await
    cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
            List<DbColumnModel> columns = new();
            while (reader.Read())
            {
                DbColumnModel col = new()
                {
                    ColumnName = reader.GetValue("column_name")?.ToString(),
                    ComputedFormular = reader.GetValue("computed_formula")?.ToString(),
                    DataType = reader.GetValue("data_type")?.ToString(),
                    Default = reader.GetValue("default")?.ToString(),
                    Description = reader.GetValue("description")?.ToString(),
                    IsComputed = bool.TryParse(reader.GetValue("is_computed")?.ToString() ?? "0", out bool bl) && bl,
                    IsIdentity = bool.TryParse(reader.GetValue("is_identity")?.ToString() ?? "0", out bl) && bl,
                    IsNullable = bool.TryParse(reader.GetValue("nullable")?.ToString() ?? "0", out bl) && bl,
                    Length = short.TryParse(reader.GetValue("length")?.ToString() ?? "0", out short mm) ? mm : (short)0,
                    Scale = short.TryParse(reader.GetValue("scale")?.ToString() ?? "0", out mm) ? mm : (short)0,
                    TableName = reader.GetValue("table_name")?.ToString(),
                    TableSchema = reader.GetValue("tabschema")?.ToString(),
                    Position = short.TryParse(reader.GetValue("position")?.ToString() ?? "0", out mm) ? mm : (short)0
                };
                columns.Add(col);
            }
            return columns;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
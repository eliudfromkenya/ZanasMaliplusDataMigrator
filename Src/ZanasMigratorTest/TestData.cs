namespace ZanasMigratorTest
{
    static internal class TestData
    {
        public static DbColumnModel[] ZanasColumns() => new DbColumnModel[]
        {
            new DbColumnModel{ColumnName="Column 8", DataType="VARCHAR" , IsIdentity=false, IsNullable=false, Default="", Length=20, Scale=3, Position=1, TableName = "Table 8"},
            new DbColumnModel{ColumnName="Column 9", DataType="VARCHAR" , IsIdentity=false, IsNullable=false, Default="", Length=20, Scale=3, Position=1, TableName = "Table 8"},
            new DbColumnModel{ColumnName="Column 10", DataType="VARCHAR" , IsIdentity=false, IsNullable=false, Default="", Length=20, Scale=3, Position=1, TableName = "Table 8"},
            new DbColumnModel{ColumnName="Column 11", DataType="VARCHAR" , IsIdentity=false, IsNullable=false, Default="", Length=20, Scale=3, Position=1, TableName = "Table 8"},
            new DbColumnModel{ColumnName="Column 2", DataType="VARCHAR" , IsIdentity=false, IsNullable=false, Default="", Length=20, Scale=3, Position=1, TableName = "Table 2"},
            new DbColumnModel{ColumnName="Column 3", DataType="VARCHAR" , IsIdentity=false, IsNullable=false, Default="", Length=20, Scale=3, Position=1, TableName = "Table 2"},
            new DbColumnModel{ColumnName="Column 5", DataType="VARCHAR" , IsIdentity=false, IsNullable=false, Default="", Length=20, Scale=3, Position=1, TableName = "Table 2"},
            new DbColumnModel{ColumnName="Column 6", DataType="VARCHAR" , IsIdentity=false, IsNullable=false, Default="", Length=20, Scale=3, Position=1, TableName = "Table 2"},
            new DbColumnModel{ColumnName="Column 7", DataType="VARCHAR" , IsIdentity=false, IsNullable=false, Default="", Length=20, Scale=3, Position=1, TableName = "Table 2"},
            new DbColumnModel{ColumnName="Column 23", DataType="VARCHAR" , IsIdentity=false, IsNullable=false, Default="", Length=20, Scale=3, Position=1, TableName = "Table 3"},
            new DbColumnModel{ColumnName="Column 24", DataType="VARCHAR" , IsIdentity=false, IsNullable=false, Default="", Length=20, Scale=3, Position=1, TableName = "Table 3"},
            new DbColumnModel{ColumnName="Column 25", DataType="VARCHAR" , IsIdentity=false, IsNullable=false, Default="", Length=20, Scale=3, Position=1, TableName = "Table 3"},
            new DbColumnModel{ColumnName="Column 26", DataType="VARCHAR" , IsIdentity=false, IsNullable=false, Default="", Length=20, Scale=3, Position=1, TableName = "Table 3"},
            new DbColumnModel{ColumnName="Column 27", DataType="VARCHAR" , IsIdentity=false, IsNullable=false, Default="", Length=20, Scale=3, Position=1, TableName = "Table 3"},
            new DbColumnModel{ColumnName="Column 28", DataType="VARCHAR" , IsIdentity=false, IsNullable=false, Default="", Length=20, Scale=3, Position=1, TableName = "Table 3"},
            new DbColumnModel{ColumnName="Column 13", DataType="VARCHAR" , IsIdentity=false, IsNullable=false, Default="", Length=20, Scale=3, Position=1, TableName = "Table 5"},
            new DbColumnModel{ColumnName="Column 14", DataType="VARCHAR" , IsIdentity=false, IsNullable=false, Default="", Length=20, Scale=3, Position=1, TableName = "Table 5"},
            new DbColumnModel{ColumnName="Column 15", DataType="VARCHAR" , IsIdentity=false, IsNullable=false, Default="", Length=20, Scale=3, Position=1, TableName = "Table 5"},
            new DbColumnModel{ColumnName="Column 16", DataType="VARCHAR" , IsIdentity=false, IsNullable=false, Default="", Length=20, Scale=3, Position=1, TableName = "Table 5"},
            new DbColumnModel{ColumnName="Column 17", DataType="VARCHAR" , IsIdentity=false, IsNullable=false, Default="", Length=20, Scale=3, Position=1, TableName = "Table 5"},
            new DbColumnModel{ColumnName="Column 18", DataType="VARCHAR" , IsIdentity=false, IsNullable=false, Default="", Length=20, Scale=3, Position=1, TableName = "Table 5"},
            new DbColumnModel{ColumnName="Column 20", DataType="VARCHAR" , IsIdentity=false, IsNullable=false, Default="", Length=20, Scale=3, Position=1, TableName = "Table 9"},
            new DbColumnModel{ColumnName="Column 21", DataType="VARCHAR" , IsIdentity=false, IsNullable=false, Default="", Length=20, Scale=3, Position=1, TableName = "Table 9"},
            new DbColumnModel{ColumnName="Column 22", DataType="VARCHAR" , IsIdentity=false, IsNullable=false, Default="", Length=20, Scale=3, Position=1, TableName = "Table 9"},
             new DbColumnModel{ColumnName="Column 23", DataType="VARCHAR" , IsIdentity=false, IsNullable=false, Default="", Length=20, Scale=3, Position=1, TableName = "Table 9"},
            new DbColumnModel{ColumnName="Column 24", DataType="VARCHAR" , IsIdentity=false, IsNullable=false, Default="", Length=20, Scale=3, Position=1, TableName = "Table 9"},
       };


        public static DbColumnModel[] MaliplusColumns() => new DbColumnModel[]
       {
            new DbColumnModel{ColumnName="Column 1", DataType="VARCHAR" , IsIdentity=false, IsNullable=true, Default="", Length=20, Scale=3, Position=1, TableName = "Table 1"},
            new DbColumnModel{ColumnName="Column 2", DataType="DOUBLE" , IsIdentity=true, IsNullable=true, Default="", Length=20, Scale=3, Position=1, TableName = "Table 1"},
            new DbColumnModel{ColumnName="Column 3", DataType="VARCHAR" , IsIdentity=false, IsNullable=false, Default="", Length=20, Scale=3, Position=1, TableName = "Table 1"},
            new DbColumnModel{ColumnName="Column 2", DataType="VARCHAR" , IsIdentity=false, IsNullable=true, Default="", Length=20, Scale=3, Position=1, TableName = "Table 2"},
            new DbColumnModel{ColumnName="Column 3", DataType="VARCHAR" , IsIdentity=true, IsNullable=false, Default="", Length=20, Scale=3, Position=1, TableName = "Table 2"},
            new DbColumnModel{ColumnName="Column 4", DataType="VARCHAR" , IsIdentity=false, IsNullable=true, Default="", Length=20, Scale=3, Position=1, TableName = "Table 2"},
            new DbColumnModel{ColumnName="Column 5", DataType="DECIMAL" , IsIdentity=false, IsNullable=false, Default="", Length=20, Scale=3, Position=1, TableName = "Table 2"},
            new DbColumnModel{ColumnName="Column 8", DataType="VARCHAR" , IsIdentity=false, IsNullable=true, Default="", Length=20, Scale=3, Position=1, TableName = "Table 3"},
            new DbColumnModel{ColumnName="Column 9", DataType="VARCHAR" , IsIdentity=false, IsNullable=false, Default="", Length=20, Scale=3, Position=1, TableName = "Table 3"},
            new DbColumnModel{ColumnName="Column 10", DataType="VARCHAR" , IsIdentity=true, IsNullable=true, Default="", Length=20, Scale=3, Position=1, TableName = "Table 3"},
            new DbColumnModel{ColumnName="Column 11", DataType="VARCHAR" , IsIdentity=false, IsNullable=true, Default="", Length=20, Scale=3, Position=1, TableName = "Table 3"},
            new DbColumnModel{ColumnName="Column 12", DataType="VARCHAR" , IsIdentity=false, IsNullable=false, Default="", Length=20, Scale=3, Position=1, TableName = "Table 4"},
            new DbColumnModel{ColumnName="Column 13", DataType="VARCHAR" , IsIdentity=true, IsNullable=false, Default="", Length=20, Scale=3, Position=1, TableName = "Table 4"},
            new DbColumnModel{ColumnName="Column 13", DataType="VARCHAR" , IsIdentity=false, IsNullable=false, Default="", Length=20, Scale=3, Position=1, TableName = "Table 5"},
            new DbColumnModel{ColumnName="Column 14", DataType="VARCHAR" , IsIdentity=false, IsNullable=false, Default="", Length=20, Scale=3, Position=1, TableName = "Table 5"},
            new DbColumnModel{ColumnName="Column 15", DataType="VARCHAR" , IsIdentity=true, IsNullable=false, Default="", Length=20, Scale=3, Position=1, TableName = "Table 5"},
            new DbColumnModel{ColumnName="Column 16", DataType="VARCHAR" , IsIdentity=false, IsNullable=false, Default="", Length=20, Scale=3, Position=1, TableName = "Table 5"},
            new DbColumnModel{ColumnName="Column 17", DataType="VARCHAR" , IsIdentity=false, IsNullable=true, Default="", Length=20, Scale=3, Position=1, TableName = "Table 5"},
            new DbColumnModel{ColumnName="Column 18", DataType="VARCHAR" , IsIdentity=false, IsNullable=false, Default="", Length=20, Scale=3, Position=1, TableName = "Table 5"},
            new DbColumnModel{ColumnName="Column 19", DataType="VARCHAR" , IsIdentity=false, IsNullable=true, Default="", Length=20, Scale=3, Position=1, TableName = "Table 6"},
            new DbColumnModel{ColumnName="Column 20", DataType="VARCHAR" , IsIdentity=false, IsNullable=false, Default="", Length=20, Scale=3, Position=1, TableName = "Table 6"},
            new DbColumnModel{ColumnName="Column 21", DataType="VARCHAR" , IsIdentity=true, IsNullable=false, Default="", Length=20, Scale=3, Position=1, TableName = "Table 6"},
            new DbColumnModel{ColumnName="Column 22", DataType="VARCHAR" , IsIdentity=false, IsNullable=true, Default="", Length=20, Scale=3, Position=1, TableName = "Table 6"},
            new DbColumnModel{ColumnName="Column 24", DataType="VARCHAR" , IsIdentity=false, IsNullable=false, Default="", Length=20, Scale=3, Position=1, TableName = "Table 7"},
            new DbColumnModel{ColumnName="Column 25", DataType="VARCHAR" , IsIdentity=true, IsNullable=false, Default="", Length=20, Scale=3, Position=1, TableName = "Table 7"},
            new DbColumnModel{ColumnName="Column 26", DataType="VARCHAR" , IsIdentity=false, IsNullable=true, Default="", Length=20, Scale=3, Position=1, TableName = "Table 7"},
            new DbColumnModel{ColumnName="Column 27", DataType="VARCHAR" , IsIdentity=false, IsNullable=false, Default="", Length=20, Scale=3, Position=1, TableName = "Table 7"}
        };
    }
}
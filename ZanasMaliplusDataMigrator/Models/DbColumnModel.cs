using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZanasMaliplusDataMigrator.Models
{
    public class DbColumnModel
    {
        private string? columnName;
        private string? tableName;
        private string? dataType;
        private short length;
        private string? tableSchema;
        private short scale, position;
        private string? @default;
        private string? description;
        private bool isNullable;
        private bool isIdentity;
        private bool isComputed;
        private string? computedFormular;

        public string? ColumnName { get => columnName; set => columnName = value; }
        public string? TableName { get => tableName; set => tableName = value; }
        public string? DataType { get => dataType; set => dataType = value; }
        public short Length { get => length; set => length = value; }
        public string? TableSchema { get => tableSchema; set => tableSchema = value; }
        public short Scale { get => scale; set => scale = value; }
        public short Position { get => position; set => position = value; }
        public string? Default { get => @default; set => @default = value; }
        public string? Description { get => description; set => description = value; }
        public bool IsNullable { get => isNullable; set => isNullable = value; }
        public bool IsIdentity { get => isIdentity; set => isIdentity = value; }
        public bool IsComputed { get => isComputed; set => isComputed = value; }
        public string? ComputedFormular { get => computedFormular; set => computedFormular = value; }
    }
}

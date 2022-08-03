namespace ZanasMaliplusDataMigrator.Services
{
    public class CreateMySQLService
    {
        public string CreateMySQLDatabase(DbColumnModel[] zanasColumns, DbColumnModel[] maliplusColumns, DBRelationship[] relationships)
        {
            var (pairs, unMatchedZanasColumns) = new MatchingService().GenerateMatchColumns(zanasColumns, maliplusColumns);
            var grps = pairs
                .GroupBy(x => x?.MaliplusColumn?.TableName)
                .Select(c => new
                {
                    Table = c.Key,
                    Columns = c.ToList()
                }).ToList();

            var sb = new StringBuilder(@"/*
 KFA TABLE MAKER

 Source Server         : MySQL
 Source Server Type    : MySQL
 Source Server Version : 80026
 Source Host           : localhost:3306
 Source Schema         : maliplus

 Target Server Type    : MySQL
 Target Server Version : 80026
 File Encoding         : 65001

 Date: 29/07/2022 21:32:29
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;
");

            grps.ForEach(v =>
            {
                sb.AppendLine($@"
-- ----------------------------
-- Table structure for {v.Table}
-- ----------------------------
DROP TABLE IF EXISTS `{v.Table}`;
CREATE TABLE `{v.Table}`  (
");
                v.Columns.ForEach(col =>
                {
                    var type = col?.MaliplusColumn?.DataType;
                    if (col?.MaliplusColumn?.Length > 0)
                    {
                        if (col?.MaliplusColumn?.Scale > 0)
                            type = $"{type}({col?.MaliplusColumn?.Length},{col?.MaliplusColumn?.Scale})";
                        else type = $"{type}({col?.MaliplusColumn?.Length})";
                    }
                    sb.AppendLine($"  `{col?.MaliplusColumn?.ColumnName}` {type} {(col?.MaliplusColumn?.IsNullable ?? false ? "NOT" : "")} NULL,");
                });
                sb.AppendLine($"  PRIMARY KEY (`{v.Columns.FirstOrDefault(m => m?.MaliplusColumn?.IsIdentity ?? false)?.MaliplusColumn?.ColumnName}`) USING BTREE) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;");
                sb.AppendLine().AppendLine().AppendLine().AppendLine();
            });
            return sb.ToString();
        }
    }
}
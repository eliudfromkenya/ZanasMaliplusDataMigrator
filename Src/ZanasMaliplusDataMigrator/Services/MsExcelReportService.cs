using OfficeOpenXml;
using System.Drawing;
using System.IO;

namespace ZanasMaliplusDataMigrator.Services;

public class MsExcelReportService
{
    private int currentRow = 3;

    public string GeneratePreMigrationReport( DbColumnModel[] maliplusColumns,DbColumnModel[] zanasColumns, DBRelationship[] maliplusRelationships, DBRelationship[] zansRelationships)
    {
        var (pairs, unMatchedZanasColumns) = new MatchingService().GenerateMatchColumns(zanasColumns, maliplusColumns);
        var grps = pairs
            .GroupBy(x => x?.MaliplusColumn?.TableName)
            .Select(c => new
            {
                Table = c.Key,
                Columns = c.ToList()
            }).ToList();

        using var excelPackage = new ExcelPackage();
        excelPackage.Workbook.Properties.Author = "KFA / Primesoft Implimentation Team";
        excelPackage.Workbook.Properties.Title = "Zanas to Maliplus Migration Report";
        excelPackage.Workbook.Properties.Subject = "Automated Data Migration";
        excelPackage.Workbook.Properties.Created = DateTime.Now;
        excelPackage.Workbook.Properties.Company = "KFA LTD";

        AddMaliplusColumns(pairs, excelPackage);
        AddZanasColumns(pairs, unMatchedZanasColumns, excelPackage);
        AddMatchedColumns(pairs, unMatchedZanasColumns, excelPackage);

        var file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Zanas Maliplus Migration Report.xlsx");
        //excelPackage.SaveAs(new FileInfo(file), "Zanas2022");
        excelPackage.SaveAs(new FileInfo(file));
        return file;
    }

    private void AddMatchedColumns(MatchingService.ColumnPair?[] pairs, DbColumnModel?[] unMatchedZanasColumns, ExcelPackage excelPackage)
    {
        var zCols = unMatchedZanasColumns.ToList();

        currentRow = 3;
        var sheet = excelPackage.Workbook.Worksheets.Add("Matched Columns Information");
        var grps = pairs
          .OrderBy(x => x?.MaliplusColumn?.TableName)
          .GroupBy(x => x?.MaliplusColumn?.TableName)
          .Select(c => new
          {
              Table = c.Key,
              Columns = c.ToList()
          }).ToList();
        sheet.Cells["A1:G1"].Merge = true;
        sheet.Cells["A1"].Value = "Matched Columns Information";
        sheet.Row(1).Height = 20;
        sheet.Row(1).Style.Font.Size = 20;
        sheet.Row(1).Style.Font.Color.SetColor(Color.Purple);
        sheet.Row(1).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
        sheet.Row(1).Style.Font.Bold = true;

        var index = 1;
        grps.ForEach(x =>
        {
            sheet.Cells[currentRow, 1, currentRow, 7].Merge = true;
            sheet.Row(currentRow).Height = 20;
            sheet.Row(currentRow).Style.Font.Size = 16;
            sheet.Row(currentRow).Style.Font.Color.SetColor(Color.DarkSlateGray);
            sheet.Row(currentRow).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            sheet.Row(currentRow).Style.Font.Bold = true;
            sheet.Cells[currentRow++, 1].Value = $"Table {index++}: {x.Table}";

            sheet.Row(currentRow).Height = 20;
            sheet.Row(currentRow).Style.Font.Size = 12;
            sheet.Row(currentRow).Style.Font.Color.SetColor(Color.DarkGray);
            sheet.Row(currentRow).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            sheet.Row(currentRow).Style.Font.Bold = true;
            // sheet.Row(currentRow).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.LightTrellis;

            var range = sheet.Cells[currentRow, 1, currentRow, 3];
            range.Style.Font.Color.SetColor(Color.RebeccaPurple);
            range.Style.Font.UnderLine = true;
            range.Style.Font.Size = 14;
            range.Merge = true;
            range.Value = "Zanas Table";

            range = sheet.Cells[currentRow, 4, currentRow, 7];
            range.Style.Font.Color.SetColor(Color.RebeccaPurple);
            range.Style.Font.UnderLine = true;
            range.Style.Font.Size = 14;
            range.Merge = true;
            range.Value = "Corresponding Maliplus Tables";
            currentRow++;

            sheet.Cells[currentRow, 1].Value = "Column Name";
            sheet.Cells[currentRow, 2].Value = "Is Primary Key";
            sheet.Cells[currentRow, 3].Value = "Data Type";

            sheet.Cells[currentRow, 4].Value = "Maliplus Table";
            sheet.Cells[currentRow, 5].Value = "Maliplus Column";
            sheet.Cells[currentRow, 6].Value = "Is Primary Key";
            sheet.Cells[currentRow, 7].Value = "Data Type";

            sheet.Cells[currentRow, 4, currentRow, 7].Style.Font.Color.SetColor(Color.RebeccaPurple);
            sheet.Cells[currentRow, 1, currentRow, 7].Style.Font.UnderLine = true;
            currentRow++;

            var tables = x.Columns.Select(v => v?.ZanasColumn?.TableName)
                .Where(n => !string.IsNullOrWhiteSpace(n))
                .Distinct()
                .ToArray();

            var zanasCols = zCols
              .Where(v => tables.Any(m => m?.Replace(" ", "")
              .Equals(v?.TableName?.Replace(" ", ""), StringComparison.CurrentCultureIgnoreCase) ?? false))
              .OrderBy(v => v?.TableName)
              .ThenBy(v => v?.ColumnName)
              .ToList();

            range = sheet.Cells[currentRow, 1, currentRow + x.Columns.Count - 1, 3];
            range.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            range = sheet.Cells[currentRow, 4, currentRow + zanasCols.Count + x.Columns.Count - 1, 7];
            range.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.DashDotDot);

            x.Columns.OrderBy(c => c?.ZanasColumn?.TableName)
            .ThenBy(c => c?.ZanasColumn?.ColumnName)
            .ToList()
            .ForEach(col =>
            {
                if ((col?.MaliplusColumn?.IsIdentity ?? false))
                    sheet.Cells[currentRow, 1, currentRow, 7].Style.Fill.SetBackground(Color.Lavender);
                sheet.Cells[currentRow, 1].Value = col?.MaliplusColumn?.ColumnName;
                sheet.Cells[currentRow, 2].Value = (col?.MaliplusColumn?.IsIdentity ?? false) ? "Yes" : "No";
                sheet.Cells[currentRow, 3].Value = col?.MaliplusColumn?.DataType;

                if (col?.ZanasColumn != null)
                {
                    sheet.Cells[currentRow, 4].Value = col?.ZanasColumn?.TableName;
                    sheet.Cells[currentRow, 5].Value = col?.ZanasColumn?.ColumnName;
                    sheet.Cells[currentRow, 6].Value = (col?.ZanasColumn?.IsIdentity ?? false) ? "Yes" : "No";
                    sheet.Cells[currentRow, 7].Value = col?.ZanasColumn?.DataType;
                    sheet.Cells[currentRow, 4].Style.Font.Bold = true;
                }
                currentRow++;
            });

            zanasCols.ForEach(col =>
            {
                sheet.Cells[currentRow, 4].Value = col?.TableName;
                sheet.Cells[currentRow, 5].Value = col?.ColumnName;
                sheet.Cells[currentRow, 6].Value = (col?.IsIdentity ?? false) ? "Yes" : "No";
                sheet.Cells[currentRow, 7].Value = col?.DataType;
                sheet.Cells[currentRow, 4].Style.Font.Bold = true;
                currentRow++;
                zCols.Remove(col);
            });
            currentRow += 2;
        });

        var zanasRemainingCols = zCols
              .OrderBy(v => v?.TableName)
              .ThenBy(v => v?.ColumnName)
              .ToList();

        currentRow += 2;

        zanasRemainingCols
            .GroupBy(m => m.TableName)
            .ToList()
            .ForEach(nb =>
            {
                currentRow += 2;
                foreach (var col in nb)
                {
                    sheet.Cells[currentRow, 4].Value = col?.TableName;
                    sheet.Cells[currentRow, 5].Value = col?.ColumnName;
                    sheet.Cells[currentRow, 6].Value = (col?.IsIdentity ?? false) ? "Yes" : "No";
                    sheet.Cells[currentRow, 7].Value = col?.DataType;
                    sheet.Cells[currentRow, 4].Style.Font.Bold = true;
                    currentRow++;
                }
            });
    }

    private void AddZanasColumns(MatchingService.ColumnPair?[] pairs, DbColumnModel?[] unMatchedZanasColumns, ExcelPackage excelPackage)
    {
        currentRow = 3;
        var sheet = excelPackage.Workbook.Worksheets.Add("Maliplus Table Information");
        var allZanasCols = pairs.Where(x => x?.ZanasColumn != null)
            .Select(x => x?.ZanasColumn)
            .Concat(unMatchedZanasColumns)
            .OrderBy(c => c?.TableName)
            .ToArray();

        var grps = allZanasCols
          .GroupBy(x => x?.TableName)
          .Select(c => new
          {
              Table = c.Key,
              Columns = c.ToList()
          }).ToList();
        sheet.Cells["A1:F1"].Merge = true;
        sheet.Cells["A1"].Value = "Maliplus Table Information";
        sheet.Row(1).Height = 20;
        sheet.Row(1).Style.Font.Size = 20;
        sheet.Row(1).Style.Font.Color.SetColor(Color.Purple);
        sheet.Row(1).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
        sheet.Row(1).Style.Font.Bold = true;

        var index = 1;
        grps.ForEach(x =>
        {
            sheet.Cells[currentRow, 1, currentRow, 6].Merge = true;
            sheet.Row(currentRow).Height = 20;
            sheet.Row(currentRow).Style.Font.Size = 16;
            sheet.Row(currentRow).Style.Font.Color.SetColor(Color.DarkSlateGray);
            sheet.Row(currentRow).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            sheet.Row(currentRow).Style.Font.Bold = true;
            sheet.Cells[currentRow++, 1].Value = $"Table {index++}: {x.Table}";

            sheet.Row(currentRow).Height = 20;
            sheet.Row(currentRow).Style.Font.Size = 12;
            sheet.Row(currentRow).Style.Font.Color.SetColor(Color.DarkGray);
            sheet.Row(currentRow).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            sheet.Row(currentRow).Style.Font.Bold = true;
            // sheet.Row(currentRow).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.LightTrellis;

            sheet.Cells[currentRow, 1].Value = "Column Name";
            sheet.Cells[currentRow, 2].Value = "Is Primary Key";
            sheet.Cells[currentRow, 3].Value = "Can Be Null";
            sheet.Cells[currentRow, 4].Value = "Data Type";
            sheet.Cells[currentRow, 5].Value = "Length";
            sheet.Cells[currentRow, 6].Value = "Scale";

            sheet.Cells[currentRow, 1, currentRow, 6].Style.Font.UnderLine = true;
            currentRow++;

            var range = sheet.Cells[currentRow, 1, currentRow + x.Columns.Count - 1, 6];
            range.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            x.Columns.ForEach(col =>
            {
                if ((col?.IsIdentity ?? false))
                    sheet.Cells[currentRow, 1, currentRow, 6].Style.Fill.SetBackground(Color.Lavender);
                sheet.Cells[currentRow, 1].Value = col?.ColumnName;
                sheet.Cells[currentRow, 2].Value = (col?.IsIdentity ?? false) ? "Yes" : "No";
                sheet.Cells[currentRow, 3].Value = (col?.IsNullable ?? false) ? "Yes" : "No";
                sheet.Cells[currentRow, 4].Value = col?.DataType;
                sheet.Cells[currentRow, 5].Value = col?.Length;
                sheet.Cells[currentRow, 6].Value = col?.Scale;
                currentRow++;
            });
            currentRow += 2;
        });
    }

    private void AddMaliplusColumns(MatchingService.ColumnPair?[] pairs, ExcelPackage excelPackage)
    {
        currentRow = 3;
        var sheet = excelPackage.Workbook.Worksheets.Add("Zanas Table Information");
        var grps = pairs
          .OrderBy(x => x?.MaliplusColumn?.TableName)
          .GroupBy(x => x?.MaliplusColumn?.TableName)
          .Select(c => new
          {
              Table = c.Key,
              Columns = c.ToList()
          }).ToList();
        sheet.Cells["A1:F1"].Merge = true;
        sheet.Cells["A1"].Value = "Zanas Table Information";
        sheet.Row(1).Height = 20;
        sheet.Row(1).Style.Font.Size = 20;
        sheet.Row(1).Style.Font.Color.SetColor(Color.Purple);
        sheet.Row(1).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
        sheet.Row(1).Style.Font.Bold = true;

        var index = 1;
        grps.ForEach(x =>
        {
            sheet.Cells[currentRow, 1, currentRow, 6].Merge = true;
            sheet.Row(currentRow).Height = 20;
            sheet.Row(currentRow).Style.Font.Size = 16;
            sheet.Row(currentRow).Style.Font.Color.SetColor(Color.DarkSlateGray);
            sheet.Row(currentRow).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            sheet.Row(currentRow).Style.Font.Bold = true;
            sheet.Cells[currentRow++, 1].Value = $"Table {index++}: {x.Table}";

            sheet.Row(currentRow).Height = 20;
            sheet.Row(currentRow).Style.Font.Size = 12;
            sheet.Row(currentRow).Style.Font.Color.SetColor(Color.DarkGray);
            sheet.Row(currentRow).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            sheet.Row(currentRow).Style.Font.Bold = true;
            // sheet.Row(currentRow).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.LightTrellis;

            sheet.Cells[currentRow, 1].Value = "Column Name";
            sheet.Cells[currentRow, 2].Value = "Is Primary Key";
            sheet.Cells[currentRow, 3].Value = "Can Be Null";
            sheet.Cells[currentRow, 4].Value = "Data Type";
            sheet.Cells[currentRow, 5].Value = "Length";
            sheet.Cells[currentRow, 6].Value = "Scale";

            sheet.Cells[currentRow, 1, currentRow, 6].Style.Font.UnderLine = true;
            currentRow++;

            var range = sheet.Cells[currentRow, 1, currentRow + x.Columns.Count - 1, 6];
            range.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            x.Columns.ForEach(col =>
            {
                if ((col?.MaliplusColumn?.IsIdentity ?? false))
                    sheet.Cells[currentRow, 1, currentRow, 6].Style.Fill.SetBackground(Color.Lavender);
                sheet.Cells[currentRow, 1].Value = col?.MaliplusColumn?.ColumnName;
                sheet.Cells[currentRow, 2].Value = (col?.MaliplusColumn?.IsIdentity ?? false) ? "Yes" : "No";
                sheet.Cells[currentRow, 3].Value = (col?.MaliplusColumn?.IsNullable ?? false) ? "Yes" : "No";
                sheet.Cells[currentRow, 4].Value = col?.MaliplusColumn?.DataType;
                sheet.Cells[currentRow, 5].Value = col?.MaliplusColumn?.Length;
                sheet.Cells[currentRow, 6].Value = col?.MaliplusColumn?.Scale;
                currentRow++;
            });
            currentRow += 2;
        });
    }
}
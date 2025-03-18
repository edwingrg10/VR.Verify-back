using VF.Verify.Domain.DTOs;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using VF.Verify.Domain.Interfaces.Services;

namespace Infrastructure.Services
{
    public class ExcelParserService : IExcelParserService
    {
        public async Task<List<ExcelRuleData>> ParseExcel(IFormFile file)
        {
            var excelData = new List<ExcelRuleData>();

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);

                using (var package = new ExcelPackage(stream))
                {
                    foreach (var worksheet in package.Workbook.Worksheets)
                    {
                        var sheetNameParts = worksheet.Name.Split(new[] { '-' }, 2);
                        if (sheetNameParts.Length != 2) continue;

                        var entityName = sheetNameParts[0].Trim();
                        var sourceName = sheetNameParts[1].Trim();

                        for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                        {
                            var ruleData = new ExcelRuleData
                            {
                                EntityName = entityName,
                                SourceName = sourceName,
                                RuleName = GetCellValue(worksheet, row, 1),
                                Operator = GetCellValue(worksheet, row, 2),
                                Value = GetCellValue(worksheet, row, 3),
                                Result = GetCellValue(worksheet, row, 4)
                            };

                            if (!string.IsNullOrEmpty(ruleData.RuleName))
                            {
                                excelData.Add(ruleData);
                            }
                        }
                    }
                }
            }
            return excelData;
        }

        private string GetCellValue(ExcelWorksheet worksheet, int row, int col)
        {
            return worksheet.Cells[row, col]?.Text?.Trim() ?? string.Empty;
        }
    }
}
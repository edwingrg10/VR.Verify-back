using OfficeOpenXml;
using VF.Verify.Domain.Interfaces.Repository.Repositories;
using VF.Verify.Domain.Interfaces.UseCases;

public class ExcelUseCase : IExcelUseCase
{
    private readonly IExcelRepository _excelRepository;

    public ExcelUseCase(IExcelRepository excelRepository)
    {
        _excelRepository = excelRepository;
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
    }

    public async Task<MemoryStream> GenerateExcelTemplateAsync(int companyId, int countryId)
    {
        var sheetNames = await _excelRepository.GetSheetNamesAsync(companyId, countryId);

        if (sheetNames.Count == 0)
        {
            return null;
        }

        var stream = new MemoryStream();
        using (var package = new ExcelPackage(stream))
        {
            foreach (var sheetName in sheetNames)
            {
                var worksheet = package.Workbook.Worksheets.Add(TruncateSheetName(sheetName));

                worksheet.Cells[1, 1].Value = "REGLA";
                worksheet.Cells[1, 2].Value = "OPERADOR";
                worksheet.Cells[1, 3].Value = "VALOR";
                worksheet.Cells[1, 4].Value = "RESULTADO";

                using (var range = worksheet.Cells[1, 1, 1, 4])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                }

                worksheet.Cells.AutoFitColumns();
            }

            package.Save();
        }

        stream.Position = 0;
        return stream;
    }

    private string TruncateSheetName(string name)
    {
        return name.Length <= 31 ? name : name[..31];
    }
}

using System.Diagnostics;
using VF.Verify.Domain.DTOs;
using VF.Verify.Domain.Interfaces.Repository.Repositories;
using VF.Verify.Domain.Interfaces.Services;

namespace VF.Verify.Infrastructure.Repository.Repositories
{
    public class ExcelRepository(IExecuteStoredProcedureService executeStoredProcedureService) : IExcelRepository
    {
        private readonly IExecuteStoredProcedureService _executeStoredProcedureService = executeStoredProcedureService;

        public async Task<List<string>> GetSheetNamesAsync(int companyId, int countryId)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var obj = new
            {
                @CompanyId = companyId,
                @CountryId = countryId
            };

            var response = await _executeStoredProcedureService.ExecuteDataStoredProcedure(false, "dbo.GetExcelSheetNames", obj, MapToListHelper.MapToList<GetExcelSheetNamesDto>);

            stopwatch.Stop();

            if (!response.IsSuccess || response.Data == null)
            {
                return new List<string>();
            }

            try
            {
                var dataList = response.Data as List<GetExcelSheetNamesDto>;

                if (dataList != null)
                {
                    return dataList
                        .Where(d => !string.IsNullOrEmpty(d.SheetName))
                        .Select(d => d.SheetName)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al convertir los datos: {ex.Message}");
            }

            return new List<string>();
        }
    }



}

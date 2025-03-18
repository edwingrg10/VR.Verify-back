using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using OfficeOpenXml;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics;
using VF.Verify.Domain.DTOs;
using VF.Verify.Domain.Interfaces.Repository.Repositories;
using VF.Verify.Domain.Interfaces.Services;

namespace VF.Verify.Infrastructure.Repository.Repositories
{
    public class RuleRepository(IExecuteStoredProcedureService executeStoredProcedureService, IExcelParserService excelParserService) : IRuleRepository
    {
        private readonly IExecuteStoredProcedureService _executeStoredProcedureService = executeStoredProcedureService;
        private readonly IExcelParserService _excelParserService = excelParserService;

        public async Task<ResponseDTO> ProcessRules(int companyCountryId, IFormFile excelFile)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var excelData = await _excelParserService.ParseExcel(excelFile);

            if (!excelData.Any())
                throw new InvalidDataException("El archivo Excel no contiene datos válidos");

            var dataTable = CreateExcelDataTable(excelData);

            if (dataTable == null || dataTable.Rows.Count == 0)
                throw new InvalidDataException("El archivo Excel no contiene datos válidos");

            var obj = new
            {
                companyCountryId = companyCountryId,
                @excelData = dataTable
            };

            var result = await _executeStoredProcedureService.ExecuteStoredProcedure(false, "dbo.usp_ProcessRules", obj);
            stopwatch.Stop();
            return result;
        }

        private DataTable CreateExcelDataTable(IEnumerable<ExcelRuleData> excelData)
        {
            var dataTable = new DataTable();

            dataTable.Columns.Add("EntityName", typeof(string));
            dataTable.Columns.Add("SourceName", typeof(string));
            dataTable.Columns.Add("RuleName", typeof(string));
            dataTable.Columns.Add("Operator", typeof(string));
            dataTable.Columns.Add("Value", typeof(string));
            dataTable.Columns.Add("Result", typeof(string));

            foreach (var item in excelData)
            {
                dataTable.Rows.Add(
                    item.EntityName,
                    item.SourceName,
                    item.RuleName,
                    item.Operator,
                    item.Value,
                    item.Result);
            }

            return dataTable;
        }

    }

}

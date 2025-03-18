namespace VF.Verify.Domain.Interfaces.UseCases
{
    public interface IExcelUseCase
    {
        Task<MemoryStream> GenerateExcelTemplateAsync(int companyId, int countryId);
    }
}

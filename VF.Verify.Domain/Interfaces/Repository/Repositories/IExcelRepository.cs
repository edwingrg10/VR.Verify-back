namespace VF.Verify.Domain.Interfaces.Repository.Repositories
{
    public interface IExcelRepository
    {
        Task<List<string>> GetSheetNamesAsync(int companyId, int countryId);
    }
}

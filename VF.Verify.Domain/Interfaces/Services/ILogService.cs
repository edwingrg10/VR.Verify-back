namespace VF.Verify.Domain.Interfaces.Services
{
    public interface ILogService
    {
        Task SaveLogsMessagesAsync(string messages);
        void SaveLogsMessages(string messages);
        void SaveInfoMessages(string messages);

    }
}

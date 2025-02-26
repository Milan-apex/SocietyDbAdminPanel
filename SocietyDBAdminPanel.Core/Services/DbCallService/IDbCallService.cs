using SocietyDBAdminPanel.Core.Models;

namespace SocietyDBAdminPanel.Core.Services.DbCallService
{
    public interface IDbCallService
    {
        Task<ConfigurationModel> GetConfigurationAsync();
    }
}

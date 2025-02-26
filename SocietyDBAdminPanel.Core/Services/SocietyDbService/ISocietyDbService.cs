using SocietyDBAdminPanel.Core.Models;

namespace SocietyDBAdminPanel.Core.Services.SocietyDbService
{
    public interface ISocietyDbService
    {
        Task<List<SocDBMstModel>> GetAllConnectionStringsAsync();
        Task<SocDBMstModel> GetSocDbByIdAsync(int id);
        Task<int> AddOrUpdateSocDbMst(AddUpdateSocDBMstModel model);
    }
}

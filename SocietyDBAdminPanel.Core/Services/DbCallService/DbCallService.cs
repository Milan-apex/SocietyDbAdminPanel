using Dapper;
using SocietyDBAdminPanel.Core.Models;
using SocietyDBAdminPanel.Db.Dapper;
using SocietyDBAdminPanel.Db.StoreProcedures;
using System.Data;

namespace SocietyDBAdminPanel.Core.Services.DbCallService
{
    public class DbCallService : IDbCallService
    {
        private readonly IDapper _dapper;
        public DbCallService(IDapper dapper)
        {
            _dapper = dapper;
        }
        public async Task<ConfigurationModel> GetConfigurationAsync()
        {
            try
            {
                var dbparams = new DynamicParameters();
                var res = await _dapper.GetAsync<ConfigurationModel>(StoreProcedure.AddNewConnectionString, null, commandType: CommandType.StoredProcedure);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

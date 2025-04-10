
using Dapper;
using SocietyDBAdminPanel.Core.Models;
using SocietyDBAdminPanel.Db.Dapper;
using System.Data;

namespace SocietyDBAdminPanel.Core.Services.SocietyDbService
{
    public class SocietyDbService : ISocietyDbService
    {
        private readonly IDapper _dapper;
        public SocietyDbService(IDapper dapper)
        {
            _dapper = dapper;
        }
        public async Task<List<SocDBMstModel>> GetAllConnectionStringsAsync()
        {
            try
            {
                var result = await _dapper.GetAllAsync<SocDBMstModel>("GetSocDbMst", null, commandType: System.Data.CommandType.StoredProcedure);

                return result.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<SocDBMstModel> GetSocDbByIdAsync(int id)
        {
            try
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("Id", id, DbType.Int32);
                var result = await _dapper.GetAsync<SocDBMstModel>("GetSocDbMstById", dynamicParameters, commandType: System.Data.CommandType.StoredProcedure);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<SocDBMstModel> GetSocInfoByIdAsync(string SocCode)
        {
            try
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("SocietyCd", SocCode, DbType.String);
                var result = await _dapper.GetAsync<SocDBMstModel>("GetSocDBMstBySocietyCd", dynamicParameters, commandType: System.Data.CommandType.StoredProcedure);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> AddOrUpdateSocDbMst(AddUpdateSocDBMstModel model)
        {
            try
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@IntID", model.IntID, DbType.Int32);
                dynamicParameters.Add("@ServerName", model.ServerName, DbType.String);
                dynamicParameters.Add("@DBName", model.DBName, DbType.String);
                dynamicParameters.Add("@UserID", model.UserID, DbType.String);
                dynamicParameters.Add("@PassWord", model.PassWord, DbType.String);
                dynamicParameters.Add("@Year", model.Year, DbType.String);
                dynamicParameters.Add("@SocietyCd", model.SocietyCd, DbType.String);
                dynamicParameters.Add("@SocName", model.SocName, DbType.String);
                dynamicParameters.Add("@SocLogo", model.SocLogo, DbType.String);
                dynamicParameters.Add("@SocAddress", model.SocAddress, DbType.String);
                dynamicParameters.Add("@@SocCity", model.SocCity, DbType.String);
                dynamicParameters.Add("@SocAddress", model.SocAddress, DbType.String);
                dynamicParameters.Add("@@SocWebSite", model.SocWebSite, DbType.String);
                var result = await _dapper.AddUpdateAsync("AddOrUpdateSocDbMst", dynamicParameters, commandType: System.Data.CommandType.StoredProcedure);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteRecordById(int ID)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@IntID", ID, DbType.Int32);

            return await _dapper.DeleteAsync("DeleteSocDbMst", parameters, CommandType.StoredProcedure);
        }
    }
}
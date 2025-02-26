using Dapper;
using System.Data;
using System.Data.Common;

namespace SocietyDBAdminPanel.Db.Dapper
{
    public interface IDapper : IDisposable
    {
        DbConnection GetDbconnection();
        T Get<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        List<T> GetAll<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        Task<IEnumerable<T>> GetAllAsync<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        Task QueryMultipleAsync<T>(string sp, DynamicParameters parms, T resultObject, CommandType commandType = CommandType.StoredProcedure);
        int Execute(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        T Insert<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        T Update<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);

        int Delete(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure); //D

        Task<T> GetAsync<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);

        Task<T> UpdateAsync<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);

        Task<T> InsertAsync<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        Task<T> DeleteAsync<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        Task<IEnumerable<T>> QueryAsync<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        Task<T> InsertAsync<T>(string sp, DynamicParameters parms, IDbConnection dbConnection, IDbTransaction dbTransaction, CommandType commandType = CommandType.StoredProcedure);

        Task<IEnumerable<T>> GetAllAsync<T>(string sp, DynamicParameters parms, IDbConnection dbConnection, IDbTransaction dbTransaction, CommandType commandType = CommandType.StoredProcedure);
        //Task<DapperPlusActionSetForAsync<T>> BulkInsertAsync<T>(IEnumerable<T> items);
        //Task<DapperPlusActionSetForAsync<T>> BulkUpdateAsync<T>(IEnumerable<T> items);
        Task QueryMultipleAsync<T>(string sp, DynamicParameters parms, T resultObject, IDbConnection con, IDbTransaction trans, CommandType commandType = CommandType.StoredProcedure);
        Task<DataSet> GetDataSetAsync(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        Task<DataTable> GetDataTableAsync(string sp, DynamicParameters parms, CommandType commandType = CommandType.Text);
        Task<T> GetAsync<T>(string sp, DynamicParameters parms, IDbConnection dbConnection, IDbTransaction dbTransaction, CommandType commandType = CommandType.Text);
        DataSet GetDataSet(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        DataSet GetDataSetByConnectionString(string Connectionstring, string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        Task<IDisposable> QueryMultipleAsync(string proc_GetTransForMultipleGenerateEdit, DynamicParameters dbparams, CommandType commandType);
    }
}

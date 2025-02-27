using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using System.Collections;

namespace SocietyDBAdminPanel.Db.Dapper
{
    public class Dapperr : IDapper
    {
        private readonly IConfiguration _config;
        private string Connectionstring = "DefaultConnection";

        public Dapperr(IConfiguration config)
        {
            _config = config;
        }

        public void Dispose()
        {

        }

        public int Execute(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            throw new NotImplementedException();
        }

        public T Get<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.Text)
        {
            using IDbConnection db = new SqlConnection(GetConnectionString);
            return db.Query<T>(sp, parms, commandType: commandType).FirstOrDefault();
        }

        public List<T> GetAll<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using IDbConnection db = new SqlConnection(GetConnectionString);
            return db.Query<T>(sp, parms, commandType: commandType).ToList();
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using IDbConnection db = new SqlConnection(GetConnectionString);
            return await db.QueryAsync<T>(sp, parms, commandType: commandType);
        }


        public async Task QueryMultipleAsync<T>(string sp, DynamicParameters parms, T resultObject, CommandType commandType = CommandType.StoredProcedure)
        {
            using IDbConnection db = new SqlConnection(GetConnectionString);

            using (var multi = await db.QueryMultipleAsync(sp, parms, commandType: commandType))
            {
                var type = typeof(T);
                var properties = type.GetProperties();

                foreach (var property in properties)
                {
                    var propertyType = property.PropertyType;

                    if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(List<>))
                    {
                        var genericArgType = propertyType.GetGenericArguments()[0];
                        var resultSet = await multi.ReadAsync(genericArgType);
                        var resultList = typeof(List<>).MakeGenericType(genericArgType);
                        var listInstance = Activator.CreateInstance(resultList);
                        foreach (var item in resultSet)
                        {
                            ((IList)listInstance).Add(item);
                        }
                        property.SetValue(resultObject, listInstance);
                    }
                    else
                    {
                        var singleResult = await multi.ReadSingleOrDefaultAsync(propertyType);
                        property.SetValue(resultObject, singleResult);
                    }
                }
            }
        }

        public async Task QueryMultipleAsync<T>(string sp, DynamicParameters parms, T resultObject, IDbConnection con, IDbTransaction trans, CommandType commandType = CommandType.StoredProcedure)
        {
            using (var multi = await con.QueryMultipleAsync(sp, parms, trans, commandType: commandType))
            {
                var type = typeof(T);
                var properties = type.GetProperties();

                foreach (var property in properties)
                {
                    var propertyType = property.PropertyType;

                    if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(List<>))
                    {
                        var genericArgType = propertyType.GetGenericArguments()[0];
                        var resultSet = await multi.ReadAsync(genericArgType);
                        var resultList = typeof(List<>).MakeGenericType(genericArgType);
                        var listInstance = Activator.CreateInstance(resultList);
                        foreach (var item in resultSet)
                        {
                            ((IList)listInstance).Add(item);
                        }
                        property.SetValue(resultObject, listInstance);
                    }
                    else
                    {
                        var singleResult = await multi.ReadSingleOrDefaultAsync(propertyType);
                        property.SetValue(resultObject, singleResult);
                    }
                }
            }
        }


        public DbConnection GetDbconnection()
        {
            return new SqlConnection(GetConnectionString);
        }

        public T Insert<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            T result;
            using IDbConnection db = new SqlConnection(GetConnectionString);
            try
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                using var tran = db.BeginTransaction();
                try
                {
                    result = db.Query<T>(sp, parms, commandType: commandType, transaction: tran).FirstOrDefault();
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }

            return result;
        }

        public T Update<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            T result;
            using IDbConnection db = new SqlConnection(GetConnectionString);
            try
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                using var tran = db.BeginTransaction();
                try
                {
                    result = db.Query<T>(sp, parms, commandType: commandType, transaction: tran).FirstOrDefault();
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }

            return result;
        }

        public int Delete(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            int result;
            using IDbConnection db = new SqlConnection(GetConnectionString);
            try
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                using var tran = db.BeginTransaction();
                try
                {
                    result = db.Execute(sp, parms, commandType: commandType, transaction: tran);
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }

            return result;
        }

        public async Task<T> GetAsync<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.Text)
        {
            using IDbConnection db = new SqlConnection(GetConnectionString);
            return await db.QueryFirstOrDefaultAsync<T>(sp, parms, commandType: commandType, commandTimeout: 0);
        }



        public async Task<T> InsertAsync<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            T result;
            using IDbConnection db = new SqlConnection(GetConnectionString);
            try
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                using var tran = db.BeginTransaction();
                try
                {
                    result = await db.QueryFirstOrDefaultAsync<T>(sp, parms, commandType: commandType, transaction: tran);
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }

            return result;
        }

        public async Task<DataSet> GetDataSetAsync(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using (IDbConnection db = new SqlConnection(GetConnectionString))
            {
                var ds = new DataSet();

                try
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();

                    using (var cmd = db.CreateCommand())
                    {
                        cmd.CommandType = commandType;
                        cmd.CommandText = sp;
                        cmd.CommandTimeout = 0;
                        if (parms != null)
                        {
                            foreach (var param in parms.ParameterNames)
                            {
                                var dp = parms.Get<dynamic>(param);
                                var dbParam = cmd.CreateParameter();
                                dbParam.ParameterName = param;
                                dbParam.Value = dp;
                                cmd.Parameters.Add(dbParam);
                            }
                        }

                        using (var da = new SqlDataAdapter((SqlCommand)cmd))
                        {
                            da.Fill(ds);
                        }
                    }
                }
                catch (Exception ex)
                {

                    throw;
                }

                return ds;
            }
        }

        public async Task<T> UpdateAsync<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            T result;
            using IDbConnection db = new SqlConnection(GetConnectionString);
            try
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                using var tran = db.BeginTransaction();
                try
                {
                    result = await db.QueryFirstAsync<T>(sp, parms, commandType: commandType, transaction: tran);
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }

            return result;
        }

        public async Task<int> AddUpdateAsync(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            int result;
            using IDbConnection db = new SqlConnection(GetConnectionString);
            try
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                using var tran = db.BeginTransaction();
                try
                {
                    result = await db.ExecuteAsync(sp, parms, commandType: commandType, transaction: tran); // Change here
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }

            return result;
        }

        public async Task<bool> DeleteAsync(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using IDbConnection db = new SqlConnection(GetConnectionString);
            try
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                var result = await db.ExecuteAsync(sp, parms, commandType: commandType);
                return result > 0; // Returns true if at least one row is affected
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }
        }


        public async Task<T> DeleteAsync<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            T result;
            using IDbConnection db = new SqlConnection(GetConnectionString);
            try
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                using var tran = db.BeginTransaction();
                try
                {
                    result = await db.QueryFirstAsync<T>(sp, parms, commandType: commandType, transaction: tran);
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }

            return result;
        }
        public async Task<IEnumerable<T>> QueryAsync<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using (var connection = new SqlConnection(GetConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    return await connection.QueryAsync<T>(sp, parms, commandType: commandType);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
            }
        }

        public async Task<T> InsertAsync<T>(string sp, DynamicParameters parms, IDbConnection dbConnection, IDbTransaction dbTransaction, CommandType commandType = CommandType.StoredProcedure)
        {
            return await dbConnection.QueryFirstOrDefaultAsync<T>(sp, parms, commandType: commandType, transaction: dbTransaction);
        }

        public async Task<T> GetAsync<T>(string sp, DynamicParameters parms, IDbConnection dbConnection, IDbTransaction dbTransaction, CommandType commandType = CommandType.Text)
        {
            return await dbConnection.QueryFirstOrDefaultAsync<T>(sp, parms, dbTransaction, commandType: commandType);
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>(string sp, DynamicParameters parms, IDbConnection dbConnection, IDbTransaction dbTransaction, CommandType commandType = CommandType.StoredProcedure)
        {
            return await dbConnection.QueryAsync<T>(sp, parms, dbTransaction, 60, commandType: commandType);
        }
        public async Task<DataTable> GetDataTableAsync(string sp, DynamicParameters parms, CommandType commandType = CommandType.Text)
        {
            using IDbConnection db = new SqlConnection(GetConnectionString);
            var result = await db.QueryAsync<dynamic>(sp, parms, commandType: commandType);
            return ToDataTable(result);
        }
        private DataTable ToDataTable(IEnumerable<dynamic> items)
        {
            var dataTable = new DataTable();
            var firstRecord = items.FirstOrDefault() as IDictionary<string, object>;

            if (firstRecord == null)
                return dataTable;

            foreach (var kvp in firstRecord)
            {
                dataTable.Columns.Add(kvp.Key);
            }

            foreach (var item in items)
            {
                var dataRow = dataTable.NewRow();
                var record = item as IDictionary<string, object>;
                foreach (var kvp in record)
                {
                    dataRow[kvp.Key] = kvp.Value;
                }
                dataTable.Rows.Add(dataRow);
            }

            return dataTable;
        }

        public DataSet GetDataSet(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using (IDbConnection db = new SqlConnection(GetConnectionString))
            {
                var ds = new DataSet();

                try
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();

                    using (var cmd = db.CreateCommand())
                    {
                        cmd.CommandType = commandType;
                        cmd.CommandText = sp;
                        cmd.CommandTimeout = 0;

                        foreach (var param in parms.ParameterNames)
                        {
                            var dp = parms.Get<dynamic>(param);
                            var dbParam = cmd.CreateParameter();
                            dbParam.ParameterName = param;
                            dbParam.Value = dp;
                            cmd.Parameters.Add(dbParam);
                        }

                        using (var da = new SqlDataAdapter((SqlCommand)cmd))
                        {
                            da.Fill(ds);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle the exception as needed
                    throw;
                }

                return ds;
            }
        }
        public DataSet GetDataSetByConnectionString(string Connectionstring, string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using (IDbConnection db = new SqlConnection(Connectionstring))
            {
                var ds = new DataSet();

                try
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();

                    using (var cmd = db.CreateCommand())
                    {
                        cmd.CommandType = commandType;
                        cmd.CommandText = sp;
                        cmd.CommandTimeout = 0;

                        foreach (var param in parms.ParameterNames)
                        {
                            var dp = parms.Get<dynamic>(param);
                            var dbParam = cmd.CreateParameter();
                            dbParam.ParameterName = param;
                            dbParam.Value = dp;
                            cmd.Parameters.Add(dbParam);
                        }

                        using (var da = new SqlDataAdapter((SqlCommand)cmd))
                        {
                            da.Fill(ds);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle the exception as needed
                    throw;
                }

                return ds;
            }
        }

        public Task<IDisposable> QueryMultipleAsync(string proc_GetTransForMultipleGenerateEdit, DynamicParameters dbparams, CommandType commandType)
        {
            throw new NotImplementedException();
        }

        private string GetConnectionString
        {
            get
            {
                  return _config.GetConnectionString(Connectionstring);
            }
            set
            {

            }

        }

    }
}

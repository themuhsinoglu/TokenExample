using AccessTokenAndRefreshToken.Common;
using AccessTokenAndRefreshToken.Common.Interfaces;
using Dapper;
using Npgsql;
using System.Data;

namespace AccessTokenAndRefreshToken.Data
{
    public class PostgreContext:IPostgreContext
    {
        string database = string.Empty;
        public PostgreContext(IConfiguration configuration)
        {
            database = ConnectionString.GetPostgreConnStr(configuration);
        }

        private IDbConnection Connection
        {
            get
            {
                return new NpgsqlConnection(database);
            }
        }
        public async Task<IEnumerable<T>> Query<T>(string query)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return await dbConnection.QueryAsync<T>(query, commandTimeout: 400);
            }
        }

        public async Task<IEnumerable<T>> Query<T>(string query, object obj)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return await dbConnection.QueryAsync<T>(query, obj, commandTimeout: 400);
            }
        }

        public async Task<T> QuerySingle<T>(string query, object obj)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return await dbConnection.QuerySingleOrDefaultAsync<T>(query, obj, commandTimeout: 400);
            }
        }

        public async Task<int> Execute(string query, object obj)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return await dbConnection.ExecuteAsync(query, obj);

            }
        }

        public async Task<int> Execute(string query, DynamicParameters p)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return await dbConnection.ExecuteAsync(query, p);
            }
        }

        public async Task<T> Execute<T>(string sp, DynamicParameters parameters, string retVal)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();

                var returnCode = await dbConnection.ExecuteAsync(
                    sql: sp,
                    param: parameters,
                    commandType: CommandType.StoredProcedure);

                return parameters.Get<T>(retVal);
            }
        }
    }
}

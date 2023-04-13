using Dapper;

namespace AccessTokenAndRefreshToken.Common.Interfaces
{
    public interface IPostgreContext
    {
        Task<IEnumerable<T>> Query<T>(string query);
        Task<IEnumerable<T>> Query<T>(string query, object obj);
        Task<int> Execute(string query, object obj);
        Task<T> Execute<T>(string sp, DynamicParameters parameters, string retVal);
        Task<int> Execute(string query, DynamicParameters p);
        Task<T> QuerySingle<T>(string query, object obj);
    }
}

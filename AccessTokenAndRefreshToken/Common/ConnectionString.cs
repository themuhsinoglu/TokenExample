using Npgsql;

namespace AccessTokenAndRefreshToken.Common
{
    public class ConnectionString
    {
        public static string GetPostgreConnStr(IConfiguration configuration)
        {
            NpgsqlConnectionStringBuilder builder = new NpgsqlConnectionStringBuilder();
            builder.Host = configuration["Postgre:ConnectionString:Server"];
            builder.Database = configuration["Postgre:ConnectionString:Database"];


            builder.Port = Convert.ToInt32(configuration["Postgre:ConnectionString:Port"]);
            builder.Username = configuration["Postgre:ConnectionString:User Id"];
            builder.Password = configuration["Postgre:ConnectionString:Password"];
            //builder.TrustServerCertificate = true;
            //builder.SslMode = SslMode.Disable;

            return new NpgsqlConnection(builder.ConnectionString).ConnectionString;
        }
    }
}

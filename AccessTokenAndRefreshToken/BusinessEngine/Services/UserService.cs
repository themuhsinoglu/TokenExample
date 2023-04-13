using AccessTokenAndRefreshToken.Common.Dtos;
using AccessTokenAndRefreshToken.Common.Interfaces;

namespace AccessTokenAndRefreshToken.BusinessEngine.Services
{
    public class UserService : IUserService
    {
        IPostgreContext postgreContext;

        public UserService(IPostgreContext postgreContext)
        {
            this.postgreContext = postgreContext;
        }

        public async Task<UserRefreshToken> AddUserRefreshTokens(UserRefreshToken user)
        {
            var sql = @"INSERT INTO public.""UserRefreshTokens"" (username,refreshtoken,isactive) VALUES (@UserName,@RefreshToken,@IsActive)";
            await postgreContext.Execute(sql, new { user.UserName, user.RefreshToken, user.IsActive });
            return user;
        }

        public void DeleteUserRefreshTokens(string username, string refreshToken)
        {
             postgreContext.Execute(@"Delete public.""UserRefreshTokens"" where username = @username and refreshtoken=@refreshToken", new { Username = username, RefreshToken = refreshToken });
        }

        public async Task<UserRefreshToken> GetSavedRefreshTokens(string username, string refreshtoken)
        {
            string query = @"Select * from public.""UserRefreshTokens"" where username=@username and refreshtoken=@refreshtoken";
            var userRefreshToken = await postgreContext.QuerySingle<UserRefreshToken>(query, new { UserName = username, RefreshToken = refreshtoken });
            return userRefreshToken;
        }

        public async Task<bool> IsValidUserAsync(User users)
        {
            string query = @"Select * from public.""Users"" where name=@Name and password=@Password";
            var user = await postgreContext.QuerySingle<User>(query, new { users.Name, users.Password });
            if (user == null)
            {
                return false;
            }
            return true;
        }
    }
}

using AccessTokenAndRefreshToken.Common.Dtos;
using AccessTokenAndRefreshToken.Common.Interfaces;

namespace AccessTokenAndRefreshToken.BusinessEngine.Services
{
    public class UserService : IUserService
    {
        IPostgreContext postgreContext;

        public async Task<UserRefreshToken> AddUserRefreshTokens(UserRefreshToken user)
        {
            var sql = @"INSERT INTO UserRefreshTokens (UserName,RefreshToken,IsActive) VALUES (@UserName,@RefreshToken,@IsActive)";
            await postgreContext.Execute(sql, new { user.UserName, user.RefreshToken, user.IsActive });
            return user;
        }

        public async void DeleteUserRefreshTokens(string username, string refreshToken)
        {
           await postgreContext.Execute("Delete UserRefreshTokens where UserName = @username and RefreshToken=@refreshToken", new { Username = username, RefreshToken = refreshToken });
        }

        public async Task<UserRefreshToken> GetSavedRefreshTokens(string username, string refreshtoken)
        {
            string query = "Select * from UserRefreshTokens where UserName=@username and RefreshToken=@refreshtoken";
            var userRefreshToken = await postgreContext.QuerySingle<UserRefreshToken>(query, new { UserName = username, RefreshToken = refreshtoken });
            return userRefreshToken;
        }

        public async Task<bool> IsValidUserAsync(User users)
        {
            string query = "Select * from Users where Name=@Name and Password=@Password";
            var user =  await postgreContext.QuerySingle<User>(query, new { Name = users.Name, Password = users.Password });
            if (user == null)
            {
                return false;
            }
            return true;
        }
    }
}

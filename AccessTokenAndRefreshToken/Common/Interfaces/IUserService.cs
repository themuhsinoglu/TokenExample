using AccessTokenAndRefreshToken.Common.Dtos;

namespace AccessTokenAndRefreshToken.Common.Interfaces
{
    public interface IUserService
    {
        Task<bool> IsValidUserAsync(User users);
        Task<UserRefreshToken> AddUserRefreshTokens(UserRefreshToken user);

        Task<UserRefreshToken>GetSavedRefreshTokens(string username, string refreshtoken);

        void DeleteUserRefreshTokens(string username, string refreshToken);
    }
}

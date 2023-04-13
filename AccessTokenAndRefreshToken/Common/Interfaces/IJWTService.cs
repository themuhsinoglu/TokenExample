using AccessTokenAndRefreshToken.Common.Dtos;
using System.Security.Claims;

namespace AccessTokenAndRefreshToken.Common.Interfaces
{
    public interface IJWTService
    {
        Task<Token> GenerateToken(string userName);
        Task<Token> GenerateRefreshToken(string userName);
        Task<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token);
    }
}

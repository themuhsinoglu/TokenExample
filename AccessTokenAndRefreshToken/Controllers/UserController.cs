using AccessTokenAndRefreshToken.Common.Dtos;
using AccessTokenAndRefreshToken.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AccessTokenAndRefreshToken.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IJWTService _jwtService;
        private readonly IUserService _userService;

        public UserController(IJWTService jwtService, IUserService userService)
        {
            _jwtService = jwtService;
            _userService = userService;
        }

        [HttpGet]
        [Route("Get")]
        public List<string> Get()
        {
            var users = new List<string>
            {
                "ahmet muhsinoğlu",
                "ibrahim ibaoglu",
                "kaan lokum"
            };
            return users;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public async Task<IActionResult> Authenticate(User user)
        {
            var validUser = await _userService.IsValidUserAsync(user);
            if (!validUser)
            {
                return Unauthorized("Incorrect username or password!!");
            }

            var token = await _jwtService.GenerateToken(user.Name);

            if (token == null)
            {
                return Unauthorized("Invalid Attempt!!");
            }

            //refresh token db kayıt

            UserRefreshToken obj = new UserRefreshToken
            {
                RefreshToken = token.Refresh_Token,
                UserName = user.Name,
                IsActive = true
            };

            await _userService.AddUserRefreshTokens(obj);
            return Ok(token);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("refresh")]
        public async Task<IActionResult> Refresh(Token token)
        {
            var principal = await _jwtService.GetPrincipalFromExpiredToken(token.Access_Token);
            var userName = principal.Identity?.Name;

            var savedRefreshToken = await _userService.GetSavedRefreshTokens(userName, token.Refresh_Token);

            if (savedRefreshToken.RefreshToken != token.Refresh_Token)
            {
                return Unauthorized("Invalid attempt!");
            }

            var newJwtToken = await _jwtService.GenerateRefreshToken(userName);
            if (newJwtToken == null)
            {
                return Unauthorized("Invalid Attempt!");
            }

            //refresh token db kayıt

            UserRefreshToken obj = new UserRefreshToken
            {
                RefreshToken = newJwtToken.Refresh_Token,
                UserName = userName
            };


            _userService.DeleteUserRefreshTokens(userName, token.Refresh_Token);

            await _userService.AddUserRefreshTokens(obj);

            return Ok(newJwtToken);





        }
    }
}

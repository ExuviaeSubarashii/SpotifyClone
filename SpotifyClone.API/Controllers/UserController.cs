using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SpotifyClone.Domain.Dtos;
using SpotifyClone.Domain.Models;
using SpotifyClone.Services.Services.PlaylistsServices;
using SpotifyClone.Services.Services.UserServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SpotifyClone.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly SpotifyCloneContext _SC;
        private readonly UserAuthentication _authentication;
        private readonly GetSuggestedPlayLists _playLists;
        private readonly UserProperties _userProperties;
        private readonly FollowManager _followManager;
        private readonly IConfiguration _config;
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        private CancellationToken _cancellationToken => _cancellationTokenSource.Token;

        public UserController(SpotifyCloneContext SC, UserAuthentication authentication, GetSuggestedPlayLists playLists, IConfiguration configuration, UserProperties userProperties, FollowManager followManager)
        {
            _SC = SC;
            _authentication = authentication;
            _playLists = playLists;
            _config = configuration;
            _userProperties = userProperties;
            _followManager = followManager;
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login(LoginDTO logindto)
        {
                if (logindto.UserEmail != null || logindto.Password != null)
                {
                    return Ok(await _authentication.LoginWithEmailAsync(logindto));
                }
                else
                { return BadRequest(); }

        

        }
        [HttpPost("Register")]
        public async Task<ActionResult> Register([FromBody] RegisterDTO register)
        {
         
                if (register != null)
                {
                    var doesUserExist = _SC.Users.Any(x => x.UserEmail == register.UserEmail);
                    if (!doesUserExist)
                    {
                        var registerUser = new User()
                        {
                            UserEmail = register.UserEmail.Trim(),
                            UserName = register.UserName.Trim(),
                            Password = register.UserPassword.Trim(),
                            Followers = "0",
                            Following = "0",
                            UserToken = CreateRegisterToken(register.UserName, register.UserEmail, register.UserPassword),
                        };
                        _SC.Users.Add(registerUser);
                        _SC.SaveChanges();
                        return Ok();
                    }
                    else
                    {
                        return ValidationProblem();
                    }
                }
                else
                {
                    return BadRequest();
                }
          

        }
        [HttpPost("AuthUser")]
        public async Task<ActionResult> AuthUser(string userToken)
        {
          
                bool isAuthed = _authentication.IsAuthenticated(userToken);
                if (isAuthed)
                    return Ok();
            else
            {

                    return NotFound();
            }
            
          
        }
        [HttpPost("GetUserProperties")]
        public async Task<ActionResult> GetUserPropertiess([FromBody] string userTokenValue)
        {
           
                if (userTokenValue != null)
                {
                    return Ok(await _userProperties.UserPropertiesGetterByToken(userTokenValue));
                }
                else
                { return BadRequest(); }

        
        }
        [HttpPost("GetUserPropertiesById")]
        public async Task<ActionResult> GetUserPropertiesByIdd([FromBody] int? userId)
        {
           
                if (userId != null)
                {
                    return Ok(await _userProperties.UserPropertiesGetterById(userId));
                }
                else
                { return BadRequest(); }

           
        }
        [HttpPost("GetUserFollowings")]
        public async Task<ActionResult> GetUserFollowings([FromBody] FollowRequestDTO request)
        {
           
                if (request != null)
                {
                    return Ok(await _followManager.GetFollowing(request.UserId));
                }
                else
                { return BadRequest(); }
           

        }
        [HttpPost("GetUserFollowers")]
        public async Task<ActionResult> GetUserFollowers([FromBody] FollowRequestDTO request)
        {
                if (request != null)
                {
                    return Ok(await _followManager.GetFollowers(request.UserId));

                }
                else
                { return BadRequest(); }
           
        }
        public string CreateToken(LoginDTO request)
        {
            List<Claim> claims = new List<Claim>();
            {
                _ = new Claim(ClaimTypes.Name, request.UserEmail, request.Password);
            }
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
                );
            var jwttoken = new JwtSecurityTokenHandler().WriteToken(token);
            return jwttoken;
        }
        public string CreateRegisterToken(string userName, string userEmail, string password)
        {
            List<Claim> claims = new List<Claim>();
            {
                _ = new Claim(ClaimTypes.Name, userName, userEmail, password);
            }
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
                );
            var jwttoken = new JwtSecurityTokenHandler().WriteToken(token);
            return jwttoken;
        }

    }

}
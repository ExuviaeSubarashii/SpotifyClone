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
        private readonly UserProfileActions _userProfileActions;
        private readonly IConfiguration _config;
        public UserController(SpotifyCloneContext SC, UserAuthentication authentication, GetSuggestedPlayLists playLists, IConfiguration configuration, UserProperties userProperties, FollowManager followManager, UserProfileActions userProfileActions)
        {
            _SC = SC;
            _authentication = authentication;
            _playLists = playLists;
            _config = configuration;
            _userProperties = userProperties;
            _followManager = followManager;
            _userProfileActions = userProfileActions;
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
                return Ok(await _authentication.RegisterAsync(register));
            }
            else
            {
                return BadRequest();
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
        public async Task<ActionResult> GetUserPropertiesByIdd([FromBody] FollowingOrNotDTO fonDTO)
        {
            if (fonDTO != null)
            {
                return Ok(await _userProperties.UserPropertiesGetterById(fonDTO));
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
        [HttpPost("ChangeEmail")]
        public async Task<ActionResult> ChangeEmail([FromBody] UpdateProfileDTO updateDto)
        {
            if (!string.IsNullOrEmpty(updateDto.NewEmail) && !string.IsNullOrEmpty(updateDto.UserToken))
            {
                return Ok(await _userProfileActions.ChangeEmail(updateDto));
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost("ChangePassword")]
        public async Task<ActionResult> ChangePassword([FromBody] UpdateProfileDTO updateDto)
        {
            if (!string.IsNullOrEmpty(updateDto.NewEmail) && !string.IsNullOrEmpty(updateDto.UserToken))
            {
                return Ok(await _userProfileActions.ChangePassword(updateDto));
            }
            else
            {
                return BadRequest("ERROR: Password can't be same / user not found / password is empty.");
            }
        }
        [HttpPost("FollowUser")]
        public async Task<ActionResult> FollowUser([FromBody] FollowOrUnfollowDto FTO) 
        {
            return Ok(await _followManager.FollowUser(FTO));
        }
        [HttpPost("UnFollowUser")]
        public async Task<ActionResult> UnFollowUser([FromBody] FollowOrUnfollowDto FTO) 
        {
            return Ok(await _followManager.UnFollowUser(FTO));
        }
    }

}
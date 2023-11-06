﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SpotifyClone.Domain.Dtos;
using SpotifyClone.Domain.Models;
using SpotifyClone.Services.Services;
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
        private readonly IConfiguration _config;
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private CancellationToken _cancellationToken => _cancellationTokenSource.Token;

        public UserController(SpotifyCloneContext SC, UserAuthentication authentication, GetSuggestedPlayLists playLists, IConfiguration configuration)
        {
            _SC = SC;
            _authentication = authentication;
            _playLists = playLists;
            _config = configuration;
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login(LoginDTO logindto)
        {
            try
            {
                _cancellationToken.ThrowIfCancellationRequested();
                if (logindto.UserEmail != null || logindto.Password != null)
                {
                    return Ok(await Task.Run(() => _authentication.LoginWithEmail(logindto)));
                }
                else
                { return BadRequest(); }

            }
            catch (OperationCanceledException)
            {

                return StatusCode(StatusCodes.Status503ServiceUnavailable, "Operation was canceled.");

            }
           

        }
        [HttpPost("Register")]
        public async Task<ActionResult> Register([FromBody] RegisterDTO register)
        {
            try
            {
                _cancellationToken.ThrowIfCancellationRequested();
                if (register != null)
                {
                    var doesUserExist = await Task.Run(() => _SC.Users.Any(x => x.UserEmail == register.UserEmail));
                    if (!doesUserExist)
                    {
                        var registerUser = new User()
                        {
                            UserEmail = register.UserEmail,
                            UserName = register.UserName,
                            Password = register.UserPassword,
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
            catch (OperationCanceledException)
            {

                return StatusCode(StatusCodes.Status503ServiceUnavailable, "Operation was canceled.");

            }
            
        }
        [HttpPost("AuthUser")]
        public async Task<ActionResult> AuthUser(string userToken)
        {
            try
            {
                _cancellationToken.ThrowIfCancellationRequested();
                bool isAuthed = await Task.Run(() => _authentication.IsAuthenticated(userToken));
                if (isAuthed)
                    return Ok();
                else
                    return NotFound();
            }
            catch (OperationCanceledException)
            {

                return StatusCode(StatusCodes.Status503ServiceUnavailable, "Operation was canceled.");

            }
            
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

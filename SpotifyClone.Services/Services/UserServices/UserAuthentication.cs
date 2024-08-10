using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SpotifyClone.Domain.Dtos;
using SpotifyClone.Domain.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SpotifyClone.Services.Services.UserServices
{
	public class UserAuthentication
	{
		private readonly SpotifyCloneContext _SC;
		private readonly IConfiguration _config;

		public UserAuthentication(SpotifyCloneContext SC)
		{
			_SC = SC;
		}
		public async Task<UserDTO> LoginWithEmailAsync(LoginDTO loginDTO)
		{
			try
			{
				var query = await _SC.Users.FirstOrDefaultAsync(x => x.UserEmail == loginDTO.UserEmail && x.Password == loginDTO.Password);
				if (query != null)
				{
					var user = new UserDTO()
					{
						Id = query.Id,
						UserEmail = query.UserEmail.Trim(),
						UserName = query.UserName.Trim(),
						Followers = query.Followers.Count().ToString(),
						Following = query.Following.Count().ToString(),
						UserToken = query.UserToken
					};
					return user;
				}
				else
				{
					return null;
				}
			}
			catch (Exception)
			{

				throw;
			}

		}
		public async Task<string> RegisterAsync(RegisterDTO register)
		{
			var doesUserExist = await _SC.Users.AnyAsync(x => x.UserEmail == register.UserEmail);
			if (doesUserExist)
			{
				return "Cannot Create Account";
			}
			var registerUser = new User()
			{
				UserEmail = register.UserEmail.Trim(),
				UserName = register.UserName.Trim(),
				Password = register.UserPassword.Trim(),
				Followers = "",
				Following = "",
				UserToken = CreateRegisterToken(register.UserName, register.UserEmail, register.UserPassword),
			};
			_SC.Users.Add(registerUser);
			_SC.SaveChanges();
			return "Successfully Created Account";
		}
		public string CreateRegisterToken(string userName, string userEmail, string password)
		{
			List<Claim> claims = new List<Claim>();
			{
				_ = new Claim(ClaimTypes.Name, userName, userEmail, password);
			}
			var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("BMNIGKHITKGSGITRPJKGJMEISNF"));

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

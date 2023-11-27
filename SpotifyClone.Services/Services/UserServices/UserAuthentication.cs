using Microsoft.EntityFrameworkCore;
using SpotifyClone.Domain.Dtos;
using SpotifyClone.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyClone.Services.Services.UserServices
{
    public class UserAuthentication
    {
        private readonly SpotifyCloneContext _SC;
        public UserAuthentication(SpotifyCloneContext SC)
        {
            _SC = SC;
        }
        public bool IsAuthenticated(string userToken)
        {
            try
            {
                var query = _SC.Users.Where(x => x.UserToken == userToken).Any();
                if (query)
                {
                    return true;
                }
                else { return false; }
            }
            catch (Exception)
            {

                throw;
            }
            
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
                    return new UserDTO();
                }
            }
            catch (Exception)
            {

                throw;
            }
           
        }
    }
}

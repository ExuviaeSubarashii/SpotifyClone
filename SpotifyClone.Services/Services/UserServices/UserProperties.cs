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
    public class UserProperties
    {
        private readonly SpotifyCloneContext _SP;
        public UserProperties(SpotifyCloneContext sP)
        {
            _SP = sP;
        }
        public async Task<UserPropertiesDTO> UserPropertiesGetterByToken(string userTokenValue)
        {
            try
            {
                UserPropertiesDTO userPropertiesDTO = new();

                var properties = await _SP.Users.Where(x => x.UserToken == userTokenValue).FirstOrDefaultAsync();
                if (properties != null)
                {
                    userPropertiesDTO = new UserPropertiesDTO()
                    {
                        UserName = properties.UserName.Trim(),
                        Followers = properties.Followers.Trim().Length,
                        Following = properties.Following.Trim().Length,
                        UserId = properties.Id
                    };
                    return userPropertiesDTO;
                }
                else
                {
                    return new UserPropertiesDTO();
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        public async Task<UserPropertiesDTO> UserPropertiesGetterById(int? userId)
        {
            try
            {
                UserPropertiesDTO userPropertiesDTO = new();

                var properties = await _SP.Users.Where(x => x.Id == userId).FirstOrDefaultAsync();
                if (properties != null)
                {
                    userPropertiesDTO = new UserPropertiesDTO()
                    {
                        UserName = properties.UserName.Trim(),
                        Followers = properties.Followers.Trim().Length,
                        Following = properties.Following.Trim().Length,
                        UserId = properties.Id
                    };
                    return userPropertiesDTO;
                }
                else
                {
                    return new UserPropertiesDTO();
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}

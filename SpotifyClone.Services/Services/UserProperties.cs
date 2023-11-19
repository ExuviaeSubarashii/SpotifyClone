using Microsoft.EntityFrameworkCore;
using SpotifyClone.Domain.Dtos;
using SpotifyClone.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyClone.Services.Services
{
    public class UserProperties
    {
        private readonly SpotifyCloneContext _SP;
        public UserProperties(SpotifyCloneContext sP)
        {
            _SP = sP;
        }
        public async Task<UserPropertiesDTO> UserPropertiesGetter(string userToken) 
        {
            UserPropertiesDTO userPropertiesDTO = new();

            var properties=await Task.Run(()=>_SP.Users.Where(x=>x.UserToken==userToken).FirstOrDefaultAsync());
            if (properties != null) 
            {
                userPropertiesDTO = new UserPropertiesDTO()
                {
                    UserName = properties.UserName.Trim(),
                    Followers = properties.Followers.Trim(),
                    Following = properties.Following.Trim()
                };
                return userPropertiesDTO;
            }
            else
            {
                return new UserPropertiesDTO();
            }
        }
    }
}

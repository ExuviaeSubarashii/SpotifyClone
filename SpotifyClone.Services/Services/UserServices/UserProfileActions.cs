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
    public class UserProfileActions
    {
        private readonly SpotifyCloneContext _SP;

        public UserProfileActions(SpotifyCloneContext sP)
        {
            _SP = sP;
        }
        public async Task<UserDTO> ChangeEmail(UpdateProfileDTO updateDto)
        {
            try
            {
                var userQuery = await _SP.Users.FirstOrDefaultAsync(x => x.UserToken == updateDto.UserToken);
                if (userQuery != null&&!string.IsNullOrEmpty(updateDto.NewEmail)) 
                {
                    userQuery.UserEmail = updateDto.NewEmail.Trim();
                    _SP.SaveChanges();
                    var result = new UserDTO()
                    {
                        UserEmail=updateDto.NewEmail.Trim(),
                    };
                    return result;
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
        public async Task<string> ChangePassword(UpdateProfileDTO updateDto)
        {
            try
            {
                var userQuery = await _SP.Users.FirstOrDefaultAsync(x => x.UserToken == updateDto.UserToken);
                if (userQuery != null && !string.IsNullOrEmpty(updateDto.NewPassword)&&updateDto.NewPassword.Trim() != userQuery.Password.Trim())
                {
                    userQuery.UserEmail = updateDto.NewEmail.Trim();
                    _SP.SaveChanges();
                    return "Password succesffully changed.";
                }
                else
                {
                    return "ERROR: Password can't be same / user not found / password is empty.";
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}

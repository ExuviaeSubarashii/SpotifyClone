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
    public class FollowManager
    {
        private readonly SpotifyCloneContext _SP;

        public FollowManager(SpotifyCloneContext sP)
        {
            _SP = sP;
        }
        public async Task<IEnumerable<FollowingDTO>> GetFollowing(int id)
        {
            try
            {
                var user = await _SP.Users.FirstOrDefaultAsync(x => x.Id == id);
                if (user == null)
                {
                    return Enumerable.Empty<FollowingDTO>();
                }
                var followingListIds = user.Following.Trim().Split(',');
                var followingdtos = await _SP.Users.Where(x => followingListIds.Contains(x.Id.ToString())).Select(x => new FollowingDTO
                {
                    UserId = x.Id,
                    UserName = x.UserName
                }).ToListAsync();

                return followingdtos;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public async Task<IEnumerable<FollowsDTO>> GetFollowers(int id)
        {
            try
            {
                var user = await _SP.Users.FirstOrDefaultAsync(x => x.Id == id);
                if (user == null)
                {
                    return Enumerable.Empty<FollowsDTO>();
                }
                var followsListIds = user.Followers.Trim().Split(',');
                var followsdto = await _SP.Users.Where(x => followsListIds.Contains(x.Id.ToString())).Select(x => new FollowsDTO
                {
                    UserId = x.Id,
                    UserName = x.UserName
                }).ToListAsync();

                return followsdto;
            }
            catch (Exception)
            {

                throw;
            }


        }

        public async Task<bool> IsFollowedOrNot(int profileId, string userToken)
        {
            var userQuery = await _SP.Users.FirstOrDefaultAsync(x => x.UserToken == userToken);

            if (userQuery != null)
            {
                var followinglist = userQuery.Following.Trim().Split(',').ToList();
                followinglist.RemoveAll(item => item == "");
                foreach (var item in followinglist)
                {
                    if (item == profileId.ToString())
                    {
                        return true;
                    }
                }
            }
            return false;

        }
        public async Task<IEnumerable<FollowingDTO>> FollowUser(FollowOrUnfollowDto FTO) 
        {
            //get the current user
            var currentUser = await _SP.Users.FirstOrDefaultAsync(x => x.UserToken == FTO.UserToken);
            //get the targeted user
            var targetUser = await _SP.Users.FirstOrDefaultAsync(x => x.Id == FTO.TargetUserId);

            //add target user's id and current user's id to eachother

            currentUser.Following = currentUser.Following.Trim() + "," + targetUser.Id;
            targetUser.Followers = targetUser.Followers.Trim() + "," + currentUser.Id;
            _SP.SaveChanges();
            return await GetFollowing(currentUser.Id);
        } 
        public async Task<IEnumerable<FollowingDTO>> UnFollowUser(FollowOrUnfollowDto FTO) 
        {
            //get the current user
            var currentUser = await _SP.Users.FirstOrDefaultAsync(x => x.UserToken == FTO.UserToken);
            //get the targeted user
            var targetUser = await _SP.Users.FirstOrDefaultAsync(x => x.Id == FTO.TargetUserId);

            //remove currentUser's id from targetUser's followers box && remove targetUser from currentUser's following box

            //set the current user
            List<string> currentUserFollowing =currentUser.Following.Trim().Split(',').ToList();
            currentUserFollowing.RemoveAll(x => x == FTO.TargetUserId.ToString());
            currentUser.Following = string.Join(',', currentUserFollowing);

            //set the target user
            List<string>targetUserFollowing = targetUser.Followers.Trim().Split(',').ToList();
            targetUserFollowing.RemoveAll(x => x == currentUser.Id.ToString());
            targetUser.Followers = string.Join(',', targetUserFollowing);


            _SP.SaveChanges();

            return await GetFollowing(currentUser.Id);
            
        }
    }
}

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
    public class FollowManager
    {
        private readonly SpotifyCloneContext _SP;

        public FollowManager(SpotifyCloneContext sP)
        {
            _SP = sP;
        }
        public async Task<List<FollowingDTO>> GetFollowing(int id)
        {
            var user = await Task.Run(() =>  _SP.Users.FirstOrDefaultAsync(x => x.Id == id));
            if (user!=null)
            {

            string[] list=user.Following.Split(",");
            if (user != null) 
            {
                List<FollowingDTO> followinglist = new();
                for (int i = 0; i<list.Length; i++)
                {
                    var followingData = list.ToList()[i];
                    var followingQuery = await Task.Run(() => _SP.Users.FirstOrDefaultAsync(x => x.Id == int.Parse(followingData)));
                    FollowingDTO following = new FollowingDTO()
                    {
                        UserId=followingQuery.Id,
                        UserName=followingQuery.UserName,
                    };
                    followinglist.Add(following);
                }
                return followinglist;
            }
            else
            {
                return new List<FollowingDTO>();
            }
            }
            else
            {
                return new List<FollowingDTO>();
            }
        }
        public async Task<List<FollowsDTO>> GetFollowers(int id)
        {
            var user = await Task.Run(() => _SP.Users.FirstOrDefaultAsync(x => x.Id == id));
            if (user != null) 
            {

            string[] list = user.Followers.Split(",");
            if (user != null)
            {
                List<FollowsDTO> followerlist = new();
                for (int i = 0; i < list.Length; i++)
                {
                    var followersData = list.ToList()[i];
                    var followersQuery = await Task.Run(() => _SP.Users.FirstOrDefaultAsync(x => x.Id == int.Parse(followersData)));
                    FollowsDTO following = new FollowsDTO()
                    {
                        UserId = followersQuery.Id,
                        UserName = followersQuery.UserName,
                    };
                    followerlist.Add(following);
                }
                return followerlist;
            }
            else
            {
                return new List<FollowsDTO>();
            }
            }
            else
            {
                return new List<FollowsDTO>();
            }
        }
    }
}

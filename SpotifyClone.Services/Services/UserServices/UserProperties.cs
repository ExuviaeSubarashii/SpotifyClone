﻿using Microsoft.EntityFrameworkCore;
using SpotifyClone.Domain.Dtos;
using SpotifyClone.Domain.Models;

namespace SpotifyClone.Services.Services.UserServices
{
	public class UserProperties
	{
		private readonly SpotifyCloneContext _SP;
		private readonly FollowManager _followManager;
		public UserProperties(SpotifyCloneContext sP, FollowManager followManager)
		{
			_SP = sP;
			_followManager = followManager;
		}
		public async Task<UserPropertiesDTO> UserPropertiesGetterByToken(string userTokenValue)
		{

			try
			{
				UserPropertiesDTO userPropertiesDTO = new();

				User? properties = await _SP.Users.Where(x => x.UserToken == userTokenValue).FirstOrDefaultAsync();
				if (properties is { })
				{
					List<string> followerCount = properties.Followers.Split(',').ToList();
					List<string> followingCount = properties.Following.Split(',').ToList();
					followerCount.RemoveAll(item => item.Trim() == "");
					followingCount.RemoveAll(item => item.Trim() == "");
					userPropertiesDTO = new UserPropertiesDTO()
					{
						UserName = properties.UserName.Trim(),
						Followers = followerCount.Count,
						Following = followingCount.Count,
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
		public async Task<UserPropertiesDTO> UserPropertiesGetterById(FollowingOrNotDTO followingOrNotDTO)
		{
			try
			{
				UserPropertiesDTO userPropertiesDTO = new();

				var properties = await _SP.Users.Where(x => x.Id == followingOrNotDTO.CurrentlyViewedUserProfileId).FirstOrDefaultAsync();
				var followerCount = properties.Followers.Split(',').ToList();
				var followingCount = properties.Following.Split(',').ToList();
				followerCount.RemoveAll(item => item.Trim() == "");
				followingCount.RemoveAll(item => item.Trim() == "");
				if (properties is { })
				{
					userPropertiesDTO = new UserPropertiesDTO()
					{
						UserName = properties.UserName.Trim(),
						Followers = followerCount.Count,
						Following = followingCount.Count,
						UserId = properties.Id,
						IsFollowing = await _followManager.IsFollowedOrNot(followingOrNotDTO.CurrentlyViewedUserProfileId, followingOrNotDTO.CurrentViewerUserToken)
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

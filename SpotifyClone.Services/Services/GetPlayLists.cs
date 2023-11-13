﻿using Microsoft.EntityFrameworkCore;
using SpotifyClone.Domain.Dtos;
using SpotifyClone.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyClone.Services.Services
{
    public class GetPlayLists
    {
        private readonly SpotifyCloneContext _SC;
        public GetPlayLists(SpotifyCloneContext SC) 
        {
            _SC = SC;
        }
        public async Task<List<PlaylistDTO>> GetAllPlayLists(string userToken)
        {
            var user = await Task.Run(() => _SC.Users.FirstOrDefaultAsync(x => x.UserToken == userToken));

            var playlists = await Task.Run(() => _SC.Playlists.Where(x => x.PlayListOwner == user.Id.ToString()).ToListAsync());
            if (playlists!=null)
            {

            List<PlaylistDTO> playlistDtos = new();

            for(int i=0; i<playlists.Count; i++) 
            {
                var playlistData = playlists.ToList()[i];
                PlaylistDTO playlist = new PlaylistDTO()
                {
                    PlayListId=playlistData.PlayListId.Trim(),
                    PlayListOwner=user.UserName.Trim(),
                    PlayListContents=playlistData.PlayListContents.Trim(),
                    PlayListType=playlistData.PlayListType.Trim(),
                    PlayListTitle=playlistData.PlayListTitle.Trim(),
                    PlayListCount=playlistData.PlayListContents.Trim().Count()
                };
                playlistDtos.Add(playlist);
            }
            return playlistDtos;
            }
            else
            {
                return new List<PlaylistDTO>();
            }
        }
        public async Task<List<PlaylistDTO>> GetPodCastAndShows(string userToken, string? playlistType)
        {
            var user = await Task.Run(() => _SC.Users.FirstOrDefaultAsync(x => x.UserToken == userToken));

            var playlists = await Task.Run(() => _SC.Playlists.Where(x => x.PlayListOwner == user.Id.ToString()&&x.PlayListType==playlistType).ToListAsync());
            if (playlists != null)
            {

                List<PlaylistDTO> playlistDtos = new();

                for (int i = 0; i < playlists.Count; i++)
                {
                    var playlistData = playlists.ToList()[i];
                    PlaylistDTO playlist = new PlaylistDTO()
                    {
                        PlayListId = playlistData.PlayListId.Trim(),
                        PlayListOwner = user.UserName.Trim(),
                        PlayListContents = playlistData.PlayListContents.Trim(),
                        PlayListType = playlistData.PlayListType.Trim(),
                        PlayListTitle = playlistData.PlayListTitle.Trim(),
                        PlayListCount = playlistData.PlayListContents.Trim().Count()

                    };
                    playlistDtos.Add(playlist);
                }
                return playlistDtos;
            }
            else
            {
                return new List<PlaylistDTO>();
            }
        }
        public async Task<List<PlaylistDTO>> GetAlbums(string userToken, string? playlistType)
        {
            var user = await Task.Run(() => _SC.Users.FirstOrDefaultAsync(x => x.UserToken == userToken));

            var playlists = await Task.Run(() => _SC.Playlists.Where(x => x.PlayListOwner == user.Id.ToString() && x.PlayListType == playlistType).ToListAsync());
            if (playlists != null)
            {

                List<PlaylistDTO> playlistDtos = new();

                for (int i = 0; i < playlists.Count; i++)
                {
                    var playlistData = playlists.ToList()[i];
                    PlaylistDTO playlist = new PlaylistDTO()
                    {
                        PlayListId = playlistData.PlayListId.Trim(),
                        PlayListOwner = user.UserName.Trim(),
                        PlayListContents = playlistData.PlayListContents.Trim(),
                        PlayListType = playlistData.PlayListType.Trim(),
                        PlayListTitle = playlistData.PlayListTitle.Trim(),
                        PlayListCount = playlistData.PlayListContents.Trim().Count()

                    };
                    playlistDtos.Add(playlist);
                }
                return playlistDtos;
            }
            else
            {
                return new List<PlaylistDTO>();
            }
        }
    }
}

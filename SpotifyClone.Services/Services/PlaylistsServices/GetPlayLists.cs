using Microsoft.EntityFrameworkCore;
using SpotifyClone.Domain.Dtos;
using SpotifyClone.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyClone.Services.Services.PlaylistsServices
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
            var user = await _SC.Users.FirstOrDefaultAsync(x => x.UserToken == userToken);

            List<PlaylistDTO> playlistDtos = new();

            var playlistIds  = await _SC.Users.Where(x => x.Id == user.Id).ToListAsync();
            string[] playlistarray= new string[0];
            foreach (var item in playlistIds)
            {
                playlistarray = item.FavoritedPlaylists.Trim().Split(',');
            }
            if (playlistarray[0]!="")
            {
                foreach (var item in playlistarray)
                {
                    var idk = await _SC.Playlists.Where(x => x.PlayListId == item).FirstOrDefaultAsync();
                    PlaylistDTO playlist = new PlaylistDTO()
                    {
                        PlayListId=idk.PlayListId.Trim(),
                        PlayListOwner=idk.PlayListOwnerName.Trim(),
                        PlayListContents=idk.PlayListContents.Trim(),
                        PlayListType=idk.PlayListType.Trim(),
                        PlayListTitle=idk.PlayListTitle.Trim(),
                        PlayListCount=idk.PlayListContents.Trim().Count(),
                        PlayListOwnerId=idk.PlayListOwner,
                    };
                    playlistDtos.Add(playlist);
                }
                return playlistDtos;
            }

            //dont delete this
            //var playlists = await _SC.Playlists.Where(x => x.PlayListOwner == user.Id).ToListAsync();

            //if (playlists != null)
            //{
            //    List<PlaylistDTO> playlistDtos = new();

            //    for (int i = 0; i < playlists.Count; i++)
            //    {
            //        var playlistData = playlists.ToList()[i];
            //        PlaylistDTO playlist = new PlaylistDTO()
            //        {
            //            PlayListId = playlistData.PlayListId.Trim(),
            //            PlayListOwner = user.UserName.Trim(),
            //            PlayListContents = playlistData.PlayListContents.Trim(),
            //            PlayListType = playlistData.PlayListType.Trim(),
            //            PlayListTitle = playlistData.PlayListTitle.Trim(),
            //            PlayListCount = playlistData.PlayListContents.Trim().Count(),
            //            PlayListOwnerId=playlistData.PlayListOwner
            //        };
            //        playlistDtos.Add(playlist);
            //    }
            //    return playlistDtos;
            //}
            else
            {
                return new List<PlaylistDTO>();
            }
        }
        public async Task<List<PlaylistDTO>> GetPodCastAndShows(string userToken, string? playlistType)
        {
            var user = await _SC.Users.FirstOrDefaultAsync(x => x.UserToken == userToken);

            var playlists = await _SC.Playlists.Where(x => x.PlayListOwner == user.Id && x.PlayListType == playlistType).ToListAsync();
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
            var user = await _SC.Users.FirstOrDefaultAsync(x => x.UserToken == userToken);

            var playlists = await _SC.Playlists.Where(x => x.PlayListOwner == user.Id && x.PlayListType == playlistType).ToListAsync();
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
        public async Task<List<PlayListContents>> GetAllPlayListContents(string id)
        {
            List<PlayListContents> playListContentsDtos = new List<PlayListContents>();

            var playlist = await _SC.Playlists.Where(x => x.PlayListId == id).ToListAsync();
            string[] songList = new string[0];

            foreach (var item in playlist)
            {
                songList = item.PlayListContents.Trim().Split(',');
            }
            if (songList[0] != "")
            {
                foreach (var item in songList)
                {
                    var idk = await _SC.Songs.Where(x => x.Id == int.Parse(item)).FirstOrDefaultAsync();
                    var result = new PlayListContents()
                    {
                        SongId = idk.Id,
                        DateAdded = idk.DateAdded,
                        Duration = idk.Duration,
                        SongName = idk.SongName,
                        SongArtist = idk.SongArtist,
                        AlbumName = idk.AlbumName,
                    };
                    playListContentsDtos.Add(result);
                }
                return playListContentsDtos;
            }
            else
            {
                return new List<PlayListContents>();
            }
        }
    }
}

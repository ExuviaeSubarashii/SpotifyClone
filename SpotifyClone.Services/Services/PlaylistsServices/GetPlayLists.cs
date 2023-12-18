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
        public async Task<IEnumerable<PlaylistDTO>> GetAllPlayLists(string userToken)
        {
            try
            {
                var user = await _SC.Users.FirstOrDefaultAsync(x => x.UserToken == userToken);
                if (user == null)
                {
                    return Enumerable.Empty<PlaylistDTO>();
                }

                var playlistIds = user.FavoritedPlaylists.Trim().Split(',');

                var playlistDtos = await _SC.Playlists
                    .Where(p => playlistIds.Contains(p.PlayListId))
                    .Select(p => new PlaylistDTO
                    {
                        PlayListId = p.PlayListId.Trim(),
                        PlayListOwner = p.PlayListOwnerName.Trim(),
                        PlayListContents = p.PlayListContents.Trim(),
                        PlayListType = p.PlayListType.Trim(),
                        PlayListTitle = p.PlayListTitle.Trim(),
                        PlayListCount = playlistIds.Count(),
                        PlayListOwnerId = p.PlayListOwner,
                        DateCreated = p.DateCreated
                    })
                    .ToListAsync();

                return playlistDtos;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public async Task<IEnumerable<PlaylistDTO>> GetPodCastAndShows(string userToken, string? playlistType)
        {
            try
            {
                var user = await _SC.Users.FirstOrDefaultAsync(x => x.UserToken == userToken);

                var playlists = await _SC.Playlists.Where(x => x.PlayListOwner == user.Id && x.PlayListType == playlistType)
                    .Select(p => new PlaylistDTO
                    {
                        PlayListId = p.PlayListId.Trim(),
                        PlayListOwner = p.PlayListOwnerName.Trim(),
                        PlayListContents = p.PlayListContents.Trim(),
                        PlayListType = p.PlayListType.Trim(),
                        PlayListTitle = p.PlayListTitle.Trim(),
                        PlayListCount = +1,
                        PlayListOwnerId = p.PlayListOwner,
                        DateCreated = p.DateCreated
                    })
                    .ToListAsync();

                return playlists ?? new List<PlaylistDTO>();
            }
            catch (Exception)
            {

                throw;
            }

        }
        public async Task<IEnumerable<PlaylistDTO>> GetAlbums(string userToken, string? playlistType)
        {
            try
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
            catch (Exception)
            {

                throw;
            }

        }
        public async Task<IEnumerable<PlayListContents>> GetAllPlayListContents(GetPlaylistContentsRequestDTO gpDTO)
        {
            try
            {
                var playlist = await _SC.Playlists
                    .Where(x => x.PlayListId == gpDTO.PlaylistId)
                    .FirstOrDefaultAsync();

                if (playlist == null || string.IsNullOrWhiteSpace(playlist.PlayListContents))
                {
                    return Enumerable.Empty<PlayListContents>();
                }

                var songList = playlist.PlayListContents.Trim().Split(',')
                    .Where(item => !string.IsNullOrWhiteSpace(item))
                    .ToArray();

                if (songList.Length == 0)
                {
                    return Enumerable.Empty<PlayListContents>();
                }

                var playListContentsDtos = await _SC.Songs
                    .Where(x => songList.Contains(x.Id.ToString()))
                    .Select(song => new PlayListContents
                    {
                        SongId = song.Id,
                        Duration = song.Duration,
                        SongName = song.SongName,
                        SongArtist = song.SongArtist,
                        AlbumName = song.AlbumName,
                        PlaylistName = playlist.PlayListTitle.Trim(),
                    })
                    .ToListAsync();

                return playListContentsDtos;
            }
            catch (Exception)
            {
                throw;
            }

        }
        public async Task<IEnumerable<PlaylistDTO>> GetPlaylistBySearch(string playlistName)
        {
            try
            {
                var searchResult = await _SC.Playlists.Where(x => x.PlayListTitle.StartsWith(playlistName))
                    .Select(x => new PlaylistDTO
                    {
                        DateCreated = DateTime.Now,
                        PlayListContents = x.PlayListContents.Trim(),
                        PlayListId = x.PlayListId.Trim(),
                        PlayListOwner = x.PlayListOwnerName.Trim(),
                        PlayListOwnerId = x.PlayListOwner,
                        PlayListTitle = x.PlayListTitle.Trim(),
                        PlayListType = x.PlayListType.Trim(),
                    })
                    .ToListAsync();
                return searchResult;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<bool> IsPlaylistFavorited(string userToken, string playListId)
        {
            try
            {
                var userQuery = await _SC.Users.FirstOrDefaultAsync(x => x.UserToken == userToken);
                if (userQuery.FavoritedPlaylists.Trim().Split(',').Contains(playListId))
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

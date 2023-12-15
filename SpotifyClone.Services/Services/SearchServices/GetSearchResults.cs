using Microsoft.EntityFrameworkCore;
using SpotifyClone.Domain.Dtos;
using SpotifyClone.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyClone.Services.Services.SearchServices
{
    public class GetSearchResults
    {
        private readonly SpotifyCloneContext _SP;

        public GetSearchResults(SpotifyCloneContext sP)
        {
            _SP = sP;
        }
        public async Task<IEnumerable<PlaylistDTO>> GetAllPlaylistSearch(string input)
        {
            var playlistResult = await _SP.Playlists.Where(x => x.PlayListTitle.Contains(input)).Select(p => new PlaylistDTO
            {
                PlayListId = p.PlayListId.Trim(),
                PlayListOwner = p.PlayListOwnerName.Trim(),
                PlayListContents = p.PlayListContents.Trim(),
                PlayListType = p.PlayListType.Trim(),
                PlayListTitle = p.PlayListTitle.Trim(),
                PlayListOwnerId = p.PlayListOwner,
                DateCreated = p.DateCreated
            }).ToListAsync();
            return playlistResult;
        }
        public async Task<IEnumerable<SongsDTO>> GetAllSongSerach(string input)
        {
            var songResults = await _SP.Songs
            .Where(x => x.SongName.Contains(input))
            .Select(s => new SongsDTO
            {
                SongId = s.Id,
                SongName = s.SongName.Trim(),
                AlbumName = s.AlbumName.Trim(),
                Duration = s.Duration,
                SongArtist = s.SongArtist.Trim()
            })
            .ToListAsync();
            return songResults;
        }
        public async Task<IEnumerable<UserDTO>> GetAllUserSearch(string input)
        {
            var userResults = await _SP.Users
                .Where(x => x.UserName.Contains(input))
                .Select(u => new UserDTO
                {
                    UserName = u.UserName.Trim(),
                    Following = u.Following.Trim(),
                    Followers = u.Followers.Trim(),
                })
                .ToListAsync();
            return userResults;
        }

    }
}

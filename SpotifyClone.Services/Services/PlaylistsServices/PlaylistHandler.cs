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
    public class PlaylistHandler
    {
        private readonly SpotifyCloneContext _SP;
        private readonly GetPlayLists _getPlayLists;
        public PlaylistHandler(SpotifyCloneContext sP, GetPlayLists getPlayLists)
        {
            _SP = sP;
            _getPlayLists = getPlayLists;
        }
        //create playlist
        public async Task<List<PlaylistDTO>> CreatePlaylist(CreatePlayListDTO playlistHandler)
        {
            if (playlistHandler != null)
            {
                var userQuery =  _SP.Users.FirstOrDefault(x => x.UserToken == playlistHandler.UserToken);
                var playlistQuery = _SP.Playlists.Where(x => x.PlayListOwner == userQuery.Id).ToList();
                var newPlaylist = new Playlist()
                {
                    PlayListId = Guid.NewGuid().ToString("D"),
                    PlayListContents = "",
                    PlayListOwner = userQuery.Id,
                    PlayListTitle = "Playlist# " + (playlistQuery.Count + 1),
                    PlayListType = "Playlist"
                };
                _SP.Playlists.Add(newPlaylist);
                _SP.SaveChanges();
                var list = _getPlayLists.GetAllPlayLists(playlistHandler.UserToken);
                return await list;
            }
            else
            {
                var list = _getPlayLists.GetAllPlayLists(playlistHandler.UserToken);
                return await list;
            }
        }
        //public async Task CreatePlaylist(CreatePlayListDTO playlistHandler)
        //{
        //    if (playlistHandler != null)
        //    {
        //        var userQuery = _SP.Users.FirstOrDefault(x => x.UserToken == playlistHandler.UserToken);
        //        var playlistQuery = _SP.Playlists.Where(x => x.PlayListOwner == userQuery.Id).ToList();
        //        var newPlaylist = new Playlist()
        //        {
        //            PlayListId = Guid.NewGuid().ToString("D"),
        //            PlayListContents = "",
        //            PlayListOwner = userQuery.Id,
        //            PlayListTitle = "Playlist# " + (playlistQuery.Count + 1),
        //            PlayListType = "Playlist"
        //        };
        //        _SP.Playlists.Add(newPlaylist);
        //        _SP.SaveChanges();
        //    }
        //}
        //delete playlist
        public async Task<Task<List<PlaylistDTO>>> DeletePlaylist(DeletePlaylistDTO deleteHandler)
        {
            var userQuery = _SP.Users.FirstOrDefault(x => x.UserToken == deleteHandler.UserToken);
            var playlistToDelete = await _SP.Playlists.FirstOrDefaultAsync(x => x.PlayListId == deleteHandler.PlaylistId);

            if (playlistToDelete != null)
            {
                _SP.Playlists.Remove(playlistToDelete);
                _SP.SaveChanges(); // Save changes to the database
                return _getPlayLists.GetAllPlayLists(deleteHandler.UserToken);
            }
            else
            {
                return (Task<List<PlaylistDTO>>)Enumerable.Empty<PlaylistDTO>();
            }
        }
        //update playlist
    }
}

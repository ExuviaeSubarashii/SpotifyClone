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
        public async Task CreatePlaylist(CreatePlayListDTO playlistHandler)
        {
            try
            {
                if (playlistHandler != null)
                {
                    var userQuery = await _SP.Users.FirstOrDefaultAsync(x => x.UserToken == playlistHandler.UserToken);
                    var playlistQuery = await _SP.Playlists.Where(x => x.PlayListOwner == userQuery.Id).ToListAsync();

                    var newPlaylist = new Playlist()
                    {
                        PlayListId = Guid.NewGuid().ToString("D"),
                        PlayListContents = "",
                        PlayListOwner = userQuery.Id,
                        PlayListTitle = "Playlist# " + (playlistQuery.Count + 1),
                        PlayListType = "Playlist",
                        PlayListOwnerName = userQuery.UserName,
                        DateCreated=DateTime.Now,
                    };
                    _SP.Playlists.Add(newPlaylist);
                    userQuery.FavoritedPlaylists = userQuery.FavoritedPlaylists.Trim() + "," + newPlaylist.PlayListId.Trim();
                    _SP.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        //delete playlist
        public async Task DeletePlaylist(DeletePlaylistDTO deleteHandler)
        {
            try
            {
                var userQuery = _SP.Users.FirstOrDefault(x => x.UserToken == deleteHandler.UserToken);
                var playlistToDelete = await _SP.Playlists.FirstOrDefaultAsync(x => x.PlayListId == deleteHandler.PlaylistId);

                if (playlistToDelete != null)
                {
                    _SP.Playlists.Remove(playlistToDelete);
                    _SP.SaveChanges(); // Save changes to the database
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        //update playlist
        public async Task UpdatePlaylist(UpdatePlaylistDTO updateDTO)
        {
            try
            {
                var playlistQuery = await _SP.Playlists.FirstOrDefaultAsync(x => x.PlayListId == updateDTO.PlayListId);
                if (playlistQuery != null)
                {

                    if(updateDTO.UpdateWay=="Change Title")
                            try
                        {
                            playlistQuery.PlayListTitle = updateDTO.NewPlaylistTitle;
                            _SP.SaveChanges();
                        }
                        catch (Exception)
                        {
                            throw;
                        }

                    if (updateDTO.UpdateWay == "Add Content")
                        try
                        {
                            playlistQuery.PlayListContents = playlistQuery.PlayListContents.Trim() + ',' + updateDTO.PlayListContents.Trim();
                            _SP.SaveChanges();
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

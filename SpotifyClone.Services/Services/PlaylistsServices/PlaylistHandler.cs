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
        public async Task<string> CreatePlaylist(CreatePlayListDTO playlistHandler)
        {
            try
            {
                if (playlistHandler != null)
                {
                    var userQuery = await _SP.Users.FirstOrDefaultAsync(x => x.UserToken == playlistHandler.UserToken);
                    var playlistQuery = await _SP.Playlists.Where(x => x.PlayListOwner == userQuery.Id).ToListAsync();
                    if (playlistQuery.Count >= 27)
                    {
                        return "Limit of Playlists Reached";
                    }
                    var newPlaylist = new Playlist()
                    {
                        PlayListId = Guid.NewGuid().ToString("D"),
                        PlayListContents = "",
                        PlayListOwner = userQuery.Id,
                        PlayListTitle = "Playlist# " + (playlistQuery.Count + 1),
                        PlayListType = "Playlist",
                        PlayListOwnerName = userQuery.UserName,
                        DateCreated = DateTime.Now,
                    };
                    _SP.Playlists.Add(newPlaylist);
                    if (userQuery.FavoritedPlaylists == null)
                    {
                        userQuery.FavoritedPlaylists = newPlaylist.PlayListId.Trim();
                        _SP.SaveChanges();
                        return "Created";
                    }
                    else
                    {
                        userQuery.FavoritedPlaylists = userQuery.FavoritedPlaylists.Trim() + "," + newPlaylist.PlayListId.Trim();
                        _SP.SaveChanges();
                        return "Created";

                    }
                }
                else
                {
                    return "Failed to Create.";
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        public async Task RemovePlaylist(DeletePlaylistDTO deleteHandler)
        {
            try
            {
                var userQuery = _SP.Users.FirstOrDefault(x => x.UserToken == deleteHandler.UserToken);
                var playlistToDelete = await _SP.Playlists.FirstOrDefaultAsync(x => x.PlayListId == deleteHandler.PlaylistId);

                if (playlistToDelete != null)
                {
                    _SP.Playlists.Remove(playlistToDelete);
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        //use this only if the user is not the creater of the playlist
        public async Task<IEnumerable<PlaylistDTO>> DeleteFromJustYourLibrary(DeletePlaylistDTO deleteHandler)
        {
            var userquery = _SP.Users.FirstOrDefault(x => x.UserToken == deleteHandler.UserToken);
            var playlistquery = userquery.FavoritedPlaylists.Split(',').ToList();
            playlistquery.Remove(deleteHandler.PlaylistId);
            playlistquery.RemoveAll(item => item == "");
            var updatedquery = "";
            for (int i = 0; i < playlistquery.Count; i++)
            {
                updatedquery += "," + playlistquery[i];
            }
            userquery.FavoritedPlaylists = updatedquery;
            _SP.SaveChanges();
            return await _getPlayLists.GetAllPlayLists(deleteHandler.UserToken);
        }
        //delete playlist from your own library
        public async Task<IEnumerable<PlaylistDTO>> DeletePlaylist(DeletePlaylistDTO deleteHandler)
        {
            var userquery= _SP.Users.FirstOrDefault(x=>x.UserToken==deleteHandler.UserToken);
            var playlistquery = userquery.FavoritedPlaylists.Split(',').ToList();
            playlistquery.Remove(deleteHandler.PlaylistId);
            playlistquery.RemoveAll(item => item == "");
            var updatedquery="";
            for (int i = 0; i < playlistquery.Count; i++)
            {
                updatedquery += ","+playlistquery[i];
            }
            userquery.FavoritedPlaylists= updatedquery;
            await RemovePlaylist(deleteHandler);
            _SP.SaveChanges();

            return await _getPlayLists.GetAllPlayLists(deleteHandler.UserToken);
        }
        public async Task<string> UpdatePlaylist(UpdatePlaylistDTO updateDTO)
        {
            try
            {
                var playlistQuery = await _SP.Playlists.FirstOrDefaultAsync(x => x.PlayListId == updateDTO.PlayListId);
                if (playlistQuery != null)
                {

                    if (updateDTO.UpdateWay == "Change Title")
                        try
                        {
                            playlistQuery.PlayListTitle = updateDTO.NewPlaylistTitle;
                            _SP.SaveChanges();
                            return $"Changed Title from {playlistQuery.PlayListTitle} to {updateDTO.NewPlaylistTitle}";
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                }
                if (updateDTO.UpdateWay == "Add Content")
                    try
                    {
                        if (playlistQuery.PlayListContents.Contains(updateDTO.PlayListContents) != true)
                        {

                            playlistQuery.PlayListContents = playlistQuery.PlayListContents.Trim() + ',' + updateDTO.PlayListContents.Trim();
                            _SP.SaveChanges();
                            return $"Added Song {updateDTO.PlayListContents} to {playlistQuery.PlayListTitle}";
                        }
                        else
                        {
                            return "This Song Already Exists in This Playlist.";
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                if (updateDTO.UpdateWay == "Remove Song")
                {
                    try
                    {
                        List<string> songListWithoutEmptyStrings = playlistQuery.PlayListContents.Trim().Split(',').ToList();
                        songListWithoutEmptyStrings.RemoveAll(item => item == "");
                        var updatedSongList = songListWithoutEmptyStrings.ToList();

                        var resultlist = updatedSongList.Where(x => x != updateDTO.PlayListContents.Trim()).ToList();
                        playlistQuery.PlayListContents = string.Join(",", resultlist);
                        _SP.SaveChanges();
                        return $"Removed song from {playlistQuery.PlayListTitle}";
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
                else
                {
                    return "Failed";
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

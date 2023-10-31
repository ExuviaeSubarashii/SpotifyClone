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
            var user = await Task.Run(() => _SC.Users.FirstOrDefault(x => x.UserToken == userToken));

            var playlists = await Task.Run(() => _SC.Playlists.Where(x => x.PlayListOwner == user.Id.ToString()).ToList());
            if (playlists!=null)
            {

            List<PlaylistDTO> playlistDtos = new();

            for(int i=0; i<playlists.Count; i++) 
            {
                var playlistData = playlists.ToList()[i];
                PlaylistDTO playlist = new PlaylistDTO()
                {
                    PlayListId=playlistData.PlayListId,
                    PlayListOwner=playlistData.PlayListOwner,
                    PlayListContents=playlistData.PlayListContents,
                    PlayListType=playlistData.PlayListType,
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

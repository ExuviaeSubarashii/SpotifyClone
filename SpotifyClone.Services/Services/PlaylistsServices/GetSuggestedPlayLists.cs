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
    public class GetSuggestedPlayLists
    {
        private readonly SpotifyCloneContext _SC;

        public GetSuggestedPlayLists(SpotifyCloneContext SC)
        {
            _SC = SC;
        }

        public async Task<IEnumerable<SuggestedPlayListDTO>> GetAllAsync(string userToken)
        {
            try
            {
                List<SuggestedPlayListDTO> playlistDTOs = new List<SuggestedPlayListDTO>();

                List<Playlist> playlists = await _SC.Playlists.ToListAsync();
                Random rnd = new Random();
                var randomPlaylists = playlists.OrderBy(x => rnd.Next()).Take(6).ToList();

                foreach (var item in randomPlaylists)
                {
                    var result = new SuggestedPlayListDTO()
                    {
                        PlayListId = item.PlayListId,
                        Title = item.PlayListTitle
                    };
                    playlistDTOs.Add(result);
                }

                return playlistDTOs;
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}

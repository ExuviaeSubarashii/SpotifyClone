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

        public async Task<IEnumerable<SuggestedPlayListDTO>> GetAllAsync()
        {
            try
            {
                List<Playlist> playlists = await _SC.Playlists.ToListAsync();
                Random rnd = new Random();
                var randomPlaylists = playlists.OrderBy(x => rnd.Next()).Take(6).Select(x=>new SuggestedPlayListDTO
                {
                    PlayListId=x.PlayListId,
                    Title=x.PlayListTitle,
                }).ToList();
                return randomPlaylists;
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}

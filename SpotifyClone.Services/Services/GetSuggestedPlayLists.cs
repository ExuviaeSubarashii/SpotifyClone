using SpotifyClone.Domain.Dtos;
using SpotifyClone.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyClone.Services.Services
{
    public class GetSuggestedPlayLists
    {
        private readonly SpotifyCloneContext _SC;
        public GetSuggestedPlayLists(SpotifyCloneContext SC)
        {
            _SC = SC;
        }
        public List<SuggestedPlayListDTO> GetAll(string userToken)
        {
            List<SuggestedPlayListDTO> playlistDTOs = new List<SuggestedPlayListDTO>();

            var playlists = _SC.Playlists.ToList();
            Random rnd = new Random();
            var randomPlaylists = playlists.OrderBy(x => rnd.Next()).Take(6).ToList();

            foreach (var item in randomPlaylists)
            {
                var result = new SuggestedPlayListDTO()
                {
                    PlayListId = item.PlayListId,
                    PlayListImage = item.PlayListImage,
                };
                playlistDTOs.Add(result); 
            }
            return playlistDTOs;
        }
    }
}

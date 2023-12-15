using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyClone.Domain.Dtos
{
    public static class SearchDTO
    {
        public static IEnumerable<SongsDTO> songs=null!;
        public static IEnumerable<PlaylistDTO> playlists=null!;
        public static IEnumerable<UserDTO> users = null!;
    }
}

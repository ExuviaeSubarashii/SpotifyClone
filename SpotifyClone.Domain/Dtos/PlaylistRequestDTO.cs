using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyClone.Domain.Dtos
{
    public class PlaylistRequestDTO
    {
        public string UserToken { get; set; }
        public string? PlayListType { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyClone.Domain.Dtos
{
    public class DeletePlaylistDTO
    {
        public string PlaylistId { get; set; } = null!;
        public string UserToken { get; set; } = null!;
    }
}

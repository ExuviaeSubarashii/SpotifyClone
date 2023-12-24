using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyClone.Domain.Dtos
{
    public class GetPlaylistOwnerDTO
    {
        public int CurrentUserId { get; set; }
        public string PlaylistId { get; set; } = null!;
    }
}

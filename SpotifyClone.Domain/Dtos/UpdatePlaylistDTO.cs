using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyClone.Domain.Dtos
{
    public class UpdatePlaylistDTO
    {
        public string? PlayListId { get; set; } = null!;
        public string? NewPlaylistTitle { get; set; } = null!;
        public string? PlayListContents { get; set; } = null!;
        public string? UpdateWay { get; set; } = null!;
    }
}

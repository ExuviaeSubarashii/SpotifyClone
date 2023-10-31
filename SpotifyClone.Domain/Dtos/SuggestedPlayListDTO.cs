using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyClone.Domain.Dtos
{
    public class SuggestedPlayListDTO
    {
        public string PlayListId { get; set; } = null!;
        public string Title { get; set; } = null!;
        public byte[]? PlayListImage { get; set; } = null!;
    }
}

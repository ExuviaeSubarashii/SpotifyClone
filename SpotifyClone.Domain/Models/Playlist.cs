using System;
using System.Collections.Generic;

namespace SpotifyClone.Domain.Models
{
    public partial class Playlist
    {
        public string PlayListId { get; set; } = null!;
        public string PlayListOwner { get; set; } = null!;
        public byte[] PlayListImage { get; set; } = null!;
        public string PlayListContents { get; set; } = null!;
        public string PlayListType { get; set; } = null!;
    }
}

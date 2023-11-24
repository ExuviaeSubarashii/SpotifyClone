using System;
using System.Collections.Generic;

namespace SpotifyClone.Domain.Models
{
    public partial class Playlist
    {
        public string PlayListId { get; set; } = null!;
        public int PlayListOwner { get; set; }
        public string PlayListContents { get; set; } = null!;
        public string PlayListType { get; set; } = null!;
        public string PlayListTitle { get; set; } = null!;
    }
}

using System;
using System.Collections.Generic;

namespace SpotifyClone.Domain.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string UserEmail { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string? Followers { get; set; } = null!;
        public string? Following { get; set; } = null!;
        public string UserToken { get; set; } = null!;
        public string? FavoritedPlaylists { get; set; } = null!;

    }
}

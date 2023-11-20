using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyClone.Domain.Dtos
{
    public class UserPropertiesDTO
    {
        public string? UserName { get; set; } = null!;
        public int? Followers { get; set; } = null!;
        public int? Following { get; set; } = null!;
        public int? UserId { get; set; }
    }
}

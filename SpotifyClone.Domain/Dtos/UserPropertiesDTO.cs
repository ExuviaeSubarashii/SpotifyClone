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
        public string? Followers { get; set; } = null!;
        public string? Following { get; set; } = null!;
    }
}

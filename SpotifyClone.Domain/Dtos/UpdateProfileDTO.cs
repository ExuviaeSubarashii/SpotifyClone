using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyClone.Domain.Dtos
{
    public class UpdateProfileDTO
    {
        public string UserToken { get; set; } = null!;
        public string? NewEmail { get; set; } = null!;
        public string? NewPassword { get; set;} = null!;
        public string? NewUserName { get; set; } = null!;
    }
}

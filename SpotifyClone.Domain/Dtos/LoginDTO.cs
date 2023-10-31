using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyClone.Domain.Dtos
{
    public class LoginDTO
    {
        public string UserName { get; set; }= null!;
        public string UserEmail{ get; set; }= null!;
        public string Password { get; set; } = null!;
    }
}

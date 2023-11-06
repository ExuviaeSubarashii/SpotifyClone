using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyClone.Domain.Dtos
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string UserEmail{ get; set; } = null!;
        public byte[] ProfileImage { get; set; } = null!;
        public string Followers { get; set; } = null!;
        public string Following { get; set; } = null!;
        public string UserToken {  get; set; } = null!;
    }
}

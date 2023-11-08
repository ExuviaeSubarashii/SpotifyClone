using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyClone.Domain.Models
{
    public partial class Songs
    {
        public int Id { get; set; }
        public string SongName { get; set; } = null!;
        public DateTime DateAdded { get; set; }
        public int Duration { get; set; }
    }
}

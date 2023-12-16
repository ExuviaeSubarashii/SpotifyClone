using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyClone.Domain.Dtos
{
    public class FollowOrUnfollowDto
    {
        public int TargetUserId { get; set; }
        public string UserToken { get; set; } = null!;
    }
}

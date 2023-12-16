using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyClone.Domain.Dtos
{
    public class FollowingOrNotDTO
    {
        public string CurrentViewerUserToken { get; set; } = null!;
        public int CurrentlyViewedUserProfileId { get; set; }
    }
}

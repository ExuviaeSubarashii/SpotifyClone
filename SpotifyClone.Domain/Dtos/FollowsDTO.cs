﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyClone.Domain.Dtos
{
    public class FollowsDTO
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
    }
}

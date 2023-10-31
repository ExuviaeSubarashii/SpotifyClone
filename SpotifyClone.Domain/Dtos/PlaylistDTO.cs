﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyClone.Domain.Dtos
{
    public class PlaylistDTO
    {
        public string PlayListId { get; set; } = null!;
        public string PlayListOwner { get; set; } = null!;
        public byte[] PlayListImage { get; set; } = null!;
        public string PlayListContents { get; set; } = null!;
        public string PlayListType { get; set; } = null!;
    }
}
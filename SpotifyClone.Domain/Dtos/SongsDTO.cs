﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyClone.Domain.Dtos
{
    public class SongsDTO
    {
        public int SongId { get; set; }
        public string SongName { get; set; } = null!;
        public int Duration { get; set; }
        public string SongArtist { get; set; } = null!;
        public string AlbumName { get; set; } = null!;
    }
}
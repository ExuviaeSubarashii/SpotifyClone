using Microsoft.EntityFrameworkCore;
using SpotifyClone.Domain.Dtos;
using SpotifyClone.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyClone.Services.Services
{
    public class SongsService
    {
        private readonly SpotifyCloneContext _SP;

        public SongsService(SpotifyCloneContext sP)
        {
            _SP = sP;
        }

        public async Task<CurrentSongDTO> SetCurrentSong(int songId)
        {
            CurrentSongDTO currentSongDTO = new();

            var getSongProperties = await Task.Run(()=>_SP.Songs.FirstOrDefault(x => x.Id==songId));

            currentSongDTO.SongName = getSongProperties.SongName.Trim();
            currentSongDTO.Duration = getSongProperties.Duration;
            currentSongDTO.SongArtist= getSongProperties.SongArtist.Trim();
            currentSongDTO.AlbumName = getSongProperties.AlbumName.Trim();

            return currentSongDTO;
        }
    }
}

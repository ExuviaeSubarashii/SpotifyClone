using Microsoft.EntityFrameworkCore;
using SpotifyClone.Domain.Dtos;
using SpotifyClone.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyClone.Services.Services.SongServices
{
    public class SongsService
    {
        private readonly SpotifyCloneContext _SP;

        public SongsService(SpotifyCloneContext sP)
        {
            _SP = sP;
        }

        public async Task<CurrentSongDTO?> SetCurrentSong(int songId)
        {
            try
            {
                var getSongProperties = await _SP.Songs.Where(x => x.Id == songId).Select(x => new CurrentSongDTO
                {
                    Duration = x.Duration,
                    AlbumName = x.AlbumName,
                    Id = x.Id,
                    SongArtist = x.SongArtist,
                    SongName = x.SongName
                }).FirstOrDefaultAsync();

                return getSongProperties??new CurrentSongDTO();

            }
            catch (Exception)
            {
                throw;
            }

        }
        public async Task<IEnumerable<SongsDTO>> GetAllSongs()
        {
            try
            {
                var songs = await _SP.Songs.Select(x => new SongsDTO
                {
                    Duration = x.Duration,
                    SongName = x.SongName,
                    SongArtist = x.SongArtist,
                    AlbumName = x.AlbumName,
                    SongId=x.Id
                }).ToListAsync();

                return songs ?? Enumerable.Empty<SongsDTO>();

            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}

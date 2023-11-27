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

        public async Task<CurrentSongDTO> SetCurrentSong(int songId)
        {
            try
            {
                CurrentSongDTO currentSongDTO = new();

                var getSongProperties = await _SP.Songs.FirstOrDefaultAsync(x => x.Id == songId);

                currentSongDTO.SongName = getSongProperties.SongName.Trim();
                currentSongDTO.Duration = getSongProperties.Duration;
                currentSongDTO.SongArtist = getSongProperties.SongArtist.Trim();
                currentSongDTO.AlbumName = getSongProperties.AlbumName.Trim();

                return currentSongDTO;
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        public async Task<List<SongsDTO>> GetAllSongs()
        {
            try
            {
                var songs = await _SP.Songs.ToListAsync();

                if (songs.Count > 0)
                {
                    List<SongsDTO> songsDtos = new();
                    for (int i = 0; i < songs.Count; i++)
                    {
                        var songlistData = songs.ToList()[i];
                        SongsDTO songsDTO = new SongsDTO()
                        {
                            AlbumName = songlistData.AlbumName.Trim(),
                            SongName = songlistData.SongName.Trim(),
                            SongArtist = songlistData.SongArtist.Trim(),
                            Duration = songlistData.Duration,
                            SongId = songlistData.Id
                        };
                        songsDtos.Add(songsDTO);
                    }
                    return songsDtos;
                }
                else
                {
                    return new List<SongsDTO>();
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}

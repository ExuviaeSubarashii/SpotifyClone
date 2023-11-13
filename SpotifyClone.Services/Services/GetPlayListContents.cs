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
    public class GetPlayListContents
    {
        private readonly SpotifyCloneContext _SP;

        public GetPlayListContents(SpotifyCloneContext sP)
        {
            _SP = sP;
        }

        public async Task<List<PlayListContents>> GetAllPlayListContents(string id)
        {
            List<PlayListContents> playListContentsDtos=new List<PlayListContents>();

            var playlist= await Task.Run(()=>_SP.Playlists.Where(x=>x.PlayListId==id).ToListAsync());
            string[] songList=new string[0];
            
            foreach (var item in playlist) 
            {
                songList = item.PlayListContents.Split(',');
            }
            foreach (var item in songList) 
            {
                var idk= await Task.Run(()=>_SP.Songs.Where(x=>x.Id==int.Parse(item)).FirstOrDefaultAsync());
                var result = new PlayListContents()
                {
                    SongId = idk.Id,
                    DateAdded=idk.DateAdded,
                    Duration=idk.Duration,
                    SongName=idk.SongName,
                    SongArtist=idk.SongArtist,
                    AlbumName = idk.AlbumName,
                };
                playListContentsDtos.Add(result);
            }
            return playListContentsDtos;
        }
    }
}

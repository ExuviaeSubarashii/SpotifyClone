using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpotifyClone.Domain.Models;
using SpotifyClone.Services.Services.SongServices;

namespace SpotifyClone.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {

        private readonly SpotifyCloneContext _SP;
        private readonly SongsService _songsService;
        public SongsController(SpotifyCloneContext sP, SongsService songsService)
        {
            _SP = sP;
            _songsService = songsService;
        }
        [HttpPost("SetCurrentSong")]
        public async Task<ActionResult> SetCurrentSong([FromBody] int songId)
        {

            if (songId != null)
            {
                var songProperties = await _songsService.SetCurrentSong(songId);
                return Ok(songProperties);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet("GetAllSongs")]
        public async Task<ActionResult> GetAllSongs()
        {
            return Ok(await _songsService.GetAllSongs());
        }
    }
}
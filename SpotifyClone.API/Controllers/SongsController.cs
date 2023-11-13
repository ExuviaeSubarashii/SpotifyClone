using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpotifyClone.Domain.Models;
using SpotifyClone.Services.Services;

namespace SpotifyClone.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {

        private readonly SpotifyCloneContext _SP;
        private readonly SongsService _songsService;
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private CancellationToken _cancellationToken => _cancellationTokenSource.Token;
        public SongsController(SpotifyCloneContext sP,SongsService songsService)
        {
            _SP = sP;
            _songsService = songsService;
        }
        [HttpPost("SetCurrentSong")]
        public async Task<ActionResult> SetCurrentSong([FromBody] int songId)
        {
            try
            {
                _cancellationToken.ThrowIfCancellationRequested();
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
            catch (OperationCanceledException)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, "Operation was canceled.");
            }
        }
    }
}

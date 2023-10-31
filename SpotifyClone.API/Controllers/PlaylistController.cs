using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpotifyClone.Domain.Dtos;
using SpotifyClone.Domain.Models;
using SpotifyClone.Services.Services;
using System.Diagnostics.Eventing.Reader;

namespace SpotifyClone.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistController : ControllerBase
    {
        private readonly SpotifyCloneContext _SC;
        private readonly GetSuggestedPlayLists _playLists;
        public PlaylistController(SpotifyCloneContext SC,GetSuggestedPlayLists playLists)
        {
            _SC = SC;
            _playLists = playLists;
        }
        [HttpPost("GetUserPlayLists")]
        public async Task<ActionResult> GetUserPlayLists(string userToken)
        {
            if (!string.IsNullOrEmpty(userToken))
            {
                return Ok(await Task.Run(() => _playLists.GetAllAsync(userToken)));
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost("GetSuggestedPlayLists")]
        public async Task<ActionResult> GetSuggestedPlayLists(string userToken)
        {
            List<SuggestedPlayListDTO> suggestedPlaylists = await _playLists.GetAllAsync(userToken);
            return Ok(suggestedPlaylists);
        }
    }
}

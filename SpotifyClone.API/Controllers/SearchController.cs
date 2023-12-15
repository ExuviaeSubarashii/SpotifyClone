using Microsoft.AspNetCore.Mvc;
using SpotifyClone.Domain.Dtos;
using SpotifyClone.Services.Services.SearchServices;

namespace SpotifyClone.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly GetSearchResults _GSR;

        public SearchController(GetSearchResults gSR)
        {
            _GSR = gSR;
        }
        [HttpPost("SearchAllPlaylists")]
        public async Task<ActionResult> SearchAllGetPlaylists([FromBody] ThatSearchThingDTO thing)
        {
            return Ok(await _GSR.GetAllPlaylistSearch(thing.inputprop));
        }
        [HttpPost("SearchAllUsers")]
        public async Task<ActionResult> SearchAllGetUsers([FromBody] ThatSearchThingDTO thing)
        {
            return Ok(await _GSR.GetAllUserSearch(thing.inputprop));
        }
        [HttpPost("SearchAllSongs")]
        public async Task<ActionResult> SearchAllGetSongs([FromBody] ThatSearchThingDTO thing)
        {
            return Ok(await _GSR.GetAllSongSerach(thing.inputprop));
        }
    }
}

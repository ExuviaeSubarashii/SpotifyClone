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
        private readonly GetPlayLists _allplayLists;
        private readonly GetPlayListContents _getPlayListContent;
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private CancellationToken _cancellationToken => _cancellationTokenSource.Token;

        public PlaylistController(SpotifyCloneContext SC, GetSuggestedPlayLists playLists, GetPlayLists getPlayLists, GetPlayListContents getPlayListContent)
        {
            _SC = SC;
            _playLists = playLists;
            _allplayLists = getPlayLists;
            _getPlayListContent = getPlayListContent;

        }
        [HttpPost("GetUserPlayLists")]
        public async Task<ActionResult> GetUserPlayLists([FromBody] PlaylistRequestDTO request)
        {
            try
            {
                _cancellationToken.ThrowIfCancellationRequested();
                if (!string.IsNullOrEmpty(request.UserToken))
                {
                    //playlist
                    if (string.IsNullOrEmpty(request.PlayListType))
                    {
                        return Ok(await Task.Run(() => _allplayLists.GetAllPlayLists(request.UserToken)));
                    }

                    //podcast & show
                    if (request.PlayListType == "Podcast")
                    {
                        return Ok(await Task.Run(() => _allplayLists.GetPodCastAndShows(request.UserToken, request.PlayListType)));
                    }
                    //albums
                    if (request.PlayListType == "Albums")
                    {
                        return Ok(await Task.Run(() => _allplayLists.GetAlbums(request.UserToken, request.PlayListType)));
                    }
                    else
                    {
                        return BadRequest();
                    }
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
        [HttpPost("GetSuggestedPlayLists")]
        public async Task<ActionResult> GetSuggestedPlayLists([FromBody] string userToken)
        {
            try
            {
                _cancellationToken.ThrowIfCancellationRequested();
                if (!string.IsNullOrEmpty(userToken))
                {
                    List<SuggestedPlayListDTO> suggestedPlaylists = await _playLists.GetAllAsync(userToken);
                    return Ok(suggestedPlaylists);
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
        [HttpPost("GetPlaylistContents")]
        public async Task<ActionResult> GetPlaylistContents([FromBody] string playlistId)
        {
            try
            {
                _cancellationToken.ThrowIfCancellationRequested();
                if (!string.IsNullOrEmpty(playlistId))
                {
                    List<PlayListContents> suggestedPlaylists = await _getPlayListContent.GetAllPlayListContents(playlistId);
                    return Ok(suggestedPlaylists);
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
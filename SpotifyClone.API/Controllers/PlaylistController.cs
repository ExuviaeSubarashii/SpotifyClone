using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpotifyClone.Domain.Dtos;
using SpotifyClone.Domain.Models;
using SpotifyClone.Services.Services.PlaylistsServices;
using System.Diagnostics.Eventing.Reader;

namespace SpotifyClone.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistController : ControllerBase
    {
        private readonly SpotifyCloneContext _SC;
        private readonly GetSuggestedPlayLists _suggestedplayLists;
        private readonly GetPlayLists _allplayLists;
        private readonly PlaylistHandler _playlistHandler;
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private CancellationToken _cancellationToken => _cancellationTokenSource.Token;

        public PlaylistController(SpotifyCloneContext SC, GetSuggestedPlayLists suggestedplayLists, GetPlayLists getPlayLists, PlaylistHandler playlistHandler)
        {
            _SC = SC;
            _suggestedplayLists = suggestedplayLists;
            _allplayLists = getPlayLists;
            _playlistHandler = playlistHandler;
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
                    List<SuggestedPlayListDTO> suggestedPlaylists = await _suggestedplayLists.GetAllAsync(userToken);
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
                    List<PlayListContents> suggestedPlaylists = await _allplayLists.GetAllPlayListContents(playlistId);
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
        [HttpPost("CreatePlayList")]
        public async Task<ActionResult> CreatePlaylist([FromBody] CreatePlayListDTO handlerDto)
        {
            var playlist = _playlistHandler.CreatePlaylist(handlerDto);
            return Ok (playlist);
        }
    }
}
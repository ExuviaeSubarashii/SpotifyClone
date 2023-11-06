﻿using Microsoft.AspNetCore.Http;
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
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private CancellationToken _cancellationToken => _cancellationTokenSource.Token;
        public PlaylistController(SpotifyCloneContext SC, GetSuggestedPlayLists playLists, GetPlayLists getPlayLists)
        {
            _SC = SC;
            _playLists = playLists;
            _allplayLists = getPlayLists;
        }
        [HttpPost("GetUserPlayLists")]
        public async Task<ActionResult> GetUserPlayLists([FromBody] string userToken)
        {
            try
            {
                _cancellationToken.ThrowIfCancellationRequested();
                if (!string.IsNullOrEmpty(userToken))
                {
                    //return Ok(await Task.Run(() => _playLists.GetAllAsync(userToken)));
                    return Ok(await Task.Run(() => _allplayLists.GetAllPlayLists(userToken)));
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
        public async Task<ActionResult> GetSuggestedPlayLists([FromBody]string userToken)
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
    }
}

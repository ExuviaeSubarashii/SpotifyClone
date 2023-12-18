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

            if (!string.IsNullOrEmpty(request.UserToken))
            {
                //playlist
                if (string.IsNullOrEmpty(request.PlayListType))
                {
                    return Ok(await _allplayLists.GetAllPlayLists(request.UserToken));
                }

                //podcast & show
                if (request.PlayListType == "Podcast")
                {
                    return Ok(await _allplayLists.GetPodCastAndShows(request.UserToken, request.PlayListType));
                }
                //albums
                if (request.PlayListType == "Albums")
                {
                    return Ok(await _allplayLists.GetAlbums(request.UserToken, request.PlayListType));
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest("User Token Required");
            }


        }
        [HttpPost("GetSuggestedPlayLists")]
        public async Task<ActionResult> GetSuggestedPlayLists()
        {
            IEnumerable<SuggestedPlayListDTO> suggestedPlaylists = await _suggestedplayLists.GetAllAsync();
            return Ok(suggestedPlaylists);
        }
        [HttpPost("GetPlaylistContents")]
        public async Task<ActionResult> GetPlaylistContents([FromBody] GetPlaylistContentsRequestDTO gpDTO)
        {

            if (!string.IsNullOrEmpty(gpDTO.PlaylistId))
            {
                IEnumerable<PlayListContents> suggestedPlaylists = await _allplayLists.GetAllPlayListContents(gpDTO);
                return Ok(suggestedPlaylists);
            }
            else
            {
                return BadRequest();
            }

        }
        [HttpPost("CreatePlayList")]
        public async Task<ActionResult> CreatePlaylist([FromBody] CreatePlayListDTO handlerDto)
        {
            return Ok(await _playlistHandler.CreatePlaylist(handlerDto));
        }
        [HttpPost("UpdatePlaylist")]
        public async Task<ActionResult> UpdatePlaylist([FromBody] UpdatePlaylistDTO handlerDto)
        {

            return Ok(await _playlistHandler.UpdatePlaylist(handlerDto));
        }
        [HttpPost("PlaylistSearch")]
        public async Task<ActionResult> PlaylistSearch([FromBody] PlaylistSearchDTO playlistName)
        {

            return Ok(await _allplayLists.GetPlaylistBySearch(playlistName.PlaylistName));
        }
        [HttpPost("DeletePlaylist")]
        public async Task<ActionResult> DeletePlaylist([FromBody] DeletePlaylistDTO deleteHandler)
        {
            return Ok(await _playlistHandler.DeletePlaylist(deleteHandler));
        }
        [HttpPost("DeleteFromJustYourLibrary")]
        public async Task<ActionResult> DeleteFromJustLibrary([FromBody] DeletePlaylistDTO deleteHandler)
        {
            return Ok(await _playlistHandler.DeleteFromJustYourLibrary(deleteHandler));
        }
        [HttpPost("IsPlaylistFavorited")]
        public async Task<ActionResult> IsPlaylistFavorited([FromBody] GetPlaylistContentsRequestDTO request)
        {
            return Ok(await _allplayLists.IsPlaylistFavorited(request.UserToken, request.PlaylistId));
        }
        [HttpPost("AddPlaylistToFavorites")]
        public async Task<ActionResult> AddPlaylistToFavorites([FromBody] AddPlaylistToFavoritesDTO aptDTO)
        {
            return Ok(await _playlistHandler.AddToFavorites(aptDTO));
        }
    }
}
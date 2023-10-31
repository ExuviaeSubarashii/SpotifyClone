using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SpotifyClone.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet("GetSuggestedPlayLists")]
        public async Task<ActionResult> GetSuggestedPlayLists(string userToken)
        {
            return Ok(GetSuggestedPlayLists(userToken));
        }
    }

}

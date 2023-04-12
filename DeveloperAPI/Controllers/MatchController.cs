using Domain.DeveloperNS.Interface;
using Domain.HttpService.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchController : ControllerBase
    {
        private readonly IMatchHttpService _matchHttpService;

        public MatchController( IMatchHttpService matchHttpService)
        {
            _matchHttpService = matchHttpService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetMyMatches()
        {
            try
            {
                var userUId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UId")?.Value);
                var organizations = await _matchHttpService.GetMyMatches(userUId);
                return Ok(organizations);
            }
            catch
            {
                return NoContent();
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> MatchOrganization(Guid organizationUId)
        {
            try
            {
                var userUId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UId")?.Value);
                var match = await _matchHttpService.MatchOrganization(userUId, organizationUId);
                return Ok(match);
            }
            catch { return NoContent(); }
        }

        [Authorize]
        [Route("organizations")]
        [HttpGet]
        public async Task<IActionResult> GetOrganizationsToMatch()
        {
            try
            {
                var userUId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UId")?.Value);
                var organizations = await _matchHttpService.GetOrganizationsToMatch(userUId);
                return Ok(organizations);
            }
            catch { return NoContent(); }
        }
    }
}

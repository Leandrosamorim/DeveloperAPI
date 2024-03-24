using Domain.DeveloperNS;
using Domain.DeveloperNS.Command;
using Domain.DeveloperNS.Interface;
using Domain.DeveloperNS.Query;
using Domain.HttpService.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeveloperController : ControllerBase
    {
        private readonly IDeveloperService _developerService;

        public DeveloperController(IDeveloperService developerService)
        {
            _developerService = developerService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create(RegisterDeveloperCmd developer)
        {
            try
            {
                var dev =  await _developerService.Create(developer);
                return Ok(dev);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [EnableCors("AllowSpecificOrigin")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]DeveloperQuery query)
        {
            try
            {
                var devs = await _developerService.Get(query);
                return Ok(devs);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
            
        }

        [EnableCors("AllowSpecificOrigin")]
        [HttpGet]
        [Route("contact")]
        public async Task<IActionResult> GetWithContact([FromQuery] DeveloperQuery query)
        {
            try
            {
                if (query.StackId == null && query.UId == null)
                    query = new DeveloperQuery()
                    {
                        UId = new List<Guid> { Guid.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UId")?.Value) }
                    };

                var devs = await _developerService.GetWithContact(query);
                return Ok(devs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Update(UpdateDeveloperCmd developer)
        {
            try
            {
                var dev = await _developerService.Update(developer);
                return Ok(dev);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid uid)
        {
            try
            {
                await _developerService.Delete(uid);
                return Ok();
            }
            catch
            {
                return BadRequest($"Could not delete {uid}");
            }
        }
    }
}

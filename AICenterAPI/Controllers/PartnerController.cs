using AICenterAPI.Attributes;
using AICenterAPI.Models;
using AICenterAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AICenterAPI.Controllers
{
    [Route("api/partners")]
    [ApiController]
    public class PartnerController : ControllerBase
    {
        private readonly IPartnerService _partnerService;

        public PartnerController(IPartnerService partnerService)
        {
            _partnerService = partnerService;
        }

        // GET: api/<PartnerController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var partners = await _partnerService.GetAllPartnerAsync();

            return Ok(new ApiResponse()
            {
                Data = partners,
                Success = true,
                Message = "Get all partner successfully"
            });
        }

        // GET api/<PartnerController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PartnerController>
        [HttpPost]
        [Authorize]
        [Permission("AboutManager")]
        public async Task<IActionResult> Post([FromForm] CreatePartnerModel model)
        {
            await _partnerService.CreatePartner(model);

            return Ok(new ApiResponse()
            {
                Success = true,
                Message = "Create partner successfully"
            });
        }

        // PUT api/<PartnerController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PartnerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

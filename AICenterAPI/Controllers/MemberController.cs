using AICenterAPI.Attributes;
using AICenterAPI.Models;
using AICenterAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AICenterAPI.Controllers
{
    [Route("api/members")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        // GET: api/<MemberController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var members = await _memberService.GetAllMembers();

            return Ok(new ApiResponse()
            {
                Data = members,
                Success = true,
                Message = "Get all member successfully"
            });
        }

        // GET api/<MemberController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<MemberController>
        [HttpPost]
        [Authorize]
        [Permission("MemberManager")]
        public async Task<IActionResult> Post([FromForm] CreateMemberModel model)
        {
            await _memberService.CreateMember(model);

            return Ok(new ApiResponse()
            {
                Success = true,
                Message = "Create member successfully"
            });
        }

        // PUT api/<MemberController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MemberController>/5
        [HttpDelete("{id}")]
        [Authorize]
        [Permission("MemberManager")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _memberService.Delete(id);
                return Ok(new ApiResponse()
                {
                    Success = true,
                    Message = "Delete member successfully"
                });
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResponse()
                {
                    Success = false,
                    Message = e.Message
                });
            }
        }

        // DELETE api/<MemberController>/multiple
        [HttpDelete("multiple")]
        [Authorize]
        [Permission("MemberManager")]
        public async Task<IActionResult> DeleteMultiple([FromBody] List<int> ids)
        {
            try
            {
                await _memberService.DeleteMultiple(ids);
                return Ok(new ApiResponse()
                {
                    Success = true,
                    Message = "Delete members successfully"
                });
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResponse()
                {
                    Success = false,
                    Message = e.Message
                });
            }
        }
    }
}

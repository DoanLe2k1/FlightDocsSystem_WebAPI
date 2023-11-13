using FlightDocsSystem.Models;
using FlightDocsSystem.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightDocsSystem.Controllers
{
    public class GroupController : Controller
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }
        [HttpGet("Search Group")]
        public async Task<ActionResult<Group>> GetGroup(int groupId)
        {
            var group = await _groupService.GetGroup(groupId);
            if (group == null)
                return NotFound();

            return Ok(group);
        }

        [HttpGet("List Groups")]
        public async Task<ActionResult<List<Group>>> GetAllGroups()
        {
            var groups = await _groupService.GetAllGroups();
            return Ok(groups);
        }
        [Authorize]
        [HttpPost("Add Group"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<int>> AddGroup(Group group)
        {
            var groupId = await _groupService.AddGroup(group);
            return CreatedAtAction(nameof(GetGroup), new { groupId }, groupId);
        }

        [HttpPut("Update Group"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateGroup(int groupId, Group group)
        {
            if (groupId != group.GroupId)
                return BadRequest();

            var result = await _groupService.UpdateGroup(group);
            if (result)
                return NoContent();
            else
                return NotFound();
        }

        [HttpDelete("Delete Group"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteGroup(int groupId)
        {
            var result = await _groupService.DeleteGroup(groupId);
            if (result)
                return NoContent();
            else
                return NotFound();
        }

        
    }
}

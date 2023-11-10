using FlightDocsSystem.Service;
using Microsoft.AspNetCore.Mvc;

namespace FlightDocsSystem.Controllers
{
    public class UserGroupController : Controller
    {
        private readonly IUserGroupService _userGroupService;

        public UserGroupController(IUserGroupService userGroupService)
        {
            _userGroupService = userGroupService;
        }

        [HttpPost("Add User to Group")]
        public IActionResult AddUserToGroup(int userId, int groupId)
        {
            try
            {
                _userGroupService.AddUserToGroup(userId, groupId);
                return Ok("User added to group successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

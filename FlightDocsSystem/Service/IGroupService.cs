using FlightDocsSystem.Models;
using Microsoft.AspNetCore.Identity;

namespace FlightDocsSystem.Service
{
    public interface IGroupService
    {
        Task<int> AddGroup(Group group);
        Task<bool> UpdateGroup(Group group);
        Task<bool> DeleteGroup(int groupId);
        Task<Group> GetGroup(int groupId);
        Task<List<Group>> GetAllGroups();
        Group GetGroupById(int groupId);
    }
}

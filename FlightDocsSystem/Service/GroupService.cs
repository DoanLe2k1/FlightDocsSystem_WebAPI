using FlightDocsSystem.Data;
using FlightDocsSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightDocsSystem.Service
{
    public class GroupService : IGroupService
    {
        private readonly FlightDocsSystemWebAPIDbContext _dbContext;
        public GroupService(FlightDocsSystemWebAPIDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> AddGroup(Group group)
        {
            _dbContext.Groups.Add(group);
            await _dbContext.SaveChangesAsync();
            return group.GroupId;
        }

        public async Task<bool> UpdateGroup(Group group)
        {
            _dbContext.Groups.Update(group);
            var rowsAffected = await _dbContext.SaveChangesAsync();
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteGroup(int groupId)
        {
            var group = await _dbContext.Groups.FindAsync(groupId);
            if (group == null)
                return false;

            _dbContext.Groups.Remove(group);
            var rowsAffected = await _dbContext.SaveChangesAsync();
            return rowsAffected > 0;
        }

        public async Task<Group> GetGroup(int groupId)
        {
            return await _dbContext.Groups.FindAsync(groupId);
        }

        public async Task<List<Group>> GetAllGroups()
        {
            return await _dbContext.Groups.ToListAsync();
        }
        public Group GetGroupById(int groupId)
        {
            return _dbContext.Groups.FirstOrDefault(g => g.GroupId == groupId);
        }

    }
}

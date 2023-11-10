using FlightDocsSystem.Data;
using FlightDocsSystem.Models;

namespace FlightDocsSystem.Service
{
    public class UserGroupService : IUserGroupService
    {
        private readonly FlightDocsSystemWebAPIDbContext _dbContext;
        public UserGroupService(FlightDocsSystemWebAPIDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddUserToGroup(int userId, int groupId)
        {
            // Kiểm tra xem user và group có tồn tại trong cơ sở dữ liệu hay không
            var user = _dbContext.Users.Find(userId);
            var group = _dbContext.Groups.Find(groupId);

            if (user == null || group == null)
                throw new Exception("User or group not found");

            // Tạo một bản ghi mới trong bảng UserGroup
            var userGroup = new UserGroup
            {
                UserId = userId,
                GroupId = groupId
            };

            _dbContext.UserGroups.Add(userGroup);
            _dbContext.SaveChanges();
        }


    }
}

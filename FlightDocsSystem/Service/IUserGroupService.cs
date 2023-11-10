namespace FlightDocsSystem.Service
{
    public interface IUserGroupService
    {
        void AddUserToGroup(int userId, int groupId);
    }
}

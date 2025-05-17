using AICenterAPI.Models;

namespace AICenterAPI.Services
{
    public interface IMemberService
    {
        public Task CreateMember(CreateMemberModel model);

        public Task<List<MemberModel>> GetAllMembers();

        public Task Delete(int id);

        public Task DeleteMultiple(List<int> ids);
    }
}

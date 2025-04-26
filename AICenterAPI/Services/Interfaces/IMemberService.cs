using AICenterAPI.Models;

namespace AICenterAPI.Services
{
    public interface IMemberService
    {
        public Task CreateMember(CreateMemberModel model);

        public Task<List<MemberModel>> GetAllMembers();
    }
}

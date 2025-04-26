using AICenterAPI.Models;

namespace AICenterAPI.Services
{
    public interface IPartnerService
    {
        public Task<List<PartnerViewModel>> GetAllPartnerAsync();

        public Task CreatePartner(CreatePartnerModel model);
    }
}

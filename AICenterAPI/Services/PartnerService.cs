using AICenterAPI.Datas;
using AICenterAPI.Models;
using AICenterAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AICenterAPI.Services
{
    public class PartnerService : IPartnerService
    {
        private readonly IPartnerRepository _partnerRepository;
        private readonly IUploadService _uploadService;
        public PartnerService(IPartnerRepository partnerRepository, IUploadService uploadService)
        {
            _partnerRepository = partnerRepository;    
            _uploadService = uploadService;
        }

        public async Task CreatePartner(CreatePartnerModel model)
        {
            var LogoUrl = "";
            if (model.Logo != null)
            {
                LogoUrl = await _uploadService.SaveImage(model.Logo);
            }
            var partner = new Partner()
            {
                Logo = LogoUrl,
                Name = model.Name,
                Status = model.Status,
                NumberOrder = null
            };
            await _partnerRepository.AddAsync(partner);
        }

        public async Task<List<PartnerViewModel>> GetAllPartnerAsync()
        {
            var partners = await _partnerRepository.GetAllAsync();
            var partnerViewModels = new List<PartnerViewModel>();
            foreach (var item in partners)
            {
                var prtnr = new PartnerViewModel()
                {
                    Id = item.Id,
                    Logo = item.Logo,
                    Name = item.Name,
                    Status = item.Status,
                };
                partnerViewModels.Add(prtnr);
            }
            return partnerViewModels;
        }
    }
}

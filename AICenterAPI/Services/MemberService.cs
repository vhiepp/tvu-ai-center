using AICenterAPI.Datas;
using AICenterAPI.Models;
using AICenterAPI.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;

namespace AICenterAPI.Services
{
    public class MemberService : IMemberService
    {
        private readonly IUploadService _uploadService;
        private readonly IMemberRepository _memberRepository;

        public MemberService(IUploadService uploadService, IMemberRepository memberRepository)
        {
            _uploadService = uploadService;
            _memberRepository = memberRepository;
        }

        public async Task CreateMember(CreateMemberModel model)
        {
            var AvatarUrl = "";
            if (model.Avatar != null)
            {
                AvatarUrl = await _uploadService.SaveImage(model.Avatar);
            }
            var member = new Member()
            {
                Avatar = AvatarUrl,
                DateOfJoin = model.DateOfJoin,
                Description = model.Description,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Facebook = model.Facebook,
                Fullname = model.Fullname,
                Github = model.Github,
                Instagram = model.Instagram,
                Linkedln = model.Linkedln,
                Tiktok = model.Tiktok,
                Pin = model.Pin,
                Position = model.Position,
                Skills = model.Skills,
            };
            await _memberRepository.AddAsync(member);
        }

        public async Task<List<MemberModel>> GetAllMembers()
        {
            var members = await _memberRepository.GetAllAsync();

            var memberModels = new List<MemberModel>();

            foreach (var item in members)
            {
                var mbr = new MemberModel()
                {
                    Id = item.Id,
                    Avatar = item.Avatar,
                    DateOfJoin = item.DateOfJoin,
                    Description = item.Description,
                    Email = item.Email,
                    PhoneNumber = item.PhoneNumber,
                    Facebook = item.Facebook,
                    Fullname = item.Fullname,
                    Github = item.Github,
                    Linkedln = item.Linkedln,
                    Tiktok = item.Tiktok,
                    Pin = item.Pin,
                    Position = item.Position,
                    Skills = item.Skills,
                    Instagram = item.Instagram,
                };
                memberModels.Add(mbr);
            }
            return memberModels;
        }
    }
}

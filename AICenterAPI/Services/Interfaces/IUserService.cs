﻿using AICenterAPI.Datas;
using AICenterAPI.Models;

namespace AICenterAPI.Services
{
    public interface IUserService
    {
        public Task<UserModel?> FindById(int id);

        public Task Create(CreateUserModel createUserModel);

        public Task<List<UserModel>> GetAllAdmin();

        public Task<UserModel?> GetAdminById(int id);

        public Task Update(int id, CreateUserModel updateUserModel);

        public Task Delete(int id);
    }
}

using AICenterAPI.Models;

namespace AICenterAPI.Services
{
    public interface IProductService
    {
        public Task CreateProduct(CreateProductModel model, int userId);

        public Task UpdateProduct(int id, CreateProductModel model);

        public Task<ProductDetailUpdateModel> FindDetailUpdateModel(int id);

        public Task<List<ProductModel>> GetAllProduct(string lang = "vi");

        public Task Delete(int id);

        public Task DeleteMultiple(List<int> ids);
    }
}

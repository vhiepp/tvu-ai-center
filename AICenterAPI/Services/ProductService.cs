using System.Text.Json;
using AICenterAPI.Datas;
using AICenterAPI.Helpers;
using AICenterAPI.Models;
using AICenterAPI.Repositories;
using Microsoft.VisualBasic;

namespace AICenterAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IUploadService _uploadService;
        private readonly IProductRepository _productRepository;
        private readonly IProductContentRepository _productContentRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryContentRepository _categoryContentRepository;
        private readonly IUserRepository _userRepository;
        private readonly SlugGenerator _slugGenerator;  

        public ProductService(
            IUploadService uploadService,
            IProductRepository productRepository,
            IProductContentRepository productContentRepository,
            IProductCategoryRepository productCategoryRepository,
            IUserRepository userRepository,
            ICategoryRepository categoryRepository,
            ICategoryContentRepository categoryContentRepository)
        {
            _uploadService = uploadService;
            _productRepository = productRepository;
            _productContentRepository = productContentRepository;
            _productCategoryRepository = productCategoryRepository;
            _userRepository = userRepository;
            _slugGenerator = new SlugGenerator();
            _categoryRepository = categoryRepository;
            _categoryContentRepository = categoryContentRepository;
        }

        public async Task CreateProduct(CreateProductModel model, int userId)
        {
            var thumb = "";
            if (model.Thumb != null)
            {
                thumb = await _uploadService.SaveImage(model.Thumb);
            }

            var user = await _userRepository.FindByIdAsync(userId);
            List<int> categories = new List<int>();
            if (model.Categories != null)
            {
                JsonDocument doc = JsonDocument.Parse($@"{model.Categories}");
                JsonElement root = doc.RootElement;
                if (root.ValueKind == JsonValueKind.Array)
                {
                    foreach (JsonElement element in root.EnumerateArray())
                    {
                        categories.Add(element.GetInt32());
                    }
                }
            }
            List<ProductContentModel> contents = new List<ProductContentModel>();
            if (model.ProductContents != null)
            {
                JsonDocument doc = JsonDocument.Parse($@"{model.ProductContents}");
                JsonElement root = doc.RootElement;
                if (root.ValueKind == JsonValueKind.Array)
                {
                    foreach (JsonElement element in root.EnumerateArray())
                    {
                        var content = new ProductContentModel()
                        {
                            Title = element.GetProperty("title").GetString(),
                            Language = element.GetProperty("language").GetString(),
                            Content = element.GetProperty("content").GetString(),
                            Slug = element.GetProperty("slug").GetString(),
                        };
                        contents.Add(content);
                    }
                }
            }

            var slug = model.Slug;
            var language = "vi";

            if (contents.Count > 0)
            {
                language = contents[0].Language;
                if (string.IsNullOrEmpty(slug))
                {
                    slug = _slugGenerator.Make(contents[0].Title ?? "product");
                }
            }

            var product = new Product()
            {
                Thumb = thumb,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                PublishedAt = DateTime.Now,
                IsPublished = model.IsPublished,
                Link = model.Link,
                AuthorAvatar = "",
                AuthorName = user.FirstName,
                AuthorId = user.Id,
                AuthorSubtitle = "",
                Slug = slug,
            };

            await _productRepository.AddAsync(product);

            foreach (var item in contents)
            {
                if (string.IsNullOrEmpty(item.Slug))
                {
                    if (item.Language != language)
                    {
                        item.Slug = _slugGenerator.Make(item.Title ?? "product");
                    }
                    else
                    {
                        item.Slug = slug;
                    }
                }

                var productContent = new ProductContent()
                {
                    ProductId = product.Id,
                    Description = item.Content,
                    Language = item.Language,
                    Title = item.Title,
                    Slug = item.Slug,
                };
                await _productContentRepository.AddAsync(productContent);
            }

            foreach (var item in categories)
            {
                var productCategory = new ProductCategory()
                {
                    ProductId = product.Id,
                    CategoryId = item,
                };
                await _productCategoryRepository.AddAsync(productCategory);
            }
        }

        public async Task Delete(int id)
        {
            var product = await _productRepository.FindByIdAsync(id);
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            await _productRepository.DeleteByIdAsync(id);
        }

        public async Task DeleteMultiple(List<int> ids)
        {
            foreach (var id in ids)
            {
                var product = await _productRepository.FindByIdAsync(id);
                if (product == null)
                {
                    throw new Exception("Product not found");
                }
                await _productRepository.DeleteByIdAsync(id);
            }
        }

        public async Task<ProductDetailUpdateModel> FindDetailUpdateModel(int id)
        {
            var product = await _productRepository.FindByIdAsync(id);
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            ProductDetailUpdateModel model = new ProductDetailUpdateModel();
            model.IsPublished = product.IsPublished;
            model.Thumb = product.Thumb;
            model.Link = product.Link;
            var categories = await _productCategoryRepository.GetByProductId(id);
            List<int> ids = new List<int>();
            foreach (var item in categories)
            {
                ids.Add(item.CategoryId);
            }
            model.CategoryIds = ids;
            var productContents = await _productContentRepository.GetByProductId(id);
            List<ProductContentModel> contents = new List<ProductContentModel>();
            foreach (var item in productContents)
            {
                var content = new ProductContentModel()
                {
                    Title = item.Title,
                    Language = item.Language,
                    Content = item.Description,
                    Slug = item.Slug,
                };
                contents.Add(content);
            }
            model.Contents = contents;
            return model;
        }

        public async Task<List<ProductModel>> GetAllProduct(string lang = "vi")
        {
            var product = await _productRepository.GetAllAsync();
            if (product == null)
                return new List<ProductModel>();
            var productModels = new List<ProductModel>();
            foreach (var item in product)
            {
                var productContent = await _productContentRepository.FindByProductIdLanguage(item.Id, lang);
                if (productContent == null)
                    continue;
                var categoryIds = await _productCategoryRepository.GetByProductId(item.Id);
                var categoryModels = new List<CategoryModel>();
                foreach (var categoryId in categoryIds)
                {
                    var category = await _categoryRepository.FindByIdAsync(categoryId.CategoryId);
                    if (category == null)
                        continue;
                    var categoryContent = await _categoryContentRepository.FindByCategoryId(category.Id, lang);
                    if (categoryContent == null)
                        continue;
                    var categoryModel = new CategoryModel()
                    {
                        Id = category.Id,
                        Name = categoryContent.Name,
                        Language = categoryContent.Language,
                        Description = categoryContent.Description,
                    };
                    categoryModels.Add(categoryModel);
                }
                var productModel = new ProductModel()
                {
                    Id = item.Id,
                    Thumb = item.Thumb,
                    CreatedAt = item.CreatedAt,
                    UpdatedAt = item.UpdatedAt,
                    PublishedAt = item.PublishedAt,
                    IsPublished = item.IsPublished,
                    AuthorAvatar = item.AuthorAvatar,
                    AuthorName = item.AuthorName,
                    AuthorSubtitle = item.AuthorSubtitle,
                    Slug = productContent.Slug,
                    Categories = categoryModels,
                    Content = new ProductContentModel()
                    {
                        Title = productContent.Title,
                        Language = productContent.Language,
                        Content = productContent.Description,
                        Slug = productContent.Slug,
                    },
                };
                productModels.Add(productModel);
            }
            return productModels;
        }

        public async Task UpdateProduct(int id, CreateProductModel model)
        {
            var product = await _productRepository.FindByIdAsync(id);
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            var thumb = "";
            if (model.Thumb != null)
            {
                thumb = await _uploadService.SaveImage(model.Thumb);
            }

            List<int> categories = new List<int>();
            if (model.Categories != null)
            {
                await _productCategoryRepository.DeleteByProductId(id);
                JsonDocument doc = JsonDocument.Parse($@"{model.Categories}");
                JsonElement root = doc.RootElement;
                if (root.ValueKind == JsonValueKind.Array)
                {
                    foreach (JsonElement element in root.EnumerateArray())
                    {
                        categories.Add(element.GetInt32());
                    }
                }
            }
            List<ProductContentModel> contents = new List<ProductContentModel>();
            if (model.ProductContents != null)
            {
                await _productContentRepository.DeleteByProductId(id);
                JsonDocument doc = JsonDocument.Parse($@"{model.ProductContents}");
                JsonElement root = doc.RootElement;
                if (root.ValueKind == JsonValueKind.Array)
                {
                    foreach (JsonElement element in root.EnumerateArray())
                    {
                        var content = new ProductContentModel()
                        {
                            Title = element.GetProperty("title").GetString(),
                            Language = element.GetProperty("language").GetString(),
                            Content = element.GetProperty("content").GetString(),
                            Slug = element.GetProperty("slug").GetString(),
                        };
                        contents.Add(content);
                    }
                }
            }
            

            product.Thumb = thumb;
            product.UpdatedAt = DateTime.Now;
            product.IsPublished = model.IsPublished;
            product.Link = model.Link;
            if (!product.IsPublished && model.IsPublished)
            {
                product.PublishedAt = DateTime.Now;
                
            }

            await _productRepository.UpdateAsync(product);

            foreach (var item in contents)
            {
                var productContent = new ProductContent()
                {
                    ProductId = product.Id,
                    Description = item.Content,
                    Language = item.Language,
                    Title = item.Title,
                    Slug = item.Slug,
                };
                await _productContentRepository.AddAsync(productContent);
            }

            foreach (var item in categories)
            {
                var productCategory = new ProductCategory()
                {
                    ProductId = product.Id,
                    CategoryId = item,
                };
                await _productCategoryRepository.AddAsync(productCategory);
            }
        }
    }
}

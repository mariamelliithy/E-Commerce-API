using Shared;

namespace Services.Abstraction
{
    public interface IProductService
    {
        public Task<IEnumerable<ProductResultDto>> GetAllProductsAsync(ProductParametersSpecifications parameters);
        public Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync();
        public Task<IEnumerable<TypeResultDto>> GetAllTypesAsync();
        public Task<ProductResultDto> GetProductByIdAsync(int id);
    }
}

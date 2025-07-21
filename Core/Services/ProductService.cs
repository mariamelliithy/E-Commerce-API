using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Services.Abstraction;
using Services.Specifications;
using Shared;

namespace Services
{
    public class ProductService(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductService
    {
        public async Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync()
        {
            var brands = await _unitOfWork.GetGenericRepository<ProductBrand, int>().GetAllAsync();
            var brandsResult = _mapper.Map<IEnumerable<BrandResultDto>>(brands);
            return brandsResult;
        }

        public async Task<IEnumerable<ProductResultDto>> GetAllProductsAsync(ProductParametersSpecifications parameters)
        {
            var products = await _unitOfWork.GetGenericRepository<Product, int>().GetAllAsync(new ProductWithBrandAndTypeSpecifications(parameters));
            var productsResult = _mapper.Map<IEnumerable<ProductResultDto>>(products);
            return productsResult;
        }

        public async Task<IEnumerable<TypeResultDto>> GetAllTypesAsync()
        {
            var types = await _unitOfWork.GetGenericRepository<ProductType, int>().GetAllAsync();
            var typesResult = _mapper.Map<IEnumerable<TypeResultDto>>(types);
            return typesResult;
        }

        public async Task<ProductResultDto> GetProductByIdAsync(int id)
        {
            var product = await _unitOfWork.GetGenericRepository<Product, int>().GetByIdAsync(new ProductWithBrandAndTypeSpecifications(id));
            var productResult = _mapper.Map<ProductResultDto>(product);
            return productResult;
        }
    }
}

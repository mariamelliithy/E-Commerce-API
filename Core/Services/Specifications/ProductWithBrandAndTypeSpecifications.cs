using Domain.Contracts;
using Domain.Entities;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    internal class ProductWithBrandAndTypeSpecifications : Specifications<Product>
    {
        public ProductWithBrandAndTypeSpecifications(int id) : base(Product => Product.Id == id) 
        {
            AddInclude(Product => Product.ProductBrand);
            AddInclude(Product => Product.ProductType);
        }

        public ProductWithBrandAndTypeSpecifications(ProductParametersSpecifications parameters) 
            : base(Product => 
            (!parameters.BrandId.HasValue || Product.BrandId == parameters.BrandId.Value) &&
            (!parameters.TypeId.HasValue || Product.TypeId == parameters.TypeId.Value)
            )
        {
            AddInclude(Product => Product.ProductBrand);
            AddInclude(Product => Product.ProductType);
            if(parameters.Sort is not null)
            {
                switch (parameters.Sort)
                {
                    case ProductSortOptions.PriceDesc:
                        SetOrderByDescending(p => p.Price); 
                        break;
                    case ProductSortOptions.PriceAsc:
                        SetOrderBy(p => p.Price);
                        break;
                    case ProductSortOptions.NameDesc:
                        SetOrderByDescending(p => p.Name);
                        break;
                    default:
                        SetOrderBy(p => p.Name);
                        break;
                }
            }
        }

    }
}

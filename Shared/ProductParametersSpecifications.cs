﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ProductParametersSpecifications
    {
        public object brandId;

        public int? TypeId { get; set; }
        public int? BrandId { get; set; }
        public ProductSortOptions? Sort { get; set; }
    }
    public enum ProductSortOptions
    {
        NameAsc,
        NameDesc,
        PriceAsc,
        PriceDesc,
    }
}

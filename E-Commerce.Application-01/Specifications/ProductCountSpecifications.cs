using E_Commerce.Application_01.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Entities.Products;

namespace E_Commerce.Application_01.Specifications
{
    public class ProductCountSpecifications : BaseSpecifications<Products, int>
    {
        public ProductCountSpecifications(ProductQueryParams queryParams) : base
        (
            P => (!queryParams.BrandId.HasValue || P.BrandId == queryParams.BrandId.Value)
              && (!queryParams.TypeId.HasValue || P.TypeId == queryParams.TypeId.Value)
              && (string.IsNullOrEmpty(queryParams.SearchValue)
                  || P.Name.ToLower().Contains(queryParams.SearchValue))
        )
        {
        }
    }
}

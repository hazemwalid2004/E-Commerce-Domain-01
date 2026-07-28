using E_Commerce.Application_01.Common;
using E_Commerce.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace E_Commerce.Application_01.Specifications
{
    public class ProductWithBrandAndTypeSpecifications: BaseSpecifications<Products, int>
    {
        // Get All
        public ProductWithBrandAndTypeSpecifications(ProductQueryParams queryParams) : base
            (P => (!queryParams.BrandId.HasValue || P.BrandId == queryParams.BrandId.Value) && (!queryParams.TypeId.HasValue || P.TypeId == queryParams.TypeId.Value) && (string.IsNullOrEmpty(queryParams.SearchValue)
            || P.Name.ToLower().Contains(queryParams.SearchValue)))
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);
        }

        public ProductWithBrandAndTypeSpecifications(int id):base(x=>x.Id ==id) 
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);
        }
    }
}

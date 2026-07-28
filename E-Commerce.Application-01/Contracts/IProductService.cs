using E_Commerce.Application_01.Common;
using E_Commerce.Application_01.DTOS.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application_01.Contracts
{
    public interface IProductService
    {
        Task<Result<IReadOnlyList<ProductDto>>> GetAllProductsAsync(CancellationToken ct = default);

        Task<Result<ProductDto>> GetProductAsync(int id, CancellationToken ct = default);

        Task<Result<IReadOnlyList<BrandDto>>> GetAllBrandsAsync(CancellationToken ct = default);

        Task<Result<IReadOnlyList<TypeDto>>> GetAllTypesAsync(CancellationToken ct = default);
    }
}

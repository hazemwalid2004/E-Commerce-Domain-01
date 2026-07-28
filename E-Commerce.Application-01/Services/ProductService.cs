using AutoMapper;
using E_Commerce.Application_01.Common;
using E_Commerce.Application_01.Contracts;
using E_Commerce.Application_01.DTOS.Products;
using E_Commerce.Application_01.Specifications;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application_01.Services
{
    public class ProductService:IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IReadOnlyList<ProductDto>>> GetAllProductsAsync(ProductQueryParams queryParams , CancellationToken ct = default)
        {
            var Spec = new ProductWithBrandAndTypeSpecifications(queryParams);
            var Repo = _unitOfWork.GetRepository<Products, int>();
            var products = await Repo.GetAllAsync(Spec,ct);
            var Data = _mapper.Map<IReadOnlyList<ProductDto>>(products);

            return Result<IReadOnlyList<ProductDto>>.Ok(Data);
        }

        public async Task<Result<ProductDto>> GetProductAsync(int id, CancellationToken ct = default)
        {
            var Spec = new ProductWithBrandAndTypeSpecifications(id);
            var product = await _unitOfWork.GetRepository<Products, int>().GetByIdAsync(id, ct);

            if (product is null)
            {
                return Result<ProductDto>.Fail( Error.NotFound("Product.NotFound",$"Product With Id: {id} Was Not Found" ));
            }
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<Result<IReadOnlyList<TypeDto>>> GetAllTypesAsync(CancellationToken ct = default)
        {
            var Types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync(ct);

            return Result<IReadOnlyList<TypeDto>>.Ok(_mapper.Map<IReadOnlyList<TypeDto>>(Types));
        }

        public async Task<Result<IReadOnlyList<BrandDto>>> GetAllBrandsAsync(CancellationToken ct = default)
        {
            var Brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync(ct);

            return Result<IReadOnlyList<BrandDto>>.Ok(_mapper.Map<IReadOnlyList<BrandDto>>(Brands));
        }





    }
}

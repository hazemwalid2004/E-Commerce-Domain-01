using AutoMapper;
using E_Commerce.Application_01.Common;
using E_Commerce.Application_01.Contracts;
using E_Commerce.Application_01.DTOS.Baskets;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application_01.Services
{
    public class BasketService(IBasketRepository basketRepository, IMapper mapper) : IBasketService
    {
        public async Task<Result<BasketDto>> CreateOrUpdateBasketAsync(BasketDto basket, CancellationToken ct = default)
        {
            var customerBasket = mapper.Map<CustomerBasket>(basket);

            var basketResult = await basketRepository.CreateOrUpdateBasketAsync(customerBasket, ct: ct);

            return basketResult != null
                ? Result<BasketDto>.Ok(mapper.Map<BasketDto>(basketResult))
                : Result<BasketDto>.Fail(Error.Failure("Can Not Create Or Update Basket"));
        }

        public async Task<Result<bool>> DeleteBasketAsync(string Id, CancellationToken ct = default)
        {
            var result = await basketRepository.DeleteBasketAsync(Id, ct);

            return result? Result<bool>.Ok(true) : Result<bool>.Fail(Error.Failure("Can Not Delete Basket"));
        }

        public async Task<Result<BasketDto>> GetBasketAsync(string Id, CancellationToken ct = default)
        {
            var basket = await basketRepository.GetBasketAsync(Id, ct);

            if (basket == null)
            {
                return Result<BasketDto>.Fail(Error.NotFound("Basket Not Found"));
            }

            return mapper.Map<BasketDto>(basket);
        }
    }
}

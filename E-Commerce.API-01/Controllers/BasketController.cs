using E_Commerce.Application_01.Common;
using E_Commerce.Application_01.Contracts;
using E_Commerce.Application_01.DTOS.Baskets;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API_01.Controllers
{
    public class BasketController(IBasketService basketService) : ApiBaseController
    {
        #region Get

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BasketDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BasketDto>> GetBasket(string Id, CancellationToken ct)
        {
            var basket = await basketService.GetBasketAsync(Id, ct);
            return ToActionResult(basket);
        }



        #endregion

        #region create or update
        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasket(BasketDto basketDto,CancellationToken ct)
        {
            var saved = await basketService.CreateOrUpdateBasketAsync(basketDto, ct);
            return ToActionResult(saved);
        }
        #endregion

        #region Delete

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteBasket(string Id, CancellationToken ct)
        {
            var result = await basketService.DeleteBasketAsync(Id, ct);
            return ToActionResult(result);
        }

        #endregion
    }
}

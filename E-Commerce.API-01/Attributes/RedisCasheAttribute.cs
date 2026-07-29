using E_Commerce.Application_01.Contracts;
using E_Commerce.Application_01.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace E_Commerce.API_01.Attributes
{
    public class RedisCacheAttribute : ActionFilterAttribute
    {
        private readonly int _durationInSec;

        public RedisCacheAttribute(int durationInSec = 90)
        {
            _durationInSec = durationInSec;
        }

        public override async Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            // Get Cache Service From Container [Not Injection Direct Into Constructor]
            var cacheService = context.HttpContext.RequestServices
                .GetRequiredService<ICasheService>();

            var cacheKey = CreateCasheKey(context.HttpContext.Request);

            var cached = await cacheService.GetAsync(cacheKey);

            // If Data Exists In Cache => Get Data From Cache And Skip Endpoint
            if (!string.IsNullOrEmpty(cached))
            {
                context.Result = new ContentResult
                {
                    Content = cached,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK
                };

                return;
            }

            // If Not Exists => Execute Endpoint And Store Result In Cache If Result Is Ok
            var executed = await next();

            if (executed.Result is OkObjectResult { Value: not null } ok)
            {
                await cacheService.SetAsync(
                    cacheKey,
                    ok.Value,
                    TimeSpan.FromSeconds(_durationInSec));
            }
        }

        private string CreateCasheKey(HttpRequest request)
        {
            var key = new StringBuilder();

            key.Append(request.Path).Append('?');

            foreach (var (k, v) in request.Query.OrderBy(q => q.Key))
                key.Append(k).Append('=').Append(v).Append('&');

            return key.ToString();
        }
    }
}

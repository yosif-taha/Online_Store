using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Online_Store.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Store.Presentation.Attributes
{
    public class CacheAttribute(int timeInSec) : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //logic

           var caheServices = context.HttpContext.RequestServices.GetRequiredService<IServicesManager>().CacheService;

            //generate key
          var caheKey =  GetCacheKey(context.HttpContext.Request);
        
            var result = await caheServices.GetAsync(caheKey);

            if(!string.IsNullOrEmpty(result))
            {
                var response = new ContentResult()
                {
                      Content = result,
                       ContentType = "application/json",
                        StatusCode = 200
                };
                context.Result = response;
                return;
            }
           var  actionContext = await next.Invoke();


            if(actionContext.Result is OkObjectResult okObjectResult)
            {
               await caheServices.SetAsync(caheKey,okObjectResult.Value,TimeSpan.FromSeconds(timeInSec));
            }
        }


        private string GetCacheKey(HttpRequest request)
        {
            var key = new StringBuilder();
            key.Append(request.Path);

            foreach(var item in request.Query)
            {
                key.Append($"|{item.Key}-{item.Value}");
            }
            return key.ToString();  
            
        }
    }
}

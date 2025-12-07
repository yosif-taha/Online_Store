using Online_Store.Domain.Exeptions.NotFound;
using Online_Store.Shared.ErrorModels;

namespace Online_Store.Web.Middlewares
{
    public class ErrorHandleMaddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandleMaddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
                if(context.Response.StatusCode == 404)
                {
                    context.Response.ContentType = "application/json";
                    var response = new ErrorDetales()
                    {
                        StatusCode = context.Response.StatusCode,
                        ErrorMessage = $"Endpoint {context.Request.Path} is not found"
                    };
                    await context.Response.WriteAsJsonAsync(response);
                }
            }
            catch (Exception ex)
            {
                //logic

                context.Response.StatusCode = ex switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                    _ => StatusCodes.Status500InternalServerError
                };

                context.Response.ContentType = "application/json";

                var response = new ErrorDetales()
                { 
                  StatusCode = context.Response.StatusCode,
                  ErrorMessage = ex.Message
                };

                await  context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}

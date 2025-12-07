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
            }
            catch (Exception ex)
            {
                //logic

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

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

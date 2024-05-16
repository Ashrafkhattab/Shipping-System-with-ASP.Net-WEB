using System.Net;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Shipping.Errors;

namespace Shipping.MiddlWares
{
    public class ExceptionMiddelware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddelware> logger;
        private readonly IHostEnvironment en;

        public ExceptionMiddelware(RequestDelegate next , ILogger<ExceptionMiddelware> logger , IHostEnvironment en)
        {
            this.next = next;
            this.logger = logger;
            this.en = en;
        }

        
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
               await next(httpContext);
            }
            catch (Exception ex)
            {

                logger.LogError(ex, ex.Message);

                httpContext.Response.ContentType = " application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var respons = en.IsDevelopment() ?
                    new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString()) :
                    new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);

                var Options = new JsonSerializerOptions(){ PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(respons,Options );

                await httpContext.Response.WriteAsync(json);    
            }
        }



    }
}

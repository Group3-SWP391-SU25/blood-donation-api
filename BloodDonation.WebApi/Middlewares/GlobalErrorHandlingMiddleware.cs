using System.Text.Json;

namespace BloodDonation.WebApi.Middlewares;

public class GlobalErrorHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }
    //private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    //{

    //    context.Response.ContentType = "application/json";
    //    context.Response.StatusCode = 500;
    //    return context.Response.WriteAsync("Error: " +exception.Message);

    //}
    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = 500;

        var error = new
        {
            success = false,
            message = exception.Message,
            statusCode = context.Response.StatusCode
        };

        var errorJson = JsonSerializer.Serialize(error);
        return context.Response.WriteAsync(errorJson);
    }
}
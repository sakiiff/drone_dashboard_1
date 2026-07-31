using Microsoft.AspNetCore.Mvc;
using drone_dashboard_1.Exceptions;
using System.Reflection;

namespace drone_dashboard_1.Exceptions;

internal sealed class GlobalExceptionHandler(
    RequestDelegate next,
    ILogger<GlobalExceptionHandler> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch(Exception ex)
        {
            logger.LogError(ex, "Unhandled exception occured");

            context.Response.ContentType = "application/json";

            if (ex is BusinessException businessException)
            {
                context.Response.StatusCode = businessException.StatusCode;

                await context.Response.WriteAsJsonAsync(
                    new ProblemDetails
                    {
                        Type = businessException.GetType().Name,
                        Title = "Business Error Occured",
                        Detail = businessException.Message,
                        Status = businessException.StatusCode,
                        Extensions =
                        {
                            ["errorCode"] = businessException.ErrorCode
                        }
                    });

                return;
            }

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            await context.Response.WriteAsJsonAsync(
                new ProblemDetails
                {
                    Type = ex.GetType().Name,
                    Title = "An unexpected error occured",
                    Detail = "Something went wrong.",
                    Status = StatusCodes.Status500InternalServerError
                });
        }
    }
}

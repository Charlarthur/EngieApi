﻿using EngieApi.Models;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace EngieApi.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILogger logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null) 
                    {
                        logger.LogError($"UnexpectedError : {contextFeature.Error}");

                        await context.Response.WriteAsync(new ErrorDetails()
                        { 
                            StatusCode = context.Response.StatusCode,
                            Message = "Internal Server Error..."
                        }.ToString());
                    }
                }
                );
            });
        }
   }
}
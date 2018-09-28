using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OwlTin.Common.Exceptions;
using Newtonsoft.Json;

namespace PyDeployer.Web.Middleware
{
    public class ErrorHandlingMiddleware
    {

        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        protected Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            if (exception is EntityValidationException)
            {
                return WriteErrorToResponse(context, HttpStatusCode.BadRequest, exception.Message);
            }
            else
            {
                throw exception;
            }
        }

        protected Task WriteErrorToResponse(HttpContext context, HttpStatusCode code, string message)
        {
            var result = JsonConvert.SerializeObject(new
            {
                error = message
            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OwlTin.Common.Exceptions;
using Newtonsoft.Json;

namespace PyDeployer.Web.Middleware
{
    public class ErrorHandlingMiddleware
    {

        private readonly RequestDelegate _next;

        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            this._next = next;
            this._logger = logger;
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

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            if (exception is EntityValidationException)
            {
                var result = new
                {
                    error = exception.Message
                };
                return WriteErrorToResponse(context, HttpStatusCode.BadRequest, result);
            }
            if (exception is Common.Exceptions.EntityValidationException validationException)
            {
                var result = new
                {
                    message = validationException.Message ?? "Validation issues exist.",
                    validationErrors = validationException.ValidationResults
                };
                return WriteErrorToResponse(context, HttpStatusCode.BadRequest, result);
            }
            _logger.LogError(exception, "Error occured processing request.");

            return WriteErrorToResponse(context, HttpStatusCode.InternalServerError, new
            {
                //TODO: Maybe change this message / behaviour  based upon a setting / environment
                message = "An error occured while processing this request"
            });
            
        }

        private Task WriteErrorToResponse(HttpContext context, HttpStatusCode code, object result)
        {
        
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(JsonConvert.SerializeObject(result));
        }

      

    }
}

using Microsoft.AspNetCore.Http;
using GLFManager.App.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace GLFManager.Middleware
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                string errorMessage = null;

                switch (ex)
                {
                    case NotFoundException e:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        errorMessage = e.Message;
                        break;
                    case NoPositionsOpenException e:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        errorMessage = e.Message;
                        break;
                    default: // some unknown error. We want to prevent generic 500 errors from being returned.
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        errorMessage = "We're sorry, your request could not be completed: " + ex.Message;
                        break;
                }

                //Return the response
                var result = JsonSerializer.Serialize(new { message = errorMessage });
                await response.WriteAsync(result);
            }
        }
    }
}

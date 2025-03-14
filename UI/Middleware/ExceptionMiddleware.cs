﻿using UI.Models;
using SH_DataAccessObjects.Common.Exceptions;
using System.Net;

namespace UI.Middleware
{
    public class ExceptionMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try {
                await _next(httpContext);
            }catch (Exception ex) {
                await HandlerExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandlerExceptionAsync(HttpContext httpContext, Exception ex)
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            CustomProblemDetails problem = new();

            switch(ex) {
                case BadRequestException badRequestException:
                    statusCode = HttpStatusCode.BadRequest;
                    problem = new CustomProblemDetails
                    {
                        Title = badRequestException.Message,
                        Status = (int)statusCode,
                        Detail = badRequestException.InnerException?.Message,
                        Type = nameof(BadRequestException),
                        Errors = badRequestException.ValidationErrors ?? new Dictionary<string, string[]>()
                    };

                    break;
                case NotFoundException notFound:
                    statusCode = HttpStatusCode.NotFound;
                    problem = new CustomProblemDetails
                    {
                        Title = notFound.Message,
                        Status = (int)statusCode,
                        Type = nameof(NotFoundException),
                        Detail = notFound.InnerException?.Message
                    };
                    break;
                 case ForbiddenAccessException forbidden:
                    statusCode = HttpStatusCode.Forbidden;
                    problem = new CustomProblemDetails
                    {
                        Title = forbidden.Message,
                        Status = (int)statusCode,
                        Detail = forbidden.InnerException?.Message,
                        Type = nameof(ForbiddenAccessException)
                    };
                    break;
                case UnauthorizedException unauthorized:
                    statusCode = HttpStatusCode.Unauthorized;
                    problem = new CustomProblemDetails
                    {
                        Title = unauthorized.Message,
                        Status = (int)statusCode,
                        Detail = unauthorized.InnerException?.Message,
                        Type = nameof(UnauthorizedException)
                    };
                    break;
                case AuthorizationException authz:
                    statusCode = HttpStatusCode.Forbidden;
                    problem = new CustomProblemDetails
                    {
                        Title = authz.Message,
                        Status = (int)statusCode,
                        Detail = authz.InnerException?.Message,
                        Type = nameof(AuthorizationException)
                    };
                    break;
                default:
                    problem = new CustomProblemDetails
                    {
                        Title = ex.Message,
                        Status = (int)statusCode,
                        Type = nameof(HttpStatusCode.InternalServerError),
                        Detail = ex.StackTrace
                    };
                    break;
            }

            httpContext.Response.StatusCode = (int)statusCode;
            await httpContext.Response.WriteAsJsonAsync(problem);
        }
    }
}

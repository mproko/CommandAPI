using Commander.Dtos;
using FluentValidation;
using System.Net;

namespace Commander.Profiles
{
    public class ValidationMappingMiddleware : IMiddleware
    {
        private readonly ILogger<ValidationMappingMiddleware> _logger;

        public ValidationMappingMiddleware(ILogger<ValidationMappingMiddleware> logger) => 
            _logger = logger;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex, ex.Message);

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var validationFailureResponse = new ValidationFailureResponse
                {
                    Errors = ex.Errors.Select(x => new ValidationResponse
                    {
                        PropertyName = x.PropertyName,
                        Message = x.ErrorMessage
                    })
                };

                await context.Response.WriteAsJsonAsync(validationFailureResponse);

                context.Response.ContentType = "application/json";
            }
        }
    }
}

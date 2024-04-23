using System.Dynamic;
using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AtonWebApi.Filters;

public class ResponseExceptionFilter(ILogger<ResponseExceptionFilter> logger) : ExceptionFilterAttribute
{
    private readonly ILogger<ResponseExceptionFilter> _logger = logger;

    public override void OnException(ExceptionContext context)
    {
        switch (context.Exception)
        {
            case ValidationException validationException:
            {
                var errorList = new List<dynamic>();

                foreach (var error in validationException.Errors)
                {
                    dynamic exception = new ExpandoObject();
                    exception.message = error.ErrorMessage;
                    errorList.Add(exception);
                    
                    logger.LogError($"ErrorMessage: {error.ErrorMessage}");
                }
            
                var result = new ObjectResult(new
                {
                    trace_id = Guid.NewGuid().ToString(),
                    errors = errorList
                })
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            
                context.Result = result;
                break;
            }
            default:
            {
                var errorList = new List<dynamic>();

                dynamic exception = new ExpandoObject();
                exception.message = context.Exception.Message;
                errorList.Add(exception);
            
                logger.LogError($"ErrorMessage: {context.Exception.Message}");
                
                var result = new ObjectResult(new
                {
                    trace_id = Guid.NewGuid().ToString(),
                    errors = errorList
                })
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            
                context.Result = result;
                break;
            }
        }
    }
}
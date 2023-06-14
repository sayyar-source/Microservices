using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ManageMoney.Infrastructure.SystemExceptions
{

    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ILogger<ExceptionMiddleware> logger, AppDBContext dbContext)
        {
            try
            {
                await _next(context);
            }
            catch (SecureException ex)
            {
                var eventId = Guid.NewGuid().ToString();
                var timestamp = DateTime.UtcNow;
                var queryParameters = context.Request.Query.ToDictionary(q => q.Key, q => q.Value.ToString());
                var bodyParameters = context.Request.Body.ToString();//.ToDictionary(x => x.Key, x => x.Value.ToString());
                var stackTrace = ex.StackTrace;


                // Save the exception details to the database table
                dbContext.Add(new ExceptionLog
                {
                    EventId = eventId,
                    Timestamp = timestamp,
                    QueryParameters = queryParameters.Values.ToString(),
                    BodyParameters = bodyParameters,
                    StackTrace = stackTrace
                });
                await dbContext.SaveChangesAsync();

                // Return a response with HTTP status 500 and the exception details in the specified format
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                {
                    type = ex.GetType().Name,
                    id = eventId,
                    data = new
                    {
                        message = ex.Message
                    }
                }));
            }
            catch (Exception ex)
            {
                var eventId = Guid.NewGuid().ToString();
                var timestamp = DateTime.UtcNow;
                var queryParameters = context.Request.Query.ToDictionary(x => x.Key, x => x.Value.ToString());
                var bodyParameters = context.Request.Form.ToDictionary(x => x.Key, x => x.Value.ToString());
                var stackTrace = ex.StackTrace;

                // Save the exception details to the database table
                dbContext.Add(new ExceptionLog
                {
                    EventId = eventId,
                    Timestamp = timestamp,
                    QueryParameters = queryParameters.First().Value,
                    BodyParameters = bodyParameters.First().Value,
                    StackTrace = stackTrace
                });
                await dbContext.SaveChangesAsync();

                // Return a response with HTTP status 500 and the exception details in the specified format
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                {
                    type = "Exception",
                    id = eventId,
                    data = new
                    {
                        message = $"Internal server error ID = {eventId}"
                    }
                }));
            }
        }


    }


}

namespace gui.Middleware
{

    using System.IO;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Options;
    using Microsoft.AspNetCore.Builder;

    public class RequestResponseLoggingOptions
    {
        public string Prefix { get; set; } = string.Empty; // default is empty string
        public bool Enabled { get; set; } = true; // default is true
    }
    public static class RequestResponseLoggingExtensions
    {
        public static IApplicationBuilder UseRequestResponseLogging(this IApplicationBuilder app, Action<RequestResponseLoggingOptions> configureOptions = null)
        {
            if (configureOptions != null)
            {
                var options = new RequestResponseLoggingOptions();
                configureOptions(options);

                // Use this overloaded method to pass the custom options directly
                return app.UseMiddleware<RequestResponseLoggingMiddleware>(options);
            }
            return app.UseMiddleware<RequestResponseLoggingMiddleware>();
        }
    }

    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestResponseLoggingMiddleware> _logger;
        private readonly RequestResponseLoggingOptions _options;

        public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger, IOptions<RequestResponseLoggingOptions> defaultOptions, RequestResponseLoggingOptions overriddenOptions = null)
        {
            _next = next;
            _logger = logger;
            _options = overriddenOptions ?? defaultOptions.Value;
        }
        public async Task Invoke(HttpContext context)
        {
            _logger.LogDebug($"Middleware initialized with Prefix: {_options.Prefix} and Enabled: {_options.Enabled}");

            if (!_options.Enabled)
            {
                await _next(context); // if logging is disabled, just continue
                return;
            }

            var prefix = _options.Prefix.Length == 0 ? string.Empty : _options.Prefix + " ";

            // Log the request
            var request = await FormatRequest(context.Request);
            _logger.LogInformation($"{prefix}Request: {request}");

            // Buffer the response
            var originalBodyStream = context.Response.Body;
            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;

                await _next(context);

                // Revert to the original body stream for reading the response
                context.Response.Body = originalBodyStream;

                // Log the response
                var response = await FormatResponse(context.Response,responseBody);
                _logger.LogInformation($"{prefix}Response: {response}");

                // Copy the response back to the original stream
                responseBody.Position = 0; // ensure starting from the beginning of the stream
                await responseBody.CopyToAsync(originalBodyStream);
                await originalBodyStream.FlushAsync();
            }
        }

        private async Task<string> FormatRequest(HttpRequest request)
        {
            request.EnableBuffering();

            var bodyPosition = request.Body.Position; // Save the current position

            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            var bodyAsText = Encoding.UTF8.GetString(buffer);

            request.Body.Position = bodyPosition; // Restore the position after reading

            return $"{request.Method} {request.Scheme}://{request.Host}{request.Path} {request.QueryString} {bodyAsText}";
        }

        private async Task<string> FormatResponse(HttpResponse response, Stream responseBody)
        {
            responseBody.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(responseBody).ReadToEndAsync();
            responseBody.Seek(0, SeekOrigin.Begin); // Reset position after reading

            return $"{response.StatusCode}: {text}";
        }

    }

}

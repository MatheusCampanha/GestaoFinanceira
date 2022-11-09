using ElmahCore;
using GestaoFinanceira.Domain.Core.Data;
using GestaoFinanceira.Domain.Core.Interfaces;
using GestaoFinanceira.Domain.Core.Logger;
using GestaoFinanceira.Domain.Core.Logger.Interfaces;
using System.Net;
using System.Text;
using System.Text.Json;

namespace GestaoFinanceira.API.Middleware
{
    public class LoggerMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly Settings _settings;

        public LoggerMiddleware(RequestDelegate requestDelegate, Settings settings)
        {
            _requestDelegate = requestDelegate;
            _settings = settings;
        }

        public async Task Invoke(HttpContext httpContext, IElmahRepository elmahLogger,
            ILogRequestResponseRepository logRequestResponseRepository)
        {
            DateTime dataEnvio = DateTime.Now;

            var request = httpContext.Request;
            var requestAsText = FormatRequest(request, _settings.LogRequestHeaders);

            try
            {
                var originalBodyStream = httpContext.Response.Body;

                using var responseBody = new MemoryStream();
                httpContext.Response.Body = responseBody;

                await _requestDelegate(httpContext);

                var responseBodyAsText = await FormatResponse(httpContext.Response);
                await responseBody.CopyToAsync(originalBodyStream);

                SaveLog(httpContext, logRequestResponseRepository, requestAsText, dataEnvio, responseBodyAsText, (long)DateTime.Now.Subtract(dataEnvio).TotalMilliseconds, null);
            }
            catch (Exception e)
            {
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var errorId = elmahLogger.LogError(new Error(e, httpContext));

                SaveLog(httpContext, logRequestResponseRepository, requestAsText, dataEnvio,
                        "Internal Server Error", (long)DateTime.Now.Subtract(dataEnvio).TotalMilliseconds, errorId);
            }

        }
        private void SaveLog(HttpContext httpContext, ILogRequestResponseRepository logRequestResponseRepository, string request, DateTime dataEnvio, string? response, long elapsedTime, string? errorId)
        {
            try
            {
                var logRequestResponse = new LogRequestResponse()
                {
                    MachineName = Environment.MachineName,
                    DataEnvio = dataEnvio,
                    DataRecebimento = DateTime.Now,
                    EndPoint = httpContext.Request.Path,
                    Method = httpContext.Request.Method,
                    StatusCodeResponse = httpContext.Response.StatusCode,
                    Request = request,
                    Response = response,
                    ErrorId = errorId,
                    TempoDuracao = elapsedTime
                };

                if (_settings.ConsoleLog)
                    Console.WriteLine(JsonSerializer.Serialize(logRequestResponse));

                logRequestResponseRepository.Inserir(logRequestResponse);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

        }

        private static string FormatRequest(HttpRequest request, string[] logRequestHeaders)
        {
            #region Read Body
            var RequestBody = new StreamReader(request.BodyReader.AsStream()).ReadToEnd();
            byte[] content1 = Encoding.UTF8.GetBytes(RequestBody.Replace("string", "NoString"));
            var bodyAsText = Encoding.Default.GetString(content1);

            //Retorno body
            var requestBodyStream = new MemoryStream();
            requestBodyStream.Seek(0, SeekOrigin.Begin);
            requestBodyStream.Write(content1, 0, content1.Length);
            request.Body = requestBodyStream;
            request.Body.Seek(0, SeekOrigin.Begin);
            #endregion

            return $"Http Request Information: {Environment.NewLine}" +
                        $"Schema:{request.Scheme} {Environment.NewLine}" +
                        $"Host: {request.Host} {Environment.NewLine}" +
                        $"Path: {request.Path} {Environment.NewLine}" +
                        $"Header: {ObterHeaders(request.Headers, logRequestHeaders)} {Environment.NewLine}" +
                        $"QueryString: {request.QueryString} {Environment.NewLine}" +
                        $"Request Body: {bodyAsText}";
        }

        private static async Task<string> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            string text = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);
            return text;
        }
        private static string ObterHeaders(IHeaderDictionary headers, string[] logRequestHeaders)
        {
            List<string> list = new();
            foreach (var key in logRequestHeaders)
            {
                if (!string.IsNullOrEmpty(headers[key]))
                    list.Add($"{key} = {headers[key]}");
            }

            return string.Join(',', list);
        }

    }

    public static class LoggerMiddlewareExtensions
    {
        public static IApplicationBuilder UseLoggerMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<LoggerMiddleware>();
            return app;
        }
    }
}

using System.Text;
using System.Text.Json;

namespace GestaoFinanceira.Infra.Data.Helpers
{
    public class RestFullHelper : IRestFullHelper
    {
        public async Task<HttpResponseMessage> Get(string baseUrlServico, string urlServico, string? schemaAuth = null, string? token = null, int? secondsTimeOut = null)
        {
            try
            {
                using HttpClient client = new(new HttpClientHandler());
                if (secondsTimeOut.HasValue)
                    client.Timeout = new TimeSpan(0, 0, secondsTimeOut.Value);

                client.DefaultRequestHeaders.Clear();

                if (!string.IsNullOrEmpty(token))
                    client.DefaultRequestHeaders.Add("Token", token);

                HttpRequestMessage request = new()
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(baseUrlServico + urlServico)
                };

                return await client.SendAsync(request);
            }
            catch (OperationCanceledException)
            {
                var retorno = new HttpResponseMessage(System.Net.HttpStatusCode.RequestTimeout)
                {
                    Content = new StringContent("Timeout na comunicação com DICT", Encoding.Unicode)
                };

                return retorno;
            }
            catch (Exception e)
            {
                throw new HttpRequestException($"{e.GetType()}: {e.Message}");
            }
        }

        public async Task<HttpResponseMessage> Get(string baseUrlServico, string urlServico, object requestBody, string? schemaAuth = null, string? token = null, int? secondsTimeOut = null)
        {
            try
            {
                string json = JsonSerializer.Serialize(requestBody);
                using HttpClient client = new(new HttpClientHandler());
                if (secondsTimeOut.HasValue)
                    client.Timeout = new TimeSpan(0, 0, secondsTimeOut.Value);

                client.DefaultRequestHeaders.Clear();

                if (!string.IsNullOrEmpty(token))
                    client.DefaultRequestHeaders.Add("Token", token);

                HttpRequestMessage request = new()
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(baseUrlServico + urlServico),
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                };

                return await client.SendAsync(request);
            }
            catch (OperationCanceledException)
            {
                var retorno = new HttpResponseMessage(System.Net.HttpStatusCode.RequestTimeout)
                {
                    Content = new StringContent("Timeout na comunicação com DICT", Encoding.Unicode)
                };

                return retorno;
            }
            catch (Exception e)
            {
                throw new HttpRequestException($"{e.GetType()}: {e.Message}");
            }
        }

        public async Task<HttpResponseMessage> Post(string baseUrlServico, string urlServico, object requestBody, string? schemaAuth = null, string? token = null, int? secondsTimeOut = null)
        {
            try
            {
                string json = JsonSerializer.Serialize(requestBody);
                HttpContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                using HttpClient client = new(new HttpClientHandler());
                if (secondsTimeOut.HasValue)
                    client.Timeout = new TimeSpan(0, 0, secondsTimeOut.Value);

                client.DefaultRequestHeaders.Clear();

                if (!string.IsNullOrEmpty(token))
                    client.DefaultRequestHeaders.Add("Token", token);

                return await client.PostAsync(new Uri(baseUrlServico + urlServico), httpContent);
            }
            catch (OperationCanceledException)
            {
                var retorno = new HttpResponseMessage(System.Net.HttpStatusCode.RequestTimeout)
                {
                    Content = new StringContent("Timeout na comunicação com DICT", Encoding.Unicode)
                };

                return retorno;
            }
            catch (Exception e)
            {
                throw new HttpRequestException($"{e.GetType()}: {e.Message}");
            }
        }

        public async Task<HttpResponseMessage> Put(string baseUrlServico, string urlServico, object requestBody, string? schemaAuth = null, string? token = null, int? secondsTimeOut = null)
        {
            try
            {
                string json = JsonSerializer.Serialize(requestBody);
                HttpContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                using HttpClient client = new(new HttpClientHandler());
                if (secondsTimeOut.HasValue)
                    client.Timeout = new TimeSpan(0, 0, secondsTimeOut.Value);

                client.DefaultRequestHeaders.Clear();

                if (!string.IsNullOrEmpty(token))
                    client.DefaultRequestHeaders.Add("Token", token);

                var teste = await client.PutAsync(new Uri(baseUrlServico + urlServico), httpContent);
                return teste;
            }
            catch (OperationCanceledException)
            {
                var retorno = new HttpResponseMessage(System.Net.HttpStatusCode.RequestTimeout)
                {
                    Content = new StringContent("Timeout na comunicação com DICT", Encoding.Unicode)
                };

                return retorno;

            }
            catch (Exception e)
            {
                throw new HttpRequestException($"{e.GetType()}: {e.Message}");
            }
        }

        public async Task<HttpResponseMessage> Delete(string baseUrlServico, string urlServico, object requestBody, string? schemaAuth = null, string? token = null, int? secondsTimeOut = null)
        {
            try
            {
                string json = JsonSerializer.Serialize(requestBody);

                using HttpClient client = new(new HttpClientHandler());
                if (secondsTimeOut.HasValue)
                    client.Timeout = new TimeSpan(0, 0, secondsTimeOut.Value);

                client.DefaultRequestHeaders.Clear();

                if (!string.IsNullOrEmpty(token))
                    client.DefaultRequestHeaders.Add("Token", token);

                HttpRequestMessage request = new()
                {
                    Method = HttpMethod.Delete,
                    RequestUri = new Uri(baseUrlServico + urlServico),
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                };

                return await client.SendAsync(request);
            }
            catch (OperationCanceledException)
            {
                var retorno = new HttpResponseMessage(System.Net.HttpStatusCode.RequestTimeout)
                {
                    Content = new StringContent("Timeout na comunicação com DICT", Encoding.Unicode)
                };

                return retorno;

            }
            catch (Exception e)
            {
                throw new HttpRequestException($"{e.GetType()}: {e.Message}");
            }
        }

        public TEntity Deserialize<TEntity>(string json)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return JsonSerializer.Deserialize<TEntity>(json, options)!;
        }
    }
}

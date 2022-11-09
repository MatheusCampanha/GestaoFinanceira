namespace GestaoFinanceira.Infra.Data.Helpers
{
    public interface IRestFullHelper
    {
        Task<HttpResponseMessage> Get(string baseUrlServico, string urlServico, string? schemaAuth = null, string? token = null, int? secondsTimeOut = null);
        Task<HttpResponseMessage> Get(string baseUrlServico, string urlServico, object requestBody, string? schemaAuth = null, string? token = null, int? secondsTimeOut = null);

        Task<HttpResponseMessage> Post(string baseUrlServico, string urlServico, object requestBody, string? schemaAuth = null, string? token = null, int? secondsTimeOut = null);

        Task<HttpResponseMessage> Put(string baseUrlServico, string urlServico, object requestBody, string? schemaAuth = null, string? token = null, int? secondsTimeOut = null);

        Task<HttpResponseMessage> Delete(string baseUrlServico, string urlServico, object requestBody, string? schemaAuth = null, string? token = null, int? secondsTimeOut = null);

        TEntity Deserialize<TEntity>(string json);
    }
}

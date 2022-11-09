using GestaoFinanceira.Domain.Core.Notifications;
using System.Text.Json.Serialization;

namespace GestaoFinanceira.Domain.Core.Queries
{
    public class QueryResult<T> : Notifiable where T : class
    {
        [JsonIgnore]
        public readonly string _dominio = typeof(T).Name.ToString().Replace("QueryResult", "");

        public QueryResult(T registro)
        {
            Registros = new List<T> { registro };
        }

        public QueryResult(ICollection<T> registros)
        {
            Registros = registros;
        }

        public QueryResult(int statusCode, T registro)
        {
            StatusCode = statusCode;
            Registros = new List<T> { registro };

            if (registro == null)
            {
                StatusCode = 422;
                AddNotification(_dominio, $"Dados de {_dominio} não encontrados");
            }
        }

        public QueryResult(int statusCode, ICollection<T> registros)
        {
            StatusCode = statusCode;
            Registros = registros;

            if (registros.Count == 0)
            {
                StatusCode = 422;
                AddNotification(_dominio, $"Dados de {_dominio} não encontrados");
            }
        }

        [JsonIgnore]
        public int StatusCode { get; set; }
        public ICollection<T> Registros { get; set; }
    }
}

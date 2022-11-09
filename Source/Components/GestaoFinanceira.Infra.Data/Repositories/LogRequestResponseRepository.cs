using Dapper;
using GestaoFinanceira.Domain.Core.Logger.Interfaces;
using GestaoFinanceira.Domain.Core.Logger;
using System.Data.SqlClient;
using System.Data;
using GestaoFinanceira.Domain.Core.Data;
using GestaoFinanceira.Infra.Data.Repositories.DicQueries;

namespace GestaoFinanceira.Infra.Data.Repositories
{
    public class LogRequestResponseRepository : ILogRequestResponseRepository
    {
        private readonly Settings _settings;

        public LogRequestResponseRepository(Settings settings)
        {
            _settings = settings;
        }

        public async Task Inserir(LogRequestResponse entidade)
        {
            using var connection = new SqlConnection(_settings.ConnectionStrings.DBGestaoFinanceira);
            var param = new DynamicParameters();
            param.Add("@MachineName", entidade.MachineName);
            param.Add("@DataEnvio", entidade.DataEnvio);
            param.Add("@DataRecebimento", entidade.DataRecebimento);
            param.Add("@EndPoint", entidade.EndPoint);
            param.Add("@Method", entidade.Method);
            param.Add("@StatusCodeResponse", entidade.StatusCodeResponse);
            param.Add("@Request", entidade.Request);
            param.Add("@Response", entidade.Response);
            param.Add("@ErrorId", entidade.ErrorId);
            param.Add("@CorrelationId", entidade.CorrelationId);
            param.Add("@TempoDuracao", entidade.TempoDuracao);

            await connection.ExecuteAsync(LogRequestResponseRepositoryDicQueries.Inserir, param, commandType: CommandType.Text);
        }
    }
}

using ElmahCore.Sql;
using ElmahCore;
using GestaoFinanceira.Domain.Core.Interfaces;
using GestaoFinanceira.Domain.Core.Data;

namespace GestaoFinanceira.Infra.Data.Repositories
{
    public class ElmahRepository : IElmahRepository
    {
        private readonly ErrorLog _errorLog;

        public ElmahRepository(Settings settings)
        {
            _errorLog = new SqlErrorLog(settings.ConnectionStrings.DBGestaoFinanceira)
            {
                ApplicationName = settings.ApplicationName
            };
        }
        public string LogError(Error error)
        {
            try
            {
                return _errorLog.Log(error);
            }
            catch (Exception ex)
            {

                throw new SystemException(ex.Message);
            }

        }
    }
}

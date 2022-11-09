namespace GestaoFinanceira.Infra.Data.Repositories.DicQueries
{
    public static class LogRequestResponseRepositoryDicQueries
    {
        #region Inserir
        public const string Inserir = @"
          INSERT INTO LogRequestResponse
                        (DataEnvio
                        ,DataRecebimento
                        ,EndPoint
                        ,Method
                        ,StatusCodeResponse
                        ,Request
                        ,Response
                        ,TempoDuracao
                        ,MachineName
                        ,ErrorId
                        ,CorrelationId)
                  VALUES
                        (@DataEnvio
                        ,@DataRecebimento
                        ,@EndPoint
                        ,@Method
                        ,@StatusCodeResponse
                        ,@Request
                        ,@Response
                        ,@TempoDuracao
                        ,@MachineName
                        ,@ErrorId
                        ,@CorrelationId)";
        #endregion
    }
}

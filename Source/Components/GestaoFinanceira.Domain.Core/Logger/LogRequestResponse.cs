namespace GestaoFinanceira.Domain.Core.Logger
{
    public class LogRequestResponse
    {
        public int LogRequestResponseId { get; set; }
        public string MachineName { get; set; } = default!;
        public DateTime DataEnvio { get; set; }
        public DateTime DataRecebimento { get; set; }
        public string EndPoint { get; set; } = default!;
        public string Method { get; set; } = default!;
        public int StatusCodeResponse { get; set; }
        public string Request { get; set; } = default!;
        public string? Response { get; set; }
        public string? ErrorId { get; set; }
        public string CorrelationId { get; set; } = default!;
        public long? TempoDuracao { get; set; }
    }
}

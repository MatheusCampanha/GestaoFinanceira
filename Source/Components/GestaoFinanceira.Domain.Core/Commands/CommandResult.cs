using GestaoFinanceira.Domain.Core.Notifications;
using System.Text.Json.Serialization;

namespace GestaoFinanceira.Domain.Core.Commands
{
    public class CommandResult : Notifiable
    {
        public CommandResult(int statusCode)
        {
            StatusCode = statusCode;
        }

        public CommandResult(int statusCode, Notifiable item)
        {
            StatusCode = statusCode;
            AddNotifications(item);
        }

        [JsonIgnore]
        public int StatusCode { get; set; }
    }
}

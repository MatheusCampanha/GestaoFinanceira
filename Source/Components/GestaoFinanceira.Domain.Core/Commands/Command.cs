using GestaoFinanceira.Domain.Core.Notifications;

namespace GestaoFinanceira.Domain.Core.Commands
{
    public abstract class Command : Notifiable
    {
        public abstract bool IsValid();
    }
}

using ElmahCore;

namespace GestaoFinanceira.Domain.Core.Interfaces
{
    public interface IElmahRepository
    {
        string LogError(Error error);
    }
}

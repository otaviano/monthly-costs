using MonthlyCosts.Domain.Core.Commands;

namespace MonthlyCosts.Domain.Core.Bus
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : Command;
    }
}

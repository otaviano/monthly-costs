using MonthlyCosts.Domain.Core.Commands;
using MonthlyCosts.Domain.Core.Events;

namespace MonthlyCosts.Domain.Core.Bus
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : Command;
        Task SendEvent<T>(T @event) where T : Event<T>;
    }
}

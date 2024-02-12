using MediatR;
using MonthlyCosts.Domain.Core.Bus;
using MonthlyCosts.Domain.Core.Commands;

namespace MonthlyCosts.Infra.Bus;

public sealed class InMemoryBus : IMediatorHandler
{
    private readonly IMediator mediator;

    public InMemoryBus(IMediator mediator)
    {
        this.mediator = mediator;
    }

    public Task SendCommand<T>(T command) where T : Command
    {
        return mediator.Send(command);
    }
}

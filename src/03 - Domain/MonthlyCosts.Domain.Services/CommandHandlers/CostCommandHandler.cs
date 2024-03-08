using AutoMapper;
using MediatR;
using MonthlyCosts.Domain.Commands;
using MonthlyCosts.Domain.Core.Bus;
using MonthlyCosts.Domain.Core.CommandHandlers;
using MonthlyCosts.Domain.Entities;
using MonthlyCosts.Domain.Events;
using MonthlyCosts.Domain.Interfaces;

namespace MonthlyCosts.Domain.Services.CommandHandlers;

public class CostCommandHandler : CommandHandler, 
    IRequestHandler<CreateCostCommand, Unit>,
    IRequestHandler<UpdateCostCommand, Unit>,
    IRequestHandler<DeleteCostCommand, Unit>
{
    protected readonly IMapper _mapper;
    protected readonly ICostSqlRepository _costRepository;
    public readonly IMessageBusPublisher _messageBusPublisher;

    public CostCommandHandler(
        IMapper mapper,
        ICostSqlRepository costRepository,
        IMessageBusPublisher messageBusPublisher)
    {
        _mapper = mapper;
        _messageBusPublisher = messageBusPublisher;
        _costRepository = costRepository;
    }
    
    public async Task<Unit> Handle(CreateCostCommand request, CancellationToken cancellationToken)
    {
        ValidateAndThrow(request);

        var cost = _mapper.Map<Cost>(request);
        var @event = _mapper.Map<CreateCostEvent>(request);
        await _costRepository.Create(cost);
        await _messageBusPublisher.SendMessageAsync(@event);

        return await Unit.Task;
    }

    public async Task<Unit> Handle(UpdateCostCommand request, CancellationToken cancellationToken)
    {
        ValidateAndThrow(request);

        var cost = _mapper.Map<Cost>(request);
        var @event = _mapper.Map<UpdateCostEvent>(request);
        await _costRepository.Update(cost);
        await _messageBusPublisher.SendMessageAsync(@event);

        return await Unit.Task;
    }

    public async Task<Unit> Handle(DeleteCostCommand request, CancellationToken cancellationToken)
    {
        ValidateAndThrow(request);

        await _costRepository.Delete(request.Id);
        var @event = _mapper.Map<DeleteCostEvent>(request);
        await _messageBusPublisher.SendMessageAsync(@event);

        return await Unit.Task;
    }
}

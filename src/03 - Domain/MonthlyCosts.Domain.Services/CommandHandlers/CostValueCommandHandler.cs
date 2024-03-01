using AutoMapper;
using MediatR;
using MonthlyCosts.Domain.Commands;
using MonthlyCosts.Domain.Core.Bus;
using MonthlyCosts.Domain.Core.CommandHandlers;
using MonthlyCosts.Domain.Entities;
using MonthlyCosts.Domain.Events;
using MonthlyCosts.Domain.Interfaces;

namespace MonthlyCosts.Domain.Services.CommandHandlers;

public class CostValueCommandHandler : CommandHandler, 
    IRequestHandler<CreateCostValueCommand, Unit>,
    IRequestHandler<DeleteCostValueCommand, Unit>
{
    protected readonly IMapper _mapper;
    protected readonly ICostValueSqlRepository _costValueRepository;
    public readonly IRabbitMQEventBus _eventBus;

    public CostValueCommandHandler(
        IMapper mapper,
        IRabbitMQEventBus eventBus,
        ICostValueSqlRepository costValueRepository)
    {
        _mapper = mapper;
        _eventBus = eventBus;
        _costValueRepository = costValueRepository;
    }
    
    public async Task<Unit> Handle(CreateCostValueCommand request, CancellationToken cancellationToken)
    {
        ValidateAndThrow(request);
        
        var costValue = _mapper.Map<CostValue>(request);
        var @event = _mapper.Map<CreateCostValueEvent>(request);
        await _costValueRepository.Create(costValue);
        _eventBus.Publish(@event);

        return await Unit.Task;
    }
    public async Task<Unit> Handle(DeleteCostValueCommand request, CancellationToken cancellationToken)
    {
        ValidateAndThrow(request);
        await _costValueRepository.Delete(request.Id);
        var @event = _mapper.Map<DeleteCostValueEvent>(request);
        _eventBus.Publish(@event);

        return await Unit.Task;
    }
}

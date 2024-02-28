using AutoMapper;
using MediatR;
using MonthlyCosts.Domain.Commands;
using MonthlyCosts.Domain.Core.Bus;
using MonthlyCosts.Domain.Core.CommandHandlers;
using MonthlyCosts.Domain.Core.Events;
using MonthlyCosts.Domain.Entities;
using MonthlyCosts.Domain.Events;
using MonthlyCosts.Domain.Interfaces;

namespace MonthlyCosts.Domain.Services.CommandHandlers;

public class CostValueCommandHandler : CommandHandler, 
    IRequestHandler<CreateCostValueCommand, Unit>,
    IRequestHandler<UpdateCostValueCommand, Unit>,
    IRequestHandler<DeleteCostValueCommand, Unit>
{
    protected readonly IMapper _mapper;
    //protected readonly ICostValueRepository costRepository;
    public readonly IRabbitMQEventBus _eventBus;

    public CostValueCommandHandler(
        IMapper mapper,
        //ICostValueRepository costRepository, 
        IRabbitMQEventBus eventBus)
    {
        _mapper = mapper;
        //_costRepository = costRepository;
        _eventBus = eventBus;
    }
    
    public async Task<Unit> Handle(CreateCostValueCommand request, CancellationToken cancellationToken)
    {
        ValidateAndThrow(request);

        var @event = _mapper.Map<CreateCostValueEvent>(request);

        _eventBus.Publish(@event);

        return await Unit.Task;
    }

    public async Task<Unit> Handle(UpdateCostValueCommand request, CancellationToken cancellationToken)
    {
        ValidateAndThrow(request);

        var @event = _mapper.Map<UpdateCostValueEvent>(request);

        _eventBus.Publish(@event);

        return await Unit.Task;
    }

    public async Task<Unit> Handle(DeleteCostValueCommand request, CancellationToken cancellationToken)
    {
        ValidateAndThrow(request);
        
        var @event = _mapper.Map<DeleteCostValueEvent>(request);
        _eventBus.Publish(@event);

        return await Unit.Task;
    }
}

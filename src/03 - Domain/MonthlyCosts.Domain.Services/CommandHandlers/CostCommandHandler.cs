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
    //protected readonly ICostRepository costRepository;
    public readonly ICostNoSqlRepository _costNoSqlRepository;
    protected readonly IRabbitMQEventBus eventBus;
    public CostCommandHandler(
        IMapper mapper,
        //ICostRepository costRepository, 
        IRabbitMQEventBus eventBus,
        ICostNoSqlRepository costNoSqlRepository)
    {
        _mapper = mapper;
        this.eventBus = eventBus;
        //_costRepository = costRepository;
        _costNoSqlRepository = costNoSqlRepository;
    }
    
    public async Task<Unit> Handle(CreateCostCommand request, CancellationToken cancellationToken)
    {
        ValidateAndThrow(request);

        var cost = _mapper.Map<Cost>(request);
        var @event = _mapper.Map<CreateCostEvent>(request);

        //await costSqlRepository.Create(cost);
        eventBus.Publish(@event);

        return await Unit.Task;
    }

    public async Task<Unit> Handle(UpdateCostCommand request, CancellationToken cancellationToken)
    {
        ValidateAndThrow(request);

        var cost = _mapper.Map<Cost>(request);
        var @event = _mapper.Map<UpdateCostEvent>(request);

        //await costRepository.Update(cost);
        eventBus.Publish(@event);

        return await Unit.Task;
    }

    public async Task<Unit> Handle(DeleteCostCommand request, CancellationToken cancellationToken)
    {
        ValidateAndThrow(request);

        //await costRepository.Delete(request.Id);
        var @event = _mapper.Map<DeleteCostEvent>(request);
        eventBus.Publish(@event);

        return await Unit.Task;
    }
}

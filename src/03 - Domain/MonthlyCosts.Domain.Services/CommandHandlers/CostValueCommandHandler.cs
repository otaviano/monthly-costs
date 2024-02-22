using AutoMapper;
using MediatR;
using MonthlyCosts.Domain.Commands;
using MonthlyCosts.Domain.Core.CommandHandlers;
using MonthlyCosts.Domain.Entities;
using MonthlyCosts.Domain.Interfaces;

namespace MonthlyCosts.Domain.Services.CommandHandlers;

public class CostValueCommandHandler : CommandHandler, 
    IRequestHandler<CreateCostValueCommand, Unit>,
    IRequestHandler<UpdateCostValueCommand, Unit>,
    IRequestHandler<DeleteCostValueCommand, Unit>
{
    protected readonly IMapper _mapper;
    //protected readonly ICostValueRepository costRepository;
    public readonly ICostValueNoSqlRepository _costNoSqlRepository;

    public CostValueCommandHandler(
        IMapper mapper,
        //ICostRepository costRepository, 
        ICostValueNoSqlRepository costValueNoSqlRepository)
    {
        _mapper = mapper;
        //_costRepository = costRepository;
        _costNoSqlRepository = costValueNoSqlRepository;
    }
    
    public async Task<Unit> Handle(CreateCostValueCommand request, CancellationToken cancellationToken)
    {
        ValidateAndThrow(request);

        var cost = _mapper.Map<CostValue>(request);

        //await homeBrokerRepository.Create(homeBroker);
        await _costNoSqlRepository.Create(cost);

        return await Unit.Task;
    }

    public async Task<Unit> Handle(UpdateCostValueCommand request, CancellationToken cancellationToken)
    {
        ValidateAndThrow(request);

        var cost = _mapper.Map<CostValue>(request);

        //await homeBrokerRepository.Update(homeBroker);
        await _costNoSqlRepository.Update(cost);

        return await Unit.Task;
    }

    public async Task<Unit> Handle(DeleteCostValueCommand request, CancellationToken cancellationToken)
    {
        ValidateAndThrow(request);
        await _costNoSqlRepository.Delete(request.Id);

        return await Unit.Task;
    }
}

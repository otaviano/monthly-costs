using AutoMapper;
using MediatR;
using MonthlyCosts.Domain.Commands;
using MonthlyCosts.Domain.Core.CommandHandlers;
using MonthlyCosts.Domain.Entities;
using MonthlyCosts.Domain.Interfaces;

namespace MonthlyCosts.Domain.Services.CommandHandlers;

public class CostCommandHandler : CommandHandler, 
    IRequestHandler<CreateCostCommand, Unit>
{
    protected readonly IMapper _mapper;
    //protected readonly ICostRepository costRepository;
    protected readonly ICostNoSqlRepository _costNoSqlRepository;

    public CostCommandHandler(
        IMapper mapper,
        //ICostRepository costRepository, 
        ICostNoSqlRepository homeBrokerNoSqlRepository)
    {
        _mapper = mapper;
        //_costRepository = costRepository;
        _costNoSqlRepository = homeBrokerNoSqlRepository;
    }
    
    public async Task<Unit> Handle(CreateCostCommand request, CancellationToken cancellationToken)
    {
        ValidateAndThrow(request);

        var cost = _mapper.Map<Cost>(request);

        //await homeBrokerRepository.Create(homeBroker);
        await _costNoSqlRepository.Create(cost);

        return await Unit.Task;
    }
}

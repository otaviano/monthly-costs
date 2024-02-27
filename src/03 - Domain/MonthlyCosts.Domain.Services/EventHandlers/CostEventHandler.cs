using AutoMapper;
using MediatR;
using MonthlyCosts.Domain.Entities;
using MonthlyCosts.Domain.Events;
using MonthlyCosts.Domain.Interfaces;

namespace MonthlyCosts.Domain.Services.EventHandlers
{
    public class CostEventHandler : IRequestHandler<CreateCostEvent>
    {
        protected readonly IMapper _mapper;
        public readonly ICostNoSqlRepository _costNoSqlRepository;

        public CostEventHandler(
           IMapper mapper,
           ICostNoSqlRepository costNoSqlRepository)
        {
            _mapper = mapper;
            _costNoSqlRepository = costNoSqlRepository;
        }

        public async Task Handle(CreateCostEvent request, CancellationToken cancellationToken)
        {
            var cost = _mapper.Map<Cost>(request);

            await _costNoSqlRepository.Create(cost);
        }
    }
}

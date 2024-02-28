using AutoMapper;
using MediatR;
using MonthlyCosts.Domain.Entities;
using MonthlyCosts.Domain.Events;
using MonthlyCosts.Domain.Interfaces;

namespace MonthlyCosts.Domain.Services.EventHandlers
{
    public class CostEventHandler : 
        IRequestHandler<CreateCostEvent>,
        IRequestHandler<UpdateCostEvent>,
        IRequestHandler<DeleteCostEvent>
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

        public async Task Handle(UpdateCostEvent request, CancellationToken cancellationToken)
        {
            var cost = _mapper.Map<Cost>(request);

            await _costNoSqlRepository.Update(cost);
        }


        public async Task Handle(DeleteCostEvent request, CancellationToken cancellationToken)
        {
            await _costNoSqlRepository.Delete(request.Id);
        }
    }
}

using AutoMapper;
using MediatR;
using MonthlyCosts.Domain.Commands;
using MonthlyCosts.Domain.Entities;
using MonthlyCosts.Domain.Events;
using MonthlyCosts.Domain.Interfaces;

namespace MonthlyCosts.Domain.Services.EventHandlers
{
    public class CostValueEventHandler : 
        IRequestHandler<CreateCostValueEvent>,
        IRequestHandler<UpdateCostValueEvent>,
        IRequestHandler<DeleteCostValueEvent>
    {
        protected readonly IMapper _mapper;
        public readonly ICostValueNoSqlRepository _costValueNoSqlRepository;

        public CostValueEventHandler(
           IMapper mapper,
           ICostValueNoSqlRepository costValueNoSqlRepository)
        {
            _mapper = mapper;
            _costValueNoSqlRepository = costValueNoSqlRepository;
        }

        public async Task Handle(CreateCostValueEvent request, CancellationToken cancellationToken)
        {
            var costValue = _mapper.Map<CostValue>(request);
            await _costValueNoSqlRepository.Create(costValue);
        }

        public async Task Handle(UpdateCostValueEvent request, CancellationToken cancellationToken)
        {
            var costValue = _mapper.Map<CostValue>(request);
            await _costValueNoSqlRepository.Update(costValue);
        }


        public async Task Handle(DeleteCostValueEvent request, CancellationToken cancellationToken)
        {
            await _costValueNoSqlRepository.Delete(request.Id);
        }
    }
}

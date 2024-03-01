using AutoMapper;
using MonthlyCosts.Domain.Entities;
using MonthlyCosts.Domain.Events;

namespace MonthlyCost.Application.MapperProfiles;

public class DomainToEventProfile : Profile
{
    public DomainToEventProfile()
    {
        CreateMap<CreateCostEvent, Cost>();
        CreateMap<UpdateCostEvent, Cost>();
        CreateMap<DeleteCostEvent, Cost>();

        CreateMap<CreateCostValueEvent, CostValue>()
            .ForMember(p => p.Cost, q => q.MapFrom(p => new Cost{ Id = p.CostId }));
        CreateMap<DeleteCostValueEvent, CostValue>();
    }
}

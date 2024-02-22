using AutoMapper;
using MonthlyCosts.Domain.Commands;
using MonthlyCosts.Domain.Entities;

namespace MonthlyCost.Application.MapperProfiles;

public class DomainToCommandProfile : Profile
{
    public DomainToCommandProfile()
    {
        CreateMap<CreateCostCommand, Cost>();
        CreateMap<UpdateCostCommand, Cost>();
        CreateMap<DeleteCostCommand, Cost>();

        CreateMap<CreateCostValueCommand, CostValue>()
            .ForMember(p => p.Cost, q => q.MapFrom(p => new Cost { Id = p.CostId }));
        CreateMap<UpdateCostValueCommand, CostValue>()
            .ForMember(p => p.Cost, q => q.MapFrom(p => new Cost { Id = p.CostId }));
        CreateMap<DeleteCostValueCommand, CostValue>();
    }
}

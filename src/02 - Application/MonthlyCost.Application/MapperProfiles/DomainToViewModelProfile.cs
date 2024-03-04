using AutoMapper;
using Humanizer;
using MonthlyCost.Application.ViewModels.v1;
using MonthlyCosts.Domain.Entities;

namespace MonthlyCost.Application.MapperProfiles;

public class DomainToViewModelProfile : Profile
{
    public DomainToViewModelProfile()
    {
        CreateMap<Cost, CostRequestViewModel>();
        CreateMap<Cost, CostResponseViewModel>()
            .ForMember(p => p.PaymentMethod, q => q.MapFrom(p => p.PaymentMethod.Humanize()));

        CreateMap<CostValue, CostValueRequestViewModel>()
            .ForMember(p => p.CostId, q => q.MapFrom(p => p.Cost.Id));
        CreateMap<CostValue, CostValueResponseViewModel>()
            .ForMember(p => p.CostId, q => q.MapFrom(p => p.Cost.Id));
    }
}
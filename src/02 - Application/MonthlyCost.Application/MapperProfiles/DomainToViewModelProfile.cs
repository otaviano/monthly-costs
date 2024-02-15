using AutoMapper;
using MonthlyCost.Application.ViewModels.v1;
using MonthlyCosts.Domain.Entities;

namespace MonthlyCost.Application.MapperProfiles;

public class DomainToViewModelProfile : Profile
{
    public DomainToViewModelProfile()
    {
        CreateMap<Cost, CostRequestViewModel>();
        CreateMap<Cost, CostResponseViewModel>();
        CreateMap<CostValue, CostValueViewModel>();
    }
}
